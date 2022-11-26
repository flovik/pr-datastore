using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using Datastore.Exceptions;
using Datastore.Interfaces;
using Newtonsoft.Json;

namespace Datastore.Datastorage;

public sealed class Storage : IStorage
{
    private readonly IDictionary<Guid, string> _cachedStorage = new ConcurrentDictionary<Guid, string>();

    public Guid Post(Dictionary<string, string> data)
    {
        var json = JsonConvert.SerializeObject(data, Formatting.Indented);
        var guid = Guid.NewGuid();
        if (!_cachedStorage.TryAdd(guid, json))
            throw new BadRequest("Couldn't add data to storage");

        return guid;
    }

    public IDictionary<Guid, string> Get()
    {
        if (_cachedStorage.Count == 0) throw new BadRequest("Storage is empty");
        return _cachedStorage;
    }

    public string Get(Guid id)
    {
        if (!_cachedStorage.TryGetValue(id, out var data))
            throw new NotFound("Provided ID is not present in the datastore");

        return data;
    }

    public string Put(Dictionary<string, string> data, Guid id)
    {
        var json = JsonConvert.SerializeObject(data, Formatting.Indented);
        if (!_cachedStorage.TryGetValue(id, out _))
            throw new NotFound("Provided ID is not present in the datastore");

        _cachedStorage[id] = json;
        return _cachedStorage[id];
    }

    public void Delete(Guid id)
    {
        if (!_cachedStorage.Remove(id))
            throw new BadRequest("Couldn't remove specified data");
    }
}