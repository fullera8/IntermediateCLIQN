using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queries
{
    //linq extension library
    public static class MyLinq
    {
        //Return infinityly long random number
        public static IEnumerable<Double> Rand()
        {
            var random = new Random();
            while (true)
            {
                yield return random.NextDouble();
            }
        }


        //glorified where clause to demonstrate how a deferred linq query works
        public static IEnumerable<T> Filter<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            //Deferred execution
            foreach (var item in source)
            {
                if (predicate(item))
                    yield return item;
            }
        }
    }
}
