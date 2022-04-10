from conans import ConanFile, tools
from conans.tools import download, untargz, check_md5, check_sha1, check_sha256, cpu_count
from conan.tools.gnu import Autotools, AutotoolsToolchain

import os
import shutil

class LibOdbSQLITEConan(ConanFile):
    settings = "os", "arch", "compiler", "build_type"
    name = "libodb_sqlite"
    version = "2.4.0"
    requires = "libodb/2.4.0", "sqlite3/3.38.1"
    default_options = {'sqlite3:shared': True}
    generators = "AutotoolsToolchain"

    def generate(self):
        libsqlite_root = self.deps_cpp_info["sqlite3"].rootpath
        libodb_root = self.deps_cpp_info["libodb"].rootpath
        
        tc = AutotoolsToolchain(self)
        tc.default_configure_install_args=True

        # Point to libpq and libodb
        tc.cxxflags = ['-I{}/include'.format(libsqlite_root),
                       '-I{}/include'.format(libodb_root) ]
        tc.ldflags = ['-L{}/lib'.format(libodb_root),
                      '-L{}/lib'.format(libsqlite_root)]
        tc.generate()    
    
    def source(self):
        zip_name = "libodb-sqlite-2.4.0.zip"
        download("https://www.codesynthesis.com/download/odb/2.4/libodb-sqlite-2.4.0.tar.gz", zip_name)
        untargz(zip_name)
        shutil.move("libodb-sqlite-2.4.0", "libodb-sqlite")
        os.unlink(zip_name)

    def build(self):
        autotools = Autotools(self)
        autotools.configure('libodb-sqlite')
        autotools.make()

    def package(self):
        autotools = Autotools(self)
        autotools.install()    

    def package_info(self):
        self.cpp_info.includedir = ['include']
        self.cpp_info.libs = ['odb-sqlite']
