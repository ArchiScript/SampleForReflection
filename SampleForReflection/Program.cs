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
            //================== примеры из практики ===============================
            //1
            Type myType = typeof(Figure);
            var myProperties = myType.GetProperties();
            var some = myType.GetMembers();

            //2
            //var triangle = new Figure() { Name = "Triangle", SideCount = 7, SideLength = 5 };
            //var myType2 = triangle.GetType();


            //3
            // var myType3 = Type.GetType("SampleForReflection.Figure", false, true);


            //4
            //Display(triangle);

            //================== ЗАГРУЗКА СБОРКИ ИЗ ДРУГОЙ ПАПКИ ====================


            // ПУТЬ К ИСКОМОЙ СБОРКЕ
            string path = Path.Combine("G:", "C#Projects", "SampleForSerialization", "SampleForSerialization", "obj", "Debug", "netcoreapp3.1", "SampleForSerialization.dll");
            Console.WriteLine(AssemblyName.GetAssemblyName(path));
            //string assemName = AssemblyName.GetAssemblyName(path).ToString();
            //Assembly assem = Assembly.Load(assemName);


            //ЗАГРУЗКА ИСКОМОЙ СБОРКИ
            Assembly assem = Assembly.LoadFrom(path);

            //ОПРЕДЕЛЕНИЕ ТИПОВ
            var assemTypes = assem.GetTypes();
            var figureType = assem.GetType("SampleForSerialization.Figure");
            var personType = assem.GetType("SampleForSerialization.Person");


            //ВЫВОД ТИПОВ
            foreach (var type in assemTypes)
            {
                Console.WriteLine(type);
            }

            //СВОЙСТВА
            var props = figureType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var prop in props)
            {
                Console.WriteLine(prop.Name);
            }
            //МЕТОДЫ 
            var methods = figureType.GetMethods(BindingFlags.Public | BindingFlags.Static);
            foreach (var meth in methods)
            {
                Console.WriteLine(meth.Name);
            }

            //ПРИВАТНЫЕ МЕТОДЫ 
            var privMeths = figureType.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var prMeth in privMeths)
            {
                Console.WriteLine(prMeth.Name);
            }
            //КОНСТРУКТОРЫ
            var constructors = personType.GetConstructors();
            
            Console.WriteLine(constructors[0]);



            //================ЭКЗЕМПЛЯР КЛАССА FIGURE ==============================
            //ИНИЦИАЦИЯ 
            object figureObjInst = Activator.CreateInstance(figureType);

            //ТИП ИНИЦИИРОВАННОГО ОБЪЕКТА
            Type figType = figureObjInst.GetType();

            //ПРИСВАИВАНИЕ ЗНАЧЕНИ1 СВОЙСТВАМ 
            PropertyInfo propName = figureType.GetProperty("Name");
            propName.SetValue(figureObjInst, "Восьмигранник");
            Console.WriteLine($"\n {propName.Name} : {propName.GetValue(figureObjInst)}");

            PropertyInfo propAge = figureType.GetProperty("SideCount");
            propAge.SetValue(figureObjInst, 8);
            Console.WriteLine($" {propAge.Name} : {propAge.GetValue(figureObjInst)}");

            //ОПРЕДЕЛЕНИЕ И ИНИЦИИРОВАНИЕ МЕТОДА С ПАРАМЕТРАМИ
            MethodInfo multiplyMeth = figType.GetMethod("Multiply");
            var resultMultiply = multiplyMeth.Invoke(figureObjInst, new object[] { 4, 4 });
            Console.WriteLine($" Результат вызова метода с параметрами {resultMultiply}");

            MethodInfo privMethodFigure = figType.GetMethod("ChangeName",BindingFlags.NonPublic| BindingFlags.Instance);
            var privResult = privMethodFigure.Invoke(figureObjInst, new object[] { "Петр" });
            Console.WriteLine($"\n Вызов приватного метода с параметром:  {privResult}");
            
            
            //=================ЭКЗЕМПЛЯР КЛАССА PERSON ==============================

            //ОПРЕДЕЛЕНИЕ КОНСТРУКТОРА И ВЫЗОВ С ПАРАМЕТРАМИ
            ConstructorInfo constructor = personType.GetConstructor(new Type[] { typeof(string), typeof(int) });
            object persReflected = constructor.Invoke(new object[] { "Василий", 12 });
            
           
            PropertyInfo persName = personType.GetProperty("Name");
            PropertyInfo persAge = personType.GetProperty("Age");
            Console.WriteLine($"\n Свойства изменены с помощью вызова конструктора класса: \n" +
                $"{persName.Name}: {persName.GetValue(persReflected)} " +
                $"{persAge.Name}: {persAge.GetValue(persReflected)}");
            


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
