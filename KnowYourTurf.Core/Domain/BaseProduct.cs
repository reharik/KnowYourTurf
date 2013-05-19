using System.Collections.Generic;
using CC.Core.CustomAttributes;
using CC.Core.Domain;
using Castle.Components.Validator;
using KnowYourTurf.Web.Controllers;

namespace KnowYourTurf.Core.Domain
{
    public class BaseProduct : DomainEntity, IPersistableObject
    {
        [ValidateNonEmpty]
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        [ValidateNonEmpty]
        public virtual string InstantiatingType { get; set; }
        [TextArea]
        public virtual string Notes { get; set; }

        private readonly IList<Document> _documents = new List<Document>();
        public virtual IEnumerable<Document> Documents { get { return _documents; } }
        public virtual void RemoveDocument(Document fieldDocument) { _documents.Remove(fieldDocument); }
        public virtual void AddDocument(Document fieldDocument)
        {
            if (!fieldDocument.IsNew() && _documents.Contains(fieldDocument)) return;
            _documents.Add(fieldDocument);
        }

        private readonly IList<Photo> _photos = new List<Photo>();
        public virtual IEnumerable<Photo> Photos { get { return _photos; } }
        public virtual void RemovePhoto(Photo fieldPhoto) { _photos.Remove(fieldPhoto); }
        public virtual void AddPhoto(Photo fieldPhoto)
        {
            if (!fieldPhoto.IsNew() && _photos.Contains(fieldPhoto)) return;
            _photos.Add(fieldPhoto);
        }

    }
}