using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Castle.Components.Validator;
using FubuMVC.Core.Util;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Localization;

namespace KnowYourTurf.Core.Html.Expressions
{
    public abstract class BoundExpression<VIEWMODEL, THIS> : HtmlCommonExpressionBase
        where VIEWMODEL : class
    {
        protected readonly string _name;
        private readonly object _rawValue;
        protected string validators;
        protected int maxStringLength;
        protected Accessor expressionAccessor;
        protected string _title;
        protected string _label;
        protected string _valueFormat = "{0}";

        protected BoundExpression(string name, object rawValue)
        {
            _name = name;
            _rawValue = rawValue;
        }


        protected BoundExpression(VIEWMODEL model, Expression<Func<VIEWMODEL, object>> expression)
        {
            expressionAccessor = ReflectionHelper.GetAccessor(expression);
            if (model != null)
            {
                _rawValue = expressionAccessor.GetValue(model);
                var valueObject = expressionAccessor.GetLocalizedEnum(_rawValue as string);
                if (valueObject != null)
                {
                    _rawValue = LocalizationManager.GetText(valueObject);
                }
            }
            // _label = ReflectionHelper.GetProperty(expression).ToHeader();
            // HtmlAttributes.Add("label",_label);
            _name = expressionAccessor.Name;
        }

        protected object rawValue
        {
            get { return _rawValue; }
        }

        public string Name
        {
            get { return _name; }
        }

        protected virtual object getValue()
        {
            return rawValue;
        }

        protected virtual string getValueAsFormattedString()
        {
            var valueToFormat = rawValue;
            var valueObject = rawValue as Enumeration;
            if (valueObject != null) valueToFormat = LocalizationManager.GetText(valueObject);

            return valueToFormat == null ? null : string.Format(_valueFormat, valueToFormat);
        }
        protected abstract THIS thisInstance();

        public THIS Format(string format)
        {
            _valueFormat = format;

            return thisInstance();
        }

    }
}