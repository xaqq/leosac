/*
    Copyright (C) 2014-2015 Islog

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

#pragma once

#include <zmqpp/zmqpp.hpp>
#include <boost/property_tree/ptree.hpp>

namespace Leosac
{
    namespace Module
    {
        /**
        * Base class for module implementation.
        *
        * It provide a base class for writing module code. The base class
        * implements `run()` as a main loop and use a reactor to handle socket
        * when they become available for reading. When implementing a module using
        * this class you're supposed to use that reactor and register your socket.
        * In case you need to override `run()`, remember to poll on the reactor,
        * or to watch the `pipe_` on way or another.
        *
        * It use a reactor to poll on the `pipe` socket that connect the module back to the module manager.
        *
        * For replication and ease of management, command can be written on the module's pipe and must
        * be implemented in the module (unless it is implemented by the BaseModule class).
        *
        * Commands:
        *   DUMP_CONFIG : serialize the current module config. (as a ptree)
        *
        * @note This class is here to help reduce code duplication. It is NOT mandatory to inherit from this base class
        * to implement a module. However, it may help.
        */
        class BaseModule
        {
        public:
            /**
            * Constructor of BaseModule. It will register the pipe_ to reactor_.
            */
            BaseModule(zmqpp::context &ctx,
                    zmqpp::socket *pipe,
                    const boost::property_tree::ptree &cfg);

            virtual ~BaseModule() = default;

            /**
            * This is the main loop of the module. It should only exit when receiving `signal::stop` on its `pipe_`
            * socket. This is completely handled by this base class.
            * In case you override this, make sure you watch the `pipe_` socket.
            */
            virtual void run();

        protected:
            /**
            * The base class register the `pipe_` socket to its `reactor_` so that this function
            * is called when the `pipe_` is available from reading.
            * This implementation switch `is_running_` to false.
            */
            virtual void handle_pipe();

            /**
            * Serialize the config_ property tree in portable text format and returns it.
            */
            std::string dump_config() const;

            /**
            * A reference to the ZeroMQ context in case you need it to create additional socket.
            */
            zmqpp::context &ctx_;

            /**
            * A reference to the pair socket that link back to the module manager.
            */
            zmqpp::socket &pipe_;

            /**
            * The configuration tree passed to the `start_module` function.
            */
            boost::property_tree::ptree config_;

            /**
            * Boolean indicating whether the main loop should run or not.
            */
            bool is_running_;

            /**
            * The reactor object we poll() on in the main loop. Register additional socket/fd here if
            * you need to.
            */
            zmqpp::reactor reactor_;
        };
    }
}
