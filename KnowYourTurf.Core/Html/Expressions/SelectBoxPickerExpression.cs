using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.UI;
using FubuMVC.Core;
using FubuMVC.Core.Util;
using KnowYourTurf.Core.Localization;
using KnowYourTurf.Core.Services;

namespace KnowYourTurf.Core.Html.Expressions
{
    public class SelectBoxPickerExpression : HtmlCommonExpressionBase
    {
        private int _rows;
        private string _name;
        private string _entityName;
        private string _headerText;
        private string _requiredNumberMessage;
        private IEnumerable<SelectListItem> _availableItems;
        private IEnumerable<SelectListItem> _selectedItems;
        private string _elementId;

        public SelectBoxPickerExpression(SelectBoxPickerDto dto)
        {
            _entityName = dto.EntityTypeName;
            _name = _entityName;
            DefaultId = _entityName;
            _availableItems = dto.AvailableListItems;
            _selectedItems = dto.SelectedListItems;
            UnEncoded = true;
        }

        protected override string theMainTagIs()
        {
            return "div";
        }

        protected override void addMainTagAttributes()
        {
            DefaultId = _name.Replace(".","_");
            HtmlAttributes.Add("name", _name);
            HtmlAttributes.Add("class", "KYT_selectboxPicker selectboxPicker");
        }

        protected override void beforeMainTagInnerText(HtmlTextWriter html)
        {
            addHeader(html);
            addValidationDiv(html);
            html.AddAttribute("class", "KYT_selectboxPickerAvailable selectboxPickerAvailable");
            html.RenderBeginTag("div");
            addSubHeader(html, "Available ");
            addSelectBox(html,"available");
            html.RenderEndTag();
            html.AddAttribute("class", "KYT_selectboxPickerButtons selectboxPickerButtons");
            html.RenderBeginTag("div");
            html.AddAttribute("class", "KYT_selectboxPickerButtonsInner selectboxPickerButtonsInner");
            html.RenderBeginTag("div");

        }

        protected override string theMainTagInnerTextIs()
        {
            return "<div>" + new StyledButtonExpression("addItem").NonLocalizedText(">>").Class("KYT_selectboxAddItem selectboxAddItem") + "</div>" +
                   "<div>" + new StyledButtonExpression("removeItem").NonLocalizedText("<<").Class("KYT_selectboxRemoveItem selectboxRemoveItem") + "</div>";
        }

        protected override void beforeEndingMainTag(HtmlTextWriter html)
        {
            html.RenderEndTag();
            html.RenderEndTag();
            html.AddAttribute("class", "KYT_selectboxPickerSelected selectboxPickerSelected");
            
            html.RenderBeginTag("div");
            addSubHeader(html, "Selected ");
            addSelectBox(html, "selected");
            html.RenderEndTag();
           
        }

        private void addHeader(HtmlTextWriter html)
        {
            html.RenderBeginTag("span");
            html.RenderBeginTag("h3");
            html.Write(_headerText);
            html.RenderEndTag();
            html.RenderEndTag();
        }

        private void addSubHeader(HtmlTextWriter html, string direction)
        {
            html.RenderBeginTag("span");
            html.RenderBeginTag("h3");
            html.Write(direction + _entityName);
            html.RenderEndTag();
            html.RenderEndTag();
        }
    
        private void addValidationDiv(HtmlTextWriter html)
        {
            html.AddAttribute("class", "KYT_selectboxPicker_error_message selectboxPicker_error_message hide");
            html.RenderBeginTag("div");
            html.RenderEndTag();
        }
        
        private void addSelectBox(HtmlTextWriter html, string direction)
        {
            html.AddAttribute("multiple", "multiple");
            html.AddAttribute("size", _rows.ToString());
            html.AddAttribute("name", direction + _name);
            html.AddAttribute("id", direction + DefaultId);
            html.AddAttribute("class", "KYT_selectboxPickerBox selectboxPickerBox selectbox" + direction + " KYT_selectbox" + direction );
            html.RenderBeginTag("select");
            addOptionItems(html, direction);
            html.RenderEndTag();
        }

        private void addOptionItems(HtmlTextWriter html, string direction)
        {
            var items = direction == "selected" ? _selectedItems : _availableItems;
            items.ForEachItem(x =>
                           {
                               html.Write("<option value='" + x.Value + "'>" + x.Text + "</option>");
                           });
        }

        protected SelectBoxPickerExpression thisInstance()
        {
            return this;
        }

        public SelectBoxPickerExpression WithRows(int rows)
        {
            _rows = rows;
            return this;
        }

        public SelectBoxPickerExpression Header(StringToken text)
        {
            _headerText = text.ToString();
            return this;
        }
    }
}