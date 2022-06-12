from conans import ConanFile, tools
from conans.tools import download, untargz, check_md5, check_sha1, check_sha256, cpu_count, patch
from conan.tools.gnu import Autotools, AutotoolsToolchain

import os
import shutil

class LibOdbConan(ConanFile):
    settings = "os", "arch", "compiler", "build_type"    
    name = "libodb"
    version = "2.4.0"
    generators = "AutotoolsToolchain"
    exports_sources = 'patch_leosac_cpp20'
    
    def generate(self):
        tc = AutotoolsToolchain(self)
        tc.default_configure_install_args=True
        tc.generate()    
    
    def source(self):
        zip_name = "libodb-2.4.0.zip"
        download("https://www.codesynthesis.com/download/odb/2.4/libodb-2.4.0.tar.gz", zip_name)
        untargz(zip_name)
        shutil.move("libodb-2.4.0", "libodb")
        os.unlink(zip_name)

    def build(self):
        tools.patch(patch_file='patch_leosac_cpp20', base_path='libodb')
        autotools = Autotools(self)
        autotools.configure(build_script_folder='libodb')
        autotools.make()

    def package(self):
        autotools = Autotools(self)
        autotools.install()    

    def package_info(self):
        self.cpp_info.includedir = ['include']
        self.cpp_info.libs = ['odb']
