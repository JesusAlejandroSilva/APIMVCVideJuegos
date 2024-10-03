namespace Domain.EntitiesLayer
{
    public class PaginatedResult<T>
    {
        public IEnumerable<T> Items { get; private set; }
        public int TotalRecords { get; private set; }
        public int PageNumber { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalRecords / PageSize);

        public PaginatedResult(IEnumerable<T> items, int totalRecords, int pageNumber, int pageSize)
        {
            Items = items;
            TotalRecords = totalRecords;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }

}
