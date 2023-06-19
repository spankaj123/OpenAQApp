using APILibrary.Models;

namespace APILibrary
{
    public interface IOpenAQProcessor
    {
        Task<AQResults> LoadAQResults(Dictionary<string, string> APIParams);
    }
}
