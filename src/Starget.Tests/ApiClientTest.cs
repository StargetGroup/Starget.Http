using System;
using System.Diagnostics;
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
            Stopwatch watch = new Stopwatch();

            ApiClient client = new ApiClient("https://www.bing.com/");
            DictRequest request = new DictRequest();
            watch.Start();
            var response = await client.GetAsync(request);
            watch.Stop();
            //Debug.WriteLine(watch.ElapsedMilliseconds);
            Assert.IsNotNull(response.Content);
        }

        [TestMethod]
        public async Task TestGetByRequest2Async()
        {
            Stopwatch watch = new Stopwatch();
            ApiClient client = new ApiClient("https://www.bing.com/dict");
            DictRequest request = new DictRequest();
            request.ApiUrl = null;
            var response = await client.GetAsync(request);
            Assert.IsNotNull(response.Content);
            watch.Start();
            request = new DictRequest();
            request.ApiUrl = null;
            await client.GetAsync(request);
            watch.Stop();
            Debug.WriteLine(watch.ElapsedMilliseconds);
        }
    }
}
