using System;
using System.Reflection;

namespace SampleForReflection
{
    class Program
    {
        static void Main(string[] args)
        {//1
            Type myType = typeof(Figure);
            var myProperties = myType.GetProperties();
            var myMethods = myType.GetMethods();

            //2
            var triangle = new Figure() { Name = "Triangle", SideCount = 5, SideLength = 5 };
            var myType2 = triangle.GetType();


            //3
            var myType3 = Type.GetType("SampleForReflection.Figure", false, true);


            //4
            Display(triangle);
        }
        private static void Display<T>(T obj)
        {
            var myType = obj.GetType();
            var properties = myType.GetProperties();
            foreach (var property in properties)
            {
                var s = property.GetValue(obj);
            }
        }
    }
}
