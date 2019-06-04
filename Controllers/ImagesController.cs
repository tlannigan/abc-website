using ca.abcmufflerandhitch.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using PhotoSauce.MagicScaler;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ca.abcmufflerandhitch.Controllers
{
    public class ImagesController
    {
        private readonly ProductContext _context;
        private IHostingEnvironment _hostingEnv;

        public ImagesController(ProductContext context, IHostingEnvironment env)
        {
            _context = context;
            _hostingEnv = env;
        }

        public void SaveImage(int ID, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                // Empty file
                string defaultFileName = "default.png";
                string sourcePath = Path.Combine(_hostingEnv.WebRootPath + @"\img\temp", defaultFileName);

                var ext = Path.GetExtension(Path.Combine(sourcePath, defaultFileName));
                var fileName = ID + ext;

                string targetPath = Path.Combine(_hostingEnv.WebRootPath + @"\img\products", fileName);

                File.Copy(sourcePath, targetPath);
            }
            else
            {
                var fileName = ID + Path.GetExtension(file.FileName);
                fileName = _hostingEnv.WebRootPath + $@"\img\temp\{fileName}";

                using (FileStream fs = File.Create(fileName))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                }

                const int size = 160;

                var path = fileName;
                var outputDirectory = string.Empty;
                outputDirectory = _hostingEnv.WebRootPath + $@"\img\products";

                var settings = new ProcessImageSettings()
                {
                    Width = size,
                    Height = size,
                    ResizeMode = CropScaleMode.Max,
                    SaveFormat = FileFormat.Png
                };

                using (var output = new FileStream(OutputPath(path, outputDirectory), FileMode.Create))
                {
                    MagicImageProcessor.ProcessImage(path, output, settings);
                    File.Delete(path);
                }
            }
        }

        public void DeleteImage(int ID)
        {
            var fileName = _hostingEnv.WebRootPath + $@"\img\products\{ID}.png";
            File.Delete(fileName);
        }

        static string OutputPath(string inputPath, string outputDirectory)
        {
            return Path.Combine(
                outputDirectory,
                Path.GetFileNameWithoutExtension(inputPath)
                + Path.GetExtension(inputPath));
        }
    }
}
