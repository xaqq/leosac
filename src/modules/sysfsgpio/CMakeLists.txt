set(SYSFSGPIO_BIN sysfsgpio)

set(SYSFSGPIO_SRCS
    init.cpp
    SysFsGpioModule.cpp
    SysFSGPIOPin.cpp
    SysFsGpioConfig.cpp
)

add_library(${SYSFSGPIO_BIN} SHARED ${SYSFSGPIO_SRCS})

set_target_properties(${SYSFSGPIO_BIN} PROPERTIES
    COMPILE_FLAGS "${MODULE_COMPILE_FLAGS}"
    )

target_link_libraries(${SYSFSGPIO_BIN} PUBLIC
        ${LEOSAC_MODULE_BASE_DEPENDENCIES}
        )

install(TARGETS ${SYSFSGPIO_BIN} DESTINATION ${LEOSAC_MODULE_INSTALL_DIR})
