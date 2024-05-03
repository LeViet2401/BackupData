using Microsoft.Extensions.Configuration;
using System.Reflection.Emit;

namespace BackupData
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            IConfiguration configuration = builder.Build();

            OptionBuilder optionBuilder = new OptionBuilder(configuration);
            
            DataProcess dataProcess;

        menu:
            if (DisplayMenu() == "1")
            {
                string selected = ImportMenu();
                if (selected == "3")
                {
                    goto menu;
                }
                else if (selected == "2")
                {
                    Console.WriteLine("Please custom option:");
                    Console.Write("[1] Type of Export File (CSV/SQL/TXT): ");
                    optionBuilder.ChangeExtension(Console.ReadLine());
                    Console.Write("[2] Create Compression File (Y/N): ");
                    if(Console.ReadLine().ToLower() == "y")
                    {
                        optionBuilder.ExportAddCompresion();
                    }
                    Console.Write("[3] Add DateTime to File Name (Y/N): ");
                    if (Console.ReadLine().ToLower() == "y")
                    {
                        optionBuilder.AddExportTime();
                    }
                }
                var datainfo = optionBuilder.Build();
                datainfo = InputDataMenu(datainfo);
                dataProcess = new DataProcess(datainfo);
                Console.WriteLine(dataProcess.ExportData());
                goto menu;
            }
            else
            {
                goto menu;
            }
        }

        static string DisplayMenu()
        {
            string selected = string.Empty;
            do
            {
                Console.WriteLine("Please select action:");
                Console.WriteLine("[1] ExportData");
                Console.WriteLine("[2] ImportData");
                selected = Console.ReadLine();

            }
            while (selected != "1" && selected != "2");
            return selected;
        }

        static string ImportMenu()
        {
            string selected = string.Empty;
            do
            {
                Console.WriteLine("Please select Option:");
                Console.WriteLine("[1] DefaultOption");
                Console.WriteLine("[2] CustomAction");
                Console.WriteLine("[3] Back to Main Menu");

                selected = Console.ReadLine();

            }
            while (selected != "1" && selected != "2" && selected != "3");
            return selected;
        }

        static DataInfo InputDataMenu(DataInfo datainfo)
        {
            Console.WriteLine("Input Data Information: ");
            Console.Write("Directory Path: ");
            datainfo.DirectoryPath = Console.ReadLine();
            Console.Write("Table Name: ");
            datainfo.FileName = Console.ReadLine();
            Console.Write("Query String: ");
            datainfo.QueryString = Console.ReadLine();
            return datainfo;
        }
    }
}
