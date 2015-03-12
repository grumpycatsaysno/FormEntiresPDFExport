using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data.Utilities.Exporters;
using Telerik.Sitefinity.Modules.GenericContent.Configuration;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Config;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config;

namespace PdfExporter
{
    public class Installer
    {
        /// <summary>
        /// Method that is called by ASP.NET before application start to register the custom Pdf exporter 
        /// </summary>
        public static void PreApplicationStart()
        {
            SystemManager.ApplicationStart += SystemManager_ApplicationStart;
        }

        private static void SystemManager_ApplicationStart(object sender, EventArgs e)
        {
            ObjectFactory.Container.RegisterType<IDataItemExporter, PdfExporter>(new InjectionConstructor());
        }
    }
}