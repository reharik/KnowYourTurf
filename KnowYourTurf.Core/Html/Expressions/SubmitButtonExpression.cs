using CC.Core;

namespace KnowYourTurf.Core.Html.Expressions
{
    public class SubmitButtonExpression : HtmlCommonExpressionBase
    {
        private readonly string _name;
        private readonly string _value;

        public SubmitButtonExpression(string value, string name)
        {
            _name = name;
            _value = value;
        }

        public override string ToString()
        {
            return @"<input type=""submit"" value=""{0}"" name=""{1}""{2}/>".ToFormat(_value, _name, GetHtmlAttributesString());
        }
    }
}