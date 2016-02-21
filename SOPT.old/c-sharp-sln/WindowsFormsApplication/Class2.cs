using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication
{
    public class StaticThrow
    {
        public static StaticThrow Instance { get; set; }
        public int Value { get; set; }
        public bool Throws { get; set; }
    }

    public class Xpto
    {
        public static readonly int fillValue;
        static Xpto()
        {
            if (StaticThrow.Instance.Throws)
            {
                fillValue = StaticThrow.Instance.Value;
                StaticThrow.Instance.Throws = false;
                throw new Exception("Throw!");
            }
        }
    }
}