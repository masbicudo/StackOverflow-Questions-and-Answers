using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOPT_Answers
{
    public class Launcher
    {
        public static void Executar()
        {
            Console.WriteLine(Environment.CurrentDirectory);
            var proc = Process.Start(@"..\AppToLaunch\ToBeLaunched.exe");
            proc.WaitForExit();
        }
    }
}
