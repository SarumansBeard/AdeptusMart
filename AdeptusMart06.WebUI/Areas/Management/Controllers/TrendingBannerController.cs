using AdeptusMart01.Core;
using AdeptusMart01.Core.Entities;
using AdeptusMart02.DataAccessLayer.Contexts;
using AdeptusMart06.WebUI.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AdeptusMart06.WebUI.Areas.Management.Controllers
{
    [Area("Management")]
    [Route("Management/TrendingBanner")]
    public class TrendingBannerController : Controller
    {
        
        private readonly AdeptusMartDbContext _context;
        private readonly IWebHostEnvironment _env;

        public TrendingBannerController(AdeptusMartDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<TrendingBanner> model = await _context
                .TrendingBanners                
                .AsNoTracking()
                .Where(x => !x.IsDeleted)
                .ToListAsync();


            return View(model);
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TrendingBanner model, IFormFile img)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                model.Id = Guid.NewGuid();
                model.RegisterTime = DateTime.UtcNow;
                model.IsDeleted = false;

                if (img != null)
                    model.BackGroundUrl = await FileUploader.UploadAsync(_env, img);
                

                await _context.TrendingBanners.AddAsync(model);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Dosya yükleme ya da kayıt sırasında hata oluştu: " + ex.Message);
                return View(model);
            }
        }


        [HttpGet("Edit")]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null || id == Guid.Empty)
            {
                return RedirectToAction(nameof(Index));
            }

            TrendingBanner? model = await _context
           .TrendingBanners           
           .FirstOrDefaultAsync(p => p.Id == id);

            


            return View(model);
        }


        [HttpPost("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, TrendingBanner model, IFormFile? img)
        {
            if (ModelState.IsValid)
            {
                if (img != null)
                {
                    await FileUploader.DeleteAsync(_env, model.BackGroundUrl);
                    model.BackGroundUrl = await FileUploader.UploadAsync(_env, img);
                }               

                _context.TrendingBanners.Update(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }                     


            return View(model);
        }


        [HttpPost("Delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null || id == Guid.Empty)
            {
                return Json(new { status = false, message = "Id Hatalı!" });
            }

            TrendingBanner? model = await _context
                .TrendingBanners
                .FindAsync(id);

            if (model == null)
            {
                return Json(new { status = false, message = "Kayıt Bulunamadı!" });
            }

            model.IsDeleted = true;
            _context.TrendingBanners.Update(model);
            await _context.SaveChangesAsync();

            return Json(new { status = true, message = "Kayıt Silindi!" });
        }



    }
}
