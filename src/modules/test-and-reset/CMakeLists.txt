set(test-and-reset_BIN test-and-reset)

set(test-and-reset_SRCS
init.cpp
TestAndResetModule.cpp
)

add_library(${test-and-reset_BIN} SHARED ${test-and-reset_SRCS})

set_target_properties(${test-and-reset_BIN} PROPERTIES
    COMPILE_FLAGS "${MODULE_COMPILE_FLAGS}"
    )

target_link_libraries(${test-and-reset_BIN} PUBLIC
        ${LEOSAC_MODULE_BASE_DEPENDENCIES}
        )

install(TARGETS ${test-and-reset_BIN} DESTINATION ${LEOSAC_MODULE_INSTALL_DIR})
