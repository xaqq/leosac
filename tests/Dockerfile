FROM debian:latest

COPY . /leosac_package/
WORKDIR /leosac_package


VOLUME /test_data

ENTRYPOINT ["/leosac_package/bin/leosac"]
