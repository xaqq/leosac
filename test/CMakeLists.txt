function(leosacCreateSingleSourceTest NAME)
## module we link against
set(MODULES_LIB wiegand led-buzzer rpleth sysfsgpio auth-file tcp-notifier)
set(HELPER_SRC  helper/FakeGPIO.cpp helper/FakeWiegandReader.cpp)

    set(TEST_NAME test-${NAME})
    add_executable(${TEST_NAME} ${NAME}.cpp ${HELPER_SRC})
    set_target_properties(${TEST_NAME} PROPERTIES
        COMPILE_FLAGS "${LEOSAC_COMPILE_FLAGS} -W -Wall -g"
        INCLUDE_DIRECTORIES "${LEOSAC_TEST_INCLUDE_DIRECTORIES}")
    target_link_libraries(${TEST_NAME} PUBLIC
            ${MODULES_LIB}
            CONAN_PKG::gtest
            )

    ## pass the data folder as a parameter.
    add_test(NAME ${TEST_NAME}
        COMMAND $<TARGET_FILE:${TEST_NAME}> ${CMAKE_CURRENT_LIST_DIR}/data)
    unset(TEST_NAME)
endfunction()

leosacCreateSingleSourceTest(version)
leosacCreateSingleSourceTest(WiegandReader)
leosacCreateSingleSourceTest(Led)
leosacCreateSingleSourceTest(Rpleth)
leosacCreateSingleSourceTest(SysFsGpioConfig)
leosacCreateSingleSourceTest(AuthFile)
leosacCreateSingleSourceTest(AuthSourceBuilder)
leosacCreateSingleSourceTest(ConfigManager)
leosacCreateSingleSourceTest(RemoteControlSecurity)
leosacCreateSingleSourceTest(RFIDCard)
leosacCreateSingleSourceTest(GroupValidator)
leosacCreateSingleSourceTest(Visitor)
leosacCreateSingleSourceTest(CredentialValidator)
leosacCreateSingleSourceTest(ScheduleValidator)
leosacCreateSingleSourceTest(Registry)
leosacCreateSingleSourceTest(ServiceRegistry)
