set(REPLICATION_BIN replication)

set(REPLICATION_SRCS
    init.cpp
    ReplicationModule.cpp
)

add_library(${REPLICATION_BIN} SHARED ${REPLICATION_SRCS})

set_target_properties(${REPLICATION_BIN} PROPERTIES
    COMPILE_FLAGS "${MODULE_COMPILE_FLAGS}"
    )

target_link_libraries(${REPLICATION_BIN} PUBLIC
        CONAN_PKG::zmqpp CONAN_PKG::boost CONAN_PKG::libodb)

install(TARGETS ${REPLICATION_BIN} DESTINATION ${LEOSAC_MODULE_INSTALL_DIR})
