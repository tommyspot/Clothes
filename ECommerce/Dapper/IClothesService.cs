using ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Dapper
{
    public interface IClothesService
    {
        List<Clothes> GetAll();
        Clothes Find(Guid? id);
        Clothes Add(Clothes employee);
        Clothes Update(Clothes employee);
        void Remove(Guid id);
        Category GetACategory(Guid id);
        Brand GetABrand(Guid id);
        List<Category> GetAllCategory();
        List<Brand> GetAllBrand();
        List<Clothes> GetAllFilteredClothes(Guid? brandId, Guid? categoryId, int? minPrice, int? maxPrice);
        Category AddCategory(Category category);
        void RemoveCategory(Guid id);
        Category UpdateCategory(Category category);
    }
}
