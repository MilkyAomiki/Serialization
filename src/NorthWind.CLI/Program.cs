using System;
using System.IO;
using NorthWind.Serialization;

namespace NorthWind.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Choose serialization method");
            Console.WriteLine("1. JSON");
            Console.WriteLine("2. XML");
            Console.WriteLine("3. Binary");

            SerializationType serializationType = 0;
			while (!(Enum.TryParse(Console.ReadLine(), out serializationType) && Enum.IsDefined<SerializationType>(serializationType)))
			{
				Console.WriteLine("Couldn't understand the input, try again.");
			}

			var serializer = new SerializerService();
			try
			{
				Console.WriteLine("Started serializing...");
				serializer.SerializeAllTables(serializationType);
           	 	Console.WriteLine($"Saved at {Path.GetFullPath(serializer.DirectoryPath)}");
			}
			catch (Exception)
			{
           	 	Console.WriteLine($"Something went wrong");
                throw;
            }
			finally
			{
                serializer.Dispose();
            }
        }
    }
}
