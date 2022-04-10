set(MQTT_BIN mqtt)

set(MQTT_SRCS
    init.cpp
    MqttConfig.cpp
    MqttExternalServer.cpp
    MqttModule.cpp
)

# Database support
set(OdbCMake_ODB_HEADERS
        ${CMAKE_CURRENT_SOURCE_DIR}/MqttConfig.hpp
        )

set(LEOSAC_ODB_INCLUDE_DIRS
        ${CMAKE_SOURCE_DIR}/src
        ${CMAKE_SOURCE_DIR}/deps/flagset
        ${CMAKE_SOURCE_DIR}/src/db

		${CONAN_INCLUDE_DIRS}
        )

set(OdbCMake_SOURCES "")
odb_compile(OdbCMake_SOURCES
        FILES ${OdbCMake_ODB_HEADERS}
        MULTI_DATABASE dynamic
        DB pgsql sqlite
        STANDARD "c++14"
        GENERATE_QUERY GENERATE_SESSION GENERATE_SCHEMA
        SCHEMA_FORMAT embedded
        SCHEMA_NAME module_mqtt
        PROFILE "boost/date-time" "boost/optional" "boost/uuid" "leosac-profile"
        INCLUDE ${LEOSAC_ODB_INCLUDE_DIRS}
        DEFAULT_POINTER "std::shared_ptr"
        PGSQL_SERVER_VERSION "9.1"
        OUTPUT_DIR ${CMAKE_BINARY_DIR}/odb_gen/modules/mqtt
        )


add_library(${MQTT_BIN} SHARED ${MQTT_SRCS} ${OdbCMake_SOURCES})

set_target_properties(${MQTT_BIN} PROPERTIES
    COMPILE_FLAGS "${MQTT_COMPILE_FLAGS}"
    )

target_link_libraries(${MQTT_BIN} PUBLIC
		CONAN_PKG::paho-mqtt-cpp
		${LEOSAC_MODULE_BASE_DEPENDENCIES}
		)

install(TARGETS ${MQTT_BIN} DESTINATION ${LEOSAC_MODULE_INSTALL_DIR})
