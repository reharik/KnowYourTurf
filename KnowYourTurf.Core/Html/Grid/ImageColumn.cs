using System.Collections.Generic;
using FubuMVC.Core.Util;
using HtmlTags;
using KnowYourTurf.Core.Domain;
using Rhino.Security.Interfaces;

namespace KnowYourTurf.Core.Html.Grid
{
    public class ImageColumn<ENTITY> : ColumnBase<ENTITY> where ENTITY : IGridEnabledClass
    {
        protected List<string> _divCssClasses;
        protected string _imageName;
        public ImageColumn()
        {
            _divCssClasses = new List<string>();
            propertyAccessor = ReflectionHelper.GetAccessor<ENTITY>(x=>x.EntityId);
            Properties[GridColumnProperties.sortable.ToString()] = "false";
            Properties[GridColumnProperties.width.ToString()] = "20";
        }
        
        public ColumnBase<ENTITY> ImageName(string imageName)
        {
            _imageName = imageName;
            Properties[GridColumnProperties.name.ToString()] = imageName;
            Properties[GridColumnProperties.header.ToString()] = BuildImage(true).ToString();
            return this;
        }
        public ColumnBase<ENTITY> ImageWidth(string width)
        {
            Properties[GridColumnProperties.width.ToString()] = width;
            return this;
        }
        public override string BuildColumn(object item, User user, IAuthorizationService _authorizationService, string gridName)
        {
            var _item = (ENTITY)item;
            var value = FormatValue(_item, user, _authorizationService);
            if (value.IsEmpty()) return null;
            var divTag = BuildDiv();
            divTag.AddClasses(new[] { "imageColumn" });
            var image = BuildImage();
            divTag.Children.Add(image);
            return divTag.ToPrettyString();
        }

        protected HtmlTag BuildImage(bool header = false)
        {
            var img = new HtmlTag("img");
            img.Attr("src", "/content/images/" + _imageName);
            if(header)
            {
                img.Style("cursor", "hand");
            }
            return img;
        }

        protected DivTag BuildDiv()
        {
            var divTag = new DivTag("imageDiv");
            divTag.Attr("title", _toolTip);
            divTag.AddClasses(_divCssClasses);
            return divTag;
        }

        public ColumnBase<ENTITY> AddClassToDiv(string cssClass)
        {
            _divCssClasses.Add(cssClass);
            return this;
        }
    }
}