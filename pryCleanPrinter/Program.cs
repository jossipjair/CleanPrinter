using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.IO;

namespace pryCleanPrinter
{
    class Program
    {
        static void Main(string[] args)
        {

            string messageIni = "  " +
                "\n/$$$$$$$$ /$$                         /$$   /$$                       /$$                        " +
                "\n| __  $$__ /| $$                     | $$  | $$                      | $$                        " +
                "\n    | $$   | $$$$$$$   /$$$$$$       | $$  | $$ /$$   /$$ /$$$$$$$  /$$$$$$    /$$$$$$   /$$$$$$ " +
                "\n    | $$   | $$__  $$ /$$__  $$      | $$$$$$$$| $$  | $$| $$__  $$| _ $$_ /  /$$__  $$ /$$__  $$" +
                "\n    | $$   | $$  \\$$ | $$$$$$$$      | $$__  $$| $$  | $$| $$  \\$$   | $$    | $$$$$$$$| $$  \\__ /" +
                "\n    | $$   | $$  | $$| $$_____ /     | $$  | $$| $$  | $$| $$  | $$  | $$ /$$| $$_____/| $$      " +
                "\n    | $$   | $$  | $$|  $$$$$$$      | $$  | $$|  $$$$$$/| $$  | $$  |  $$$$/|  $$$$$$$| $$      " +
                "\n    |__/   |__/  |__/ \\_______/      |__/  |__/ \\______/ |__/  |__/   \\___/   \\_______/|__/ " +
                "\n                                                                                         v1.0                         " +
                "\n                                                                                         JEJ                          ";
            Console.Title = "CLEAN PRINTER v1.0";
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Clear();
            Console.WriteLine(messageIni);

            Console.WriteLine("                                         CLEAN PRINTER v1.0");
            Console.WriteLine("%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%");
            Console.WriteLine("\n");

            Console.WriteLine("** Reiniciando Servicio **");
            string commandReboot = "net start spooler";
            runCommandCmd(commandReboot, false);

            Console.WriteLine("** Deteniendo Servicio **");
            string commandStop = "net stop spooler";
            runCommandCmd(commandStop, true);

            Console.WriteLine("** Limpiando Memoria e Iniciando Servicio**");
            string dir = @"C:\WINDOWS\system32\spool\PRINTERS\";

            try
            {
                List<string> strFiles = Directory.GetFiles(dir, "*", SearchOption.AllDirectories).ToList();

                foreach (string fichero in strFiles)
                {
                    File.Delete(fichero);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al limpiar: Ejecute en modo administrador");
            }

            Console.WriteLine("Iniciando Servicio...");
            string commandIni = "net start spooler";
            runCommandCmd(commandIni, true);
            Console.WriteLine("Iniciciando Panel de Control -> Dispositivos e Impresoras");
            Console.WriteLine("\n");

            //Muestra ventana Impresoras y dispositivos
            runCommandCmd("control printers", false);

            Console.Write("Pulsa Enter para finalizar");

            ConsoleKeyInfo keyInfo;
            while (true)
            {
                //Valida si se ha pulsado ENTER para finalizar el programa
                keyInfo = Console.ReadKey();
                if (keyInfo.Key.ToString().Equals("Enter"))
                {
                    Environment.Exit(0);
                }
            }

            void runCommandCmd(string comand, bool viewStatus)
            {

                //Iniciamos CMD
                ProcessStartInfo processInit = new ProcessStartInfo("cmd", "/c " + comand);

                //Redireccion Stream
                processInit.RedirectStandardOutput = true;
                processInit.UseShellExecute = false;

                //Proceso en BackGround
                processInit.CreateNoWindow = false;

                //Inicializa el proceso
                Process process = new Process();
                process.StartInfo = processInit;
                process.Start();

                //Consigue la salida de la Consola(Stream) y devuelve una cadena de texto
                string result = process.StandardOutput.ReadToEnd();
                if (viewStatus)
                {
                    Console.Write(result);
                }
            }
        }


    }
}
