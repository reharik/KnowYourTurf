using CC.Core;
using CC.Core.Html.CCUI.Builders;
using CC.Core.Html.CCUI.HtmlConventionRegistries;
using HtmlTags;

namespace KnowYourTurf.Core.Html.HtmlConventionRegistries
{
    public class KYTKOHtmlConventionRegistry : CCHtmlConventionsKO
    {
        public KYTKOHtmlConventionRegistry()
        {
            EditorsChain();
        }

        public override void EditorsChain()
        {
            Editors.Builder<TimePickerBuilder2>();
//            Editors.Builder<ImageBuilder2>();
            Editors.Builder<PictureGallery>();
            base.EditorsChain();
        }
    }
}