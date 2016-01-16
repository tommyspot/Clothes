using AutoMapper;
using ECommerce.Dapper;
using ECommerce.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.Controllers
{
    public class AdminController : Controller
    {
        private IClothesService _clothesService = new ClothesService();
        public ActionResult Index(int? page, Guid? brandId, Guid? categoryId, int? min, int? max)
        {
            int pageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PageSize"]);

            var clothesList = _clothesService.GetAll();
            if(brandId != Guid.Empty && brandId.HasValue)
            {
                clothesList = clothesList.Where(c => c.BrandId == brandId).ToList();
            }
            if(categoryId != Guid.Empty && categoryId.HasValue)
            {
                clothesList = clothesList.Where(c => c.CategoryId == categoryId).ToList();
            }
            if(min.HasValue)
            {
                clothesList = clothesList.Where(c => c.Price >= min).ToList();
            }
            if (max.HasValue)
            {
                clothesList = clothesList.Where(c => c.Price <= max).ToList();
            }

            List<ClothesViewModel> clothes = Mapper.Map<List<Clothes>, List<ClothesViewModel>>(clothesList);

            var categoryList = _clothesService.GetAllCategory();
            categoryList.Insert(0, new Category { Id = Guid.Empty, Name = "---- Select ----" });
            ViewBag.CategoryDDL = new SelectList(categoryList, "Id", "Name");

            var brandList = _clothesService.GetAllBrand();
            brandList.Insert(0, new Brand { Id = Guid.Empty, Name = "---- Select ----" });
            ViewBag.BrandDDL = new SelectList(brandList, "Id", "Name");


            ClothesViewViewModel model = new ClothesViewViewModel
            {
                ClothesViewModel = new PagedList<ClothesViewModel>(clothes, page ?? 1, pageSize),
                FilterBrand = brandId,
                FilterCategory = categoryId,
                MinPrice = min,
                MaxPrice = max,
            };

            return View(model);
        }

        public ActionResult Delete(Guid id)
        {
            try
            {
                _clothesService.Remove(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //public ActionResult Create()
        //{
        //    var categoryList = _clothesService.GetAllCategory();
        //    categoryList.Insert(0, new Category { Id = Guid.Empty, Name = "---- Select ----" });
        //    ViewBag.CategoryDDL = new SelectList(categoryList, "Id", "Name");

        //    var brandList = _clothesService.GetAllBrand();
        //    brandList.Insert(0, new Brand { Id = Guid.Empty, Name = "---- Select ----" });
        //    ViewBag.BrandDDL = new SelectList(brandList, "Id", "Name");

        //    //ViewBag.Category = new Func<Guid, Category>(_clothesService.GetACategory);
        //    //ViewBag.Brand = new Func<Guid, Brand>(_clothesService.GetABrand);
        //    return View();
        //}
        public ActionResult Create()
        {
            var categoryList = _clothesService.GetAllCategory();
            categoryList.Insert(0, new Category { Id = Guid.Empty, Name = "---- Select ----" });
            ViewBag.CategoryDDL = new SelectList(categoryList, "Id", "Name");

            var brandList = _clothesService.GetAllBrand();
            brandList.Insert(0, new Brand { Id = Guid.Empty, Name = "---- Select ----" });
            ViewBag.BrandDDL = new SelectList(brandList, "Id", "Name");

            return PartialView();
        }
        [HttpPost]
        public ActionResult Create(ClothesCreateViewModel clothes, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var fileName = String.Empty;
                if (file.ContentLength > 0) 
                {
                    fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images"), fileName);
                    file.SaveAs(path);
                }
                clothes.Image = fileName;
                //update clothes info
                var clothesEntity = Mapper.Map<ClothesCreateViewModel, Clothes>(clothes);

                _clothesService.Add(clothesEntity);
                return RedirectToAction("Index");
            }

            return View(clothes);
        }


        public ActionResult CreateCategory()
        {
            ViewBag.CategoryList = _clothesService.GetAllCategory();           
            return View();
        }

        [HttpPost]
        public ActionResult CreateCategory(CategoryViewModel category)
        {
            if (ModelState.IsValid)
            {
                var categoryEntity = Mapper.Map<CategoryViewModel, Category>(category);

                _clothesService.AddCategory(categoryEntity);
                return RedirectToAction("CreateCategory");
            }
            return View();
        }

        public ActionResult DeleteCategory(Guid id)
        {
            try
            {
                _clothesService.RemoveCategory(id);
                return RedirectToAction("CreateCategory");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult EditCategory(Guid id)
        {
            var category = _clothesService.GetACategory(id);
            var model = Mapper.Map<Category, CategoryViewModel>(category);
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult UpdateCategory(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var categoryEntity = Mapper.Map<CategoryViewModel, Category>(model);

                _clothesService.UpdateCategory(categoryEntity);
                return RedirectToAction("CreateCategory");
            }
            return PartialView("EditCategory", model);
        }
    }
}