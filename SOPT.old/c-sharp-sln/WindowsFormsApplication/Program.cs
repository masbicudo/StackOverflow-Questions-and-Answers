using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Seguranca;

namespace WindowsFormsApplication
{
    static class Program
    {
        struct TesteStruct
        {
            public int valor;
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var arr1 = new[] { 1, 2, 3, 4, 5, 6 };
            var arr2 = new[] { 4, 5 };
            //arr2 = new int[0];

            var pos = FindSubList(arr1, arr2);

            List<byte> bytes = new List<byte> { 10, 20, 30, 40, 50, 60, 70, 80 };
            List<byte> pattern = new List<byte> { 30, 40, 50 };
            List<byte> result = bytes.Where(e => pattern.Contains(e)).ToList<byte>();
            IEnumerable<byte> result1 = bytes.Intersect(pattern).ToArray();

            TesteStruct t;
            t.valor = 10;
            var xpto = t.valor;

            StaticThrow.Instance = new StaticThrow();
            StaticThrow.Instance.Value = 10;
            StaticThrow.Instance.Throws = true;
            try { var value1 = Xpto.fillValue; }
            catch { }
            StaticThrow.Instance.Throws = false;
            StaticThrow.Instance.Value = 20;
            var value2 = Xpto.fillValue;

            // resposta
            var enc = StringEncritacao.Encritacao("MASB");
            var dec = StringEncritacao.Decriptacao(enc);

            // resposta 
            var obj = new Class2();
            obj.Get<A1>(1);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        public static int FindSubList<T>(IList<T> mainList, IEnumerable<T> subEnumerable)
        {
            var subList = subEnumerable as IList<T>;
            var subListCount = subList != null ? subList.Count : subEnumerable.Count();
            for (int it = 0; it <= mainList.Count - subListCount; it++)
                if (!subEnumerable.Where((t, it2) => !mainList[it + it2].Equals(t)).Any())
                    return it;

            return -1;
        }
    }
}
