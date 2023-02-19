using System;
using Open.Nat;

namespace upnp // Note: actual namespace depends on the project name.
{
    static class Program
    {
        static async Task Main(string[] args)
        {

            try
            {

                if (args.Length == 0 || args[0] == "-?" || args[0] == "-h" || args[0] == "--help")
                {
                    Commands.ShowHelp();
                    return;
                }
                else if (args[0] == "-a" || args[0] == "--add")
                {
                    await Commands.AddMapping(args);
                    return;
                }
                else if (args[0] == "-d" || args[0] == "--delete")
                {
                    await Commands.DeleteMapping(args);
                    return;
                }
                else if (args[0] == "-c" || args[0] == "--clear")
                {
                    await Commands.ClearAllMappings();
                    return;
                }
                else if (args[0] == "-l" || args[0] == "--list")
                {
                    await Commands.ListAllMappings();
                    return;
                }
                else if (args[0] == "--install")
                {
                    Setup.Install();
                    return;
                }
                else if (args[0] == "--uninstall")
                {
                    Setup.Uninstall();
                    return;
                }
                else
                {
                    Console.WriteLine("\n{0} is not a vaild argument. Please type -? for futher help.", args[0]);
                }

            } catch (Exception ex)
            {
                Console.WriteLine("\nAn error occured while executing the command:\n");
                Console.WriteLine(ex.Message);
            }

        }

        

    }
}