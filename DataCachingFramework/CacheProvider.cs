using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;
using System.Security.Cryptography;

namespace DataCachingFramework
{
    public class CacheProvider
    {
        static byte[] additionalEntropy = { 5, 3, 7, 1, 5 };//как соль для шифрования 

        public void CacheConnections(List<ConnectionString> connections)
        {
            try
            {
                var xmlSerializer = new XmlSerializer(typeof(List<ConnectionString>));

                var memoryStream = new MemoryStream();//для хранения сериолизованных данных в памяти текущего приложения
                                                      //MemoryStream - класс который позволяет хранить поток данных в памяти текущего приложения 

                var xmlWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);//промежуточный вспомогательный объект который 
                                                                               //который связан с memorystream и позволяет в этот поток записывать некоторый xml документ

                xmlSerializer.Serialize(xmlWriter, connections);//связываем сериализатор с xmlwriter и с коннектом к БД
                byte[] protectedData = Protect(memoryStream.ToArray());
                File.WriteAllBytes($"{AppDomain.CurrentDomain.BaseDirectory}data.protected", protectedData);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Serialize data error.");
            }
        }

        public List<ConnectionString> GetConnectionFromCache()
        {
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<ConnectionString>));
                byte[] data = File.ReadAllBytes($"{AppDomain.CurrentDomain.BaseDirectory}data.protected");
                return (List<ConnectionString>)xmlSerializer.Deserialize(new MemoryStream(Unprotect(data)));
            }
            catch(Exception ex)
            {
                Console.WriteLine("Deserialize data error.");
                return null;
            }            
        }

        private byte[] Protect(byte[] data)
        {
            try
            {
                return ProtectedData.Protect(data, additionalEntropy, DataProtectionScope.LocalMachine);
            }
            catch(CryptographicException e)
            {
                Console.WriteLine($"Protect error.\n{e.Message}");
                return null;
            }            
        }

        private byte[] Unprotect(byte[] data)
        {
            try
            {
                return ProtectedData.Unprotect(data, additionalEntropy, DataProtectionScope.LocalMachine);
            }
            catch(CryptographicException e) 
            {
                Console.WriteLine($"Unprotect error.\n{e.Message}");
                return null;
            }            
        }
    }
}
