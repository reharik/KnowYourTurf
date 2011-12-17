using HtmlTags;

namespace KnowYourTurf.Core.Html.Expressions
{
    public class ValueObjectSelector:HtmlTagExpressionBase
    {
        private string _rootId;
        private string _collectionName;
        private string _templateName;

        /// <param name="collectionName">Collection name on model which should be looped through</param>
        /// <param name="templateName">Template name for partial code</param>
        public ValueObjectSelector(string collectionName, string templateName) : base(new DivTag(""))
        {
            _collectionName = collectionName;
            _templateName = templateName;
        }

        public HtmlTag ToHtmlTag()
        {
            ElementId(_rootId);
            AddClass("valueObjectSelectorRoot");
            var rootTag = ToHtmlTagBase();
            var ul = new HtmlTag("ul");

            addChildItems(ul);


            rootTag.Children.Add(ul);
            return rootTag;
        }

        private void addChildItems(HtmlTag ul)
        {
            var partial = "@foreach (var item in @Model." + _collectionName + " }){ @Html.Partial(\"" + _templateName + "\", item) }";
            ul.Text(partial);
        }

        public ValueObjectSelector RootId(string rootId)
        {
            _rootId = rootId;
            return this;
        }
    }
}