namespace RetroVideo.Models
{
    public partial class BaseResponseDto<T>
    {
        #region Properties

        public T Data { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }

        #endregion

        #region Functions

        public static BaseResponseDto<T> Successful(T data, string message = "")
        {
            return new BaseResponseDto<T> { Data = data, Message = message, IsSuccess = true };
        }

        public static BaseResponseDto<T> Failed(string message)
        {
            return new BaseResponseDto<T> { Message = message, IsSuccess = false };
        }
        public static BaseResponseDto<T> Failed(T data, string message)
        {
            return new BaseResponseDto<T> { Data = data, Message = message, IsSuccess = false };
        }

        public static BaseResponseDto<T> NotFound(T data, string message)
        {
            return new BaseResponseDto<T> { Data = data ?? default, Message = message, IsSuccess = false };
        }

        #endregion
    }
}
