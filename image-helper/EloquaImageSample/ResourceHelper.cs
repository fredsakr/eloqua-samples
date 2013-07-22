using System.IO;
using System.Reflection;

namespace EloquaImageSample
{
    internal static class ResourceHelper
    {
        public static byte[] GetEmbeddedResource(string fileName)
        {
            var assembly = Assembly.GetExecutingAssembly();

            using (var stream = assembly.GetManifestResourceStream(fileName))
            {
                using (var memoryStream = new MemoryStream())
                {
                    if (stream != null) stream.CopyTo(memoryStream);
                    return memoryStream.ToArray();
                }
            }
        }
    }
}
