using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Enums;
using KnowYourTurf.Core.Localization;

namespace KnowYourTurf.Web.Controllers
{
    public class Document:DomainEntity
    {
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual DocumentCategory DocumentCategory { get; set; }
        [ValueOf(typeof(DocumentFileType))]
        public virtual string FileType { get; set; }
        public virtual string FileUrl { get; set; }
    }
}