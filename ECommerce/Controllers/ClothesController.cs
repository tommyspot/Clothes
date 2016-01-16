using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerce.Dapper;
using AutoMapper;
using ECommerce.Models;

namespace ECommerce.Controllers
{
    public class ClothesController : Controller
    {
        private IClothesService _clothesService = new ClothesService();
        //
        // GET: /Employee/

        public ActionResult Index()
        {
            var clothesList = _clothesService.GetAll();

            List<List<Clothes>> groupOf4ClothesList = this.GetGroupOf4ClothesList(clothesList);
            List<List<ClothesViewModel>> model = Mapper.Map<List<List<Clothes>>, List<List<ClothesViewModel>>>(groupOf4ClothesList);

            return View(model);
        }

        //
        // GET: /Employee/Details/5

        //public ActionResult Details(Guid? id)
        //{
        //    return View(_dashboard.Find(id));
        //}
        public ViewResult Details(Guid id)
        {
            Clothes clothes = _clothesService.Find(id);
            ClothesViewModel model = Mapper.Map<Clothes, ClothesViewModel>(clothes);

            return View(model);
        }
        //
        // GET: /Employee/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Employee/Create

        [HttpPost]
        public ActionResult Create([Bind(Include = "Name,Quantity")] Clothes employee)
        {
            if (ModelState.IsValid)
            {
                _clothesService.Add(employee);
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        //
        // GET: /Employee/Edit/5

        public ActionResult Edit(Guid id)
        {
            return View(_clothesService.Find(id));
        }

        // POST: /User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,Name,Quantity")] Clothes employee, Guid id)
        {
            if (ModelState.IsValid)
            {
                _clothesService.Update(employee);
            }
            return View(employee);
        }

        //
        // GET: /Employee/Delete/5

        public ActionResult Delete(Guid id)
        {
            return View(_clothesService.Find(id));
        }

        //
        // POST: /Employee/Delete/5

        [HttpPost]
        public ActionResult Delete(Guid id, FormCollection collection)
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

        private List<List<Clothes>> GetGroupOf4ClothesList(List<Clothes> clothesList)
        {
            List<List<Clothes>> groupOf4ClothesList = new List<List<Clothes>>();
            List<Clothes> group4 = new List<Clothes>();
            for (int i = 1; i < clothesList.Count + 1; i++)
            {
                group4.Add(clothesList[i - 1]);

                if (i % 4 == 0)
                {
                    groupOf4ClothesList.Add(group4);
                    group4 = new List<Clothes>();
                }
            }
            if (group4.Count > 0)
            {
                groupOf4ClothesList.Add(group4);
            }

            return groupOf4ClothesList;
        }
    }
}
