using APILibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APILibrary
{
    public class MockOpenAQProcessor : IOpenAQProcessor
    {
        public async Task<AQResults> LoadAQResults(Dictionary<string, string> APIParams)
        {
            // Simulate API response
            AQResults aqResults = new AQResults();
            aqResults.results = new Result[1];
            Result aqResult = new Result();
            aqResult.location = "Tolovana / Columbia";
            aqResults.results[0] = aqResult;
            return aqResults;
        }
    }
}
