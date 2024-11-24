using Magnus.Core.Entities;
using Magnus.Core.ValueObjects;

namespace Magnus.Core.Tests.Entities.Fixtures;
public class ClientTestsFixture : IDisposable
{
    public Client GenerateValidClient()
    {
        return new Client("Jhon Doe", new Document("05915598706") );
    }
    
    public void Dispose()
    {
        
    }
}