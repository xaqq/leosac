set(MONITOR_BIN monitor)

set(MONITOR_SRCS
    init.cpp
    MonitorModule.cpp
)

add_library(${MONITOR_BIN} SHARED ${MONITOR_SRCS})

set_target_properties(${MONITOR_BIN} PROPERTIES
    COMPILE_FLAGS "${MODULE_COMPILE_FLAGS}"
    )

file(COPY ${CMAKE_CURRENT_SOURCE_DIR}/ping.sh DESTINATION ${LEOSAC_BINARY_DIR})


target_link_libraries(${MONITOR_BIN} PUBLIC
          ${LEOSAC_MODULE_BASE_DEPENDENCIES} )

install(TARGETS ${MONITOR_BIN} DESTINATION ${LEOSAC_MODULE_INSTALL_DIR})

install(FILES ${CMAKE_CURRENT_SOURCE_DIR}/ping.sh
  PERMISSIONS OWNER_EXECUTE OWNER_WRITE OWNER_READ
  DESTINATION scripts
      )
