using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace WindowsFormsApplication.Sopt_2014_Mai_06_Any_All
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var zeroElements = new int[0];
            var any = zeroElements.Any(x => x == 0);
            var all = zeroElements.All(x => x == 0);
        }
    }
}