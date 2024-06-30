
using System.Linq.Expressions;

public static class DynamicQuery
{
    // Applies filters to the query
    public static IQueryable<T> ApplyFilters<T> (IQueryable<T> query, List<FilterCriteria> filters)
    {
        foreach (var filter in filters)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
           
            var member = Expression.Property(parameter, filter.PropertyName);
           
            var constant = Expression.Constant(Convert.ChangeType(filter.Value, member.Type));
         
            var comparison = Expression.Equal(member, constant);
            
            var lambda = Expression.Lambda<Func<T, bool>>(comparison, parameter);
          
            query = query.Where(lambda);
        }
        return query;
    }

    // Applies sorting to the query
    public static IQueryable<T> ApplySorting<T>(IQueryable<T> query, List<SortCriteria> sorts)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        foreach (var sort in sorts)
        {
            var member = Expression.Property(parameter, sort.PropertyName);
           
            var lambda = Expression.Lambda(member, parameter);

            query = query.Provider.CreateQuery<T>(
                Expression.Call(
                    typeof(Queryable),
                    sort.Ascending ? "OrderBy" : "OrderByDescending",
                    [query.ElementType, member.Type],
                    query.Expression,
                    Expression.Quote(lambda)));
        }
        return query;
    }

    // Applies both filters and sorting to the query
    public static IQueryable<T> ApplyFiltersAndSorting<T> (IQueryable<T> query, List<FilterCriteria> filters, List<SortCriteria> sorts)
    {
        query = ApplyFilters(query, filters);
        query = ApplySorting(query, sorts);
        return query;
    }
}
