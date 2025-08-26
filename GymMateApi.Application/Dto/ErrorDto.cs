namespace GymMateApi.Application.Dto
{
    public class ErrorDto
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
