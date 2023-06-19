using APILibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAQTests
{
    [TestFixture]
    public class OpenAQServiceTests
    {
        [Test]
        public async Task GetDataFromApi_ReturnsMockData()
        {
            //We can perform unit test cases for any scenario. This is only for demo purposes.
            IOpenAQProcessor apiService = new MockOpenAQProcessor();
            Dictionary<string, string> apiParams = new Dictionary<string, string>();
            apiParams.Add("limit", "10");
            apiParams.Add("page", "1");
            apiParams.Add("sort", "desc");
            apiParams.Add("radius", "1000");
            apiParams.Add("order_by", "lastUpdated");
            apiParams.Add("dumpRaw", "false");
            var data = await apiService.LoadAQResults(apiParams);
            Assert.AreEqual("Tolovana / Columbia", data.results[0].location);
        }
    }
}
