using laundry.project.Business;
using laundry.project.Entities;
using laundry.project.Infrastructure;
using laundry.project.Infrastructure.Sender;
using laundry.project.Presentation;
using System;
using System.Collections.Generic;
using System.IO;

namespace laundry.project
{
    class Program
    {
        static void Main(string[] args)
        {
            SetConsoleSizeToMax();
            "Reading configuration file...".WriteLineRight(ConsoleColor.Blue);

            ConfigurationManager configService = new();
            DisplayManager displayService = new DisplayManager();

            "Configuration File Path: ".WriteLeft(ConsoleColor.White);
            string fileConfigFilePath = Console.ReadLine();

            "Configuration File Name: ".WriteLeft(ConsoleColor.White);
            string fileConfigFileName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(fileConfigFilePath) || string.IsNullOrWhiteSpace(fileConfigFileName))
            {
                Console.WriteLine("Invalid path or file name.");
                return;
            }

            List<Machine> machines;
            try
            {
                machines = configService.SetConfig(Path.Combine(fileConfigFilePath, fileConfigFileName));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading configuration: {ex.Message}");
                return;
            }

            if (machines == null || machines.Count == 0)
            {
                Console.WriteLine("No machines were configured.");
                return;
            }

            SensorManager sensor = new SensorManager();
            ISender sender = new AzureSender();
            

            StateMachineManager.LancerStateMachine(machines, sensor, sender);
            displayService.DicplayConfiguration(machines, Structure.MapInputMachine);
            displayService.DicplayMenuPrincipale(machines, Structure.MapInputMachine);
        }

        static void SetConsoleSizeToMax()
        {
            try
            {
                int maxWidth = Console.LargestWindowWidth;
                int maxHeight = Console.LargestWindowHeight;

                Console.SetWindowSize(maxWidth, maxHeight);
                Console.SetBufferSize(maxWidth, maxHeight);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error setting console size: {ex.Message}");
            }
        }
    }
}