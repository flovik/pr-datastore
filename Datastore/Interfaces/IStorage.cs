namespace Datastore.Interfaces;

public interface IStorage
{
    Guid Post(Dictionary<string, string> data);
    IDictionary<Guid, string> Get();
    string Get(Guid id);
    string Put(Dictionary<string, string> data, Guid id);
    void Delete(Guid id);
}