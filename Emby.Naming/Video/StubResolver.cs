using System;
using System.IO;
using System.Linq;
using Emby.Naming.Common;

namespace Emby.Naming.Video
{
    public static class StubResolver
    {
        public static StubResult ResolveFile(string path, NamingOptions options)
        {
            if (path == null)
            {
                return default(StubResult);
            }

            var extension = Path.GetExtension(path);

            if (!options.StubFileExtensions.Contains(extension, StringComparer.OrdinalIgnoreCase))
            {
                return default(StubResult);
            }

            var result = new StubResult()
            {
                IsStub = true
            };

            path = Path.GetFileNameWithoutExtension(path);
            var token = Path.GetExtension(path).TrimStart('.');

            foreach (var rule in options.StubTypes)
            {
                if (string.Equals(rule.Token, token, StringComparison.OrdinalIgnoreCase))
                {
                    result.StubType = rule.StubType;
                    break;
                }
            }

            return result;
        }
    }
}
