cmake_minimum_required(VERSION 3.18.4 FATAL_ERROR)
project(LEOSAC)

include(${CMAKE_BINARY_DIR}/conanbuildinfo.cmake)
conan_basic_setup(TARGETS)
#set(CMAKE_SKIP_RPATH 1)

set(CMAKE_INSTALL_RPATH "$ORIGIN/../lib/")
set(CMAKE_BUILD_WITH_INSTALL_RPATH ON)

# Use ccache if available
find_program(CCACHE_BIN ccache)
if (CCACHE_BIN)
    set(CMAKE_CXX_COMPILER_LAUNCHER ${CCACHE_BIN})
endif()

# Version definitions
add_definitions(-DLEOSAC_VERSION_MAJOR=0)
add_definitions(-DLEOSAC_VERSION_MINOR=8)
add_definitions(-DLEOSAC_VERSION_PATCH=0)

set(CPACK_PACKAGE_VERSION_MAJOR "0")
set(CPACK_PACKAGE_VERSION_MINOR "8")
set(CPACK_PACKAGE_VERSION_PATCH "0")

# Avoid source tree pollution
if (CMAKE_SOURCE_DIR STREQUAL CMAKE_BINARY_DIR)
    message(WARNING "In-source builds are not encouraged. Make a separate folder for building:\nmkdir build; cd build; cmake ..;cd ..\nBefore that, remove the files already created:\nrm -rf CMakeCache.txt CMakeFiles")
endif ()

# Force CMAKE_BUILD_TYPE
if (CMAKE_BUILD_TYPE STREQUAL "")
    message(WARNING "Please specify a build type\nExample: cmake .. -DCMAKE_BUILD_TYPE=Debug")
endif ()

list(APPEND CMAKE_MODULE_PATH ${CMAKE_SOURCE_DIR}/cmake)

# ODB stuff
#find_package(ODB REQUIRED ODB_EXECUTABLE)

set(ODB_EXECUTABLE odb)
include(${CMAKE_CURRENT_LIST_DIR}/cmake/UseODB.cmake)

set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} -std=c++20 -Wno-undef -Wno-shadow -Wno-unknown-pragmas")

#Build Options
option(LEOSAC_BUILD_MODULES "build-modules" ON)
option(LEOSAC_BUILD_TESTS "build-tests" OFF)
option(LEOSAC_GPROF "gprof" OFF)

if (LEOSAC_ENABLE_COVERAGE)
    set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} --coverage")
endif()

if (LEOSAC_GPROF)
    set(CMAKE_EXE_LINKER_FLAGS "${CMAKE_EXE_LINKER_FLAGS} -pg")
endif ()

set(CMAKE_RUNTIME_OUTPUT_DIRECTORY ${LEOSAC_BINARY_DIR})
set(CMAKE_LIBRARY_OUTPUT_DIRECTORY ${LEOSAC_BINARY_DIR})
set(CMAKE_ARCHIVE_OUTPUT_DIRECTORY ${LEOSAC_BINARY_DIR})

add_subdirectory(src)
add_subdirectory(cfg)

if (LEOSAC_BUILD_TESTS)
    enable_testing()
    add_subdirectory(test)
endif ()

file(COPY ${CMAKE_CURRENT_SOURCE_DIR}/scripts/build_ipconfig.sh DESTINATION ${LEOSAC_BINARY_DIR})
file(COPY ${CMAKE_CURRENT_SOURCE_DIR}/scripts/load_ipconfig.sh DESTINATION ${LEOSAC_BINARY_DIR})

install(FILES ${CMAKE_CURRENT_SOURCE_DIR}/scripts/build_ipconfig.sh
        PERMISSIONS OWNER_EXECUTE OWNER_WRITE OWNER_READ
        DESTINATION scripts
        )

install(FILES ${CMAKE_CURRENT_SOURCE_DIR}/scripts/load_ipconfig.sh
        PERMISSIONS OWNER_EXECUTE OWNER_WRITE OWNER_READ
        DESTINATION scripts
        )
