using ashishprac.Data;
using ashishprac.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ashishprac.Controllers
{
    public class Products_SecondController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public Products_SecondController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

 
        //to create list--paramsters here -id get karne ke liye and product form me changes  new value ko le jaane ke liye
        public async Task<IActionResult> Create(Product  products)

        {
            if (ModelState.IsValid)
            {
                _dbContext.Add(products);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(products);
        }

        // READ - Get all products
        public async Task<IActionResult> Index()
        { 
          var products = await _dbContext.Products.ToListAsync();
            return View(products);
        }

        public async Task<IActionResult> Edit(int id)

        {
            var products = await _dbContext.Products.FindAsync(id);
            if(products==null)
            {
                return NotFound();
            }
            return View(products);

        }

        //now for save changes after editing

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product products)
        {
            if (id != products.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _dbContext.Update(products);
                    await _dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_dbContext.Products.Any(e => e.Id == id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                    return RedirectToAction("Index");

                }

                return View(products);

            }


        public async Task<IActionResult> Delete(int id)
        {
            var product = await _dbContext.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _dbContext.Products.Remove(product);  // Remove product from the context
            await _dbContext.SaveChangesAsync();  // Save changes to DB
            return RedirectToAction(nameof(Index));  // Redirect back to the list
        }

    }
}
