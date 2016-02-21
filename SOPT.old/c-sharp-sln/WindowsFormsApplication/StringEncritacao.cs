using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Web;
using System.IO;
using System.Security.Cryptography;

namespace Seguranca
{
    /// <summary>
    /// Summary description for StringEncritacao
    /// </summary>
    public static class StringEncritacao
    {
        public static string Encritacao(string sourceData)
        {
            //define a chave e inicializa o valor do vetor
            byte[] key = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            byte[] iv = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            //converte o dado para array
            byte[] sourceDataBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(sourceData);
            //obter fluxo de memoria
            MemoryStream tempStream = new MemoryStream();
            //apanha o codificador e o fluxo de codificao
            DESCryptoServiceProvider encryptor = new DESCryptoServiceProvider();
            CryptoStream encryptionStream = new CryptoStream(tempStream, encryptor.CreateEncryptor(key, iv),
                CryptoStreamMode.Write);
            //dado de encriptacao
            encryptionStream.Write(sourceDataBytes, 0, sourceDataBytes.Length);
            encryptionStream.FlushFinalBlock();

            //poe o byte no array
            byte[] encryptedDataBytes = tempStream.GetBuffer();

            //converte o dado de encriptacao para string
            return Convert.ToBase64String(encryptedDataBytes, 0, (int)tempStream.Length);

            //
            // TODO: Add constructor logic here
            //

        }

        public static string Decriptacao(string sourceData)
        {
            //define a chave inicializacao  valores vecto
            byte[] key = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            byte[] iv = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            //convert o dado para array de byte
            byte[] encryptedDataBytes = Convert.FromBase64String(sourceData);
            //apanha o codigo do fluxo memoria e enche 
            MemoryStream tempStream = new MemoryStream(encryptedDataBytes, 0, encryptedDataBytes.Length);

            //apanha o decriptador e decriptar o fluxo
            DESCryptoServiceProvider decryptor = new DESCryptoServiceProvider();
            CryptoStream decryptionStream = new CryptoStream(tempStream, decryptor.CreateDecryptor(key, iv),
                CryptoStreamMode.Read);

            //desicriptar
            StreamReader allDataReader = new StreamReader(decryptionStream);
            return allDataReader.ReadToEnd();

        }
    }
}