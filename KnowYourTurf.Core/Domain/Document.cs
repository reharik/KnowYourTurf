using CC.Core.Domain;
using CC.Core.Localization;
using Castle.Components.Validator;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Enums;

namespace KnowYourTurf.Web.Controllers
{
    public class Document : DomainEntity, IPersistableObject
    {
        [ValidateNonEmpty]
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual DocumentCategory DocumentCategory { get; set; }
        [ValueOf(typeof(DocumentFileType))]
        public virtual string FileType { get; set; }
        [ValidateNonEmpty]
        public virtual string FileUrl { get; set; }
    }
}