using System.Collections.Generic;
using System.Linq;

namespace WebScraper.Enum
{
    public static class WebScraperEnum
    {
        public enum WebSites
        {
            NuBank,
        };

        public static IEnumerable<T> GetValues<T>()
        {
            return System.Enum.GetValues(typeof(T)).Cast<T>();
        }

        public static List<string> GetWebSites()
        {
            return System.Enum.GetNames(typeof(WebSites)).ToList();
        }
    }
}
