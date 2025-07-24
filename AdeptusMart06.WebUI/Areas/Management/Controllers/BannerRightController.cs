using AdeptusMart01.Core.Entities;
using AdeptusMart01.Core;
using AdeptusMart06.WebUI.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AdeptusMart02.DataAccessLayer.Contexts;

namespace AdeptusMart06.WebUI.Areas.Management.Controllers
{
    [Area("Management")]
    public class BannerRightController : Controller
    {

        private readonly AdeptusMartDbContext _context;
        private readonly IWebHostEnvironment _env;

        public BannerRightController(AdeptusMartDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<BannerRight> model = await _context
                .BannerRights
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
        public async Task<IActionResult> Create(BannerRight model, IFormFile img, IFormFile? VideoFile)
        {
            if (ModelState.IsValid)
            {
                model.Id = Guid.NewGuid();
                model.RegisterTime = DateTime.UtcNow;
                model.IsDeleted = false;

                if (img != null)
                {
                    model.BackGroundUrl = await FileUploader.UploadAsync(_env, img, "images");
                }

                if (VideoFile != null)
                {
                    model.VideoUrl = await FileUploader.UploadAsync(_env, VideoFile, "videos");
                }

                await _context.BannerRights.AddAsync(model);
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

            BannerRight? model = await _context
           .BannerRights
           .FindAsync(id);

            if (model == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, BannerRight model, IFormFile? img, IFormFile? VideoFile)
        {
            if (ModelState.IsValid)
            {
                var existingEntity = await _context.BannerRights.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                if (existingEntity == null)
                {
                    return RedirectToAction(nameof(Index));
                }

                if (img != null)
                {
                    await FileUploader.DeleteAsync(_env, existingEntity.BackGroundUrl);
                    model.BackGroundUrl = await FileUploader.UploadAsync(_env, img, "images");
                }
                else
                {
                    model.BackGroundUrl = existingEntity.BackGroundUrl;
                }

                if (VideoFile != null)
                {
                    await FileUploader.DeleteAsync(_env, existingEntity.VideoUrl);
                    model.VideoUrl = await FileUploader.UploadAsync(_env, VideoFile, "videos");
                }
                else
                {
                    model.VideoUrl = existingEntity.VideoUrl;
                }

                model.IsDeleted = existingEntity.IsDeleted;
                model.RegisterTime = existingEntity.RegisterTime;

                _context.BannerRights.Update(model);
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

            BannerRight? model = await _context
                .BannerRights
                .FindAsync(id);

            if (model == null)
            {
                return Json(new { status = false, message = "Kayıt Bulunamadı!" });
            }

            model.IsDeleted = true;
            _context.BannerRights.Update(model);
            await _context.SaveChangesAsync();

            return Json(new { status = true, message = "Kayıt Silindi!" });
        }
    }
}
