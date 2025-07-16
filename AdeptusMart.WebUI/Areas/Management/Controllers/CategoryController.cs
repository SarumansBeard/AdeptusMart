using AdeptusMart01.Core;
using AdeptusMart01.Core.Entities;
using AdeptusMart02.DataAccessLayer.Contexts;
using AdeptusMart04.WebUI.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdeptusMart04.WebUI.Areas.Management.Controllers
{
    [Area("Management")]
    public class CategoryController : Controller
    {

        private readonly AdeptusMartDbContext _context;
        private readonly IWebHostEnvironment _env;

        public CategoryController(AdeptusMartDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Category> model = await _context
                .Categories
                .AsNoTracking()
                .Where(x => !x.IsDeleted)
                .ToListAsync();


            return View(model);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category model, IFormFile img)
        {
            if (ModelState.IsValid)
            {
                model.Id = Guid.NewGuid();
                model.RegisterTime = DateTime.UtcNow;
                model.IsDeleted = false;
                model.ImageUrl = await FileUploader.UploadAsync(_env, img);

                await _context.Categories.AddAsync(model);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View(model);

        }




        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null || id == Guid.Empty)
            {
                return RedirectToAction(nameof(Index));
            }

            Category? model = await _context
           .Categories
           .FindAsync(id);

            if (model == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Category model, IFormFile? img)
        {
            if (ModelState.IsValid)
            {
                if (img != null)
                {
                    await FileUploader.DeleteAsync(_env, model.ImageUrl);
                    model.ImageUrl = await FileUploader.UploadAsync(_env, img);
                }

                _context.Categories.Update(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null || id == Guid.Empty)
            {
                return Json(new { status = false, message = "Id Hatalı!" });
            }

            Category? model = await _context
                .Categories
                .FindAsync(id);

            if (model == null)
            {
                return Json(new { status = false, message = "Kayıt Bulunamadı!" });
            }

            model.IsDeleted = true;
            _context.Categories.Update(model);
            await _context.SaveChangesAsync();

            return Json(new { status = true, message = "Kayıt Silindi!" });
        }
    }
}
