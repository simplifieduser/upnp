using Open.Nat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace upnp
{
    public abstract class Commands
    {

        public static async Task AddMapping(string[] args)
        {
            // 1st arg

            if (args.Length <= 1)
            {
                Console.WriteLine("\nMissing argument [port]. Please type -? for futher help.");
                return;
            }

            int port;
            bool result = Int32.TryParse(args[1], out port);

            if (!result || port < 0 || port > 65535)
            {
                Console.WriteLine("\n{0} is not a vaild argument. Please enter a vaild port number between 0 and 65535.", args[1]);
                return;
            }

            // 2nd arg

            Protocol protocol = Protocol.Tcp;
            bool bothProtocols = false;

            if (args.Length <= 2)
            {
                bothProtocols = true;
            }
            else
            {
                if (args[2].Equals("tcp", StringComparison.OrdinalIgnoreCase))
                {
                    protocol = Protocol.Tcp;
                }
                else if (args[2].Equals("udp", StringComparison.OrdinalIgnoreCase))
                {
                    protocol = Protocol.Udp;
                }
                else
                {
                    Console.WriteLine("\n{0} is not a vaild argument. Please enter a vaild protocol, either tcp or udp.", args[2]);
                    return;
                }
            }

            // add mappings

            NatDevice device = await Init();

            if (bothProtocols)
            {
                await device.CreatePortMapAsync(new Mapping(Protocol.Tcp, port, port));
                await device.CreatePortMapAsync(new Mapping(Protocol.Udp, port, port));
                Console.WriteLine("\nAdded mappings for port {0}.", args[1]);
            }
            else
            {
                await device.CreatePortMapAsync(new Mapping(protocol, port, port));
                Console.WriteLine("\nAdded mapping for port {0} ({1} only).", args[1], args[2].ToUpper());
            }

        }

        public static async Task DeleteMapping(string[] args)
        {
            // 1st arg

            if (args.Length <= 1)
            {
                Console.WriteLine("\nMissing argument [port]. Please type -? for futher help.");
                return;
            }

            int port;
            bool result = Int32.TryParse(args[1], out port);

            if (!result || port < 0 || port > 65535)
            {
                Console.WriteLine("\n{0} is not a vaild argument. Please enter a vaild port number between 0 and 65535.", args[1]);
                return;
            }

            // 2nd arg

            Protocol protocol = Protocol.Tcp;
            bool bothProtocols = false;

            if (args.Length <= 2)
            {
                bothProtocols = true;
            }
            else
            {
                if (args[2].Equals("tcp", StringComparison.OrdinalIgnoreCase))
                {
                    protocol = Protocol.Tcp;
                }
                else if (args[2].Equals("udp", StringComparison.OrdinalIgnoreCase))
                {
                    protocol = Protocol.Udp;
                }
                else
                {
                    Console.WriteLine("\n{0} is not a vaild argument. Please enter a vaild protocol, either tcp or udp.", args[2]);
                    return;
                }
            }

            // delete mappings

            NatDevice device = await Init();

            if (bothProtocols)
            {
                await device.DeletePortMapAsync(new Mapping(Protocol.Tcp, port, port));
                await device.DeletePortMapAsync(new Mapping(Protocol.Udp, port, port));
                Console.WriteLine("\nDeleted mappings for port {0}.", args[1]);
            }
            else
            {
                await device.DeletePortMapAsync(new Mapping(protocol, port, port));
                Console.WriteLine("\nDeleted mapping for port {0} ({1} only).", args[1], args[2].ToUpper());
            }
        }

        public static async Task ClearAllMappings()
        {
            // clear all mappings

            NatDevice device = await Init();

            IEnumerable<Mapping> mappings = await device.GetAllMappingsAsync();

            if (mappings.Count() < 1)
            {
                Console.WriteLine("\nCurrently there are no open port mappings.");
                return;
            }

            foreach (Mapping mapping in mappings)
            {
                await device.DeletePortMapAsync(mapping);
            }

            Console.WriteLine("\nCleared all currently open port mappings.");
        }

        public static async Task ListAllMappings()
        {
            NatDevice device = await Init();

            IEnumerable<Mapping> mappings = await device.GetAllMappingsAsync();

            if (mappings.Count() < 1)
            {
                Console.WriteLine("\nCurrently there are no open port mappings.");
                return;
            }

            Console.WriteLine("\nList of all port mappings:\n");

            foreach (Mapping mapping in mappings)
            {
                Console.WriteLine(" - {0} : {1}", mapping.PrivatePort, mapping.Protocol.ToString().ToUpper());
            }
        }

        public static void ShowHelp()
        {
            Console.WriteLine("\n" +
                "Universal Plug 'n Play Manager by SimplifedUser\n" +
                "List of valid commands:\n\n" +
                " -a [port] ([protocol])  Add port mappings for the specified port (and protocol).\n" +
                " -d [port] ([protocol])  Delete port mappings for the specified port (and protocol).\n" +
                " -c                      Clear all currently open port mappings.\n" +
                " -l                      List currently opened ports mappings.\n" +
                " -?                      Show this help screen.\n\n" +
                "Additionally there are these setup commands available:\n\n" +
                " --install               Add location of this program to user's path.\n" +
                " --uninstall             Remove location of this program from user's path.\n" +
                "\n");
        }

        private static async Task<NatDevice> Init()
        {
            NatDiscoverer discoverer = new NatDiscoverer();
            return await discoverer.DiscoverDeviceAsync();
        }
    }
}
