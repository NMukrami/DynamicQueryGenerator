using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        var context = new AppDbContext();

        // Add sample data
        context.People.AddRange(
            new Person { Id = 1, Name = "John", Age = 30 },
            new Person { Id = 2, Name = "Jane", Age = 35 },
            new Person { Id = 3, Name = "John", Age = 40 },
            new Person { Id = 4, Name = "Alice", Age = 35 },
            new Person { Id = 5, Name = "Bob", Age = 28 },
            new Person { Id = 6, Name = "Charlie", Age = 50 },
            new Person { Id = 7, Name = "David", Age = 45 },
            new Person { Id = 8, Name = "John", Age = 32 },
            new Person { Id = 9, Name = "Alex", Age = 35 }
        );
        context.SaveChanges();


        // Define filter criteria
        var filters = new List<FilterCriteria>
        {
            new FilterCriteria { PropertyName = "Name", Value = "John" }
        };

        // Define sort criteria
        var sorts = new List<SortCriteria>
        {
            new SortCriteria { PropertyName = "Age", Ascending = true }
        };

        // Apply filters and sorting
        var query = context.People.AsQueryable();
        var result = DynamicQuery.ApplyFiltersAndSorting(query, filters, sorts).ToList();

        // Print results
        foreach (var person in result)
        {
            Console.WriteLine($"Id: {person.Id}, Name: {person.Name}, Age: {person.Age}");
        }


        Console.ReadKey();
    }
}
