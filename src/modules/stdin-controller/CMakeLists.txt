set(STDINCONTROLLER_BIN stdin-controller)

set(STDINCONTROLLER_SRCS
    init.cpp
    StdinControllerModule.cpp
)

add_library(${STDINCONTROLLER_BIN} SHARED ${STDINCONTROLLER_SRCS})

set_target_properties(${STDINCONTROLLER_BIN} PROPERTIES
    COMPILE_FLAGS "${MODULE_COMPILE_FLAGS}"
    )

target_link_libraries(${STDINCONTROLLER_BIN} PUBLIC
        CONAN_PKG::zmqpp
        CONAN_PKG::boost
        )

install(TARGETS ${STDINCONTROLLER_BIN} DESTINATION ${LEOSAC_MODULE_INSTALL_DIR})
