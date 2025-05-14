using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Reflection;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;

namespace HttpFlowFinder
{
    // Do not forget to update version number and author (company attribute) in AssemblyInfo.cs class
    // To generate Base64 string for Images below, you can use https://www.base64-image.de/
    [Export(typeof(IXrmToolBoxPlugin)),
        ExportMetadata("Name", "Http Flow Finder"),
        ExportMetadata("Description", "List power automate flows that are triggered by http requests"),
        // Please specify the base64 content of a 32x32 pixels image
        ExportMetadata("SmallImageBase64", "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"),
        // Please specify the base64 content of a 80x80 pixels image
        ExportMetadata("BigImageBase64", "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"),
        ExportMetadata("BackgroundColor", "Lightblue"),
        ExportMetadata("PrimaryFontColor", "Black"),
        ExportMetadata("SecondaryFontColor", "Gray")]
    public class HttpFlowFinder : PluginBase
    {
        public override IXrmToolBoxPluginControl GetControl()
        {
            return new HttpFlowFinderControl();
        }

        /// <summary>
        /// Constructor 
        /// </summary>
        public HttpFlowFinder()
        {
            // If you have external assemblies that you need to load, uncomment the following to 
            // hook into the event that will fire when an Assembly fails to resolve
            // AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(AssemblyResolveEventHandler);
        }

        /// <summary>
        /// Event fired by CLR when an assembly reference fails to load
        /// Assumes that related assemblies will be loaded from a subfolder named the same as the Plugin
        /// For example, a folder named Sample.XrmToolBox.MyPlugin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private Assembly AssemblyResolveEventHandler(object sender, ResolveEventArgs args)
        {
            Assembly loadAssembly = null;
            Assembly currAssembly = Assembly.GetExecutingAssembly();

            // base name of the assembly that failed to resolve
            var argName = args.Name.Substring(0, args.Name.IndexOf(","));

            // check to see if the failing assembly is one that we reference.
            List<AssemblyName> refAssemblies = currAssembly.GetReferencedAssemblies().ToList();
            var refAssembly = refAssemblies.Where(a => a.Name == argName).FirstOrDefault();

            // if the current unresolved assembly is referenced by our plugin, attempt to load
            if (refAssembly != null)
            {
                // load from the path to this plugin assembly, not host executable
                string dir = Path.GetDirectoryName(currAssembly.Location).ToLower();
                string folder = Path.GetFileNameWithoutExtension(currAssembly.Location);
                dir = Path.Combine(dir, folder);

                var assmbPath = Path.Combine(dir, $"{argName}.dll");

                if (File.Exists(assmbPath))
                {
                    loadAssembly = Assembly.LoadFrom(assmbPath);
                }
                else
                {
                    throw new FileNotFoundException($"Unable to locate dependency: {assmbPath}");
                }
            }

            return loadAssembly;
        }
    }
}