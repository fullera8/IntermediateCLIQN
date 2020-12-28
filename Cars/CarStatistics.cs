using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars
{
    public class CarStatistics
    {
        public double Max { get; set; }
        public double Min { get; set; }
        public double Avg { get; set; }
        private double Total { get; set; }
        private int Count { get; set; }

        //constructor
        public CarStatistics()
        {
            Max = double.MinValue;
            Min = double.MaxValue;
        }

        //find min max
        public CarStatistics Accumulate(Car car)
        {
            Count++;
            Total += car.Combined;

            Max = Math.Max(Max, car.Combined);
            Min = Math.Min(Min, car.Combined);

            return this;
        }

        //find average
        public CarStatistics Compute()
        {
            Avg = Total / Count;
            return this;
        }
    }
}
