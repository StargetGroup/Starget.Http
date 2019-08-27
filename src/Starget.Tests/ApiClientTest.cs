using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Starget.Http.Client;

namespace Starget.Tests
{
    [TestClass]
    public class ApiClientTest
    {
        [TestMethod]
        public async Task TestGetByUrlAsync()
        {
            ApiClient client = new ApiClient("https://www.bing.com/");
            var response = await client.GetByUrlAsync(null);
            Assert.IsNotNull(response.Content);
        }

        [TestMethod]
        public async Task TestGetByRequestAsync()
        {
            ApiClient client = new ApiClient("https://www.bing.com/");
            DictRequest request = new DictRequest();
            var response = await client.GetAsync(request);
            Assert.IsNotNull(response.Content);
        }
    }
}
