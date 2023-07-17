using Core.Utilities.Abstract;

namespace Core.Utilities.Concrete
{
    public class ResultData<T> : Result, IResultData<T>
    {
        public ResultData(T data, bool success, string message) : base(success, message)
        {
            Data = data;
        }

        public ResultData(T data, bool success) : base(success)
        {
            Data = data;
        }

        public T Data { get; }
    }
}
