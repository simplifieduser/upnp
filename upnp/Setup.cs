using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace upnp
{
    public abstract class Setup
    {

        public static void Install()
        {

            string dir = Environment.CurrentDirectory;
            string? path = Environment.GetEnvironmentVariable("path", EnvironmentVariableTarget.User);

            if (path == null) {
                Console.WriteLine("\nCannot find enviormental variable \"path\" for current user.");
                return;
            }
            if (!Directory.Exists(dir)) {
                Console.WriteLine("\nCannot retrieve current working directory.");
                return;
            } 

            if (path.EndsWith(";"))
            {
                path += dir;
            }
            else
            {
                path += ";" + dir;
            }

            Environment.SetEnvironmentVariable("path", path, EnvironmentVariableTarget.User);
            Console.WriteLine("\nSurcessfully added this application to users path.\n" +
                "Please note that if you move the application the uninstall command will not work anymore.");

        }

        public static void Uninstall()
        {

            string dir = Environment.CurrentDirectory;
            string? path = Environment.GetEnvironmentVariable("path", EnvironmentVariableTarget.User);

            if (path == null)
            {
                Console.WriteLine("\nCannot find enviormental variable \"path\" for current user.");
                return;
            }
            if (!Directory.Exists(dir))
            {
                Console.WriteLine("\nCannot retrieve current working directory.");
                return;
            }

            int startIndex = path.IndexOf(dir);

            if (startIndex == -1)
            {
                Console.WriteLine("\nCannot find application in path.\n" +
                    "Have you moved the applications elsewhere since setup?");
                return;
            }

            int endIndex = path.IndexOf(";", startIndex);

            if (endIndex != -1)
            {
                path = path.Remove(startIndex, endIndex - startIndex);
            }
            else
            {
                path = path.Remove(startIndex);
            }

            Environment.SetEnvironmentVariable("path", path, EnvironmentVariableTarget.User);
            Console.WriteLine("\nSurcessfully removed this application from users path.");

        }

    }
}
