//
// Created by xaqq on 3/12/22.
//

#include <zmqpp/poller.hpp>
#include <zmqpp/socket.hpp>
#include <zmqpp/context.hpp>

int main() {
    zmqpp::context ctx;
    zmqpp::poller p;
    zmqpp::socket s(ctx, zmqpp::socket_type::push);

    s.send("toto");

}