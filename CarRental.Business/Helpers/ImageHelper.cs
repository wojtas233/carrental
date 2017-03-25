using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CarRental.Business.Helpers
{
    public static class ImageHelper
    {
        private static readonly string UploadsFolderPath = @"/Uploads";
        private static readonly string ImageFolderPath = @"/Images/";
        private static readonly string PngExt = ".png";

        public static string SaveToFolder(HttpPostedFileBase file, EnitityTypesEnum entityType, string id, string name)
        {
            var originalFilename = Path.GetFileName(file.FileName);
            string fileId = Guid.NewGuid().ToString().Replace("-", "");

            var folderPath = Path.Combine(HttpContext.Current.Request.MapPath(UploadsFolderPath + ImageFolderPath), entityType.ToString())
                .Replace("\\", @"/");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            var path = Path.GetFullPath(Path.Combine(folderPath, id + name + fileId + PngExt)).Replace("\\", @"/");
            
            file.SaveAs(path);

            var startIndex = path.IndexOf(UploadsFolderPath);
            path = path.Substring(startIndex, path.Length - startIndex);

            return path;
        }

        public static void DeleteFromFolder(string path)
        {
            var absolutePath = HttpContext.Current.Request.MapPath(path);
            if (Directory.Exists(Path.GetDirectoryName(absolutePath)))
            {
                File.Delete(absolutePath);
            }
        }
    }
}
