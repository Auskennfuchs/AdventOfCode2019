using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Day3
{
    class WireCross
    {
        public (int, int) GetCrossingDistance(String wire1, String wire2)
        {
            var map1 = new Dictionary<int, List<int>>();
            var map2 = new Dictionary<int, List<int>>();
            AddWire(wire1, map1, map1);
            var crossings = AddWire(wire2, map2, map1);
            var distances = crossings.Select(c => Math.Abs(c.Item1) + Math.Abs(c.Item2)).OrderBy(a => a);
            var closestCrossing = distances.ElementAt(0);

            var steps1 = CalcSteps(wire1, crossings);
            var steps2 = CalcSteps(wire2, crossings);

            var sumSteps = crossings.Select((c, idx) => steps1.Find(s => s.Item1 == idx).Item2 + steps2.Find(s => s.Item1 == idx).Item2).OrderBy(a => a);

            return (closestCrossing, sumSteps.ElementAt(0));
        }

        private void AddMapPoint(int x, int y, Dictionary<int, List<int>> map)
        {
            if (!map.ContainsKey(y))
            {
                map.Add(y, new List<int>());
            }
            if (map[y].IndexOf(x) == -1)
            {
                map[y].Add(x);
            }
        }

        private bool IsCrossing(int x, int y, Dictionary<int, List<int>> map)
        {
            return map.ContainsKey(y) && map[y].IndexOf(x) >= 0;
        }

        private List<(int, int)> AddWire(String wire, Dictionary<int, List<int>> map, Dictionary<int, List<int>> checkMap)
        {
            var parsedWire = ParseWire(wire);
            int startX = 0;
            int startY = 0;
            var crossings = new List<(int, int)>();
            parsedWire.ForEach((elem) =>
            {
                int length = elem.Item3;
                var dirX = elem.Item1;
                var dirY = elem.Item2;
                crossings.AddRange(AddPoints(startX, startY, dirX, dirY, length, map, checkMap));
                startX += length * dirX;
                startY += length * dirY;

            });
            return crossings;
        }

        private List<(int, int)> CalcSteps(String wire, List<(int, int)> crossings)
        {
            var parsedWire = ParseWire(wire);
            var startX = 0;
            var startY = 0;
            var steps = 0;
            var crossSteps = new List<(int, int)>();
            parsedWire.ForEach((elem) =>
            {
                int length = elem.Item3;
                var dirX = elem.Item1;
                var dirY = elem.Item2;
                var (cs, s) = AddSteps(startX, startY, dirX, dirY, length, steps, crossings);
                crossSteps.AddRange(cs);
                steps = s;
                startX += length * dirX;
                startY += length * dirY;

            });

            return crossSteps;
        }

        private List<(int, int)> AddPoints(int x, int y, int dirX, int dirY, int length, Dictionary<int, List<int>> map, Dictionary<int, List<int>> checkMap)
        {
            var crossings = new List<(int, int)>();
            for (var i = 1; i <= length; i++)
            {
                var px = x + i * dirX;
                var py = y + i * dirY;
                if (IsCrossing(px, py, checkMap))
                {
                    crossings.Add((px, py));
                }
                AddMapPoint(px, py, map);
            }
            return crossings;
        }

        private (List<(int, int)>, int) AddSteps(int x, int y, int dirX, int dirY, int length, int steps, List<(int, int)> crossings)
        {
            var crosses = new List<(int, int)>();
            for (var i = 1; i <= length; i++)
            {
                var px = x + i * dirX;
                var py = y + i * dirY;
                var crossed = crossings.FindIndex(m => m.Item1 == px && m.Item2 == py);
                if (crossed != -1)
                {
                    crosses.Add((crossed, steps + i));
                }
            }
            return (crosses, steps + length);
        }

        private List<(int, int, int)> ParseWire(String wire)
        {
            var dirs1 = wire.Split(',');
            return dirs1.Select(d =>
            {
                var dir = d.Substring(0, 1);
                int.TryParse(d.Substring(1), out int length);
                var dirX = 0;
                var dirY = 0;
                switch (dir)
                {
                    case "L":
                        dirX = -1;
                        break;
                    case "R":
                        dirX = 1;
                        break;
                    case "U":
                        dirY = -1;
                        break;
                    case "D":
                        dirY = 1;
                        break;
                }
                return (dirX, dirY, length);
            }).ToList();
        }
    }
}
