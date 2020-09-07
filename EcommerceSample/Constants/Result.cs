using System;
namespace EcommerceSample.Constants
{
    public class Result
    {
        public bool IsSucceed { get; set; }
        public string Message { get; set; }

        public Result()
        {

        }

        public Result(bool isSucceed)
        {
            IsSucceed = isSucceed;
        }
    }
}
