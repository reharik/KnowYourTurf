using System.Reflection;

namespace KnowYourTurf.Core.Html.Expressions
{
    public static class HtmlExpressionExtensions
    {
        public static EXPRESSION Attributes<EXPRESSION>(this EXPRESSION expression, object attributes)
            where EXPRESSION : HtmlCommonExpressionBase
        {
            foreach (PropertyInfo prop in attributes.GetType().GetProperties())
            {
                expression.HtmlAttributes.Add(prop.Name, prop.GetValue(attributes, null).ToString());
            }

            return expression;
        }

        public static EXPRESSION Attr<EXPRESSION>(this EXPRESSION expression, string name, string value)
            where EXPRESSION : HtmlCommonExpressionBase
        {
            expression.HtmlAttributes.Add(name, value);
            return expression;
        }

        public static EXPRESSION ReadOnly<EXPRESSION>(this EXPRESSION expression) where EXPRESSION : HtmlCommonExpressionBase
        {
            expression.HtmlAttributes.Add("disabled", "disabled");
            return expression;
        }

        public static EXPRESSION Class<EXPRESSION>(this EXPRESSION expression, string className)
            where EXPRESSION : HtmlCommonExpressionBase
        {
            expression.CssClasses.Add(className);
            return expression;
        }

        public static EXPRESSION ElementId<EXPRESSION>(this EXPRESSION expression, string id)
            where EXPRESSION : HtmlCommonExpressionBase
        {
            expression.HtmlAttributes.Add("id", id);
            return expression;
        }

    }
}