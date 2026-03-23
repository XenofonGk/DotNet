using System.ComponentModel.Design;
using Microsoft.AspNetCore.Mvc;
using MyProject.Data;
using MyProject.Models;
using MyProject.ViewModels;
namespace MyProject.Controllers{

public class CategoryController : Controller
{
    private readonly ApplicationDbContext _db;
        // 1 arg constructor, dependency injection
    public CategoryController(ApplicationDbContext db)
    {
        // c# handles the new/objects by itself
        _db = db;
    }

    // Getting all categories from db and pass to view
    // [Route("categories")]
    public IActionResult Index()
    {
        // Before we change it to ViewModel
        // _db.Categories which is a DbSet<Category>
        // var objCategoryList = _db.Categories.ToList();

        // return View(objCategoryList);

        var vm = new CategoryVM
        {
            Categories = _db.Categories.ToList(),
            PageTitle = "Category List",
            TotalCount = _db.Categories.Count()
        };
        return View(vm);
    }

    public IActionResult Create()
        {
            return View();
        }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Category obj)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category Created Succesfully";
            return RedirectToAction("Index");
            }
                              

            return View(obj);
        }

    public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var category =_db.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Category obj)
        {
              if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
            TempData["success"] = "Category Edited Succesfully";
            return RedirectToAction("Index");
            }
            return View(obj);
        }

    public IActionResult Delete(int? id)
        {
        if (id == null)
            {
                return NotFound();
            }
            var category =_db.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeletePost(int? id)
    {
        var category = _db.Categories.Find(id);
        if (category == null)
            {
                return NotFound();
            }        
         _db.Categories.Remove(category);
         _db.SaveChanges();            
         TempData["Success"] = "Category Deleted Succesfully";


         return RedirectToAction("Index");

    }
}
}