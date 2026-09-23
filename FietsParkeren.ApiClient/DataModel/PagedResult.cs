namespace FietsParkeren.ApiClient.DataModel
{
    public class PagedResult<T>
    {
        public T Data { get; set; }

        public int Total { get; set; }
    }
}
