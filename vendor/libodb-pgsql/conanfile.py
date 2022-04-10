from conans import ConanFile, tools
from conans.tools import download, untargz, check_md5, check_sha1, check_sha256, cpu_count
from conan.tools.gnu import Autotools, AutotoolsToolchain

import os
import shutil

class LibOdbPGSQLConan(ConanFile):
    settings = "os", "arch", "compiler", "build_type"
    name = "libodb_pgsql"
    version = "2.4.0"
    requires = "libodb/2.4.0", "libpq/13.4"
    default_options = {'libpq:shared': True}
    generators = "AutotoolsToolchain"

    def generate(self):
        libpq_root = self.deps_cpp_info["libpq"].rootpath
        libodb_root = self.deps_cpp_info["libodb"].rootpath
        
        tc = AutotoolsToolchain(self)
        tc.default_configure_install_args=True

        # Point to libpq and libodb
        tc.cxxflags = ['-I{}/include'.format(libpq_root),
                       '-I{}/include'.format(libodb_root) ]
        tc.ldflags = ['-L{}/lib'.format(libpq_root),
                      '-L{}/lib'.format(libodb_root)]
        tc.generate()    
    
    def source(self):
        zip_name = "libodb-pgsql-2.4.0.zip"
        download("https://www.codesynthesis.com/download/odb/2.4/libodb-pgsql-2.4.0.tar.gz", zip_name)
        untargz(zip_name)
        shutil.move("libodb-pgsql-2.4.0", "libodb-pgsql")
        os.unlink(zip_name)

    def build(self):
        autotools = Autotools(self)
        autotools.configure('libodb-pgsql')
        autotools.make()

    def package(self):
        autotools = Autotools(self)
        autotools.install()    

    def package_info(self):
        self.cpp_info.includedir = ['include']
        self.cpp_info.libs = ['odb-pgsql']
