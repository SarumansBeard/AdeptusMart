namespace AdeptusMart06.WebUI.Utils
{

    public class FileUploader
    {
        public static async Task<string> UploadAsync(IWebHostEnvironment hostEnvironment, IFormFile file, string subFolder = "")
        {
            string wwwRootPath = hostEnvironment.WebRootPath;

            string fileName = FileNameControl(Path.GetFileNameWithoutExtension(file.FileName));
            string extension = Path.GetExtension(file.FileName);
            fileName = fileName + DateTime.Now.ToString("yyyyMMddHHmmssfff") + extension;

            string folderPath = string.IsNullOrEmpty(subFolder) ? "upload" : $"upload/{subFolder}";
            string fullPath = Path.Combine(wwwRootPath, folderPath);
            Directory.CreateDirectory(fullPath); // klasör yoksa oluştur

            string path = Path.Combine(fullPath, fileName);

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return $"/{folderPath}/{fileName}";
        }

        public static async Task<bool> DeleteAsync(IWebHostEnvironment hostEnvironment, string? path)
        {
            try
            {
                if (!string.IsNullOrEmpty(path))
                {
                    string wwwRootPath = hostEnvironment.WebRootPath;
                    string filePath = Path.Combine(wwwRootPath + path);
                    FileInfo fileInfo = new FileInfo(filePath);
                    if (fileInfo.Exists)
                    {
                        fileInfo.Delete();
                        return true;
                    }
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        private static string FileNameControl(string filename)
        {
            var invalids = Path.GetInvalidFileNameChars();
            filename = string.Join("_", filename.Split(invalids, StringSplitOptions.RemoveEmptyEntries)).TrimEnd('.');
            filename = filename.Replace("ş", "s").Replace("ç", "c").Replace("ü", "u").Replace("ğ", "g").Replace("ı", "i").Replace("ö", "o");
            filename = filename.Replace("Ş", "S").Replace("Ç", "C").Replace("Ü", "U").Replace("Ğ", "G").Replace("İ", "I").Replace("Ö", "O");
            return filename;
        }
    }

}
