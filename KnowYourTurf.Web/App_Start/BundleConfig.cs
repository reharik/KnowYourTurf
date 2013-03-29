namespace KnowYourTurf.Web.App_Start
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Web.Optimization;

    public class BundleConfig
    {
        public class AsIsBundleOrderer : IBundleOrderer
        {
            public virtual IEnumerable<FileInfo> OrderFiles(BundleContext context, IEnumerable<FileInfo> files)
            {
                if (context == null)
                    throw new ArgumentNullException("context");
                if (files == null)
                    throw new ArgumentNullException("files");
                return files;
            }
        }
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            var scriptBundle = new ScriptBundle("~/content/scripts/files");
            scriptBundle.Orderer = new AsIsBundleOrderer();
            scriptBundle.Include("~/content/scripts/jqueryPlugins/jquery-1.9.0.js");
            scriptBundle.Include("~/content/scripts/jqueryPlugins/jquery-migrate-1.0.0.js");
            scriptBundle.Include("~/content/scripts/jqueryPlugins/jquery-ui-1.10.0.custom.js");
            scriptBundle.Include("~/content/scripts/jqueryPlugins/cc.tokeninput2.js");
            scriptBundle.Include("~/content/scripts/jqueryPlugins/grid.locale-en.js");
            scriptBundle.Include("~/content/scripts/jqueryPlugins/jquery.jqGrid.src.js");
            scriptBundle.Include("~/content/scripts/jqueryPlugins/jquery-tmpl.js");
            scriptBundle.Include("~/content/scripts/jqueryPlugins/jquery.miniColors.js");
            scriptBundle.Include("~/content/scripts/jqueryPlugins/jquery.fileinput.js");
            scriptBundle.Include("~/content/scripts/jqueryPlugins/jquery.easing.1.3.js");
            scriptBundle.Include("~/content/scripts/jqueryPlugins/jquery.galleryview-3.0-dev.js");
            scriptBundle.Include("~/content/scripts/jqueryPlugins/jquery.ui.timepicker.js");

            scriptBundle.Include("~/content/scripts/jqueryPlugins/noty/jquery.noty.js");
            scriptBundle.Include("~/content/scripts/jqueryPlugins/noty/inline.js");
            scriptBundle.Include("~/content/scripts/jqueryPlugins/noty/default.js");

            scriptBundle.Include("~/content/scripts/externalHelpers/fullcalendar.js");
            scriptBundle.Include("~/content/scripts/externalHelpers/underscore.js");
            scriptBundle.Include("~/content/scripts/externalHelpers/backbone.js");
            scriptBundle.Include("~/content/scripts/externalHelpers/backbone.marionette.js");
            scriptBundle.Include("~/content/scripts/externalHelpers/knockout-2.1.0.debug.js");
            scriptBundle.Include("~/content/scripts/externalHelpers/knockout.mapping-lastest.debug.js");

            scriptBundle.Include("~/content/scripts/KYT/KYT.App.js");
            scriptBundle.Include("~/content/scripts/KYT/KYT.Controller.js");
            scriptBundle.Include("~/content/scripts/KYT/KYT.Mixins.js");
            scriptBundle.Include("~/content/scripts/KYT/KYT.Header.js");
            scriptBundle.Include("~/content/scripts/KYT/KYT.Views.js");
//          scriptBundle.Include(d~/content/scripts/("KYT/KYT.OneOffViews.js");
            scriptBundle.Include("~/content/scripts/KYT/KYT.Routing.js");
            scriptBundle.Include("~/content/scripts/KYT/KYT.State.js");
//          scriptBundle.Include(d~/content/scripts/("KYT/KYT.Services.js");
            scriptBundle.Include("~/content/scripts/KYT/KYT.WorkflowManager.js");
            scriptBundle.Include("~/content/scripts/KYT/Fields/KYT.FieldsApp.js");
            scriptBundle.Include("~/content/scripts/KYT/Fields/KYT.FieldsApp.Menu.js");
            scriptBundle.Include("~/content/scripts/KYT/Fields/KYT.FieldsApp.Routing.js");
            scriptBundle.Include("~/content/scripts/KYT/Fields/KYT.FieldsApp.Views.js");
            scriptBundle.Include("~/content/scripts/KYT/Fields/KYT.Services.js");

            scriptBundle.Include("~/content/scripts/KYT/Reporting/KYT.ReportingApp.Routing.js");
            scriptBundle.Include("~/content/scripts/KYT/Reporting/KYT.ReportingApp.Views.js");

            scriptBundle.Include("~/content/scripts/KYT/CC.ModelBinding/CC.CustomBindings.js");
            scriptBundle.Include("~/content/scripts/KYT/CC.ModelBinding/CC.Elements.js");
            scriptBundle.Include("~/content/scripts/KYT/CC.ModelBinding/CC.NotificationService.js");
            scriptBundle.Include("~/content/scripts/KYT/CC.ModelBinding/CC.ModelService.js");
            scriptBundle.Include("~/content/scripts/KYT/CC.ModelBinding/CC.ValidationRules.js");
            scriptBundle.Include("~/content/scripts/KYT/CC.ModelBinding/CC.ValidationRunner.js");
            scriptBundle.Include("~/content/scripts/KYT/CC.ModelBinding/CC.AjaxFileUpload.js");

            scriptBundle.Include("~/content/scripts/externalHelpers/select2.js");
            scriptBundle.Include("~/content/scripts/externalHelpers/superfish.js");
            scriptBundle.Include("~/content/scripts/externalHelpers/ba-debug.js");
            scriptBundle.Include("~/content/scripts/internalHelpers/cc.ccMenu.js");
            scriptBundle.Include("~/content/scripts/internalHelpers/jquery.cc.grid.js");
            scriptBundle.Include("~/content/scripts/internalHelpers/cc.grid.columnService.js");
            scriptBundle.Include("~/content/scripts/internalHelpers/cc.gridHelpers.js");
            scriptBundle.Include("~/content/scripts/internalHelpers/kyt.gridSearch.js");
            scriptBundle.Include("~/content/scripts/internalHelpers/kyt.repository.js");
            scriptBundle.Include("~/content/scripts/internalHelpers/jquery.kyt.calendar.js");

            scriptBundle.Include("~/content/scripts/externalHelpers/json2.js");
            scriptBundle.Include("~/content/scripts/externalHelpers/xdate.js");
            

            var styleBundle = new StyleBundle("~/content/css/files");
            styleBundle.Include("~/content/css/bootstrap.css");
            styleBundle.Include("~/content/css/jquery.miniColors.css");
            styleBundle.Include("~/content/css/fullcalendar.css");
            styleBundle.Include("~/content/css/token-input.css");
            styleBundle.Include("~/content/css/smoothness/jquery-ui-1.10.0.custom.css");
            styleBundle.Include("~/content/css/fileinput.css");
            styleBundle.Include("~/content/css/fileinputenhanced.css");
            styleBundle.Include("~/content/css/ui.jqgrid.css");
            styleBundle.Include("~/content/css/fg.menu.css");
            styleBundle.Include("~/content/css/select2.css");
            styleBundle.Include("~/content/css/jquery.galleryview-3.0-dev.css");
            styleBundle.Include("~/content/css/main.css");
            styleBundle.Include("~/content/css/KYT.css");
            styleBundle.Include("~/content/css/CC.css");
            bundles.Add(scriptBundle);
            bundles.Add(styleBundle);
        }
    }
}