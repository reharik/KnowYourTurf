using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using FubuMVC.Core;
using KnowYourTurf.Core;
using KnowYourTurf.Web;
using StructureMap;
using log4net.Config;

namespace Generator
{
    static class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Initialize();
                //                var command = new EnterStringsCommand(ObjectFactory.GetInstance<ILocalizedStringLoader>(), ObjectFactory.GetInstance<IRepository>());
                //var command = new RebuildDatabaseCommand(ObjectFactory.GetInstance<ISessionSource>(), ObjectFactory.GetInstance<IRepository>(), ObjectFactory.GetInstance<ILocalizedStringLoader>(),ObjectFactory.GetInstance<PersistenceModel>());
                IGeneratorCommand command = null;

                var commands = ObjectFactory.GetAllInstances<IGeneratorCommand>();
                if (args.Length == 0) displayHelpAndExit(args, commands);
                command = commands.FirstOrDefault(c => c.toCanonicalCommandName() == args[0].toCanonicalCommandName());
                if (command == null) //displayHelpAndExit(args, commands);
                {
//                    command = ObjectFactory.Container.GetInstance<IGeneratorCommand>("defaultsecuritysetup");
                    command = ObjectFactory.Container.GetInstance<IGeneratorCommand>("rebuilddatabase");
                }
                command.Execute(args);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                if (Debugger.IsAttached)
                {
                    Console.ReadLine();
                }

                Environment.Exit(1);
            }
        }

        private static void displayHelpAndExit(string[] args, IEnumerable<IGeneratorCommand> commands)
        {
            if (args.Length > 0) Console.WriteLine("Unrecognized Command:" + args[0]);
            Console.WriteLine("Valid Commands are: ");

            var maxLength = commands.Max(c=>c.toCanonicalCommandName().Length);

            commands.Each(
                c =>
                Console.WriteLine("    {0, " + (maxLength + 1) + "} -> {1}", c.toCanonicalCommandName(), c.Description));

            Environment.Exit(-1);
        }

        private static void Initialize()
        {
           // Bootstrapper.Restart();
            HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();

            ObjectFactory.Initialize(x =>
                                         {
                                             x.AddRegistry(new GenRegistry());
                                             x.AddRegistry(new CommandRegistry());
                                         });
            XmlConfigurator.ConfigureAndWatch(new FileInfo(locateFileAsAbsolutePath("log4net.config")));
            //ObjectFactory.AssertConfigurationIsValid();


           // HibernatingRhinos.NHibernate.Profiler.Appender.NHibernateProfiler.Initialize();


        }

        public static string toCanonicalCommandName(this string commandText)
        {
            return commandText.Trim().ToLowerInvariant();
        }

        public static string toCanonicalCommandName(this IGeneratorCommand command)
        {
            return command.GetType().toCannonicalCommandName();
        }

        public static string toCannonicalCommandName(this Type commandType)
        {
            return commandType.Name.toCanonicalCommandName().Replace("command", "");
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
