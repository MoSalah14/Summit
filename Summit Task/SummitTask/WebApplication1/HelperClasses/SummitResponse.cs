namespace Summit_Task.HelperClasses
{
    public class SummitResponse<T>
    {

        public bool IsError { get; set; }
        public T? Data { get; set; }

        public string? Message { get; set; }
    }
}
