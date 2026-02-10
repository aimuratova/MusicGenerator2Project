using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MusicGenerator.BLL.Localization
{
    public static class LocaleLoader
    {
        public static JsonElement Load(string locale)
        {            
            var path = Path.Combine(
                AppContext.BaseDirectory,
                "Localization",
                "Locales",
                $"{locale}.json"
            );

            var json = File.ReadAllText(path);
            return JsonDocument.Parse(json).RootElement;
        }
    }
}
