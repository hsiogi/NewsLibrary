using Newtonsoft.Json;
using System;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace NewsLibrary.IntegrationTests
{
    public class RssParserTests
    {
        private readonly ITestOutputHelper TestOutputHelper;
        public RssParserTests(ITestOutputHelper testOutputHelper)
        {
            TestOutputHelper = testOutputHelper;
        }

        [Fact]
        public async void GetItemsAsync_Sample_Success()
        {
            string source = "Yahoo©_¼¯ªÑ¥«";
            var result = await RssParser.GetItemsAsync("https://tw.stock.yahoo.com/rss?category=tw-market", source);
            

            Assert.NotEmpty(result);

            foreach (var item in result)
            {
                Assert.Equal(source, item.Source);
                Assert.True(item.PublishDate.ToUnixTimeSeconds() > 100000);
                Assert.NotEmpty(item.Link);
                Assert.NotEmpty(item.Description);
                Assert.NotEmpty(item.Title);
            }


        }
    }
}
