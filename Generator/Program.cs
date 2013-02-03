using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;
using CC.Core;
using StructureMap;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Web;

namespace Generator
{

    static class Program
    {
        private static void Main(string[] args)
        {

            try
            {
                args = new[] {GetConnectionString()};
//                args = new[] {ConfigurationManager.AppSettings["KnowYourTurf.sql_server_connection_string"]};
                
                Initialize();
                var sessionFactoryConfiguration =
                    ObjectFactory.Container.With("connectionStr")
                                 .EqualTo(args[0])
                                 .GetInstance<ISessionFactoryConfiguration>();
                ObjectFactory.Container.Inject(sessionFactoryConfiguration);

//                ObjectFactory.Container.GetInstance<IGeneratorCommand>("securityupdate").Execute(new[] {""});

                var commands = GetDesiredCommands();
                commands.ForEachItem(x => x.Execute(args));

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

        private static string GetConnectionString()
        {
            var xdoc = XDocument.Load(@"connectionstrings.config");
//                var xdoc = XDocument.Load(@"..\..\..\..\connectionstrings.config");
            var connStrings = xdoc.Descendants("add").Where(x => x.Attribute("key").Value.StartsWith("constring_"));
            Console.WriteLine("Please select the database you would like to work with:");
            connStrings.ForEachItem(x => { Console.WriteLine(x.Attribute("key").Value.Replace("constring_", "")); });
            string connectionString = string.Empty;
            while (connectionString.IsEmpty())
            {
                var database = Console.ReadLine();
                var connStringNode = connStrings.FirstOrDefault(x => x.Attribute("key").Value == "constring_"+database);
                if (connStringNode != null)
                {
                    connectionString = connStringNode.Attribute("value").Value;
                }
                else
                {
                    Console.WriteLine("Please enter a valid database and remember it's case senstitive");
                }
            }
            return connectionString;
        }

        private static List<IGeneratorCommand> GetDesiredCommands()
        {
            IGeneratorCommand command;
            var commands = ObjectFactory.GetAllInstances<IGeneratorCommand>();

            Console.WriteLine("Please select the commands to execute.  Use a comma deliniated string:");

            commands.ForEachItem(x => Console.WriteLine(x.toCanonicalCommandName()));

            bool validEntry = false;
            var selectedCommands = new List<IGeneratorCommand>();

            while (!validEntry)
            {
                var commandNames = Console.ReadLine();
                var selectedCommandNames = commandNames.Split(',');
                validEntry = getCommandsFromList(commands, selectedCommandNames, selectedCommands);
            }
            return selectedCommands;
        }

        private static bool getCommandsFromList(IList<IGeneratorCommand> commandNames, string[] selectedCommandNames, List<IGeneratorCommand> selectedCommands)
        {
            foreach (var x in selectedCommandNames)
            {
                var command = commandNames.FirstOrDefault(c => c.toCanonicalCommandName() == x.Trim());
                if (command == null)
                {
                    Console.WriteLine(x + " is not a valid command name");
                    selectedCommands.Clear();
                    return false;
                }
                selectedCommands.Add(command);
            }
            return true;
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


    }
}
