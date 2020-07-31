using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace C_Testing
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Please Enter your VPN name: ");
            string vpnName = Console.ReadLine();
            Console.WriteLine("Please Enter your Workstation Number: ");
            string pcName = Console.ReadLine();
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
            System.Diagnostics.Process.Start("mstsc", "/v:" + "\"" + Workstation + "\"";);



        }
    }
}
