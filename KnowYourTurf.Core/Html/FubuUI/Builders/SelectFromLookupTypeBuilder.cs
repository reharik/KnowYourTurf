using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Services;
using FubuMVC.UI.Configuration;
using HtmlTags;
using StructureMap;
using System.Linq;

namespace KnowYourTurf.Core.Html.FubuUI.Builders
{
    public class SelectFromILookupTypeBuilder : ElementBuilder
    {
        protected override bool matches(AccessorDef def)
        {
            return def.Accessor.PropertyType.GetInterface(typeof(ILookupType).Name)!=null;
        }

        public override HtmlTag Build(ElementRequest request)
        {
            Action<SelectTag> action = x =>
            {
                var value = request.RawValue;
                var valueType = request.Accessor.PropertyType;
                var repository = ObjectFactory.Container.GetInstance<IRepository>();
                var getIdsFromPrincipal = ObjectFactory.Container.GetInstance<ISessionContext>();
                repository.DisableFilter("CompanyConditionFilter");
                var findAll = typeof(Repository).GetMethod("FindAll");
                var genericFindAll = findAll.MakeGenericMethod(new []{valueType});
                var lookupTypes = (IEnumerable<ILookupType>)genericFindAll.Invoke(repository, new object[] { });
                
                var selectListItemService = ObjectFactory.Container.GetInstance<ISelectListItemService>();
                var selectListItems = selectListItemService.CreateLookupList(lookupTypes, l => l.Name, l => l.EntityId, true);
                repository.EnableFilter("CompanyConditionFilter", "CompanyId", getIdsFromPrincipal.GetCompanyId());
                //if (selectListItems == null) return;

                selectListItems.Each(option => x.Option(option.Text, option.Value.IsNotEmpty() ? option.Value : ""));

                if (value != null && value.ToString().IsNotEmpty())
                {
                    var lookupType = value as ILookupType;
                    x.SelectByValue(lookupType.EntityId.ToString());
                }
                x.AddClass("kyt_fixedWidthDropdown");
                x.AddClass("fixedWidthDropdown");
               

            };
            return new SelectTag(action);
        }
    }
}