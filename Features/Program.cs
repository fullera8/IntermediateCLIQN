using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Features.Linq;

namespace Features
{
    class Program
    {
        static void Main(string[] args)
        {
            IEnumerable<Employee> developers = new Employee[]
            {
                new Employee { Id = 1, Name = "Scott"},
                new Employee { Id = 2, Name = "Chris"}
            };

            IEnumerable<Employee> sales = new List<Employee>()
            {
                new Employee { Id = 3, Name = "Alex"}
            };

            //Console.WriteLine(developers.Count());

            //IEnumerator<Employee> enumerator = developers.GetEnumerator();
            //while (enumerator.MoveNext())
            //{
            //    Console.WriteLine(enumerator.Current.Name);
            //}

            //demonstration of the func and action
            Func<int, int> square = x => x * x;
            Func<int, int, int> add = (x, y) =>
                {
                    int temp = x + y;
                    return temp;
                };
            Action<int> write = x => Console.WriteLine(x);

            //using the func and action
            write(square(add(3, 5)));

            //enumerates through the developers and returns where the names start with "S"
            //foreach (var employee in developers.Where(employee => employee.Name.StartsWith("S")))
            //    Console.WriteLine(employee.Name);

            foreach (var employee in developers.Where(e => e.Name.Length == 5)
                                                .OrderBy(e => e.Name))
                Console.WriteLine(employee.Name);

            //breaking up the examples
            Console.WriteLine("********");

            //demonstrating Query syntax
            var query = from developer in developers
                        where developer.Name.Length == 5
                        orderby developer.Name descending
                        select developer;

            foreach (var employee in query)
                Console.WriteLine(employee.Name);
        }

        //private static bool NameStartsWithS(Employee employee)
        //{
        //    return employee.Name.StartsWith("S");
        //}
    }
}
