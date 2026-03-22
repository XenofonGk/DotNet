using Microsoft.AspNetCore.Mvc;
using MyProject.Data;
using MyProject.Models;
namespace MyProject.Controllers{

public class SupplierController : Controller
    {
        private readonly ApplicationDbContext _db;

        public SupplierController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var objSupplierList = _db.Supplier.ToList();

            return View(objSupplierList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Supplier obj)
        {
            if (ModelState.IsValid)
            {
                _db.Supplier.Add(obj);
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
            var supplier = _db.Supplier.Find(id);
            if (supplier == null)
            {
                return NotFound();
            }
            return View(supplier);
        }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Supplier obj)
        {
            if (ModelState.IsValid)
            {
                _db.Supplier.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

// Delete get
    public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var supplier = _db.Supplier.Find(id);
            if (supplier == null)
            {
                return NotFound();
            }
            return View(supplier);
        }


// Delete post
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeletePost(int? id)
        {
            var supplier = _db.Supplier.Find(id);
            if (supplier == null)
            {
                return NotFound();
            }
            _db.Supplier.Remove(supplier);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}