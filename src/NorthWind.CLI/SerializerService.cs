using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NorthWind.Data;
using NorthWind.Serialization;

namespace NorthWind.CLI
{
    class SerializerService : IDisposable
    {
		public IMapper Mapper { get; set; }
		public NorthWindContext Context { get; set; }
        public string DirectoryPath { get; } = Path.Combine("..", "..", "Data");

        public SerializerService()
		{
            Mapper = GetMapper();
			Context = new NorthWindContext();
        }

		public void SerializeAllTables(SerializationType serializationMethod)
		{
            CreateDirectory();

            SerializeDbSet<CLI.Models.Order, Data.Models.Order>(Context.Orders, serializationMethod, "Orders");
            SerializeDbSet<CLI.Models.OrderDetail, Data.Models.OrderDetail>(Context.OrderDetails, serializationMethod, "OrderDetails");
            SerializeDbSet<CLI.Models.Category, Data.Models.Category>(Context.Categories, serializationMethod, "Categories");
            SerializeDbSet<CLI.Models.Customer, Data.Models.Customer>(Context.Customers, serializationMethod, "Customers");
            SerializeDbSet<CLI.Models.Employee, Data.Models.Employee>(Context.Employees, serializationMethod, "Employees");
            SerializeDbSet<CLI.Models.Product, Data.Models.Product>(Context.Products, serializationMethod, "Products");
            SerializeDbSet<CLI.Models.Shipper, Data.Models.Shipper>(Context.Shippers, serializationMethod, "Shippers");
            SerializeDbSet<CLI.Models.Supplier, Data.Models.Supplier>(Context.Suppliers, serializationMethod, "Suppliers");
		}

		public void SerializeDbSet<Dto, T>(DbSet<T> set, SerializationType serializationType, string filename)
			where T: class
		{
            var col = Mapper.Map<List<Dto>>(set.ToList());

            Serializer.SerializeToFile(serializationType, col, Path.Combine(DirectoryPath, filename));
		}

		private void CreateDirectory()
		{
            Directory.CreateDirectory(DirectoryPath);
		}

		public IMapper GetMapper()
		{
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Data.Models.Category, CLI.Models.Category>();
                cfg.CreateMap<Data.Models.Customer, CLI.Models.Customer>();
                cfg.CreateMap<Data.Models.Employee, CLI.Models.Employee>();
                cfg.CreateMap<Data.Models.Order, CLI.Models.Order>();
                cfg.CreateMap<Data.Models.OrderDetail, CLI.Models.OrderDetail>();
                cfg.CreateMap<Data.Models.Product, CLI.Models.Product>();
                cfg.CreateMap<Data.Models.Shipper, CLI.Models.Shipper>();
                cfg.CreateMap<Data.Models.Supplier, CLI.Models.Supplier>();
            });

            return config.CreateMapper();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
