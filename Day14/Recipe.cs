using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Day14
{
    class Product
    {
        public int Minimum { get; set; }

        public Dictionary<int, Product> ingredient = new Dictionary<int, Product>();

        public int GetIngredientCount()
        {
            if (ingredient.Count == 0)
            {
                return Minimum;
            }
            var ingredientCount = ingredient.Aggregate(0, (res, i) => res + i.Key * i.Value.GetIngredientCount());
            return ingredientCount * Minimum;
        }

        public int Produce(int num)
        {
            if (ingredient.Count == 0)
            {
                return num * Minimum;
            }
            var singleUnit = ingredient.Aggregate(0, (res, i) => res + i.Value.Produce(i.Key));
            return num * Minimum * singleUnit + (num - Minimum) * singleUnit;
        }
    }

    class Recipe
    {
        private Dictionary<string, Product> products = new Dictionary<string, Product>();

        public void Part1()
        {
            products.Add("ORE", new Product() { Minimum = 1 });
            using var sr = new StreamReader("recipe2.txt");
            while (!sr.EndOfStream)
            {
                var line = sr.ReadLine();
                var parts = line.Split(" => ");
                var element = ParseElement(parts[1]);
                var result = FindProduct(element.Item2);
                result.Minimum = element.Item1;
                result.ingredient = ParseIngredients(parts[0]);
                Console.WriteLine($"{parts[0]}:{parts[1]}");
            }
            var fuel = products["FUEL"];
            Console.WriteLine($"ORE count {fuel.GetIngredientCount()}");
            Console.WriteLine($"ORE count {fuel.Produce(1)}");
        }

        private Dictionary<int, Product> ParseIngredients(string input)
        {
            var res = new Dictionary<int, Product>();
            var parts = input.Split(", ");
            Array.ForEach(parts, p =>
            {
                var (amount, name) = ParseElement(p);
                res.Add(amount, FindProduct(name));
            });
            return res;
        }

        private (int, string) ParseElement(string input)
        {
            var parts = input.Split(" ");
            int.TryParse(parts[0], out var amount);
            return (amount, parts[1]);
        }

        private Product FindProduct(string name)
        {
            if (products.ContainsKey(name))
            {
                return products[name];
            }
            var p = new Product();
            products.Add(name, p);
            return p;
        }
    }
}
