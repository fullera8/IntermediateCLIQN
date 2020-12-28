using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queries
{
    class Program
    {
        static void Main(string[] args)
        {

            //Demonstrates that though this is an infinite loop,
            //Deferred will stop it.
            var numbers = MyLinq.Rand().Where(n => n > 0.5)
                                        .Take(10);
            foreach (var number in numbers)
                Console.WriteLine(number);

            Console.WriteLine("*****");
            
            //instansiate a list of movies
            var movies = new List<Movie>
            {
                new Movie{Title = "The Dark Knight", Rating = 8.9f, Year = 2008},
                new Movie{Title = "The King's Speech", Rating = 8.0f, Year = 2010},
                new Movie{Title = "Casablanca", Rating = 8.5f, Year = 1942},
                new Movie{Title = "Star Wars V", Rating = 8.7f, Year = 1980}
            };

            //Add a linq query to grab movies after 2000
            //extension syntax
            IEnumerable<Movie> moviesAfterY2K = movies.Where(m => m.Year > 2000);
            foreach (var movie in moviesAfterY2K)
                Console.WriteLine(movie.Title);

            Console.WriteLine("*****");

            //query syntax
            IEnumerable<Movie> moviesAfter2000 = from movie in movies
                                                 where movie.Year > 2000
                                                 select movie;
            foreach (var movie in moviesAfter2000)
                Console.WriteLine(movie.Title);

            Console.WriteLine("*****");

            //Using the Filter extension method from MyLinq
            var moviesY2KFilter = movies.Filter(m => m.Year > 2000);
            var enumerator = moviesY2KFilter.GetEnumerator();
            while (enumerator.MoveNext())
                Console.WriteLine(enumerator.Current.Title);
        }
    }
}
