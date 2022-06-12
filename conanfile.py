from conans import ConanFile, CMake


class LeosacConan(ConanFile):
    settings = "os", "arch", "compiler", "build_type"
    name = "leosac"
    version = "0.0.8"
    requires = "zmqpp/4.2.0", \
               "libodb_pgsql/2.4.0", \
               "libodb_sqlite/2.4.0", \
               "libodb_boost/2.4.0", \
               "boost/1.78.0", \
               "openssl/1.1.1k", \
               "libcurl/7.80.0", \
               "libscrypt/1.22", \
               "tclap/1.2.4", \
               "nlohmann_json/3.10.5", \
               "spdlog/1.9.2", \
               "date/3.0.1", \
               "websocketpp/0.8.2", \
               "libgpiod/1.6.3", \
               "zlib/1.2.12" # override
    

    options = {
        'build_test': [True, False],
        'build_module_mqtt': [True, False],
        'build_module_lla': [True, False],
        'coverage': [True, False]
    }

    generators = 'cmake'
    default_options = {
        'build_test': True,
        'build_module_mqtt': True,
        'build_module_lla': True,
        'coverage': False,
        'gtest:shared': True,
        'libpq:shared': True,
        'zeromq:shared': True,
        'zmqpp:shared': True,
        'boost:without_stacktrace': True,
        'boost:without_test': True,
        'paho-mqtt-c:shared': True,
        'paho-mqtt-cpp:shared': True
    }

    exports_sources = "CMakeLists.txt", "cmake*", "src*", "scripts*", "cfg*", "deps*", "test*"
    keep_imports = True

    def requirements(self):
        if self.options.build_test:
            self.requires('gtest/1.11.0')
        if self.options.build_module_mqtt:
            self.requires('paho-mqtt-cpp/1.2.0')
        if self.options.build_module_lla:
            self.requires('LogicalAccess/2.4.0')
    
    def build(self):
        cmake = CMake(self)
        if self.options.build_test:
            cmake.definitions['LEOSAC_BUILD_TESTS'] = True
        if self.options.build_module_mqtt:
            cmake.definitions['LEOSAC_BUILD_MODULE_MQTT'] = True
        if self.options.build_module_lla:
            cmake.definitions['LEOSAC_BUILD_MODULE_LLA'] = True
        if self.options.coverage:
            cmake.definitions['LEOSAC_ENABLE_COVERAGE'] = True

        cmake.configure()
        cmake.build()

    def package(self):
        cmake = CMake(self)
        cmake.install()
        self.copy('*', src='conanlibs', dst='lib', symlinks=True)
        # Todo strip libraries.

    def imports(self):
        # We copy in two places, make it easier for package() method.
        # Not too great
        self.copy("lib*.so*", dst='lib', src="lib")
        self.copy("lib*.so*", dst='conanlibs', src="lib")
