using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCachingFramework
{
    internal class Program
    {
        static void Main(string[] args)
        {
#if DEBUG
            Console.Title = Properties.Settings.Default.ApplicationNameDebug;
#else
            Console.Title = Properties.Settings.Default.ApplicationName;
#endif

            if (string.IsNullOrEmpty(Properties.Settings.Default.FIO) || Properties.Settings.Default.Age <= 0)
            {
                Console.Write("Введите ФИО: ");
                Properties.Settings.Default.FIO = Console.ReadLine();

                Console.Write("Укажите ваш возраст: ");
                if (int.TryParse(Console.ReadLine(), out int age))
                {
                    Properties.Settings.Default.Age = age;
                }
                else
                {
                    Properties.Settings.Default.Age = 0;
                }
                Properties.Settings.Default.Save(); 
            }

            Console.WriteLine($"ФИО: {Properties.Settings.Default.FIO}");
            Console.WriteLine($"Возраст: {Properties.Settings.Default.Age}");

            // %USERPROFILE%\AppData\Local\
            //зашифровать данные
            ConnectionString connectionString1 = new ConnectionString
            {
                Database = "Database1",
                Host = "localhost",
                Password = "12345",
                UserName = "User1",
            };

            ConnectionString connectionString2 = new ConnectionString
            {
                Database = "Database2",
                Host = "localhost",
                Password = "11425",
                UserName = "User2",
            };

            List<ConnectionString> connections = new List<ConnectionString>();
            connections.Add(connectionString1);
            connections.Add(connectionString2);

            CacheProvider cacheProvider = new CacheProvider();
            cacheProvider.CacheConnections(connections);

            //расшифровать данные
            //CacheProvider cacheProvider = new CacheProvider();
            //List<ConnectionString> connections = cacheProvider.GetConnectionFromCache();
            
            //foreach(var connection in connections) 
            //{
            //    Console.WriteLine($"{connection.Host} {connection.Database} {connection.UserName} {connection.Password}");
            //}

            Console.ReadKey();
        }
    }
}
