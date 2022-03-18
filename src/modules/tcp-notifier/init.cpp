/*
    Copyright (C) 2014-2016 Leosac

    This file is part of Leosac.

    Leosac is free software: you can redistribute it and/or modify
    it under the terms of the GNU Affero General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    Leosac is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
    GNU Affero General Public License for more details.

    You should have received a copy of the GNU Affero General Public License
    along with this program. If not, see <http://www.gnu.org/licenses/>.
*/

#include "TcpNotifier.hpp"
#include <tools/log.hpp>

using namespace Leosac::Module::TCPNotifier;

extern "C" {
const char *get_module_name()
{
    return "TCP_NOTIFIER";
}
}

/**
* Entry point for the TCP Notifier module.
*/
extern "C" __attribute__((visibility("default"))) bool
start_module(zmqpp::socket *pipe, boost::property_tree::ptree cfg,
             zmqpp::context &zmq_ctx, Leosac::CoreUtilsPtr utils)
{
    return Leosac::Module::start_module_helper<TCPNotifierModule>(pipe, cfg, zmq_ctx,
                                                                  utils);
}
