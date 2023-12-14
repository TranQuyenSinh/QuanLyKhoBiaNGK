using System.ComponentModel;
using ExcelDataReader;
using QuanLyKhoBiaNGK.Data;
using QuanLyKhoBiaNGK.Models;

namespace QuanLyKhoBiaNGK.Seeders
{
	public class Seeder
	{
		public static void SeedCategories(ApplicationDbContext context, string fileName)
		{
			var categoryReader = GetExcelReader(fileName);
			{
				while (categoryReader.Read())
				{
					categoryReader.Read();
					var item = new Category()
					{
						Name = categoryReader.GetValue(0).ToString()
					};
					context.Categories.Add(item);
				}
			} while (categoryReader.NextResult()) ;

			context.SaveChanges();
		}

		public static void SeedCustomers(ApplicationDbContext context, string fileName)
		{
			var customerReader = GetExcelReader(fileName);
			{
				while (customerReader.Read())
				{
					customerReader.Read();
					var item = new Customer()
					{
						FullName = customerReader.GetString(1),
						Birth = DateTime.Parse(customerReader.GetValue(2).ToString()),
						Gender = customerReader.GetValue(3).ToString() == "1",
						Address = customerReader.GetString(4),
						Phone = customerReader.GetString(5),
						Email = customerReader.GetString(6),
					};
					context.Add(item);
				}
			} while (customerReader.NextResult()) ;

			context.SaveChanges();
		}

		public static void SeedProducts(ApplicationDbContext context, string fileName)
		{
			var productReader = GetExcelReader(fileName);
			var categories = context.Categories.ToList();
			var rand = new Random();

			do
			{
				while (productReader.Read())
				{
					productReader.Read();
					var item = new Product()
					{
						Name = productReader.GetString(0),
						Description = productReader.GetString(1),
						InventoryLevel = Int32.Parse(productReader.GetValue(2).ToString()),
						Inventory = Int32.Parse(productReader.GetValue(3).ToString()),
						Unit = productReader.GetString(4),
						PurchasePrice = Int32.Parse(productReader.GetValue(5).ToString()),
						RetailPrice = Int32.Parse(productReader.GetValue(6).ToString()),
						WholesalePrice = Int32.Parse(productReader.GetValue(7).ToString()),
						CategoryId = categories[rand.Next(0, categories.Count)].Id,
					};
					context.Add(item);
				}
			} while (productReader.NextResult());

			context.SaveChanges();
		}

		public static IExcelDataReader GetExcelReader(string fileName)
		{
			var filePath = $"{Directory.GetCurrentDirectory()}{@"\DataExample"}\\{fileName}";
			var stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
			var reader = ExcelReaderFactory.CreateReader(stream);

			return reader;


		}
	}
}
