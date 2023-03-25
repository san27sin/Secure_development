using Grpc.Net.Client;//создает клиента который позволяет делать клиента по протоколу
using Microsoft.Data.SqlClient;
using static ClientServiceProtos.ClientService;

namespace CardStorageClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Создать клиента ...");
            Console.ReadLine();

            using (var channel = GrpcChannel.ForAddress("http://localhost:5015/"))//создаем один канал
            {
                //может быть множество клиентов
                var client = new ClientServiceClient(channel);

                var response = client.Create(new ClientServiceProtos.CreateClientRequest
                {
                    FirstName="Александр",
                    Surname="Синицын",
                    Patronymic="Романович"
                });

                Console.WriteLine($"ClientId: {response.ClientId}; ErrCode: {response.ErrorCode}; ErrMessage: {response.ErrorMessage}");
            }
        }
    }
}