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
        ExportMetadata("Description", "Lists all Power Automate flows triggered by HTTP requests and display their HTTP endpoint URLs"),
        // Please specify the base64 content of a 32x32 pixels image
        ExportMetadata("SmallImageBase64", "iVBORw0KGgoAAAANSUhEUgAAACAAAAAiCAYAAAA+stv/AAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAARCSURBVFhHxZdfaFtVHMe/SZomIU2XmNKtWW3XIWuxwpy+GChq65h/GL4VRGWgD3sRQZCioIgjZmhRLCj4YOeDAx+KSJEioj6YVuzqhG3tNJttZ2fTVZOsWbs09+beJMffOTlp063NTZSrHzgkv3PPzfnd3/f3+50bCyPwP2KVn4JQKIREIiEtYHp6GsPDw9Iyhy0OjI+PIx6PSwtIJpOYmJiQljlscSCfz4tRjqIo8ps5bMmBvr4+IYHL5YLdbofFYkEgEMDIyIhcYQLcgRK9vb0sEokwCr0Yo6OjrL+/X141hy0ScLxeL/x+vxgej0fOmsdtDtwKl8FUZCQEVHYsnU5Li7FUKsWi0ai0zKHmRqQWGCZSOfyaLiCTZ9hdb8ED3jrc3WCTK2rDUIISCm323oKK9xeyaLQVZfn8Tw06uR9N5/H6rIJvkrqYr4WqIhBTC2LjgQ4H9jisOHODRyCP5SyjHAGOBerR6rTiq4SOizQ/0OFE1ZnDHajEml5gL0czTMtv2q9ezojvoTmFqTQ/cKloc86v5di7vyvSMsYwAuF5FQX65FrxyE/fzOPJ+M+4g2Xx2bKGp1vq8b17P1Z9AQQoOpzVHMMzFJWDHuO8qOjAdRL49JKGl/Y5hD2bKeAL0n3ooU4cfewILmQsONrZikMP9uHHwP0IH3AJRylPcWJewYm7XOK+inAHduLTpSy7QqleIjyvsEyuwNra2kS5vkUSlPg2qbEIeVziJK0ltQypWAWLSgF3UnJRRMXQ6NHsVgs5DUxNTWHx3BlMTk5ibT2DIJXiD5ScpbX7XFYs0P1GVJTgtd8U+Oyb+fzd9RwO++sQ7jmAIz1BXFYteLhjD5qeehHO3Xs3rnOi63m80ObEfY0GeSDisANv3xLGN2eLIS9JEC6TYIXCM7SgSouxU4squ8ZLxICKEhwi78+u5qQF7KqzIK5tBqw8dF/GdTzeVHx6Du2PFlkVlai4otdvx9fUXHg18C6X1At4bmYdBw8/gaHFHCIrOZy8oiJEpfrJUlZUCeUiVmi9u9rOLCOxI6epEo5fXGdnb+SYki+wj/5Q2S83c+Iab0ScD6+qbIbmzlED4Ncf+WmNpUiSajCM0bPUUFocFjRSdJ1UAcdbHTgV05ClBOfpOUONif/IPXQY3UuS8ZR+g+rfW5a8lajqLOCNhR9EXFPe+ZZp949jWTqginnwTqcLCQr9B1dVPNpkR49vMxeMqMqBEhfoaUf/0sEfbp52H1nW8cp+p3BwF00+v7ceHkrUWqjJgRL8hmt0QvI+00zvA401blrOP3KgGnRdR3d398YrXXNzMwYHBxEMBoVdwjQHNE1De3s7YrGYnKGat1pve8c07hT/EpvNtjG2e8E1NQJutxtdXV3C5hKMjY2JPz3lmOoAnRmYm5uTMxAO/acS8M0aGho2xnYSmJ4DRpgmAYf+2MDn80lrO4C/AXLRTXRNt+xcAAAAAElFTkSuQmCC"),
        ExportMetadata("BigImageBase64", "iVBORw0KGgoAAAANSUhEUgAAAEAAAABFCAYAAAD6pOBtAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAlzSURBVHhe7Zt7UFTXHcd/C8uyu2FVlBIePiA8AqSpj0ajEvDRNjGWWGPHTKl12tpE004bp/aPtnQ6VDttadKZZCbGpDPNZNpmkgFJTXXaFOojNb5QwAKWaFVAQF4S9v1mOf39zh50H3dRdy+md+Az3rlzz5693PM9v/N7nLuqGAJTmDhxnrJEFKCtrQ2qq6uhr69PtITT0tICNTU10N/fL1qUR0QBDhw4ALt27YLOzk7REs7+/fuhoqICOjo6RIvymHAJ2O12MBqN4koa6uNyucSV8phQALPZDCaTSVxJMzg4CCMjI+JKeUw7QXGeskTMA/bs2QOVlZVQXl4OixcvFq3B1NXVwdGjR3m02Lx5s2hVGCSAFLt37yZhJA+tVsvS09OZwWBgKpWKYSgU31Iet10CFAoPHToUdNDMnzx5EjZs2CB6KZfbCkDmX1ZWFnSUlpZCdnY2ZGVliV7KZToKiPOUZVoAcQ4DvfvNIxK3+1wJRBQgJycH1q1bBxkZGaIlnLy8PN4nLS1NtCiP6Q0RcZ6y3DMLuOoYgxPGUWi3+aDPPQbWUQa6eBWkalRQmBQPy2eq4XOGeIi7xy5lUgUw4yDf7HXDW70ecI8xWDU7gQ+y0+mDl7vcfLAVD2i5GB+hOMMeBl/P0MD35ifCPO29Mc5JEcCHd9zX7YaqDheUpSbAd3FAi3DgRD/O/oozVnD5AJ5IUUOXawyOLDWAGsW45hzjgr2JgpWna6AyVwsG+mAyIQHkpNfpYyVnLOypJivrcPhEqx/vGGOrGyzsdx1OVornBpOXfQX77b7sFD38GD1j7IV2O8v9l4mdxT6TiawCtFpGWdaHJrb3movhWMP42X8d7OlmK/+sFEWiwd3AtbEAv3PGGD7QelwTc4+Z2F8HPaJFfmQT4JJtlM3Dhz00JP2wxz7xshyc0RGcXYKsZHx2P7jhYYUfmZljNFy2drwvifr3CPeNFVkEsKBt0wCq+92iJRgTfv4ADv74yK1ZDhSAeK7Nzn580SGugmlBy8pEcS+iGHIji6v94UUn4OShI1NBDzq1UK/6k0tO2HS/BkqS1aLFv7MSyIsP6uDdfg9cwDA5Dk46HBzywtt9HiBXuLXVDhgwZCXmKHDWPArfaLHD8+jpG0w+aMRr1AAWzojnIS8lQQW/7XTBkWwr6H1u8S2Ar/3bBhU5Ot6HnsARp4a9zlRosoxCnj4eGvHswfs8OjMeilG4kuQE+OVVJzz5mQR4fl6iuIsMcDuIgfWNVvZOX7DpD+M6PzzsZS93uth99Ua2/LSF6R9eTkJHPPRZ+eyZ8zaWh0uFlkI3RpNQj/AxLgFymO7g4BITMVkAZXdfPGeFy6UzeRwPpW7YC5VXXHB6uQFKS0r4Nhptsmq1WvjbDS88OksNj+RlQ0FBAX8Ft3PnTnh/0AuvdqPFYG4gxcZmG0+WnknTiJYY4TJEya+vOtlPL0k7LuLxc1b23oDfexcXF/OZHhoa4tePoRM8Zwp3ahQIKFq0Y3ooBd1vE4ZSuYjJCf4TZ3g9rkkpetERXLD64CnMBO8GLA9gC2aB5BCl+NIcNU+bKduUg6gFQP8ErTjAz6Ozk+IfKA45LPSBklBzpL2UL6NodcOj4ioYSo0zE+PgsuNWtIiFqAXADA5m4MNQRScFRYTiZGlxbiE9jQsNarhk9/EoIEXeffHQgf5HDqJ2ghfxAdeetcE3M6WdUc2AB5ZhiZul82v8dvkXoLf5NOh0Or6N5kIb1uBHccIMHv/V61Cw/tbbpdewmPo23lsvIfAH6EC/vyARnp0beziM2gJINrREbo5SBz12ZqLfXOmgwRK01Zafnw+6+bkwPzcf1qxZA9u2bYOc1Dk3+9JBus3G9RPYNn7MxHbMu+SBu8Io6MRK7yFMfyNB1WBg/h4aBSgVbjJLe3qK/6lHjDfrhlC2tthY7YB02n23RG0B6Ti7VNtHmolCXKdtAWltKBNN4ADel9LqWRE8aDdGGLk2TKK+S2KcCjLQHK9E8MarZqvh8CfSnpyYSIAj+L1S/L7U8Cn8fYzCFiXdzsHeGTHJuHqCQa7FeN2K+Tzt/90t72AO8NX7pfOHZrxnti4ekiJEn7slJgE2YoVXjd5eCi1aSDmmrG+gNw8ElzXfK/SiLpjuhyU0FP7aML/YkCodXar7vfB0BHGiIaZagNZ/wQkz1C5K4lXdOFi+8ySJkqG919ywAnP+Ez/YBI72RpjxViNok+fACCqgR5HojydhPkFrOk8fB13OMViAIeAlLI/TcIkFYkO1HjxugcaVBvRB8viAmMvhP/S6oXbAy1PeBtMonDP7+OA+i4KQKN04IPwH+4r0OCAV9x1kvCvPWGEvti3EPlgS8A3RWrSmP173wFIsgZstPl5gkXhUDj+GBxVK13FJ/f4hvf+PywEJEAu00bnohJmtarDwsviKPbiMNWKH+cdMYSFvBZbIgW10n6WnLKwmYFepB9cIXdMGKZXJsw4bWT9mUHISswBEMw6E9gO7QnaBx6EKjkQKfPZQAaqwsqQdYqnIT3uFj5wysz9dlyf2ByLLQlqMBdGePB2UNdnghid8RW1Cp/UwmvrPLztFi59xP34ezZ1S3zfQtEN9O9UD5S12/uZoKzpVuZHHkyDfwrz9O3M1sPqsVbJQofX+/qCHb5IEQo5tS6sd9uHgQ50eOdON52286HqlUMZ1H4iwBNn4M5op7eW/i/4g1JxPG738M1rbK3EJnMclsAXT2h9J7Aafwr4Fx818w0XeVR9MzFFAiv9gpvbcBQfmAgC/yNVBSUBW90qXC2rRm1MOsGSGmleVR5cZbu4bUJb3mw4XnMKI8hpazRMp8sV8KSZFAIJuuh/D2os4GNolpsxu7Rz/y9FnL9jhLygCmfyHy5J4YkRvjnmYw84vZGlhxzwNT6Ymm0kTIJAWTIpof58GSTNM697s9e8H0PrOx8KJ4j1tr9G7A5my3DvinggQCk44psIMEnCGKdn5NPlUBPh/QpECOBwOyMzMFFfS7NixA6qqqsRVZBQpAP0vlaSkJL6/SL9UC4R+vpueng5LliyB7du3i9bIKFoA+h1zc3OzaI0O2TJBpTItgDhPWRTtA/R6PRQVFYnWYOrr6yE5OVlcRUbRAtAbJo0muEQuLCzk0eHgwYOQkpIiWiOj+CjQ1NQkWoO501+xK94HjP9kP/S4U6ajgDhPWaYFEOcpiyKjAD1yT08PD4Gx/ncdRQogHwD/A46/SiZFVa6tAAAAAElFTkSuQmCC"),
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