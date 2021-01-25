namespace KassaExpert.ATrustConnector.Lib.ResponseDto
{
    public class Response : IResponse
    {
        internal Response(bool isSuccessful, string? errorMessage)
        {
            IsSuccessful = isSuccessful;

            if (!isSuccessful)
            {
                ErrorMessage = errorMessage;
            }
        }

        public bool IsSuccessful { get; }

        public string? ErrorMessage { get; }
    }

    public sealed class Response<T> : Response, IResponse<T>
    {
        internal Response(bool isSuccessful, T? payload, string? errorMessage) : base(isSuccessful, errorMessage)
        {
            if (isSuccessful)
            {
                Payload = payload;
            }
        }

        public T? Payload { get; }
    }
}