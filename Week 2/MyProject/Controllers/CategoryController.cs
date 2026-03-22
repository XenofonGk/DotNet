using System.ComponentModel.Design;
using Microsoft.AspNetCore.Mvc;
using MyProject.Data;
using MyProject.Models;
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
    public IActionResult Index()
    {
        // _db.Categories which is a DbSet<Category>
        var objCategoryList = _db.Categories.ToList();

        return View(objCategoryList);

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

         return RedirectToAction("Index");

    }
}
}