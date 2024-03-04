using System;

namespace ConveyorBeltSushi
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var r = int.Parse(Console.ReadLine());
            var g = int.Parse(Console.ReadLine());
            var b = int.Parse(Console.ReadLine());
            var cost = (r * 3) + (g * 4) + (b * 5);
            Console.Write(cost);
        }
    }
}
