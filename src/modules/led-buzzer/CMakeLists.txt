set(LEDBUZZER_BIN led-buzzer)

set(LEDBUZZER_SRCS
        LEDBuzzerModule.cpp
        LedBuzzerImpl.cpp
        init.cpp
        ws/WSHelperThread.cpp
        ws/CRUDHandler.cpp
        )

add_library(${LEDBUZZER_BIN} SHARED ${LEDBUZZER_SRCS})

set_target_properties(${LEDBUZZER_BIN} PROPERTIES
        COMPILE_FLAGS "${MODULE_COMPILE_FLAGS}"
        )

install(TARGETS ${LEDBUZZER_BIN} DESTINATION ${LEOSAC_MODULE_INSTALL_DIR})

target_link_libraries(${LEDBUZZER_BIN} PUBLIC websock-api   ${LEOSAC_MODULE_BASE_DEPENDENCIES} )
