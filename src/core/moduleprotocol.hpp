/**
 * \file moduleprotocol.hpp
 * \author Thibault Schueller <ryp.sqrt@gmail.com>
 * \brief ModuleProtocol class declaration
 */

#ifndef MODULEPROTOCOL_HPP
#define MODULEPROTOCOL_HPP

#include <map>
#include <string>
#include <list>

#include "modules/imodule.hpp"
#include "imoduleprotocol.hpp"

class IAccessPointModule;
class IDoorModule;
class IAuthModule;
class ILoggerModule;

class ModuleProtocol : public IModuleProtocol
{
    typedef void (ModuleProtocol::*RegisterFunc)(IModule*);

public:
    explicit ModuleProtocol();
    ~ModuleProtocol() = default;

    ModuleProtocol(const ModuleProtocol& other) = delete;
    ModuleProtocol& operator=(const ModuleProtocol& other) = delete;

public:
    virtual void    logMessage(const std::string& message) override;
    virtual void    createAuthRequest(const std::string& source, const std::string& target, const std::string& content) override;
    virtual void    authorize(AuthRequest::Uid id, bool granted) override;

public:
    void    sync();
    void    registerModule(IModule* module);
    void    printDebug();

private:
    void    processAuthRequest(AuthRequest& ar);
    void    registerDoorModule(IModule* module);
    void    registerAccessPointModule(IModule* module);
    void    registerAuthModule(IModule* module);
    void    registerLoggerModule(IModule* module);
    void    registerActivityMonitorModule(IModule* module);

private:
    AuthRequest::Uid                            _authCounter;
    std::map<AuthRequest::Uid, AuthRequest>     _requests;
    std::map<IModule::ModuleType, RegisterFunc> _registrationHandler;
    std::list<ILoggerModule*>                   _loggerModules;
    std::map<std::string, IDoorModule*>         _doorModules;
    std::map<std::string, IAccessPointModule*>  _apModules;
    IAuthModule*                                _authModule;
};

#endif // MODULEPROTOCOL_HPP
