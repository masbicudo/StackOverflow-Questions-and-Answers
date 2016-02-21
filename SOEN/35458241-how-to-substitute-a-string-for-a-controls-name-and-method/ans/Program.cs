using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ans
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var txt = Regex.Replace("01234567894", @"^(\d{3})(\d{3})(\d{4})(\d?)$", m =>
                $"({m.Groups[1]}) {m.Groups[2]}-{m.Groups[3]}{(m.Groups[4].Success ? " ext " + m.Groups[4].Value : "")}");

            var t = new Test();
            t.Items = new List<List<int>>
            {
                new List<int> { 1, 2 },
                new List<int> { 11, 12 }
            };

            var r = t.Flatten;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form2());

        }
    }

    class Test
    {
        public List<List<int>> Items { get; set; }

        public IEnumerable ItemsSource { get { return this.Items; } }

        public List<object> Flatten
        {
            get { return this.ItemsSource.Cast<IEnumerable>().SelectMany(item => item.Cast<object>()).ToList(); }
        }
    }
}
