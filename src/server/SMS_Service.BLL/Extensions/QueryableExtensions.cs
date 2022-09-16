namespace SMS_Service.BLL.Extensions
{
	public static class QueryableExtensions
    {
        public static IQueryable<TSource> Page<TSource>(this IQueryable<TSource> source, int pageSize, int pageNumber)
        {
            var skip = (pageNumber - 1) * pageSize;
            if (skip > 0)
            {
                source = source.Skip(skip);
            }
            source = source.Take(pageSize);

            return source;
        }
    }
}
