using AdeptusMart01.Core.Entities;
using AdeptusMart01.Core;
using AdeptusMart06.WebUI.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Mvc.Rendering;
using AdeptusMart02.DataAccessLayer.Contexts;

namespace AdeptusMart06.WebUI.Areas.Management.Controllers
{
    [Area("Management")]
    [Route("Management/Product")]
    public class ProductController : Controller
    {

        private readonly AdeptusMartDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ProductController(AdeptusMartDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            List<Product> model = await _context
                .Products
                .Include(p => p.Category)
                .AsNoTracking()
                .Where(x => !x.IsDeleted)
                .ToListAsync();


            return View(model);
        }


        [HttpGet("Index/{id:guid}")]
        public async Task<IActionResult> Index(Guid id)
        {
            List<Product> model = await _context
                .Products
                .Include(p => p.Category)
                .AsNoTracking()
                .Where(x => x.IsDeleted == false && x.CategoryId == id)
                .ToListAsync();

            return View(model);
        }



        [HttpGet("Create")]
        public async Task<IActionResult> Create()
        {
            List<Category>? categories = await _context
                .Categories
                .AsNoTracking()
                .Where(x => x.IsDeleted == false)
                .ToListAsync();

            if (categories == null || categories.Count <= 0)
                return RedirectToAction(nameof(Index));

            SelectList categoriesSelectList = new
            SelectList(categories, nameof(Category.Id), nameof(Category.Name));

            //SelectList categoriesSelectList = new
            //    SelectList(categories, "Id", "Name");

            ViewData["Categories"] = categoriesSelectList;
            return View();
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product model, IFormFile img1, IFormFile img2, IFormFile img3, IFormFile img4)
        {
            if (ModelState.IsValid)
            {
                model.Id = Guid.NewGuid();
                model.RegisterTime = DateTime.UtcNow;
                model.IsDeleted = false;

                if (img1 != null)
                    model.ImageUrl1 = await FileUploader.UploadAsync(_env, img1);

                if (img2 != null)
                    model.ImageUrl2 = await FileUploader.UploadAsync(_env, img2);

                if (img3 != null)
                    model.ImageUrl3 = await FileUploader.UploadAsync(_env, img3);

                if (img4 != null)
                    model.ImageUrl4 = await FileUploader.UploadAsync(_env, img4);

                await _context.Products.AddAsync(model);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
                
            }

            List<Category>? categories = await _context
             .Categories
             .AsNoTracking()
             .Where(x => x.IsDeleted == false)
             .ToListAsync();

            if (categories == null)
                return RedirectToAction(nameof(Index));

            SelectList categoriesSelectList;

            if (model.CategoryId != Guid.Empty && model.CategoryId != null)
                categoriesSelectList = new
                    SelectList(categories,
                    nameof(Category.Id),
                    nameof(Category.Name),
                    model.CategoryId);
            else
                categoriesSelectList = new
                    SelectList(categories, nameof(Category.Id), nameof(Category.Name));

            ViewData["Categories"] = categoriesSelectList;


            return View(model);
           
        }


        [HttpGet("Edit")]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null || id == Guid.Empty)
            {
                return RedirectToAction(nameof(Index));
            }

            Product? model = await _context
           .Products           
           .FindAsync(id);

            if (model == null)
                return RedirectToAction(nameof(Index));

            List<Category>? categories = await _context
              .Categories
              .AsNoTracking()
              .Where(x => x.IsDeleted == false)
              .ToListAsync();

            if (categories == null)
                return RedirectToAction(nameof(Index));

            SelectList categoriesSelectList;

            if (model.CategoryId != Guid.Empty && model.CategoryId != null)
                categoriesSelectList = new
                    SelectList(categories,
                    nameof(Category.Id),
                    nameof(Category.Name),
                    model.CategoryId);
            else
                categoriesSelectList = new
                SelectList(categories, nameof(Category.Id), nameof(Category.Name));

            ViewData["Categories"] = categoriesSelectList;


            return View(model);
        }


        [HttpPost("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Product model, IFormFile? img1, IFormFile? img2, IFormFile? img3, IFormFile? img4)
        {            
            if (ModelState.IsValid)
            {
                if (img1 != null)
                {
                    await FileUploader.DeleteAsync(_env, model.ImageUrl1);
                    model.ImageUrl1 = await FileUploader.UploadAsync(_env, img1);
                }
                if (img2 != null)
                {
                    await FileUploader.DeleteAsync(_env, model.ImageUrl2);
                    model.ImageUrl2 = await FileUploader.UploadAsync(_env, img2);
                }
                if (img3 != null)
                {
                    await FileUploader.DeleteAsync(_env, model.ImageUrl3);
                    model.ImageUrl3 = await FileUploader.UploadAsync(_env, img3);
                }
                if (img4 != null)
                {
                    await FileUploader.DeleteAsync(_env, model.ImageUrl4);
                    model.ImageUrl4 = await FileUploader.UploadAsync(_env, img4);
                }

                _context.Products.Update(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            List<Category>? categories = await _context
              .Categories
              .AsNoTracking()
              .Where(x => x.IsDeleted == false)
              .ToListAsync();

            if (categories == null)
                return RedirectToAction(nameof(Index));

            SelectList categoriesSelectList;

            if (model.CategoryId != Guid.Empty && model.CategoryId != null)
                categoriesSelectList = new
                    SelectList(categories,
                    nameof(Category.Id),
                    nameof(Category.Name),
                    model.CategoryId);
            else
                categoriesSelectList = new
                    SelectList(categories, nameof(Category.Id), nameof(Category.Name));

            ViewData["Categories"] = categoriesSelectList;



            return View(model);
        }


        [HttpPost("Delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null || id == Guid.Empty)
            {
                return Json(new { status = false, message = "Id Hatalı!" });
            }

            Product? model = await _context
                .Products
                .FindAsync(id);

            if (model == null)
            {
                return Json(new { status = false, message = "Kayıt Bulunamadı!" });
            }

            model.IsDeleted = true;
            _context.Products.Update(model);
            await _context.SaveChangesAsync();

            return Json(new { status = true, message = "Kayıt Silindi!" });
        }
    }
}
