FROM debian:bullseye

RUN apt-get update
RUN apt-get install -y python3 python3-pip
RUN pip3 install conan
RUN apt-get install -y gcc-10 g++-10 g++-10-arm-linux-gnueabi gcc-10-arm-linux-gnueabi gcc-10-aarch64-linux-gnu g++-10-aarch64-linux-gnu \
cmake ccache g++-9 odb

COPY vendor /leosac_vendored

WORKDIR /leosac_vendored
# Export recipe for homemade conan package around some deps
RUN cd libodb && conan export .
RUN cd libodb-boost && conan export .
RUN cd libodb-pgsql  && conan export .
RUN cd libodb-sqlite && conan export .
RUN cd libscrypt  && conan export .

VOLUME /leosac
VOLUME /root/.conan/data

WORKDIR /leosac

CMD conan create --build missing --profile:build conan/profile --profile:host conan/profilearm32 .
