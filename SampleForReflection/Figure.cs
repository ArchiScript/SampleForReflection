using System;
using System.Collections.Generic;
using System.Text;

namespace SampleForReflection
{
    class Figure
    {
        public string Name { get; set; }
        public int SideCount { get; set; }
        public double SideLength { get; set; }

        public void Display(string name)
        {
            Console.WriteLine(name);
        }
    }
}
