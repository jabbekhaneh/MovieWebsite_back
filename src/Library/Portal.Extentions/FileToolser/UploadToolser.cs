using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using System.Net.Http.Headers;

namespace Portal.Extentions.FileToolser
{
    public static class UploadToolser
    {
        //_________________________________________
        public static string Upload(IFormFile file, string path)
        {
            try
            {
                var fileName = string.Empty;
                var newFileName = string.Empty;
                fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                var myUniqueFileName = Convert.ToString(Guid.NewGuid());
                var FileExtension = Path.GetExtension(fileName);
                newFileName = myUniqueFileName + FileExtension;
                fileName = Path.Combine(Directory.GetCurrentDirectory(), path, newFileName);
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
                using (FileStream fs = File.Create(fileName))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                }
                return newFileName;
            }
            catch (Exception)
            {

                throw;
            }
        }
        //_________________________________________
        public static string UploadImageAndResize(IFormFile imageFile,
            int height, int width, string path, string pathResize)
        {

            string uniqCode = Guid.NewGuid().ToString().Replace("-", "");
            if (imageFile.Length > 0)
            {
                FileInfo fileInfo = new FileInfo(imageFile.FileName);
                string getExt = fileInfo.Extension.ToLower();
                string newFileName = "img_" + uniqCode + getExt;
                string imgUrlSave = Path.Combine(path, newFileName);
                string imgThumbUrlSave = Path.Combine(pathResize, newFileName);
                using (var stream = new MemoryStream())
                {
                    imageFile.CopyTo(stream);

                    using (FileStream fs = new FileStream(imgUrlSave, FileMode.Create, FileAccess.Write))
                    {
                        imageFile.CopyTo(fs);
                        stream.CopyTo(fs);
                        fs.Flush();
                    }
                    var image = Image.Load(imageFile.OpenReadStream());
                    IImageEncoder imageEncoder = new JpegEncoder()
                    {
                        Quality = 100,
                        ColorType = JpegColorType.YCbCrRatio444,
                    };
                    image.Mutate(x => x.Resize(width, height));
                    image.Save(imgThumbUrlSave, imageEncoder);
                }

                return newFileName;

            }
            return null;
        }
        //_________________________________________
        public static async Task<List<string>> UploadRangeAsync(IList<IFormFile> files, string path)
        {
            List<string> lstImgName = new List<string>();
            var fileName = string.Empty;
            var newFileName = string.Empty;
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var myUniqueFileName = Convert.ToString(Guid.NewGuid());
                    var FileExtension = Path.GetExtension(fileName);
                    newFileName = myUniqueFileName + FileExtension;
                    fileName = Path.Combine(Directory.GetCurrentDirectory(), path, newFileName);

                    if (File.Exists(fileName))
                    {
                        File.Delete(fileName);
                    }
                    using (FileStream fs = System.IO.File.Create(fileName))
                    {
                        await file.CopyToAsync(fs);
                        fs.Flush();
                    }
                    lstImgName.Add(newFileName);
                }
            }
            return lstImgName;
        }
        //_________________________________________
        public static List<string> UploadRange(IList<IFormFile> files, string path)
        {
            List<string> lstImgName = new List<string>();
            var fileName = string.Empty;
            var newFileName = string.Empty;
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var myUniqueFileName = Convert.ToString(Guid.NewGuid());
                    var FileExtension = Path.GetExtension(fileName);
                    newFileName = myUniqueFileName + FileExtension;
                    fileName = Path.Combine(Directory.GetCurrentDirectory(), path, newFileName);

                    if (File.Exists(fileName))
                    {
                        File.Delete(fileName);
                    }
                    using (FileStream fs = System.IO.File.Create(fileName))
                    {
                        file.CopyTo(fs);
                        fs.Flush();
                    }
                    lstImgName.Add(newFileName);
                }
            }
            return lstImgName;
        }
        //_________________________________________
        public static bool Remove(string fileName, string path)
        {

            string pathUpload = Path.Combine(Directory.GetCurrentDirectory(), path, fileName);

            if (File.Exists(pathUpload))
            {
                File.Delete(pathUpload);
                return true;
            }

            return false;
        }
        //_________________________________________
        public static bool RemoveRange(IList<string> fileNames, string path)
        {
            if (fileNames.Count > 0)
            {

                foreach (var item in fileNames)
                {
                    string pathUpload = Path.Combine(Directory.GetCurrentDirectory(), path, item);
                    if (File.Exists(pathUpload))
                    {
                        File.Delete(pathUpload);

                    }
                }
                return true;
            }
            return false;
        }
        //_________________________________________
        public static string ResizeImage(IFormFile imageFile, int height, int width, string path)
        {
            string uniqCode = Guid.NewGuid().ToString().Replace("-", "");
            FileInfo fileInfo = new FileInfo(imageFile.FileName);
            string getExt = fileInfo.Extension.ToLower();
            string newFileName = "img_" + uniqCode + getExt;
            string imgThumbUrlSave = Path.Combine(Directory.GetCurrentDirectory(), path, newFileName);
            using (var stream = new MemoryStream())
            {
                imageFile.CopyTo(stream);
                var image = Image.Load(imageFile.OpenReadStream());
                var widthT = image.Width;
                var heightT = image.Height;
                IImageEncoder imageEncoder = new JpegEncoder()
                {
                    Quality = 100,
                    ColorType = JpegColorType.YCbCrRatio444
                };
                image.Mutate(x => x.Resize(width, height));
                image.Save(imgThumbUrlSave, imageEncoder);
            }
            return newFileName;
        }
        //_________________________________________
    }
}
