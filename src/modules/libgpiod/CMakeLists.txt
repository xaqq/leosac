set(LIBGPIOD_BIN libgpiod)

set(LIBGPIOD_SRCS
    init.cpp
    LibgpiodModule.cpp
    LibgpiodPin.cpp
    LibgpiodConfig.cpp
)

add_library(${LIBGPIOD_BIN} SHARED ${LIBGPIOD_SRCS})

set_target_properties(${LIBGPIOD_BIN} PROPERTIES
    COMPILE_FLAGS "${MODULE_COMPILE_FLAGS}"
    )

target_link_libraries(${LIBGPIOD_BIN} PUBLIC
    ${LEOSAC_MODULE_BASE_DEPENDENCIES}
    CONAN_PKG::libgpiod)

install(TARGETS ${LIBGPIOD_BIN} DESTINATION ${LEOSAC_MODULE_INSTALL_DIR})
