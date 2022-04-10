set(TCP_NOTIFIER_BIN tcp-notifier)

set(TCP_NOTIFIER_SRCS
    init.cpp
    TcpNotifier.cpp
    NotifierInstance.cpp
    ProtocolHandler.cpp
    protocols/PushSimpleCardNumber.cpp
)

add_library(${TCP_NOTIFIER_BIN} SHARED ${TCP_NOTIFIER_SRCS})

set_target_properties(${TCP_NOTIFIER_BIN} PROPERTIES
    COMPILE_FLAGS "${MODULE_COMPILE_FLAGS}"
    )

target_link_libraries(${TCP_NOTIFIER_BIN})

target_link_libraries(${TCP_NOTIFIER_BIN} PUBLIC
        CONAN_PKG::zmqpp CONAN_PKG::boost CONAN_PKG::libodb)

install(TARGETS ${TCP_NOTIFIER_BIN} DESTINATION ${LEOSAC_MODULE_INSTALL_DIR})
