using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace TrueDotBin.CosmosUtils.Resources
{
    /// <summary>
    /// A utility for loading embedded resources.
    /// </summary>
    public static class EmbeddedResourceLoader
    {
        /// <summary>
        /// Tries to load an embedded resource with the given assembly.
        /// </summary>
        /// <param name="path">The resource's path in the assembly, e.g., "AssemblyName.Folder.Resource.txt"</param>
        /// <param name="assembly">The assembly to load that resource from.</param>
        /// <param name="bytes">The output bytes of the resource.</param>
        /// <returns>Whether the resource was loaded successfully.</returns>
        public static bool TryLoadResource(string path, Assembly assembly, out byte[] bytes)
        {
            try
            {
                using var stream = assembly.GetManifestResourceStream(path);
                using var ms = new MemoryStream();

                stream?.CopyTo(ms);

                bytes = ms.ToArray();
                return true;
            }
            catch
            {
                bytes = [];
                return false;
            }
        }
    }
}
