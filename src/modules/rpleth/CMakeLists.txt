set(RPLETH_BIN rpleth)

set(RPLETH_SRCS
    init.cpp
    RplethModule.cpp
    rplethpacket.cpp
    rplethprotocol.cpp
    network/circularbuffer.cpp
)

add_library(${RPLETH_BIN} SHARED ${RPLETH_SRCS})

set_target_properties(${RPLETH_BIN} PROPERTIES
    COMPILE_FLAGS "${MODULE_COMPILE_FLAGS}"
   )

target_link_libraries(${RPLETH_BIN} PUBLIC
          ${LEOSAC_MODULE_BASE_DEPENDENCIES} )

install(TARGETS ${RPLETH_BIN} DESTINATION ${LEOSAC_MODULE_INSTALL_DIR})
