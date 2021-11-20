using Microsoft.SyndicationFeed;
using Microsoft.SyndicationFeed.Rss;
using NewsLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace NewsLibrary
{
    public static class RssParser
    {
        public static async Task<List<NewsItem>> GetItemsAsync(string inputUri, string source)
        {
            using (var xmlReader = XmlReader.Create(inputUri, new XmlReaderSettings() { Async = true }))
            {
                var feedReader = new RssFeedReader(xmlReader);

                List<NewsItem> result = new List<NewsItem>();

                while (await feedReader.Read())
                {
                    if (feedReader.ElementType == SyndicationElementType.Item)
                    {
                        ISyndicationItem item = await feedReader.ReadItem();
                        result.Add(new NewsItem()
                        {
                            Description = item.Description,
                            PublishDate = item.Published,
                            Link = item.Links.FirstOrDefault().Uri.ToString(),
                            Title = item.Title,                            
                            Source = source
                        }); ;
                    }
                }
                return result;
            }

        }
    }
}
