using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyOnlineStore.Data;
using MyOnlineStore.Models;

namespace MyOnlineStore.Controllers
{
    public class ProductController : Controller
    {
     
        MyContext db = new MyContext();
       
        [HttpGet]
        public IActionResult Index()
        {
            var Products = db.Products.Include(e => e.Category);
            return View(Products);
        }
  
        [HttpGet]
        public IActionResult Details(int id)
        {
            var Product = db.Products.Include(e => e.Category).FirstOrDefault(e => e.CategoryId == id);
            if (Product == null)
            {
                return RedirectToAction("Index");
            }
            return View(Product);
        }
     
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag._Categories = new SelectList(db.Categories, "CategoryId", "Name");
            return View();
        }
        
        [HttpPost]
        public IActionResult Create(Product product)
        {
            var existEmail = db.Products.FirstOrDefault(e => e.Title == product.Title);
            if (existEmail != null)
            {
                ModelState.AddModelError("", "Email Already Exist");
                ViewBag._Categories = new SelectList(db.Categories, "CategoryId", "Name");
                return View();
            }

            ModelState.Remove("Category");
            if (product!= null && ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "All Fields required");
            ViewBag._Categories = new SelectList(db.Categories, "CategoryId", "Name");
            return View();
        }
        
        [HttpGet]
        public IActionResult Edit(int id)
        {

            var product = db.Products.Include(e => e.Category).FirstOrDefault(e => e.CategoryId == id);
            if (product == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag._Categories = new SelectList(db.Categories, "CategoryId", "Name");
            return View(product);
        }
        
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            ModelState.Remove("Category");
            if (product != null && ModelState.IsValid)
            {
              
                db.Products.Update(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag._Categories = new SelectList(db.Categories, "CategoryId", "Name");
            return View(product);
        }
 
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var product = db.Products.Find(id);
            if (product == null)
            {
                return RedirectToAction("Index");
            }
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
      
    }
}
