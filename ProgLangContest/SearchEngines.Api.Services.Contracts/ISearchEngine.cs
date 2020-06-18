
namespace SearchEngines.Api.Services.Contracts
{
    public interface ISearchEngine
    {
        string Name { get; }
        long Search(string searchTerm);
    }
}
