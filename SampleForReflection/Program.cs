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

            DirectoryInfo projectsDir = new DirectoryInfo($@"G:\C#Projects");
            var projFolders = projectsDir.EnumerateDirectories();
            var targetProject = "SampleForFileStream";
            var targetPrFolder =
                from proj in projFolders
                where proj.Name == targetProject
                select proj;



            //string path = @"G:\C#Projects\SampleForFileStream\SampleForFileStream\obj\Debug\netcoreapp3.1\SampleForFileStream.dll";
            //string path = @"G:\C#Projects\SampleForSerialization\SampleForSerialization\bin\Debug\netcoreapp3.1\SampleForSerialization.dll"
            //string path = Path.Combine("G:", "C#Projects", "SampleForFileStream", "SampleForFileStream", "obj", "Debug", "netcoreapp3.1", "SampleForFileStream.dll");
            string path = Path.Combine("G:", "C#Projects", "SampleForSerialization", "SampleForSerialization", "obj", "Debug", "netcoreapp3.1", "SampleForSerialization.dll");
            Console.WriteLine(AssemblyName.GetAssemblyName(path));
            
            //Console.WriteLine($"это текущая сборка и ее адрес {AppDomain.CurrentDomain.BaseDirectory}"); 

            Assembly assem = Assembly.LoadFrom(path);
            var assemTypes = assem.GetTypes();
            var assemType = assem.GetType("SampleForSerialization.DataSerializer");
            foreach (var item in assemType.GetMethods())
            {
                Console.WriteLine(item.Name);
            }
            foreach (var type in assemTypes)
            {
                Console.WriteLine(type);
            }
            var props = assemType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var item in props)
            {
                Console.WriteLine(item.Name);
            }
            var methods = assemType.GetMethods(BindingFlags.Public | BindingFlags.Static);
            foreach (var item in methods)
            {
                Console.WriteLine(item.Name);
            }
            object obj1 = Activator.CreateInstance(assemType);

            


            //ConstructorInfo cons = assemType.GetConstructor(new Type[] { });
            //object persReflected = cons.Invoke(new object[] {});


            //Console.WriteLine($"invoked {persReflected}");
            //Person obj = (Person)persReflected;
            var psAge = assemType.GetProperty("Age");
            
            //Console.WriteLine(persAge.GetValue(persReflected));
            //var props = targetType.GetProperties(BindingFlags.Public | BindingFlags.Instance);



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
