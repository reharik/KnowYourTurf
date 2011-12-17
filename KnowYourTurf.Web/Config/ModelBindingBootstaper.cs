using System.Web.Mvc;

namespace KnowYourTurf.Web.Config
{
    public static class ModelBindingBootstaper
    {
        public static void Bootstrap()
        {
            ModelBinders.Binders.Add(typeof(double), new DoubleModelBinder());
            ModelBinders.Binders.Add(typeof(double?), new DoubleModelBinder());
        }
    }
}