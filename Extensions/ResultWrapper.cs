namespace VerstaTestTask.Extensions
{
    public sealed class ResultWrapper<T> where T : class
    {
        private ResultWrapper() 
        { 
        }

        public static ResultWrapper<T> CreateFromResult(T result)
        {
            return new ResultWrapper<T>
            {
                IsOk = true,
                Data = result
            };
        }

        public static ResultWrapper<T> CreateFromException(Exception ex)
        {
            return new ResultWrapper<T>
            {
                IsOk = false,
                Exception = ex
            };
        }

        public T Data { get; private set; }

        public Exception Exception { get; private set; }
        public bool IsOk { get; private set; }
    }
}
