using System.Net.WebSockets;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace LeosacAPI;

public class LeosacApi
{
    private ILogger<LeosacApi> _logger;
    private ClientWebSocket _webSocket;
    
    /// <summary>
    /// The userId we are authenticate with.
    /// </summary>
    public int UserId { get; private set; }

    public LeosacApi(ClientWebSocket webSocket, ILogger<LeosacApi> logger)
    {
        _webSocket = webSocket;
        _logger = logger;
    }

    private async Task WriteAsync(ClientMessage msg, CancellationToken cancellationToken)
    {
        var bytes = JsonSerializer.SerializeToUtf8Bytes(msg);
        await _webSocket.SendAsync(new ArraySegment<byte>(bytes),
            WebSocketMessageType.Text,
            true,
            cancellationToken);
    }
    
    private async Task<ServerMessage> ReadAsync(CancellationToken cancellationToken)
    {
        var readbuf = new byte[1024 * 1024];
        var response = await _webSocket.ReceiveAsync(readbuf, cancellationToken);
        if (response.MessageType == WebSocketMessageType.Close)
        {
            throw new ApplicationException("WS closed");
        }
        else
        {
            var msg = JsonSerializer.Deserialize<ServerMessage>(
                new ReadOnlySpan<byte>(readbuf, 0, response.Count));
            if (msg != null)
            {
                return msg;
            }
            else
            {
                throw new ApplicationException("failed to deserialize");
            }
        }    
    }

    private async Task<ServerMessage> WaitResponse(string messageUuid, CancellationToken cancellationToken)
    {
        while (true)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var m = await ReadAsync(cancellationToken);
            if (m.uuid == messageUuid)
                return m;

            // Message w/o uuid are not a response, generally a notification.
            if (m.uuid == "" && m.StatusCode != APIStatusCode.SUCCESS)
            {
                throw new LeosacApiException(m.StatusCode, m.StatusString);
            } 
            // Otherwise just discard and keep waiting
        }        
    }

    public async Task<string> GetVersionAsync(CancellationToken cancellationToken)
    {
        var msg = new ClientMessage
        {
            Type = "get_leosac_version",
            Content = new{}
        };

        await WriteAsync(msg, cancellationToken);
        var response = await ReadAsync(cancellationToken);
        return response.content["version"]!.GetValue<string>();
    }

    public async Task AuthenticateAsync(string username, string password, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Attempting API login with username={username} and password={password}");
        var msg = new ClientMessage
        {
            Type = "create_auth_token",
            Content = new
            {
                username = username,
                password = password
            }
        };
        
        await WriteAsync(msg, cancellationToken);
        var response = await WaitResponse(msg.Uuid, cancellationToken);
        if (response.StatusCode != APIStatusCode.SUCCESS)
        {
            throw new LeosacApiException(response.StatusCode, response.StatusString);
        }

        if (response.content["status"]!.GetValue<int>() != 0)
        {
            throw new InvalidApiCredentialException();
        }

        UserId = response.content["user_id"]!.GetValue<int>();
    }

    public async Task ChangePasswordAsync(string password, string newPassword, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Attempting API login with userId={UserId} and password={password}");
        var msg = new ClientMessage
        {
            Type = "password_change",
            Content = new
            {
                user_id = UserId,
                current_password = password,
                new_password = newPassword
            }
        };

        await WriteAsync(msg, cancellationToken);
        var response = await WaitResponse(msg.Uuid, cancellationToken);
        if (response.StatusCode != APIStatusCode.SUCCESS)
        {
            throw new LeosacApiException(response.StatusCode, response.StatusString);
        }
    }
}
