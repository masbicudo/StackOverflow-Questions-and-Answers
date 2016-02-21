using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace WindowsFormsApplication.Sopt_2014_Abr_30_XDocument
{
    public class Program
    {
        private const string XML = @"
<paises>

<pais>
        <nome-pais>Austrália</nome-pais>    
        <consulados>        
            <consulado>
                <nome-consulado>Consulado-Geral da Austrália em São Paulo - SP </nome-consulado>        
                <endereco>Alameda Santos, 700 - 9º andar conjunto 92, Cerqueira César, São Paulo-SP</endereco>      
                <cep>01418-100</cep>                
                <fax>(11) 3171-2889</fax>
                <geral>1</geral>
                <honorario>0</honorario>
                <nome_img_bandeira>flag_australia</nome_img_bandeira>
                <url-site></url-site>
                <emails></emails>
                <nome-chefia>Mark Argar</nome-chefia>
                <telefones>
                    <telefone>(11) 2112-6200</telefone>
                    <telefone>(11) 2112-6215 </telefone>
                    <telefone>(11) 3171-2851 </telefone>
                </telefones>
                <observacao>Auxílio a cidadãos australianos: (0xx11) 3171-2851</observacao>
                <expediente></expediente>
                <jurisdicao></jurisdicao>
            </consulado>
        </consulados>
    </pais>

<pais>
        <nome-pais>Áustria</nome-pais>
        <consulados>        
            <consulado>
                <nome-consulado>Consulado-Geral da Áustria em São Paulo - SP</nome-consulado>       
                <endereco>Av. Dr. Cardoso de Melo 1470 - Conj. 711 - Ed. Net Office - Vila Olímpia, São Paulo-SP</endereco>     
                <cep>04548-005</cep>                
                <fax>(11) 3926-6798</fax>
                <geral>1</geral>
                <honorario>0</honorario>
                <nome_img_bandeira>flag_austria</nome_img_bandeira>
                <url-site></url-site>
                <emails>
                    <email>consuladosp@austria.org.br</email>
                </emails>
                <nome-chefia>Dr. Ingomar Lochschmidt (Cônsul), Stefan Nemetz (Vice-Cônsul)</nome-chefia>
                <telefones>
                    <telefone>(11) 3842-7500</telefone>
                </telefones>
                <observacao></observacao>
                <expediente></expediente>
                <jurisdicao></jurisdicao>
            </consulado>
            <consulado>
                <nome-consulado>Consulado-Geral da Áustria em São Paulo - SP - Departamento Comercial</nome-consulado>      
                <endereco>Av. Dr. Cardoso de Melo 1340 - 7º andar - Conj. 71 - Vila Olímpia, São Paulo-SP</endereco>        
                <cep>04548-004</cep>                
                <fax>(11) 3842-5330</fax>
                <geral>1</geral>
                <honorario>0</honorario>
                <nome_img_bandeira>flag_austria</nome_img_bandeira>
                <url-site></url-site>
                <emails>
                    <email>SaoPaulo@advantageaustria.org</email>
                    <email>saopaulo@wko.at</email>
                </emails>
                <nome-chefia>Dr. Ingomar Lochschmidt (Cônsul), Stefan Nemetz (Vice-Cônsul)</nome-chefia>
                <telefones>
                    <telefone>(11) 3044-9944</telefone>
                </telefones>
                <observacao></observacao>
                <expediente></expediente>
                <jurisdicao></jurisdicao>
            </consulado>
        </consulados>
    </pais>     
</paises>
";

        class ListaConsulado
        {

            public class Paises
            {
                public Paises()
                {
                    this.NomePais = string.Empty;
                    Consulados = new List<Consulado>();
                }
                public Paises(String NomePais)
                {
                    this.NomePais = NomePais;
                    Consulados = new List<Consulado>();
                }
                public Paises(String NomePais, List<Consulado> Consulados)
                {
                    this.NomePais = NomePais;
                    this.Consulados = Consulados;
                }

                public Paises(List<Consulado> Consulados)
                {
                    this.NomePais = string.Empty;
                    this.Consulados = Consulados;
                }
                public String NomePais { get; set; }
                public IList<Consulado> Consulados { get; set; }
            }

            public class Consulado
            {
                public string NomePais { get; set; }
                public string NomeConsulado { get; set; }
                public string Endereco { get; set; }
                public string Cep { get; set; }
                public string Fax { get; set; }
                public string Geral { get; set; }
                public string Honorario { get; set; }
                public string UrlSite { get; set; }
                public string NomeChefia { get; set; }
                public string Observacao { get; set; }
                public string Expediente { get; set; }
                public string Jurisdicao { get; set; }
                public List<string> Telefone { get; set; }
                public List<string> Email { get; set; }
            }
        }

        public static void Main(string[] args)
        {
            XDocument xmlDoc = XDocument.Parse(XML);
            var consulados =
                (from c in xmlDoc.Descendants("consulado")
                 let p = c.Ancestors("pais").Single()
                 let nomepais = p.Element("nome-pais").Value
                 where nomepais == "Austrália"
                 select new ListaConsulado.Consulado
                 {
                     NomePais = nomepais,
                     Cep = "Cep: " + c.Element("cep").Value,
                     Endereco = c.Element("endereco").Value,
                     NomeConsulado = c.Element("nome-consulado").Value,
                     Telefone = c.Descendants("telefone").Select(t => t.Value).ToList(),
                     Email = c.Descendants("email").Select(t => t.Value).ToList(),
                 }).ToList();
        }
    }
}