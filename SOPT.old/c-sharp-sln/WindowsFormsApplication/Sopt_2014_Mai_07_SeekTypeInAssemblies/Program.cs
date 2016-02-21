using System;
using System.Linq;
using System.Reflection;

namespace WindowsFormsApplication.Sopt_2014_Mai_07_SeekTypeInAssemblies
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var computador = GetInstanciaDoTipo<Computador>("Calculadora");

            var usarReflexao = new UsandoReflexaoParaAcessarObjeto();
            usarReflexao.Objeto = computador;
            var resultado1 = usarReflexao.TentarSomar();
            var resultado2 = usarReflexao.SomarUsandoDynamic();
            var resultado3 = computador.Somar(1, 2);

            var mets = usarReflexao.ListarMetodos();
        }

        private static T GetInstanciaDoTipo<T>(string nomeTipo)
        {
            return
                (T)Assembly.GetExecutingAssembly()
                    .GetTypes()
                    .Where(t => t.Name == nomeTipo)
                    .Where(t => typeof(T).IsAssignableFrom(t))
                    .Select(Activator.CreateInstance)
                    .First();
        }

        abstract class Computador
        {
            public int Valor { get; set; }

            public abstract int Somar(int a, int b);

            public virtual int Somar0(int a, int b)
            {
                return a + b;
            }
        }

        class Calculadora : Computador
        {
            public int Valor2 { get; set; }

            public override int Somar(int a, int b)
            {
                return a + b;
            }

            public int Somar2(int a, int b)
            {
                return a + b;
            }
        }

        /// <summary>
        /// Esta classe usa reflexão para acessar o objeto, seja lá qual o tipo dele.
        /// Também há um método usando dynamic, para exemplificar como é mais fácil usar dynamic do que reflection,
        /// quando queremos executar algo dinamicamente.
        /// </summary>
        public class UsandoReflexaoParaAcessarObjeto
        {
            public object Objeto { get; set; }

            public string[] ListarMetodos()
            {
                var t = Objeto.GetType();
                return t.GetMethods()
                    .Where(mi => mi.DeclaringType == t)
                    .Where(mi => !mi.IsSpecialName)
                    .Select((mi, i) => i + " [+] " + mi.Name)
                    .ToArray();
            }

            public int? TentarSomar()
            {
                if (this.Objeto != null)
                {
                    var type = this.Objeto.GetType();
                    var methodInfo = type.GetMethod("Somar");
                    var result = methodInfo.Invoke(this.Objeto, new object[] { 1, 2 });
                    return (int)result;
                }

                return null;
            }

            public int? SomarUsandoDynamic()
            {
                if (this.Objeto != null)
                {
                    dynamic dyn = this.Objeto;
                    return dyn.Somar(1, 2);
                }

                return null;
            }
        }
    }
}