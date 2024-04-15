namespace UserManagementService.Models
{
    public class PagedResult<T>
    {
        public IEnumerable<T> content { get; set; }
        public int totalPages { get; set; }
        public int totalElements { get; set; }
        public int size { get; set; }
        public int number { get; set; }
    }
}