from conans import ConanFile, tools
from conans.tools import download, untargz, check_md5, check_sha1, check_sha256, cpu_count
from conan.tools.gnu import Autotools, AutotoolsToolchain

import os
import shutil

class LibOdbBoostConan(ConanFile):
    settings = "os", "arch", "compiler", "build_type"
    name = "libodb_boost"
    version = "2.4.0"
    requires = "libodb/2.4.0", "boost/1.74.0"
    default_options = {'boost:shared': True}
    generators = "AutotoolsToolchain"

    def generate(self):
        boost_root = self.deps_cpp_info["boost"].rootpath
        libodb_root = self.deps_cpp_info["libodb"].rootpath
        
        tc = AutotoolsToolchain(self)
        tc.default_configure_install_args=True

        # Point to libpq and libodb
        tc.cxxflags = ['-I{}/include'.format(boost_root),
                       '-I{}/include'.format(libodb_root) ]
        tc.ldflags = ['-L{}/lib'.format(boost_root),
                      '-L{}/lib'.format(libodb_root)]
        tc.generate()    
    
    def source(self):
        zip_name = "libodb-boost-2.4.0.zip"
        download("https://www.codesynthesis.com/download/odb/2.4/libodb-boost-2.4.0.tar.gz", zip_name)
        untargz(zip_name)
        shutil.move("libodb-boost-2.4.0", "libodb-boost")
        os.unlink(zip_name)

    def build(self):
        autotools = Autotools(self)
        autotools.configure('libodb-boost')
        autotools.make()

    def package(self):
        autotools = Autotools(self)
        autotools.install()    

    def package_info(self):
        self.cpp_info.includedir = ['include']
        self.cpp_info.libs = ['odb-boost']
