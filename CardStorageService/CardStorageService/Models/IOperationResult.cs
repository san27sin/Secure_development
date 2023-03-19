namespace CardStorageService.Models
{
    //интерфейс сигнализирует нам о том, что класс который имплементирует подобный интерфейс
    //должен возвращать код ошибки и текст ошибки в случае, если произошло исключение
    public interface IOperationResult
    {
        int ErrorCode { get; }

        string? ErrorMessage { get; }        
    }
}
