using System.Text;
using HtmlTags;

namespace KnowYourTurf.Core.Html.Expressions
{
    public class AlphaNumericPickerExpression 
    {
        private string _id;

        public AlphaNumericPickerExpression WithId(string id)
        {
            _id = id;
            return this;
        }

        public HtmlTag ToHtmlTag()
        {
            var root = new HtmlTag("form");
            if (!string.IsNullOrEmpty(_id))
            {
                root.Attr("id", _id);
            }
            var innerDiv = new HtmlTag("div");
            innerDiv.Append(new StyledButtonExpression("all", true).NonLocalizedText("All").ToHtmlTag());
            for (int i = 65; i < 91; i++)
            {
                string letter = ((char)i).ToString();
                innerDiv.Append(new StyledButtonExpression(letter, true).NonLocalizedText(letter).ToHtmlTag());
            }
            root.Append(innerDiv);
            return root;
        }

    }
}