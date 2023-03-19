using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoKeysVerification
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("~~ Центр генерации сертификатов ~~~\n");
                Console.WriteLine("1. Создать корневой сертификат");
                Console.WriteLine("2. Создать сертификат");
                Console.WriteLine("Выберите подпрограмму (0 - завершение работы приложения): ");
                if (int.TryParse(Console.ReadLine(), out int no))
                {
                    switch (no)
                    {
                        case 0:
                            Console.WriteLine("Завршение работы приложения.");
                            Console.ReadKey();
                            return;
                        case 1:
                            CertificateConfiguration certificateConfiguration = new CertificateConfiguration
                            {
                                CertName = "MyCompany CA",
                                OutFolder = @"C:\Users\san27sin\ver_key",
                                Password = "12345678",
                                CertDuration = 30
                            };
                            CertificateGenerationProvider certificateGenerationProvider = new CertificateGenerationProvider();
                            certificateGenerationProvider.GenerateRootCertificate(certificateConfiguration);
                            Console.WriteLine("Успех!");
                            break;
                        case 2:
                            int counter = 0;
                            var certificateExplorerProvider = new CertificateExplorerProvider(true);
                            certificateExplorerProvider.LoadCertificates();
                            foreach (var certificate in certificateExplorerProvider.Certificates)
                            {
                                Console.WriteLine($"{counter++} >>> {certificate}");
                            }
                            Console.Write("Укажите номер корневого сертификата: ");

                            var addCertificateConfiguration = new CertificateConfiguration
                            {
                                RootCertificate = certificateExplorerProvider.Certificates[int.Parse(Console.ReadLine())].Certificate,
                                CertName = "ITDepartment",
                                OutFolder = @"C:\Users\san27sin\ver_key",
                                Password = "12345678",
                            };
                            CertificateGenerationProvider certificateGenerationProvider2 = new CertificateGenerationProvider();
                            certificateGenerationProvider2.GenerateCertificate(addCertificateConfiguration);
                            Console.WriteLine("Успех!");
                            break;
                        default:
                            Console.WriteLine("Некорректный номер подпрограммы. Пожалуйста, повторите ввод.");
                            break;
                    }
                }
                else
                    Console.WriteLine("Некорректный номер подпрограммы. Пожалуйста, повторите ввод.");
            }
        }
    }
}
