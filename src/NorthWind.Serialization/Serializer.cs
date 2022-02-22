using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;
using System.Xml.Serialization;

namespace NorthWind.Serialization
{
    public class Serializer
    {
		public static void SerializeToFile<T>(SerializationType type, List<T> list, string fileName)
		{
            var result = type switch
            {
                SerializationType.Json => JsonSerialize(list),
                SerializationType.XML => XmlSerialize(list),
                SerializationType.Binary => BinarySerialize(list),
				_ => throw new ArgumentException(nameof(type))
            };

			fileName += type switch
            {
                SerializationType.Json => ".json",
                SerializationType.XML => ".xml",
                SerializationType.Binary => ".bin",
				_ => throw new ArgumentException(nameof(type))
            };

			File.WriteAllText(fileName, result);
        }

		public static string JsonSerialize<T>(ICollection<T> collection)
		{
            return JsonSerializer.Serialize(collection);
        }

		public static string XmlSerialize<T>(List<T> collection)
		{
            var serializer = new XmlSerializer(typeof(List<T>));

            using StringWriter writer = new StringWriter();

            serializer.Serialize(writer, collection);
            return writer.ToString();
        }

		public static string BinarySerialize<T>(ICollection<T> collection)
		{
			BinaryFormatter serializer = new BinaryFormatter();

            using MemoryStream stream = new MemoryStream();

			serializer.Serialize(stream, collection);
            return Convert.ToBase64String(stream.ToArray());;
		}
    }
}
