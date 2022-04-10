include(GoogleTest)

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
    gtest_discover_tests(${TEST_NAME}
            EXTRA_ARGS ${CMAKE_CURRENT_SOURCE_DIR}/data
            XML_OUTPUT_DIR ${CMAKE_BINARY_DIR}/gtest_result)
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
