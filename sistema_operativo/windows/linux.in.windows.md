# Windows Subsystem for Linux Installation - WSL


The Windows Subsystem for Linux lets developers run a GNU/Linux environment -- including most command-line tools, utilities, and applications -- directly on Windows, unmodified, without the overhead of a traditional virtual machine or dual-boot setup.


Windows 10 Will Finally Offer Easy Access to Linux Files

You can open a File Explorer window directly in the current directory from within a Linux shell environment. Just type the following command into the Bash shell:

```
explorer.exe .
```

https://www.howtogeek.com/fyi/windows-10-will-finally-offer-easy-access-to-linux-files/


Error Fix: WSL Not Working After Upgrading to Windows 11

Fix 1: Enable WSL
Fix 2: Enable Virtual Machine Platform and Hyper-V
Fix 3: Repair or Reinstall the Linux Distribution App
https://www.isunshare.com/windows-11/error-fix-wsl-not-working-after-upgrading-to-windows-11.html



# Commandos


List available Linux distributions

The following is a list of valid distributions that can be installed.

```
wsl --list --online
```


To view all of your available Linux distributions and their root file systems in Windows File explorer, in the address bar enter: \\wsl$


To run a command as administrator (user "root"), use "sudo <command>".
See "man sudo_root" for details.


# Referencias


Install Ubuntu on WSL2 on Windows 10
- To launch, use "ubuntu2204" on the command-line prompt or Windows Terminal, or click on the Ubuntu 22.04.2 LTS tile in the Start Menu.
https://ubuntu.com/tutorials/install-ubuntu-on-wsl2-on-windows-10#1-overview


Forgot Linux Password on WSL? Here's How to Reset it Easily
- The root user in WSL is unlocked and doesn’t have a password set. This means that you can switch to the root user and then use the power of root to reset the password.
https://itsfoss.com/reset-linux-password-wsl/