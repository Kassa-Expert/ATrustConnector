namespace KassaExpert.ATrustConnector.Lib
{
    public interface IResponse
    {
        bool IsSuccessful { get; }

        string? ErrorMessage { get; }
    }

    public interface IResponse<out T> : IResponse
    {
        T? Payload { get; }
    }
}