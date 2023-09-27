using System;
using System.IO;

namespace cs2_server_maker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Counter Strike 2 | Server Maker | Pre-release";
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Counter Strike 2 | Server Maker\n");
            Console.ResetColor();
            Console.Write("by ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("astral | https://github.com/sadcloudisgay/cs2-server-maker\n");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nVersion | 1.0.0");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nPress Enter to continue ...");
            Console.ReadLine();

            // Set the default path variable without the newline character
            string defaultPath = "C:\\Program Files (x86)\\Steam\\steamapps\\common\\Counter-Strike Global Offensive\\game\\bin\\win64";

            // Call the AskForCSGOPath method to get the CS:GO path
            string csgoPath = AskForCSGOPath(defaultPath);

            // Now you can use the csgoPath variable for further processing
            Console.WriteLine($"CS2 path entered: {csgoPath}\n");

            // Replace all occurrences of "de_inferno" with "de_mirage" in the generated files
            ReplaceInFiles(csgoPath, "de_inferno", "de_mirage");

            // Call the CreateGameModeFiles method and pass csgoPath as an argument
            CreateGameModeFiles(csgoPath);

            // Continue with the rest of your code...
            Console.ReadLine(); // Keep the console window open
        }

        public static string AskForCSGOPath(string defaultPath)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nPlease enter your CS2 path");
            Console.ResetColor();
            Console.Write("\nExample: ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(defaultPath);
            Console.ResetColor(); // Reset color here to avoid affecting the rest of the output
            Console.Write("\nPath: ");
            Console.ForegroundColor = ConsoleColor.Yellow;

            // Read the user input and store it in the 'csgoPath' variable
            string csgoPath = Console.ReadLine();
            Console.ResetColor();

            // If the user didn't provide input, use the default path
            if (string.IsNullOrWhiteSpace(csgoPath))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                csgoPath = defaultPath;
                Console.ResetColor();
            }

            return csgoPath;
        }

        public static void ReplaceInFiles(string directory, string searchString, string replaceString)
        {
            string[] files = Directory.GetFiles(directory, "*.bat");
            foreach (string file in files)
            {
                string content = File.ReadAllText(file);
                content = content.Replace(searchString, replaceString);
                File.WriteAllText(file, content);
            }
        }

        public static void CreateGameModeFiles(string csgoPath)
        {
            // Loop to create four files with different game_mode values
            for (int gameMode = 0; gameMode <= 3; gameMode++)
            {
                string fileName;
                string gameModeName;

                switch (gameMode)
                {
                    case 0:
                        fileName = $"{csgoPath}\\Casual_server.bat";
                        gameModeName = "Casual";
                        break;
                    case 1:
                        fileName = $"{csgoPath}\\Competitive_server.bat";
                        gameModeName = "Competitive";
                        break;
                    case 2:
                        fileName = $"{csgoPath}\\Wingman_server.bat";
                        gameModeName = "Wingman";
                        break;
                    default:
                        continue; // Skip unsupported game modes
                }

                // Set the game_mode value and write it to the file
                string command = $"start cs2.exe -dedicated -port 27015 -usercon +game_type 0 +game_mode {gameMode} +map de_mirage";
                System.IO.File.WriteAllText(fileName, command);

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"\nCreated files for {gameModeName} mode");

                // Create the corresponding config file for this mode
                CreateConfigFile($"{csgoPath}\\config_{gameModeName}.cfg");
            }
        }

        public static void CreateConfigFile(string configFileName)
        {
            // Configuration content for each mode
            string configContent = @"// add your config here";

            // Write the configuration content to the config file
            File.WriteAllText(configFileName, configContent);

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\nCreated config files for that mode");
        }
    }
}
