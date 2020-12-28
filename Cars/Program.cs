using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq.Expressions;

namespace Cars
{
    class Program
    {
        static void Main(string[] args)
        {
            //process csv file
            //var records = ProcessFileCompact("fuel.csv");
            //var manufacturers = ProcessManufacturers("manufacturers.csv");

            ////demo of expressions, this will not be used anywhere
            //Func<int, int> square = x => x * x;
            //Expression<Func<int, int, int>> Add = (x, y) => x + y;
            //Func<int, int, int> addI = Add.Compile();

            //var addResult = addI(3, 5);
            //Console.WriteLine(Add);
            //Console.WriteLine(addResult);

            //setup the initial xml for element oriented xml
            //ExtractXML(records);

            ////Query the XML data
            //QueryXML();

            //DO NOT USE THIS IN PRODUCTION
            /*
                This will wipe out and recreate the table every time the schema changes. 
                With this being a demo, you will change the schema frequently, making this whole process less painful
            */
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<CarDb>());
            
            //insert data into a sql db
            //InsertSqlData();

            //query data from the db
            //QuerySqlData();
            //Advanced LINQ Query
            QueryAdvancedSQLData();
            //Find most fuel efficent cars based on combined, highway, city in descending and Name in ascending
            //var fuelQuery = 
            //                cars.Where(c => c.Manufacturer == "BMW" && c.Year == 2016)
            //                    .OrderByDescending(c => c.Combined)
            //                    .ThenByDescending(c => c.Highway)
            //                    .ThenByDescending(c => c.City)
            //                    .ThenBy(c => c.Name)
            //                    //this is uneccessary because we do not need to concrete type the output. This is to demonstrate what happens implicitly
            //                    //using anonymous sytax allows for smaller data object (fewer columns) this is more efficent for LINQ.
            //                    .Select(c => new { c.Manufacturer, c.Year, c.Combined, c.Highway, c.City, c.Name});

            ////flattening the collection with select many
            //var flat = 
            //    cars.SelectMany(c => c.Name)
            //        .OrderBy(c => c);
            //foreach (var car in flat.Take(10))
            //{
            //    Console.WriteLine(car);
            //}

            //Console.WriteLine("*****");

            ////Finds the most fuel efficent car based on combined, highway, city in descending and Name in ascending and check that the conditions exist
            //string manufacturer = "BMW";
            //int year = 2016;

            //Console.WriteLine("The Top Car is");
            //if (cars.Any(c => c.Manufacturer == manufacturer && c.Year == year))
            //{
            //    var top =
            //        cars.Where(c => c.Manufacturer == manufacturer && c.Year == year)
            //            .OrderByDescending(c => c.Combined)
            //            .ThenByDescending(c => c.Highway)
            //            .ThenByDescending(c => c.City)
            //            .ThenBy(c => c.Name)
            //            //This is non-deferred, must execute everything to find the top result. Remeber, use these at the end
            //            .FirstOrDefault();
            //    Console.WriteLine($"{top.Manufacturer} {top.Name}: {top.Combined}, {top.Highway}, {top.City}".PadLeft(25));
            //}
            //else
            //    Console.WriteLine("No Top Car".PadLeft(25));

            //Console.WriteLine("*****");

            //foreach (var car in fuelQuery.Take(10))
            //{
            //    Console.WriteLine($"{car.Manufacturer} {car.Name}: {car.Combined}, {car.Highway}, {car.City}");
            //}

            //Console.WriteLine("******");

            ////Join with Query Syntax, join manufacturer and country info to cars
            //var queryManufacturer =
            //    from car in cars
            //    join manufacturer in manufacturers
            //        on car.Manufacturer equals manufacturer.Name
            //    orderby car.Combined descending, car.Name ascending
            //    select new
            //    {
            //        manufacturer.Headquarters,
            //        car.Name,
            //        car.Combined
            //    };

            ////grab the top 10 manufactureres cars
            //foreach (var car in queryManufacturer.Take(10))
            //    Console.WriteLine($"{car.Headquarters} {car.Name}: {car.Combined}");

            //Console.WriteLine("******");

            ////Join with Extension Method Syntax, join manufacturer and country info to cars
            //var queryExtenMan =
            //    cars.Join(manufacturers,
            //                c => c.Manufacturer,
            //                m => m.Name,
            //                (c, m) => new
            //                {
            //                    m.Headquarters,
            //                    c.Name,
            //                    c.Combined
            //                })
            //        .OrderByDescending(c => c.Combined)
            //        .ThenBy(c => c.Name);


            ////grab the top 10 manufactureres cars
            //foreach (var car in queryExtenMan.Take(10))
            //    Console.WriteLine($"{car.Headquarters} {car.Name}: {car.Combined}");


            //Console.WriteLine("******");

            ////Composite key query syntax
            //var queryManufacturerComposite =
            //    from car in cars
            //    join  manufacturer in manufacturers
            //        on  new { car.Manufacturer, car.Year } 
            //            equals new { Manufacturer = manufacturer.Name, manufacturer.Year}
            //    orderby car.Combined descending, car.Name ascending
            //    select new
            //    {
            //        manufacturer.Headquarters,
            //        car.Name,
            //        car.Combined
            //    };

            ////grab the top 10 manufactureres cars
            //foreach (var car in queryManufacturerComposite.Take(10))
            //    Console.WriteLine($"{car.Headquarters} {car.Name}: {car.Combined}");

            //Console.WriteLine("******");

            ////Join with Extension Method Syntax composite key, join manufacturer and country info to cars
            //var queryExtenManComposite =
            //    cars.Join(manufacturers,
            //                c => new { c.Manufacturer, c.Year },
            //                m => new { Manufacturer = m.Name, m.Year },
            //                (c, m) => new
            //                {
            //                    m.Headquarters,
            //                    c.Name,
            //                    c.Combined
            //                })
            //        .OrderByDescending(c => c.Combined)
            //        .ThenBy(c => c.Name);

            ////grab the top 10 manufactureres cars
            //foreach (var car in queryExtenManComposite.Take(10))
            //    Console.WriteLine($"{car.Headquarters} {car.Name}: {car.Combined}");

            //Console.WriteLine("******");
            ////grab the 2 most fuel efficent cars from each manufacturer, query synax
            //var queryGroup =
            //    from car in cars
            //    orderby car.Combined descending
            //    group car by car.Manufacturer.ToUpper();

            ////Demonstrates the Key, the field that is being grouped by
            ////foreach (var company in queryGroup)
            ////    Console.WriteLine($"{company.Key} has {company.Count()} cars");

            ////shows the query syntax car list
            //foreach (var company in queryGroup)
            //{
            //    Console.WriteLine($"{company.Key}:");
            //    foreach (var car in company.Take(2))
            //        Console.WriteLine($"\t{car.Name}: {car.Combined}");
            //}


            //Console.WriteLine("******");
            ////grab the 2 most fuel efficent cars from each manufacturer, extension method syntax
            //var queryGroupExt =
            //    cars.OrderByDescending(c => c.Combined)
            //        .GroupBy(g => g.Manufacturer.ToUpper());

            ////shows the extension method syntax car list
            //foreach (var company in queryGroupExt)
            //{
            //    Console.WriteLine($"{company.Key}:");
            //    foreach (var car in company.Take(2))
            //        Console.WriteLine($"\t{car.Name}: {car.Combined}");
            //}

            //Console.WriteLine("******");
            ////GroupJoin to show country and manufacturer at the same time, query syntax
            //var queryGroupJoin =
            //    from manufacturer in manufacturers
            //        join car in cars 
            //            on manufacturer.Name.ToUpper() equals car.Manufacturer.ToUpper()
            //            into carGroup
            //    select new
            //    {
            //        Manufacturer = manufacturer,
            //        Cars = carGroup
            //    };

            ////Write out the fuel efficent cars in the car group
            //foreach (var company in queryGroupJoin)
            //{
            //    Console.WriteLine($"{company.Manufacturer.Name}: {company.Manufacturer.Headquarters}");
            //    foreach (var car in company.Cars.OrderByDescending(c => c.Combined).Take(2))
            //        Console.WriteLine($"\t{car.Name}: {car.Combined}");
            //}

            //Console.WriteLine("******");
            ////GroupJoin to show country and manufacturer at the same time, query syntax
            //var queryExtMetGroupJoin =
            //    manufacturers.GroupJoin(cars,
            //                            m => m.Name,
            //                            c => c.Manufacturer,
            //                            (m, g) =>
            //                                new
            //                                {
            //                                    Manufacturer = m,
            //                                    Cars = g
            //                                }
            //                            )
            //                    .OrderBy(m => m.Manufacturer.Name);

            ////Write out the fuel efficent cars in the car group
            //foreach (var company in queryExtMetGroupJoin)
            //{
            //    Console.WriteLine($"{company.Manufacturer.Name}: {company.Manufacturer.Headquarters}");
            //    foreach (var car in company.Cars.OrderByDescending(c => c.Combined).Take(2))
            //        Console.WriteLine($"\t{car.Name}: {car.Combined}");
            //}


            //Console.WriteLine("******");
            ////CHALLENGE: grab the 2 most fuel efficent cars from each country
            //var queryBestFuelCountry =
            //    cars.Join(manufacturers,
            //                c => c.Manufacturer,
            //                m => m.Name,
            //                (c, m) => new
            //                {
            //                    m.Headquarters,
            //                    c.Name,
            //                    c.Combined,
            //                    c.Manufacturer
            //                })
            //        .OrderByDescending(c => c.Combined)
            //        .GroupBy(m => m.Headquarters);

            ////displays most fuel efficent cars by country
            //foreach (var country in queryBestFuelCountry)
            //{
            //    Console.WriteLine($"{country.Key}");
            //    foreach (var car in country.Take(3))
            //        Console.WriteLine($"\t({car.Manufacturer}) {car.Name}: {car.Combined}");
            //}

            //Console.WriteLine("*****");
            ////Aggreagate data, show min, max, and avg fuel efficency for each manufacturer
            //var queryAgg =
            //    from car in cars
            //    group car by car.Manufacturer into carGroup
            //    orderby carGroup.Average(c => c.Combined) descending
            //    select new
            //    {
            //        Name = carGroup.Key,
            //        Max = carGroup.Max(c => c.Combined),
            //        Min = carGroup.Min(c => c.Combined),
            //        Avg = carGroup.Average(c => c.Combined)
            //    };

            ////Show the aggregate fuel efficency by car manufacturer, ordered by average fuel efficency
            //foreach (var manufacturer in queryAgg)
            //{
            //    Console.WriteLine($"{manufacturer.Name}");
            //    Console.WriteLine($"\t Avg:{manufacturer.Avg:N2}");
            //    Console.WriteLine($"\t Min:{manufacturer.Min:N2}");
            //    Console.WriteLine($"\t Max:{manufacturer.Max:N2}");
            //}

            //Console.WriteLine("*****");
            ////Aggreagate data extension syntax, show min, max, and avg fuel efficency for each manufacturer 
            ////This is more efficent because of the way aggregate works, only needs to loop through the list once.
            //var queryAggExt =
            //    cars.GroupBy(c => c.Manufacturer)
            //        .Select(g =>
            //                {
            //                    var result = g.Aggregate(new CarStatistics(),
            //                                            (acc, c) => acc.Accumulate(c),
            //                                            acc => acc.Compute());
            //                    return new
            //                    {
            //                        Name = g.Key,
            //                        Avg = result.Avg,
            //                        Min = result.Min,
            //                        Max = result.Max
            //                    };
            //                })
            //        .OrderByDescending(r => r.Avg);

            ////Extension method syntax is the same in this case
            //foreach (var manufacturer in queryAggExt)
            //{
            //    Console.WriteLine($"{manufacturer.Name}");
            //    Console.WriteLine($"\t Avg:{manufacturer.Avg:N2}");
            //    Console.WriteLine($"\t Min:{manufacturer.Min:N2}");
            //    Console.WriteLine($"\t Max:{manufacturer.Max:N2}");
            //}


        }

        private static void QueryAdvancedSQLData()
        {
            var db = new CarDb();
            db.Database.Log = Console.WriteLine;

            var query =
                db.Cars.GroupBy(c => c.Manufacturer)
                        .Select(g => new
                        {
                            Name = g.Key,
                            Cars = g.OrderByDescending(c => c.Combined).Take(2)
                        });

            foreach (var group in query)
            {
                Console.WriteLine(group.Name);
                foreach (var car in group.Cars)
                    Console.WriteLine($"\t{car.Name}: {car.Combined}");
            }
        }

        private static void QuerySqlData()
        {
            var db = new CarDb();
            //Adds the logs to the output
            //db.Database.Log = Console.WriteLine;

            var query =
                db.Cars.Where(c => c.Manufacturer == "BMW")
                        .OrderByDescending(c => c.Combined)
                        .ThenBy(c => c.Name)
                        .Take(10);

            foreach (var car in query)
                Console.WriteLine($"{car.Name}: {car.Combined}");
        }

        private static void InsertSqlData()
        {
            //process csv file
            var records = ProcessFileCompact("fuel.csv");
            /*  
                This is the default config connection, this will connect to the local db and connect to a table of the same name(CarDb).
                This will also create a schema with the same properties of the class and the same data types.By default if there is a class called "ID"
                it will make that the PK. 
            */
            //You can change this but for this demonstration, this is all the more you will do.
            var db = new CarDb();

            //place the cars into the db if the table is empty
            if (!db.Cars.Any())
            {
                foreach (var car in records)
                    db.Cars.Add(car);
                db.SaveChanges();
            }
        }

        private static void QueryXML()
        {
            //load in the xml for linq to parse
            var document = XDocument.Load("fuel.xml");

            //create the namespaces
            var ns = (XNamespace)"https://stuff.gov";
            var ex = (XNamespace)"https://stuff.gov/morestuff";

            //This is what we use Linq against because elements is an Ienumerable
            var query =
                document.Element(ns + "Cars")?.Elements(ex + "Car").Where(e => e.Attribute("Manufacturer")?.Value == "BMW")
                                                                .Select(e => e.Attribute("Name").Value);

            foreach (var name in query)
                Console.WriteLine(name);
        }

        /// <summary>
        /// Extracts Car csv records into an XML file
        /// </summary>
        /// <param name="records"></param>
        private static void ExtractXML(List<Car> records)
        {
            //create the namespaces
            var ns = (XNamespace)"https://stuff.gov";
            var ex = (XNamespace)"https://stuff.gov/morestuff";

            //
            var doc = new XDocument();
            var cars = new XElement(ns + "Cars",
                records.Select(record => new XElement(ex + "Car",
                                                        new XAttribute("Name", record.Name),
                                                        new XAttribute("Combined", record.Combined),
                                                        new XAttribute("Manufacturer", record.Manufacturer)
                                                        )
                                )
                );

            //Set the namespace extensions to references to cut down on file size.
            cars.Add(new XAttribute(XNamespace.Xmlns + "ex", ex));

            //Create the xml format and save it as an .xml file
            doc.Add(cars);
            doc.Save("fuel.xml");
        }

        //Grab the list of manufacturer details
        private static List<Manufacturer> ProcessManufacturers(string path)
        {
            var query =
                File.ReadAllLines(path)
                    .Where(l => l.Length > 1)
                    .Select(l =>
                    {
                        var columns = l.Split(',');
                        return new Manufacturer
                        {
                            Name = columns[0],
                            Headquarters = columns[1],
                            Year = int.Parse(columns[2])
                        };
                    });

            return query.ToList();
        }

        //Process File simple way
        private static List<Car> ProcessFile(string path)
        {
            //Open the file with system.IO and convert it to a concrete type
            return
                File.ReadAllLines(path)
                    .Skip(1)
                    .Where(line => line.Length > 1)
                    .Select(Car.ParseFromCSV)
                    .ToList();
        }

        //Process file compact way
        private static List<Car> ProcessFileCompact(string path)
        {
            var query =
                File.ReadAllLines(path)
                    .Skip(1)
                    .Where(line => line.Length > 1)
                    .ToCar();

            return query.ToList();
        }

        
    }
}
