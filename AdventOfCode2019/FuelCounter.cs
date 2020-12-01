using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2019
{
    class FuelCounter
    {
        public int GetFuel(int mass)
        {
            var massFuel = CalcFuel(mass);
            var remaining = CalcFuel(massFuel);
            do
            {
                massFuel+= remaining;
                remaining = CalcFuel(remaining);
            } while (remaining > 0);

            return massFuel;
        }

        private int CalcFuel(int mass)
        {
            return (int)Math.Floor(mass / 3.0f) - 2;
        }
    }
}
