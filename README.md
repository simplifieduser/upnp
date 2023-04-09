UPnP Console Application
========================

This is a simple .NET console application to manage port forwarding on compatible routers using the Universal Plug and Play (UPnP) protocol.

Features
--------

*   Add port mappings for specified ports and protocols (TCP, UDP, or both)
*   Delete port mappings for specified ports and protocols (TCP, UDP, or both)
*   Clear all currently open port mappings
*   List currently opened port mappings
*   Add or remove the application location from the user's PATH

Prerequisites
-------------

*   .NET 6.0 Runtime: The application requires .NET 6.0 Runtime to be installed on your system. You can download it from the [.NET 6.0 download page](https://dotnet.microsoft.com/download/dotnet/6.0).
*   UPnP-compatible router: A router with UPnP support is required for this application to work. Note that on some router models, the UPnP feature may need to be manually enabled in the router settings.

Installation
------------

1.  Download the latest release from the [releases page](https://github.com/simplifieduser/upnp/releases) which contains a pre-built `upnp.exe` file.
2.  Extract the contents of the release archive to a directory of your choice.
3.  Optionally, you can add the application to your PATH by running the following command from the installation directory in the command prompt: `upnp.exe --install`. This will make the `upnp` command available globally for your user.

Usage
-----

To use the application, open the installation directory in the command prompt and enter `upnp.exe`. If you have added the application to your PATH using the `--install` command, you can simply use the `upnp` command from any directory.

The available commands are as follows:

*   `-a [port] ([protocol])`: Add port mappings for the specified port (and protocol)
    
    *   `port`: The port number (0-65535) you want to forward.
    *   `protocol` (optional): The protocol you want to forward, either `tcp` or `udp`. If not specified, both protocols will be forwarded.
*   `-d [port] ([protocol])`: Delete port mappings for the specified port (and protocol)
    
    *   `port`: The port number (0-65535) you want to remove forwarding for.
    *   `protocol` (optional): The protocol you want to remove forwarding for, either `tcp` or `udp`. If not specified, both protocols will be removed.
*   `-c`: Clear all currently open port mappings.
    
*   `-l`: List currently opened ports mappings.
    
*   `-?`: Show help screen.
    

Additionally, there are these setup commands available:

*   `--install`: Add location of this program to user's path.
*   `--uninstall`: Remove location of this program from user's path.

Dependencies
------------

This project uses the Open.NAT library, which is included in the project. You can find more information about Open.NAT [here](https://github.com/lontivero/Open.NAT).

Building from Source
--------------------

1.  Clone the repository or download the source code.
2.  Open a command prompt or terminal in the root directory of the project.
3.  Run the following command to build the project: `dotnet build`.
4.  The compiled `upnp.exe` file can be found in the `bin\Debug\net6.0` (or `bin\Release\net6.0`) directory.