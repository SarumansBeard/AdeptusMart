using AdeptusMart01.Core;
using AdeptusMart01.Core.Entities;
using AdeptusMart02.DataAccessLayer.Contexts;
using AdeptusMart04.WebUI.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdeptusMart04.WebUI.Areas.Management.Controllers
{
    [Area("Management")]
    public class BannerLeftController : Controller
    {

        private readonly AdeptusMartDbContext _context;
        private readonly IWebHostEnvironment _env;

        public BannerLeftController(AdeptusMartDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<BannerLeft> model = await _context
                .BannerLefts
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
        public async Task<IActionResult> Create(BannerLeft model, IFormFile img)
        {
            if (ModelState.IsValid)
            {
                model.Id = Guid.NewGuid();
                model.RegisterTime = DateTime.UtcNow;
                model.IsDeleted = false;
                model.BackGroundUrl = await FileUploader.UploadAsync(_env, img);

                await _context.BannerLefts.AddAsync(model);
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

                 BannerLeft? model = await _context
                .BannerLefts
                .FindAsync(id);

            if (model == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, BannerLeft model , IFormFile? img)
        {
            if (ModelState.IsValid)
            {
                if(img != null)
                {
                    await FileUploader.DeleteAsync(_env, model.BackGroundUrl);
                    model.BackGroundUrl = await FileUploader.UploadAsync(_env, img);
                }

                _context.BannerLefts.Update(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            if(id == null || id == Guid.Empty)
            {
                return Json(new { status = false, message = "Id Hatalı!" });
            }

            BannerLeft? model = await _context
                .BannerLefts
                .FindAsync(id);

            if (model == null)
            {
                return Json(new { status = false, message = "Kayıt Bulunamadı!" });
            }

            model.IsDeleted = true;
            _context.BannerLefts.Update(model);
            await _context.SaveChangesAsync();

            return Json(new { status = true, message = "Kayıt Silindi!" });
        }
    }
}
