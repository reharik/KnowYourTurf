using System;
using System.Collections.Generic;
using FubuMVC.Core.Util;
using HtmlTags;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Localization;
using Rhino.Security.Interfaces;

namespace KnowYourTurf.Core.Html.Grid
{
    public interface IGridColumn
    {
        Accessor propertyAccessor { get; set; }
        IDictionary<string, string> Properties { get; set; }
        string Operation { get; set; }
        int ColumnIndex { get; set; }
        string BuildColumn(object item, User user, IAuthorizationService _authorizationService, string gridName = "");
    }

    public class ColumnBase<ENTITY> : IGridColumn, IEquatable<ColumnBase<ENTITY>> where ENTITY : IGridEnabledClass
    {
        protected string _toolTip;
        public ColumnBase()
        {
            Properties = new Dictionary<string, string>();
            Properties[GridColumnProperties.align.ToString()] = GridColumnAlign.Left.ToString();
        }

        public Accessor propertyAccessor { get; set; }
        public IDictionary<string, string> Properties { get; set; }
        public string Operation { get; set; }

        public int ColumnIndex { get; set; }

        public virtual string BuildColumn(object item, User user, IAuthorizationService _authorizationService, string gridName = "") 
        {
            return FormatValue((ENTITY)item, user, _authorizationService);
        }

        protected string FormatValue(ENTITY item, User user, IAuthorizationService _authorizationService)
        {
            bool isAllowed = !Operation.IsNotEmpty() || _authorizationService.IsAllowed(user, Operation);
            if (!isAllowed) return null;
            var propertyValue = propertyAccessor.GetValue(item);
            var value = propertyValue;
            if (propertyValue != null)
            {
                var instanceOfEnum = propertyAccessor.GetLocalizedEnum(propertyValue.ToString());
                value = instanceOfEnum != null ? instanceOfEnum.Key : propertyValue;
                if (value.GetType() == typeof (DateTime) || value.GetType() == typeof (DateTime?))
                {
                    value = propertyAccessor.Name.ToLowerInvariant().Contains("time")
                                ? ((DateTime) value).ToShortTimeString()
                                : ((DateTime) value).ToShortDateString();
                }
            }
            return value == null ? null : value.ToString();
        }

        public ColumnBase<ENTITY> HideHeader()
        {
            Properties.Remove(GridColumnProperties.header.ToString());
            return this;
        }

        public ColumnBase<ENTITY> DisplayHeader(StringToken header)
        {
            Properties[GridColumnProperties.header.ToString()] = header.ToString();
            return this;
        }

        public ColumnBase<ENTITY> Align(GridColumnAlign align)
        {
            Properties[GridColumnProperties.align.ToString()] = align.ToString().ToLowerInvariant();
            return this;            
        }

        public ColumnBase<ENTITY> ToolTip(StringToken toolTip)
        {
            _toolTip = toolTip.ToString();
            return this;
        }

        public ColumnBase<ENTITY> Width(int width)
        {
            Properties[GridColumnProperties.width.ToString()] = width.ToString();
            return this;            
        }

        public ColumnBase<ENTITY> IsSortable(bool isSortable)
        {
            Properties[GridColumnProperties.sortable.ToString()] = isSortable.ToString().ToLowerInvariant();
            return this;
        }

        public ColumnBase<ENTITY> SecurityOperation(string operation)
        {
            Operation = operation;
            return this;
        }

        #region IEquatable

        public bool Equals(ColumnBase<ENTITY> obj)
        {
            bool val = false;
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.Properties.ContainsKey(GridColumnProperties.name.ToString())
                && Properties.ContainsKey(GridColumnProperties.name.ToString())
                && Equals(obj.Properties[GridColumnProperties.name.ToString()], Properties[GridColumnProperties.name.ToString()]))
                val = true;
            if (obj.Properties.ContainsKey(GridColumnProperties.header.ToString())
                && Properties.ContainsKey(GridColumnProperties.header.ToString())
                && Equals(obj.Properties[GridColumnProperties.header.ToString()], Properties[GridColumnProperties.header.ToString()]))
                val = true;
            if (obj.Properties.ContainsKey(GridColumnProperties.sortable.ToString())
               && Properties.ContainsKey(GridColumnProperties.sortable.ToString())
               && Equals(obj.Properties[GridColumnProperties.sortable.ToString()], Properties[GridColumnProperties.sortable.ToString()]))
                val = true;
            if (obj.Properties.ContainsKey(GridColumnProperties.formatoptions.ToString())
               && Properties.ContainsKey(GridColumnProperties.formatoptions.ToString())
               && Equals(obj.Properties[GridColumnProperties.formatoptions.ToString()], Properties[GridColumnProperties.formatoptions.ToString()]))
                val = true;
            return val;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(ColumnBase<ENTITY>)) return false;
            return Equals((ColumnBase<ENTITY>)obj);
        }

        #endregion
    }

    public enum ColumnAction
    {
        Display,
        Edit,
        Redirect,
        Delete,
        Calculator,
        Other
    }
}