using System;
using System.Linq;
using HtmlTags;

namespace KnowYourTurf.Core.Html.FubuUI.Tags
{
    public class RadioButtonListTag : HtmlTag
    {
    
  private const string SelectedAttributeKey = "checked";

        public RadioButtonListTag()
            : base("div")
        {
        }

        public RadioButtonListTag(Action<RadioButtonListTag> configure) : this()
        {
            configure(this);
        }

        public RadioButtonListTag TopOption(string display, object value, string name, Action<HtmlTag> optionAction)
        {
            var option = TopOption(display, value, name);
            if(optionAction != null ) optionAction(option);
            return this;
        }

        public HtmlTag TopOption(string display, object value, string name)
        {
            var button = MakeRadioButton(display, value, name);
            InsertFirst(button);
            return button;
        }

        public HtmlTag AddRadioButton(string display, object value, string name)
        {
            var button = MakeRadioButton(display, value, name);
            Append(button);
            return button;
        }

        public RadioButtonListTag DefaultRadioButton(string display, string name)
        {
            var option = TopOption(display, "",name);
            MarkOptionAsSelected(option);

            return this;
        }

        private static HtmlTag MakeRadioButton(string display, object value, string name)
        {
            DivTag divTag = new DivTag(display);
            HtmlTag radioButton = new RadioButtonTag(false).Attr("value", value).Attr("name",name);
            HtmlTag label = new HtmlTag("label").Text(display);
            label.Append(radioButton);
            divTag.Append(label);
            return divTag;
        }

        public void SelectByValue(object value) 
        {
            // this child child structure is based on the html structure of each radio button in the list
            // if this structure changes so must this child child business.
            var child = Children.FirstOrDefault(x => x.Children[0].Children[0].Attr("value").Equals(value));

            if (child != null)
            {
                MarkOptionAsSelected(child);
            }
        }

        private void MarkOptionAsSelected(HtmlTag radioButtonTag)
        {
            // this child child structure is based on the html structure of each radio button in the list
            // if this structure changes so must this child child business.
            var prevSelected = Children.FirstOrDefault(x => x.Children[0].Children[0].HasAttr(SelectedAttributeKey));

            if (prevSelected != null)
            {
                prevSelected.RemoveAttr(SelectedAttributeKey);
            }
            // this child child structure is based on the html structure of each radio button in the list
            // if this structure changes so must this child child business.
            radioButtonTag.Children[0].Children[0].Attr(SelectedAttributeKey, SelectedAttributeKey);
        }
    }
}