using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Day6
{

    class Planet
    {
        public string Name { get; set; }

        public int CountOrbits(int parentOrbits)
        {
            return orbitters.Aggregate(parentOrbits, (res, o) => res + o.CountOrbits(parentOrbits + 1));
        }

        private List<Planet> orbitters = new List<Planet>();

        public void AddOrbitter(string name)
        {
            orbitters.Add(new Planet() { Name = name });
        }
        public void AddOrbitter(Planet p)
        {
            orbitters.Add(p);
        }

        public Planet Find(string name)
        {
            if (name == Name)
            {
                return this;
            }
            foreach (var o in orbitters)
            {
                var res = o.Find(name);
                if (res != null)
                {
                    return res;
                }
            }
            return null;
        }

        public List<Planet> RouteToPlanet(Planet p)
        {
            if (orbitters.Contains(p))
            {
                return new List<Planet>() { this };
            }
            foreach (var o in orbitters)
            {
                var route = o.RouteToPlanet(p);
                if (route != null)
                {
                    route.Add(this);
                    return route;
                }
            }
            return null;
        }
    }

    class OrbitMap
    {
        List<Planet> planets = new List<Planet>();

        public void Run()
        {
            using (var sr = new StreamReader("mapdata.txt"))
            {
                while (!sr.EndOfStream)
                {
                    var entry = sr.ReadLine();
                    var parts = entry.Split(')');
                    var planetName = parts[0];
                    var orbitterName = parts[1];
                    var planet = FindPlanet(planetName);
                    if (planet == null)
                    {
                        planet = new Planet()
                        {
                            Name = planetName
                        };
                        planets.Add(planet);
                    }
                    var existingOrbit = FindPlanet(orbitterName);
                    if (existingOrbit == null)
                    {
                        planet.AddOrbitter(orbitterName);
                    }
                    else
                    {
                        planet.AddOrbitter(existingOrbit);
                        if (planets.Contains(existingOrbit))
                        {
                            planets.Remove(existingOrbit);
                        }
                    }
                }
            }
            var comPlanet = planets[0];
            Console.WriteLine($"{comPlanet.CountOrbits(0)}");

            var you = comPlanet.Find("YOU");
            var san = comPlanet.Find("SAN");
            var routeYou = comPlanet.RouteToPlanet(you);
            var routeSan = comPlanet.RouteToPlanet(san);
            var crossingJumpsYou = FindCrossingPlanet(routeYou, routeSan);
            var crossingJumpsSan = FindCrossingPlanet(routeSan, routeYou);
            Console.WriteLine($"Jumps: {crossingJumpsYou + crossingJumpsSan}");

        }

        private Planet FindPlanet(string name)
        {
            foreach (var p in planets)
            {
                var res = p.Find(name);
                if (res != null)
                {
                    return res;
                }
            }
            return null;
        }

        private int FindCrossingPlanet(List<Planet> routeYou, List<Planet> routeSan)
        {
            int i = 0;
            foreach (var y in routeYou)
            {
                if (routeSan.Contains(y))
                {
                    return i;
                }
                i++;
            }
            return 0;
        }

    }
}
