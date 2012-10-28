using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web;
using CC.Core;
using ImageResizer;
using KnowYourTurf.Core.Services;

namespace KnowYourTurf.Web.Services
{
    public interface IFileHandlerService
    {
        string SaveAndReturnUrlForFile(string root);
        bool DoesImageExist(string url);
        void DeleteFile(string url);
        bool RequsetHasFile();
    }

    public class FileHandlerService : IFileHandlerService
    {
        private readonly ISessionContext _sessionContext;

        public FileHandlerService(ISessionContext sessionContext)
        {
            _sessionContext = sessionContext;
        }

        public bool DoesImageExist(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "HEAD";

            bool exists;
            try
            {
                request.GetResponse();
                exists = true;
            }
            catch
            {
                exists = false;
            }
            return exists;

        }

        private HttpPostedFile getFile()
        {
             var file = HttpContext.Current.Request.Files.AllKeys.Length > 0 &&
                   HttpContext.Current.Request.Files[0].ContentLength > 0
                       ? HttpContext.Current.Request.Files[0]
                       : null;
            return file;
        }

        public bool RequsetHasFile()
        {
            var file = getFile();
            return file != null && file.ContentLength > 0;
        }

        public string SaveAndReturnUrlForFile(string root)
        {
            var file = getFile();
            if (file == null || file.ContentLength <= 0) return null;

            var pathForFile = GetPhysicalPathForFile(root);
            var exists = Directory.Exists(pathForFile);
            if (!exists)
            {
                Directory.CreateDirectory(pathForFile);
            }

            var fileExtension = new FileInfo(file.FileName).Extension.ToLower();
            var generatedFileName = Guid.NewGuid() + fileExtension;
            if (fileExtension == ".gif" || fileExtension == ".jpg" || fileExtension == ".jpeg" || fileExtension == ".png")
            {
                GenerateVersions(file, pathForFile, generatedFileName);
                generatedFileName = generatedFileName.Substring(0, generatedFileName.LastIndexOf(".")) + ".jpg";
            }
            else
            {
                file.SaveAs(pathForFile + generatedFileName);
            }
            return GetUrlForFile(generatedFileName, root);
        }

        public void DeleteFile(string url)
        {
            var mapPath = _sessionContext.MapPath(url);
            File.Delete(mapPath);
            File.Delete(mapPath.AddImageSizeToName("thumb"));
            File.Delete(mapPath.AddImageSizeToName("large"));
        }

        public void GenerateVersions(HttpPostedFile file, string pathForFile, string origFileName)
        {
            var versions = new Dictionary<string, string>
                                                      {
                                                          {"_thumb", "width=100&height=100&crop=auto&format=jpg"},
                                                          {"", "maxwidth=200&maxheight=200&crop=autoformat=jpg"},
                                                          {"_large", "maxwidth=400&maxheight=400format=jpg"}
                                                      };
            var fileNameNoExtension = origFileName.Substring(0, origFileName.LastIndexOf("."));
            foreach (var suffix in versions.Keys)
            {
                var appndFileName = fileNameNoExtension + suffix;
                var fullFilePath = Path.Combine(pathForFile, appndFileName);
                ImageBuilder.Current.Build(file, fullFilePath, new ResizeSettings(versions[suffix]), false, true);
            }
        }

        private string GetUrlForFile(string fileName, string root)
        {
            var companyId = _sessionContext.GetCompanyId();
            root = root.Replace("\\", "/");
            return @"/" + root + @"/" + companyId + @"/" + fileName;
        }

        private string GetPhysicalPathForFile(string root)
        {
            var companyId = _sessionContext.GetCompanyId();
            return ImageResizer.Util.PathUtils.AppPhysicalPath + root + @"\" + companyId + @"\";
        }

    }
}