using KnowYourTurf.Core.Domain.Tools.CustomAttributes;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace KnowYourTurf.Core.Config
{
    public class TextAreaConvention : AttributePropertyConvention<TextAreaAttribute>
    {
        //Make sure the properties with the [TextArea] attribute will be nvarChar(MAX) in the database
        protected override void Apply(TextAreaAttribute attribute, IPropertyInstance instance)
        {
            instance.Length(10000);
        }
    }
}