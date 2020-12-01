using System;
using System.Linq;

namespace AdventOfCode2019
{
    class Program
    {
        static void Main(string[] args)
        {
            var fuelCounter = new FuelCounter();
            int fuelTotal = FuelModules.Modules.Select(m => fuelCounter.GetFuel(m)).Aggregate(0, (res, m) => res + m);
            Console.WriteLine($"Fuel total for modules: {fuelTotal}");
        }
    }
}
