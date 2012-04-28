using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using FubuMVC.Core;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Services;
using FubuMVC.UI;
using FubuMVC.UI.Tags;
using Microsoft.Practices.ServiceLocation;
using NHibernate.Mapping;
using StructureMap;
using StructureMap.Configuration;
using log4net.Config;

namespace KnowYourTurf.Web.Config
{
    public class Bootstrapper
    {
        private static bool _hasStarted;

        public static void Restart()
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
            if (testing)
            {
                StructureMapBootstrapperTesting.Bootstrap();
            }
            else
            {
                StructureMapBootstrapper.Bootstrap();
                ModelBindingBootstaper.Bootstrap();
            }
            // sets SM as CSL
            ServiceLocator.SetLocatorProvider(() => new StructureMapServiceLocator());
            // sets MVCDependencyResolver to use the CSL
            DependencyResolver.SetResolver(new StructureMapServiceLocator());
            HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();

            XmlConfigurator.ConfigureAndWatch(new FileInfo(locateFileAsAbsolutePath("log4net.config")));

            var library = ObjectFactory.Container.GetInstance<TagProfileLibrary>();
            var conventions = ObjectFactory.Container.GetAllInstances<HtmlConventionRegistry>();
            conventions.Each(library.ImportRegistry);

            //SecurityBootstrapper.Bootstrap();
        }

        private static string locateFileAsAbsolutePath(string filename)
        {
            if (Path.IsPathRooted(filename))
                return filename;
            string applicationBase = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            string path = Path.Combine(applicationBase, filename);
            if (!File.Exists(path))
            {
                path = Path.Combine(Path.Combine(applicationBase, "bin"), filename);
                if (!File.Exists(path))
                    path = Path.Combine(Path.Combine(applicationBase, ".."), filename);
            }
            return path;
        }
    }
}