//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Web;
//using ImageResizer;

//namespace KnowYourTurf.Core.Services
//{
//    public interface IFileUploaderService
//    {
//        IEnumerable<UploadFilesViewModel> UploadFiles();
//    }

//    public class FileUploaderService : IFileUploaderService
//    {
//        private readonly ISessionContext _sessionContext;

//        public FileUploaderService(ISessionContext sessionContext)
//        {
//            _sessionContext = sessionContext;
//        }

//        public void DeleteFile(string url)
//        {
//            var path = _sessionContext.MapPath(url);

//            if (File.Exists(path))
//            {
//                File.Delete(path);
//            }
//        }

//        public UploadFilesViewModel GenerateVersions(FileHandlerDto fileHandlerDto)
//        {
//            var versions = new Dictionary<string, string>
//                                                      {
//                                                          {"_thumb", "width=100&height=100&crop=auto&format=jpg"},
//                                                          {"", "maxwidth=200&maxheight=200&crop=autoformat=jpg"},
//                                                          {"_large", "maxwidth=400&maxheight=400format=jpg"}
//                                                      };
//            var fileNameNoExtension = fileHandlerDto.FileName.Substring(0, fileHandlerDto.FileName.LastIndexOf("."));
//            var newFileName = fileNameNoExtension + ".jpg";
//            foreach (var suffix in versions.Keys)
//            {
//                var appndFileName = fileNameNoExtension + suffix;
//                var fullFilePath = Path.Combine(fileHandlerDto.PhysicalPath, appndFileName);
//                ImageBuilder.Current.Build(fileHandlerDto.File, fullFilePath, new ResizeSettings(versions[suffix]), false, true);
//            }
//            return new UploadFilesViewModel
//                       {
//                           name = fileHandlerDto.File.FileName,
//                           size = fileHandlerDto.File.ContentLength,
//                           type = fileHandlerDto.File.ContentType,
//                           url = GetUrlForFile(newFileName, fileHandlerDto.Root),
//                           delete_url = "/Home/Delete/" + fileHandlerDto.File.FileName,
//                           thumbnail_url = GetUrlForFile(GetFileNameBySuffix(newFileName, "thumb"), fileHandlerDto.Root),
//                           delete_type = "GET",
//                       };
//        }

//        private UploadFilesViewModel UploadWholeFile(FileHandlerDto fileHandlerDto)
//        {

//            fileHandlerDto.File.SaveAs(fileHandlerDto.PhysicalPath);

//            return new UploadFilesViewModel
//                       {
//                           name = fileHandlerDto.File.FileName,
//                           size = fileHandlerDto.File.ContentLength,
//                           type = fileHandlerDto.File.ContentType,
//                           url = GetUrlForFile(fileHandlerDto.FileName, fileHandlerDto.Root),
//                           delete_url = "/Home/Delete/" + fileHandlerDto.File.FileName,
//                           thumbnail_url = GetUrlForFile(GetFileNameBySuffix(fileHandlerDto.FileName, "thumb"), fileHandlerDto.Root),
//                           delete_type = "GET",
//                       };
//        }

//        public IEnumerable<UploadFilesViewModel> UploadFiles(string root)
//        {
//            var results = new List<UploadFilesViewModel>();
//            var pathForFile = GetPhysicalPathForFile(root);
//            var exists = Directory.Exists(pathForFile);
//            if (!exists)
//            {
//                Directory.CreateDirectory(pathForFile);
//            }
//            HttpContext.Current.Request.Files.ForEachItem(x=>
//                {
//                    var fileExtension = new FileInfo(x.FileName).Extension.ToLower();
//                    var generatedFileName = Guid.NewGuid() + fileExtension;
//                    var fileHandlerDto = new FileHandlerDto
//                                             {
//                                                 File = x,
//                                                 PhysicalPath = pathForFile,
//                                                 FileName = generatedFileName,
//                                                 Root = root
//                                             };
//                    if (fileExtension == ".gif" || fileExtension == ".jpg" || fileExtension == ".jpeg" || fileExtension == ".png")
//                    {
//                        results.Add(GenerateVersions(fileHandlerDto));
//                    }
//                    else
//                    {
//                        results.Add(UploadWholeFile(fileHandlerDto));
//                    }        
//                });
//            return results;
//        }

//        private string GetPhysicalPathForFile(string root)
//        {
//            var companyId = _sessionContext.GetCompanyId();
//            return ImageResizer.Util.PathUtils.AppPhysicalPath + root + @"\" + companyId + @"\";
//        }

//        private string GetUrlForFile(string fileName, string root)
//        {
//            var companyId = _sessionContext.GetCompanyId();
//            return @"/" + root + @"/" + companyId + @"/" + fileName;
//        }

//        public string GetFileNameBySuffix(string fileName, string suffix)
//        {
//            var extenstion = fileName.Substring(fileName.Length - 4);
//            return fileName.Substring(0, fileName.Length - 4) + "_" + suffix + extenstion;
//        }
//    }

//    public class FileHandlerDto 
//    {
//        public HttpPostedFile File { get; set; }
//        public string PhysicalPath { get; set; }
//        public string FileName { get; set; }
//        public string Root { get; set; }
//    }

//    public class UploadFilesViewModel
//    {
//        public string name { get; set; }
//        public int size { get; set; }
//        public string type { get; set; }
//        public string url { get; set; }
//        public string delete_url { get; set; }
//        public string thumbnail_url { get; set; }
//        public string delete_type { get; set; }
//    }
//}