using System;
using System.Reflection;
using System.IO;
using System.Linq;

namespace SampleForReflection
{
    class Program
    {
        static void Main(string[] args)
        {

            //1
            Type myType = typeof(Figure);
            var myProperties = myType.GetProperties();
            var some = myType.GetMembers();

            //2
            //var triangle = new Figure() { Name = "Triangle", SideCount = 7, SideLength = 5 };
            //var myType2 = triangle.GetType();


            //3
            var myType3 = Type.GetType("SampleForReflection.Figure", false, true);


            //4
            //Display(triangle);

            //string path = @"G:\C#Projects\SampleForFileStream\SampleForFileStream\obj\Debug\netcoreapp3.1\SampleForFileStream.dll";
            //string path = @"G:\C#Projects\SampleForSerialization\SampleForSerialization\bin\Debug\netcoreapp3.1\SampleForSerialization.dll"
            //string path = Path.Combine("G:", "C#Projects", "SampleForFileStream", "SampleForFileStream", "obj", "Debug", "netcoreapp3.1", "SampleForFileStream.dll");

            // ПУТЬ К ИСКОМОЙ СБОРКЕ
            string path = Path.Combine("G:", "C#Projects", "SampleForSerialization", "SampleForSerialization", "obj", "Debug", "netcoreapp3.1", "SampleForSerialization.dll");
            Console.WriteLine(AssemblyName.GetAssemblyName(path));
            //Console.WriteLine($"это текущая сборка и ее адрес {AppDomain.CurrentDomain.BaseDirectory}"); 

            //ЗАГРУЗКА ИСКОМОЙ СБОРКИ
            Assembly assem = Assembly.LoadFrom(path);

            
            var assemTypes = assem.GetTypes();
            var assemType = assem.GetType("SampleForSerialization.Person");

            
            //ТИПЫ
            foreach (var type in assemTypes)
            {
                Console.WriteLine(type);
            }

            //СВОЙСТВА
            var props = assemType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var prop in props)
            {
                Console.WriteLine(prop.Name);
            }
            //МЕТОДЫ 
            var methods = assemType.GetMethods(BindingFlags.Public | BindingFlags.Static);
            foreach (var item in methods)
            {
                Console.WriteLine(item.Name);
            }

            //ПРИВАТНЫЕ МЕТОДЫ 
            var privMeths = assemType.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var prMeth in privMeths)
            {
                Console.WriteLine(prMeth.Name);
            }

            //ИНИЦИАЦИЯ 
            object obj1 = Activator.CreateInstance(assemType);

            //ПРИСВАИВАНИЕ СВОЙСТВАМ ЗНАЧЕНИЯ
            PropertyInfo propName = assemType.GetProperty("Name");
            propName.SetValue(obj1, "Восьмигранник", null);
            Console.WriteLine($"{propName.Name} : {propName.GetValue(obj1)}");

            PropertyInfo propAge = assemType.GetProperty("SideCount");
            Console.WriteLine($"{propAge.Name} : {propAge.GetValue(obj1)}") ;

            /*ConstructorInfo cons = assemType.GetConstructor(new Type[] { });
            object persReflected = cons.Invoke(new object[] {"dkfj",4 });*/

            MethodInfo privMeth = assemType.GetMethod("StrangeName", BindingFlags.NonPublic | BindingFlags.Instance);
           object strNameRes = privMeth.Invoke(obj1, new object[] {"Абдул" });
            
            Console.WriteLine(privMeth.Name);


            /*Type type4 = Type.GetType("SampleForSerialization.Figure", false, true);
            Console.WriteLine(type4.FullName);*/
            
           








        }
        private static void Display<T>(T obj)
        {
            var myType = obj.GetType();
            var properties = myType.GetProperties();
            foreach (var property in properties)
            {
                var s = property.GetValue(obj);
                Console.WriteLine(s);
            }
        }
    }
}
