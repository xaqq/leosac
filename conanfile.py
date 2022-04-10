from conans import ConanFile, CMake


class LeosacConan(ConanFile):
    settings = "os", "arch", "compiler", "build_type"    
    name = "leosac"
    version = "0.0.8"
    requires = "zmqpp/4.2.0", "libodb_pgsql/2.4.0", \
               "libodb_sqlite/2.4.0", \
               "libodb_boost/2.4.0", \
               "boost/1.74.0", \
               "openssl/1.1.1k", \
               "libcurl/7.80.0", \
               "libscrypt/1.22", \
               "tclap/1.2.4", \
               "nlohmann_json/3.10.5", \
               "spdlog/1.9.2"

    generators = 'cmake'
    default_options = {
        'libpq:shared': True,
        'zeromq:shared': True,
        'zmqpp:shared': True,
        'boost:without_stacktrace': True }

    exports_sources = "CMakeLists.txt", "cmake*", "src*", "scripts*", "cfg*", "deps*", "test*"
    keep_imports = True

    def build(self):
        cmake = CMake(self)
        cmake.configure()
        cmake.build()

    def package(self):
        cmake = CMake(self)
        cmake.install()
        self.copy('*', src='conanlibs', dst='conanlibs')

    def imports(self):
        self.copy("lib*.so*", dst='conanlibs', src="lib")
