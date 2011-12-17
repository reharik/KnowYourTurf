using System;
using System.Collections.Generic;
using HtmlTags;

namespace KnowYourTurf.Core.Html.Expressions
{
    public class HtmlTagExpressionBase
    {
        private HtmlTag _rootTag;
        private List<string> cssClasses;
        private Dictionary<string, string> attributes;
        private string _id;

        public HtmlTagExpressionBase(HtmlTag tag)
        {
            _rootTag = tag;
        }

        public HtmlTagExpressionBase(string tagType)
        {
            _rootTag = new HtmlTag(tagType);
        }

        public HtmlTag ToHtmlTagBase()
        {
            _rootTag.Attr("id",_id);
            if (cssClasses != null)
            {
                _rootTag.AddClasses(cssClasses);
            }
            addAttributes();
            return _rootTag;
        }

        private void addAttributes()
        {
            if (attributes != null)
            {
                attributes.Each(x => _rootTag.Attr(x.Key, x.Value));
            }
        }

        public HtmlTagExpressionBase AddClass(string className)
        {
            if(cssClasses==null)cssClasses=new List<string>();
            if(className.Contains(" "))throw new Exception("cssClasses may not contain spaces");
            cssClasses.Add(className);
            return this;
        }

        public HtmlTagExpressionBase AddClasses(IEnumerable<string> classes)
        {
            if (cssClasses == null) cssClasses = new List<string>();
            cssClasses.AddRange(classes);
            return this;
        }

        public HtmlTagExpressionBase AddAttr(string attr, string value)
        {
            if (attributes== null) attributes= new Dictionary<string, string>();
            attributes.Add(attr,value);
            return this;
        }

        public HtmlTagExpressionBase ElementId(string id)
        {
            _id = id;
            return this;
        }
    }
}