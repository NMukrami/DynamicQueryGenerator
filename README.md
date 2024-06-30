# Dynamic Query Generator

## Usage

1. Add the `DynamicQueryGenerator` library to your project.
2. Define your filtering and sorting criteria.
3. Call `ApplyFiltersAndSorting` with your queryable and criteria.

```csharp
var filters = new List<FilterCriteria>
{
    new FilterCriteria { PropertyName = "Name", Value = "John" }
};

var sorts = new List<SortCriteria>
{
    new SortCriteria { PropertyName = "Age", Ascending = true }
};

var query = context.People.AsQueryable();
var result = DynamicQuery.ApplyFiltersAndSorting(query, filters, sorts).ToList();
