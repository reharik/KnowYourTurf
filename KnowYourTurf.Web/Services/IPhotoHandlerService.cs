using System;
using System.IO;
using System.Web;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Services;
using FubuMVC.Core;
using xVal.ServerSide;

namespace KnowYourTurf.Web.Services
{
    public interface IUploadedFileHandlerService
    {
        void DeleteFile(string url);
        ICrudManager SaveUploadedFile(string systemFolder, string fileNameNoExtension, ICrudManager crudManager);
        string GetUploadedFileUrl(string serverDirectory);
        string GetUploadedFileUrl(string serverDirectory, string fileNameNoExtension);
    }

    public class UploadedFileHandlerService : IUploadedFileHandlerService
    {
        private readonly ISessionContext _sessionContext;

        public UploadedFileHandlerService(ISessionContext sessionContext)
        {
            _sessionContext = sessionContext;
        }

        public void DeleteFile(string url)
        {
            var mapPath = _sessionContext.MapPath(url);
            File.Delete(mapPath);
        }

        public ICrudManager SaveUploadedFile(string systemFolder, string fileNameNoExtension, ICrudManager crudManager)
        {
            var file = _sessionContext.RetrieveUploadedFile();
            if (file == null || file.ContentLength <= 0 || crudManager.HasFailingReport()) return crudManager;

            var folderPath = _sessionContext.MapPath(systemFolder);
            var extension = file.FileName.Substring(file.FileName.LastIndexOf("."));
            var newName = fileNameNoExtension + extension;
            string filePath = folderPath + "\\" + newName;
            var exists = Directory.Exists(folderPath);
            if (!exists)
            {
                Directory.CreateDirectory(folderPath);
            }
            //TODO insert rules engine here
            var fileExists = File.Exists(filePath);
            CrudReport crudReport= new CrudReport();
            if (fileExists)
            {
                crudReport.AddErrorInfo(new ErrorInfo("Photo",WebLocalizationKeys.FILE_ALREADY_EXISTS.ToString()));
                crudManager.AddCrudReport(crudReport);
                return crudManager;
            }
            file.SaveAs(filePath);
            crudReport.Success = true;
            crudManager.AddCrudReport(crudReport);
            return crudManager;
        }

        public string GetUploadedFileUrl(string serverDirectory)
        {
            return GetUploadedFileUrl(serverDirectory, "");
        }

        public string GetUploadedFileUrl(string serverDirectory, string fileNameNoExtension)
        {
            var file = _sessionContext.RetrieveUploadedFile();
            if (file==null || file.ContentLength <= 0) return string.Empty;
            var fileName = file.FileName;
            if(fileNameNoExtension.IsNotEmpty())
            {
                var extension = file.FileName.Substring(file.FileName.LastIndexOf("."));
                fileName = fileNameNoExtension + extension;
            }
            return serverDirectory + "/" + fileName;
        }
    }
}