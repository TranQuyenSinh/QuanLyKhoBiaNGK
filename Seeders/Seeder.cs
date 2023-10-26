using IronXL;
using QuanLyKhoBiaNGK.Data;
using QuanLyKhoBiaNGK.Models;

namespace QuanLyKhoBiaNGK.Seeders
{
	public class Seeder
	{
		public static void SeedCategories(ApplicationDbContext context, string fileName)
		{
			var workSheet = GetWorkSheet(fileName).First();

			for (var i = 2; i <= workSheet.RowCount; i++)
			{
				var item = new Category()
				{
					Name = workSheet[$"B{i}"].StringValue,
				};
				context.Categories.Add(item);
			}
			context.SaveChanges();
		}

		public static void SeedCustomers(ApplicationDbContext context, string fileName)
		{
			var workSheet = GetWorkSheet(fileName).First();

			for (var i = 2; i <= workSheet.RowCount; i++)
			{
				var item = new Customer()
				{
					FirstName = workSheet[$"B{i}"].StringValue,
					LastName = workSheet[$"C{i}"].StringValue,
					Birth = workSheet[$"D{i}"].DateTimeValue,
					Gender = workSheet[$"E{i}"].BoolValue,
					Address = workSheet[$"F{i}"].StringValue,
					Phone = workSheet[$"G{i}"].StringValue,
					Email = workSheet[$"H{i}"].StringValue,
				};
				context.Customers.Add(item);
			}
			context.SaveChanges();
		}

		public static void SeedProducts(ApplicationDbContext context, string fileName)
		{
			var productWorkSheet = GetWorkSheet(fileName).First();
			var priceWorkSheet = GetWorkSheet(fileName)[1];
			var categories = context.Categories.ToList();
			Random rand = new Random();

			for (var i = 2; i <= productWorkSheet.RowCount; i++)
			{
				var item = new Product()
				{
					Name = productWorkSheet[$"B{i}"].StringValue,
					Description = productWorkSheet[$"C{i}"].StringValue,
					InventoryLevel = productWorkSheet[$"D{i}"].IntValue,
					Inventory = productWorkSheet[$"E{i}"].IntValue,
					CategoryId = categories[rand.Next(0, categories.Count)].Id,
				};
				context.Products.Add(item);
			}
			context.SaveChanges();

			var products = context.Products.ToList();
			for (var i = 2; i <= priceWorkSheet.RowCount; i++)
			{
				var item = new Price()
				{
					Unit = priceWorkSheet[$"B{i}"].StringValue,
					ConversionRate = priceWorkSheet[$"C{i}"].IntValue,
					PurchasePrice = priceWorkSheet[$"D{i}"].IntValue,
					RetailPrice = priceWorkSheet[$"E{i}"].IntValue,
					WholesalePrice = priceWorkSheet[$"F{i}"].IntValue,
					ProductId = products[rand.Next(0, products.Count)].Id,
				};
				context.Prices.Add(item);
			}
			context.SaveChanges();
		}

		public static WorksheetsCollection GetWorkSheet(string fileName)
		{
			var filePath = $"{Directory.GetCurrentDirectory()}{@"\DataExample"}\\{fileName}";
			WorkBook workBook = WorkBook.Load(filePath);
			return workBook.WorkSheets;
		}
	}
}
