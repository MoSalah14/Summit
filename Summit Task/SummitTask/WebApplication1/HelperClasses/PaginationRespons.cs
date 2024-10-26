namespace Summit_Task.HelperClasses
{
    public class PaginationRespons<T> : SummitResponse<T> where T : class
    {
        public int? TotalCount { get; set; }
        public int? pageSize { get; set; }
        public int? pageIndex { get; set; }

    }
}
