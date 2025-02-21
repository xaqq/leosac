namespace LeosacAPI;

/**
 * Those are the Leosac API status code.
 *
 * They currently apply only to the WebSocket API.
 *
 * @note This enum is provided by core because the audit system relies on it.
 */
public enum APIStatusCode
{
    /**
     * Indicate that the request's processing went well.
     */
    SUCCESS = 0x00,

    /**
     * A failure for an unknown reason.
     */
    GENERAL_FAILURE = 0x01,

    /**
     * The websocket connection is not allowed to make the
     * requested API call.
     */
    PERMISSION_DENIED = 0x02,

    /**
     * The websocket connection is rate limited, and it already
     * sent too many packets.
     */
    RATE_LIMITED = 0x03,

    /**
     * The source packet was malformed.
     */
    MALFORMED = 0x04,

    /**
     * The API method (ie, message's type) does not exist.
     */
    INVALID_CALL = 0x05,

    /**
     * The request took too long to process.
     * This is mostly here as a placeholder, as this status_code will mostly
     * be used internaly by the Javascript web app to signal a lack of response.
     */
    TIMEOUT = 0x06,

    /**
     * The session has been aborted.
     * This is likely due to the expiration of the token used to
     * authenticate.
     */
    SESSION_ABORTED = 0x07,

    /**
     * The requested entity cannot be found.
     */
    ENTITY_NOT_FOUND = 0x08,

    /**
     * An internal database operation threw an exception.
     */
    DATABASE_ERROR = 0x09,

    /**
     * Unknown status.
     * Mostly useful as a default value somewhere.
     */
    UNKNOWN = 0x0A,

    /**
     * Some internal API rules has failed.
     *
     * This is likely to model validation.
     */
    MODEL_EXCEPTION = 0x0B,

    /**
     * One of the argument of the call had a invalid
     * value / type.
     */
    INVALID_ARGUMENT = 0x0C
};