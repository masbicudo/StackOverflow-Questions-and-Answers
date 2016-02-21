using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOPT_Answers
{
    class InFixToPostFix
    {
        public static string DoIt2(string expressao)
        {
            var pilha = new Stack(20);

            for (int it = 0; it < expressao.Length; it++)
            {
                pilha.Push(expressao[it]);

            }
        }

        private class Node
        {
            public char Operador { get; set; }
            public Node Node1 { get; set; }
            public Node Node2 { get; set; }
        }

        public static String DoIt(String expressao)
        {
            String resultado = null;
            Stack pilha = new Stack();
            char caracter;
            int prioridade = 0;

            for (int i = 0; i < expressao.Length; i++)
            {
                caracter = expressao[i];

                if (IsOperando(caracter))
                {
                    resultado += caracter;
                }
                else if (IsOperador(caracter))
                {
                    prioridade = ObterPrioridade(caracter);
                    while ((pilha.Count != 0) && (ObterPrioridade(Convert.ToChar(pilha.Peek())) >= prioridade))
                    {
                        resultado += pilha.Pop().ToString();
                    }

                    pilha.Push(caracter);
                }
                else if ('(' == caracter)
                {
                    pilha.Push(caracter);
                }
                else if (')' == caracter)
                {
                    String item = pilha.Pop().ToString();
                    while (!item.Equals("("))
                    {
                        resultado += item;
                        item = pilha.Pop().ToString();
                    }
                }
            }

            while (pilha.Count != 0)
            {
                resultado += pilha.Pop().ToString();
            }

            return resultado;
        }

        private static int ObterPrioridade(char caracter)
        {
            int retorno = 0;
            String pri2 = "+-";
            String pri3 = "*/";
            if ('(' == caracter)
            {
                retorno = 1;
            }
            else if (pri2.IndexOf(caracter) >= 0)
            {
                retorno = 2;
            }
            else if (pri3.IndexOf(caracter) >= 0)
            {
                retorno = 3;
            }
            else if ('^' == caracter)
            {
                retorno = 4;
            }
            return retorno;
        }

        private static bool IsOperando(char caracter)
        {
            String letras = "ABCDEFGHIJKLMNOPQRSTUVXZ";
            return (letras.IndexOf(caracter) >= 0);
        }

        private static bool IsOperador(char caracter)
        {
            String operadores = "+-*/^";
            return (operadores.IndexOf(caracter) >= 0);
        }
    }
}
