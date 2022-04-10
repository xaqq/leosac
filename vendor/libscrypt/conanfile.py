from conans import ConanFile, tools
from conans.tools import download, untargz, check_md5, check_sha1, check_sha256, cpu_count, chdir
from conan.tools.gnu import Autotools, AutotoolsToolchain

import os
import shutil

class LibScryptConan(ConanFile):
    settings = "os", "arch", "compiler", "build_type"    
    name = "libscrypt"
    version = "1.22"

    def generate(self):
        tc = AutotoolsToolchain(self)
        tc.default_configure_install_args=True
        env = tc.environment()
        env.define('PREFIX', '{}/{}'.format(self.folders._base_build, "out"))
        env.define('LDFLAGS', '-Wl,-soname,libscrypt.so.0')
        tc.generate(env)    
    
    def source(self):
        zip_name = "v1.22.tar.gz"
        download("https://github.com/technion/libscrypt/archive/refs/tags/v1.22.tar.gz", zip_name)
        untargz(zip_name)
        shutil.move("libscrypt-1.22", "libscrypt")
        os.unlink(zip_name)

    def build(self):
        with tools.chdir('libscrypt'):
            autotools = Autotools(self)
            autotools.make()

    def package(self):
        with tools.chdir('libscrypt'):
            self.copy('libscrypt*so*', dst='lib', symlinks=True, keep_path=False)
            self.copy('*libscrypt.h*', dst='include', keep_path=False)

    def package_info(self):
        self.cpp_info.includedir = ['include']
        self.cpp_info.libs = ['scrypt']
