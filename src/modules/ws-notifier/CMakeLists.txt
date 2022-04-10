set(WS_NOTIFIER_BIN ws-notifier)

set(WS_NOTIFIER_SRCS
    init.cpp
        WebServiceNotifier.cpp
)

add_library(${WS_NOTIFIER_BIN} SHARED ${WS_NOTIFIER_SRCS})

set_target_properties(${WS_NOTIFIER_BIN} PROPERTIES
    COMPILE_FLAGS "${MODULE_COMPILE_FLAGS}"
    )

target_link_libraries(${WS_NOTIFIER_BIN} PUBLIC
        ${LEOSAC_MODULE_BASE_DEPENDENCIES}
        CONAN_PKG::libcurl
        )

install(TARGETS ${WS_NOTIFIER_BIN} DESTINATION ${LEOSAC_MODULE_INSTALL_DIR})
