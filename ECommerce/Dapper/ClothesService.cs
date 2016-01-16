using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;
using ECommerce.Models;

namespace ECommerce.Dapper
{
    public class ClothesService : IClothesService
    {
        private static readonly string connString = ConfigurationManager.ConnectionStrings["clothesDB"].ConnectionString;
        private IDbConnection _db = new SqlConnection(connString);
        //private IDbConnection _db = new SqlConnection("Data Source=.; Database= Clothes; Integrated Security=True;");    

        private const string GETALL_CLOTHES_QUERY = @"SELECT * FROM Clothes";
        private const string GETALL_CATEGORY_QUERY = @"SELECT * FROM Category";
        private const string GETALL_BRAND_QUERY = @"SELECT * FROM Brand";
        private const string INSERT_CLOTHES_QUERY = @"INSERT INTO Clothes(Name, Color, Size, Quantity, Price, Image, Description, CategoryId, BrandId)
                                                        OUTPUT inserted.Id
                                                        VALUES(@Name, @Color, @Size, @Quantity, @Price, @Image, @Description, @CategoryId, @BrandId); ";
        private const string INSERT_CATEGORY_QUERY = @"INSERT INTO Category(Name)
                                                        OUTPUT inserted.Id VALUES(@Name); ";

        public List<Clothes> GetAll()
        {
            List<Clothes> empList = this._db.Query<Clothes>(GETALL_CLOTHES_QUERY).ToList();
            return empList;
        }

        public Clothes Find(Guid? id)
        {
            string query = "SELECT * FROM Clothes WHERE Id = '" + id + "'";
            return this._db.Query<Clothes>(query).SingleOrDefault();
        }

        public Clothes Add(Clothes employee)
        {
            //var sqlQuery = "INSERT INTO Clothes (Name, Quantity) VALUES(@Name, @Quantity); " + "SELECT CAST(SCOPE_IDENTITY() as int)";
            var employeeId = this._db.Query<Guid>(INSERT_CLOTHES_QUERY, employee).FirstOrDefault();
            employee.Id = employeeId;
            return employee;

        }
        
        public Clothes Update(Clothes employee)
        {
            var sqlQuery =
            "UPDATE Clothes " +
            "SET Name = @Name, " +
            "Quantity = @Quantity " +
            "WHERE Id = @Id";
            this._db.Execute(sqlQuery, employee);
            return employee;
        }

        public void Remove(Guid id)
        {
            var sqlQuery = ("Delete From Clothes Where Id = '" + id + "'");
            this._db.Execute(sqlQuery);
        }

        public Category GetACategory (Guid id)
        {
            var categoryList = this._db.Query<Category>(GETALL_CATEGORY_QUERY).ToList();
            return categoryList.Where(c => c.Id == id).FirstOrDefault();
        }
        public Brand GetABrand(Guid id)
        {
            var brandList = this._db.Query<Brand>(GETALL_BRAND_QUERY).ToList();
            return brandList.Where(c => c.Id == id).FirstOrDefault();
        }
        public List<Category> GetAllCategory()
        {
            List<Category> categoryList = this._db.Query<Category>(GETALL_CATEGORY_QUERY).ToList();
            return categoryList;
        }
        public List<Brand> GetAllBrand()
        {
            List<Brand> brandList = this._db.Query<Brand>(GETALL_BRAND_QUERY).ToList();
            return brandList;
        }
        public List<Clothes> GetAllFilteredClothes(Guid? brandId, Guid? categoryId, int? minPrice, int? maxPrice)
        {
            List<Clothes> clothesList = this._db.Query<Clothes>(GETALL_CLOTHES_QUERY).ToList();
            return clothesList
                        .Where(c => c.BrandId == brandId && c.CategoryId == categoryId
                                 && c.Price >= minPrice && c.Price <= maxPrice).ToList();
        }

        public Category AddCategory(Category category)
        {
            var categoryId = this._db.Query<Guid>(INSERT_CATEGORY_QUERY, category).FirstOrDefault();
            category.Id = categoryId;
            return category;
        }
        public void RemoveCategory(Guid id)
        {
            var sqlQuery = ("Delete From Category Where Id = '" + id + "'");
            this._db.Execute(sqlQuery);
        }

        public Category UpdateCategory(Category category)
        {
            var sqlQuery =
            "UPDATE Category " +
            "SET Name = @Name " +
            "WHERE Id = @Id";
            this._db.Execute(sqlQuery, category);
            return category;
        }

    }
}