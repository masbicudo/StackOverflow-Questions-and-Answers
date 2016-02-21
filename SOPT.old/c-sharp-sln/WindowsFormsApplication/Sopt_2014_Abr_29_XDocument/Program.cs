using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace WindowsFormsApplication.Sopt_2014_Abr_29_XDocument
{
    public class Program
    {
        private const string XML = @"
<paises>
<pais>
    <nome-pais>África do Sul</nome-pais>
    <consulados>        
        <consulado>
            <nome-consulado>Consulado da República da África do Sul</nome-consulado>        
            <endereco>Av. Paulista 1754, 12º andar</endereco>       
            <cep>01310-100</cep>
            <telefones>
                <telefone>(11)3265-0449</telefone>
                <telefone>(11)3265-0540</telefone>
            </telefones>
        </consulado>
    </consulados>
</pais>
<pais>
    <nome-pais>Albânia</nome-pais>  
    <consulados>        
        <consulado>
            <nome-consulado>Consulado da República da Albânia</nome-consulado>                  
            <cep>01310-100</cep>
            <telefone>(11) 3283-3305</telefone>
        </consulado>
    </consulados>
</pais>
</paises>
";

        public class ListaConsulado
        {
            public class Consulado
            {
                public string NomePais { get; set; }
                public List<string> Telefone { get; set; }
            }
        }

        public static void Main(string[] args)
        {
            XDocument xmlDoc = XDocument.Parse(XML);
            var consuladosComTelefones = xmlDoc
                .Element("paises")
                .Elements("pais")
                .SelectMany(
                    p => p.Element("consulados") == null ? null : p.Element("consulados")
                        .Elements("consulado")
                        .Select(c => new ListaConsulado.Consulado
                        {
                            NomePais = p.Element("nome-pais").Value,
                            Telefone = c.Element("telefones") == null ? null : c.Element("telefones")
                                .Elements("telefone")
                                .Select(t => t.Value).ToList(),
                        })).ToList();
        }
    }
}