using System.Collections.Generic;
using KnowYourTurf.Core.Html.Menu;

namespace KnowYourTurf.Web.Config
{
    public interface IMenuConfig
    {
        IList<MenuItem> Build(bool withoutPermissions = false);
    }
}