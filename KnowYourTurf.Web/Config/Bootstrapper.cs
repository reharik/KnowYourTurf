using System.Web.Mvc;
using KnowYourTurf.Core;
using FubuMVC.UI;
using FubuMVC.UI.Tags;
using KnowYourTurf.Core.Services;
using Microsoft.Practices.ServiceLocation;
using StructureMap;

namespace KnowYourTurf.Web.Config
{
    public class Bootstrapper
    {
        private static bool _hasStarted;

        public static void Restart(bool testing = false)
        {
            if (_hasStarted)
            {
                ObjectFactory.ResetDefaults();
            }
            else
            {
                Bootstrap();
                _hasStarted = true;
            }
        }

        public static void Bootstrap()
        {
            new Bootstrapper().Start();
        }

        public static void BootstrapTest()
        {
            new Bootstrapper().Start(true);
        }

        private void Start(bool testing = false)
        {
            if(testing)
                StructureMapBootstrapperTesting.Bootstrap();
            else
                StructureMapBootstrapper.Bootstrap();
            ServiceLocator.SetLocatorProvider(() => new StructureMapServiceLocator());
            //ControllerBuilder.Current.SetControllerFactory(new StructureMapControllerFactory(ObjectFactory.Container));
            DependencyResolver.SetResolver(new StructureMapDependencyResolver(ObjectFactory.Container));
            HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();

            var library = ObjectFactory.Container.GetInstance<TagProfileLibrary>();
            var conventions = ObjectFactory.Container.GetAllInstances<HtmlConventionRegistry>();
            conventions.Each(library.ImportRegistry);
        }
    }
}