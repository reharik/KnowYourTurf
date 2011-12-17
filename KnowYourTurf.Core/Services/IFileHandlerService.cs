using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using ImageResizer;

namespace KnowYourTurf.Core.Services
{
    public interface IFileHandlerService
    {
        HttpPostedFile RetrieveUploadedFile();
        string GetUrlForFile(HttpPostedFile file, string leafDirectoryName);
        bool SaveUploadedFile(HttpPostedFile file, string leafDirectoryName);
        string GetPhysicalPathForFile(HttpPostedFile file, string leafDirectoryName);
        string GetGeneratedFileName(HttpPostedFile file);
    }

    public class FileHandlerService : IFileHandlerService
    {
        private readonly ISessionContext _sessionContext;

        public FileHandlerService(ISessionContext sessionContext)
        {
            _sessionContext = sessionContext;
        }

        public HttpPostedFile RetrieveUploadedFile()
        {
            return HttpContext.Current.Request.Files.AllKeys.Length > 0 &&
                   HttpContext.Current.Request.Files[0].ContentLength > 0
                       ? HttpContext.Current.Request.Files[0]
                       : null;
        }

        public string GetUrlForFile(HttpPostedFile file, string leafDirectoryName)
        {
            var companyId = _sessionContext.GetCompanyId();
            return @"/Company/" + companyId + @"/" + leafDirectoryName + @"/" + GetGeneratedFileName(file);
        }

        public string GetPhysicalPathForFile(HttpPostedFile file, string leafDirectoryName)
        {
            var companyId = _sessionContext.GetCompanyId();
            return ImageResizer.Util.PathUtils.AppPhysicalPath + @"Company\" + companyId + @"\" + leafDirectoryName + @"\";
        }

        public string GetGeneratedFileName(HttpPostedFile file)
        {
            var dateTimeForFileName = string.Format("{0:yyyyMMdd-HHmmss}", DateTime.Now);
            var fileName = dateTimeForFileName + file.FileName;
            return fileName;
        }

        public bool SaveUploadedFile(HttpPostedFile file, string leafDirectoryName)
        {
            var generatedFileName = GetGeneratedFileName(file);

            if (file.ContentLength <= 0) return false;

            var pathForFile = GetPhysicalPathForFile(file, leafDirectoryName);
            var exists = Directory.Exists(pathForFile);
            if (!exists)
            {
                Directory.CreateDirectory(pathForFile);
            }
            var fileExtension = new FileInfo(file.FileName).Extension.ToLower();
            if (fileExtension == ".gif" || fileExtension == ".jpg" || fileExtension == ".jpeg" || fileExtension == ".png")
            {
                //The resizing settings can specify any of 30 commands.. See http://imageresizing.net for details.
                ResizeSettings resizeCropSettings = new ResizeSettings();
                resizeCropSettings.Width = 200;
                resizeCropSettings.Height = 200;
                //  resizeCropSettings.Format = "jpg";
                resizeCropSettings.CropMode = CropMode.Auto;
                string fileName = Path.Combine(pathForFile, generatedFileName);
                GenerateVersions(file, leafDirectoryName, generatedFileName);
                fileName = ImageBuilder.Current.Build(file, fileName, resizeCropSettings, false, false);
                //TODO insert rules engine here
            }
            else
            {
                file.SaveAs(pathForFile + generatedFileName);
            }
            return true;
        }

        //temp
        public bool GenerateVersions(HttpPostedFile file, string leafDirectoryName, string origFileName)
        {
            Dictionary<string, string> versions = new Dictionary<string, string>();
            //Define the version to generate
            versions.Add("_thumb", "width=100&height=100&crop=auto&format=jpg"); //Crop to square thumbnail
            versions.Add("_medium", "maxwidth=400&maxheight=400format=jpg"); //Fit inside 400x400 area, jpeg

            //Loop through each uploaded file
            foreach (string fileKey in HttpContext.Current.Request.Files.Keys)
            {
                if (file.ContentLength <= 0) continue; //Skip unused file controls.
                var uploadFolder = GetPhysicalPathForFile(file, leafDirectoryName);
                if (!Directory.Exists(uploadFolder)) Directory.CreateDirectory(uploadFolder);
                //Generate each version
                string[] spFiles = origFileName.Split('.');
                foreach (string suffix in versions.Keys)
                {
                    string appndFileName = spFiles[0] + suffix;
                    string fileName = Path.Combine(uploadFolder, appndFileName);
                    fileName = ImageBuilder.Current.Build(file, fileName, new ResizeSettings(versions[suffix]), false,
                                                          true);
                }

            }
            return true;
        }
    }
}