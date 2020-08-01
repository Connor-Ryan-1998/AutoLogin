using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
namespace C_Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            readConfig();
            Console.WriteLine("Do you want to save config/enable autolaunch [Y/N]: ");
            string autoLaunch = Console.ReadLine();
            Console.WriteLine("Please Enter your VPN name: ");
            string vpnName = Console.ReadLine();
            Console.WriteLine("Please Enter your Workstation Number: ");
            string pcName = Console.ReadLine();
            config(autoLaunch, vpnName, pcName);
            vpnConnection(vpnName, pcName);
            Console.ReadKey();
        }

        static void vpnConnection(string VPN, string Workstation)
        {
            if (String.IsNullOrEmpty(Workstation))
            {
                Workstation = "INTOV8WS64";
            }

            if (String.IsNullOrEmpty(VPN))
            {
                VPN = "Intov8 BNE";
            }
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "rasdial.exe";
            startInfo.Arguments = "\"" + VPN + "\"";
            Process.Start(startInfo);
            Task.Delay(3000).Wait();
            System.Diagnostics.Process.Start("mstsc", "/v:" + "\"" + Workstation + "\"");
        }

        static void config(string autoconfig, string VPN, string Workstation)
        {
            string fileName = @".\config.txt";
            if (autoconfig == "Y")
            {
                try
                {
                    if (File.Exists(fileName)) ;
                    {
                        File.Delete(fileName);
                    }

                    using (FileStream fs = File.Create(fileName))
                    {
                        Byte[] config = new UTF8Encoding(true).GetBytes(" Config:" + autoconfig + ";");
                        fs.Write(config, 0, config.Length);
                        Byte[] vpn = new UTF8Encoding(true).GetBytes(" VPN:" + VPN + ";");
                        fs.Write(vpn, 0, vpn.Length);
                        Byte[] workstation = new UTF8Encoding(true).GetBytes(" Workstation:" + Workstation + ";");
                        fs.Write(workstation, 0, workstation.Length);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }
        static void readConfig()
        {
            string fileName = @".\config.txt";
            if (File.Exists(fileName))
            {
                using (StreamReader sr = File.OpenText(fileName))
                {
                    string configuration = "";
                    string autoLaunch = "";
                    string vpn = "";
                    string workStation = "";

                    while ((configuration = sr.ReadLine()) != null)
                    {
                        string[] split = configuration.Split(";");
                        foreach (var word in split)
                        {
                            if (word.Contains("Config"))
                            {
                                autoLaunch = word.Substring(word.IndexOf(":") + 1);
                            }
                            if (word.Contains("VPN"))
                            {
                                vpn = word.Substring(word.IndexOf(":") + 1);
                            }
                            if (word.Contains("Workstation"))
                            {
                                workStation = word.Substring(word.IndexOf(":") + 1);
                            }
                        }
                    }
                    if (autoLaunch == "Y")
                    {
                        ProcessStartInfo startInfo = new ProcessStartInfo();
                        startInfo.FileName = "rasdial.exe";
                        startInfo.Arguments = "\"" + vpn + "\"";
                        Process.Start(startInfo);
                        Task.Delay(3000).Wait();
                        System.Diagnostics.Process.Start("mstsc", "/v:" + "\"" + workStation + "\"");
                        System.Environment.Exit(1);
                    }
                }
            }
        }
    }
}
