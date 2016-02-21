using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SOPT_Answers
{
    class Program
    {
        static void Main(string[] args)
        {
            var result = InFixToPostFix.DoIt("(A + B) * (C + D)");


            Directory.GetFiles("C:\\", "*.txt", SearchOption.AllDirectories);

            Launcher.Executar();
        }

        private static int a = 0;
        private static int b = 0;
        private static readonly object locker = new object();
        private static readonly ManualResetEventSlim mreA = new ManualResetEventSlim(true);
        private static readonly ManualResetEventSlim mreB = new ManualResetEventSlim(true);

        static void A()
        {
            while (true)
            {
                lock (locker)
                {
                    // se não tem Bs executando, então o A pode executar
                    if (b == 0)
                    {
                        // o A vai executar, então devemos:
                        //  - impedir o acesso de Bs
                        //  - incrementar o contador de As
                        mreB.Reset();
                        a++;
                        break;
                    }
                }

                // já tem Bs executando, vamos ficar esperando um sinal para continuar A
                mreA.Wait();
            }

            Console.WriteLine("A está executando");

            lock (locker)
            {
                // o A terminou, então:
                //  - decrementamos o contador de As
                //  - liberamos os Bs, se não houver mais As executando
                a--;
                if (a == 0)
                    mreB.Set();
            }
        }

        static void B()
        {
            while (true)
            {
                lock (locker)
                {
                    // se não tem As executando, então o B pode executar
                    if (a == 0)
                    {
                        // o B vai executar, então devemos:
                        //  - impedir o acesso de As
                        //  - incrementar o contador de Bs
                        mreA.Reset();
                        b++;
                        break;
                    }
                }

                // já tem As executando, vamos ficar esperando um sinal para continuar B
                mreB.Wait();
            }

            Console.WriteLine("B está executando");

            lock (locker)
            {
                // o B terminou, então:
                //  - decrementamos o contador de Bs
                //  - liberamos os As, se não houver mais Bs executando
                b--;
                if (b == 0)
                    mreA.Set();
            }
        }

        static void Main2(string[] args)
        {
        }
    }
}
