using Magnus.Core.Tests.Entities.Fixtures;

namespace Magnus.Core.Tests.Entities;

[Collection(nameof(ClientCollection))]
public class ClientTest(ClientTestsFixture fixture)
{
    [Fact(DisplayName = "Client SetName Successfully")]
    [Trait("Entity", "Client")]
    public void When_SetName_Then_NamePropertyIsSet()
    {
        //Arrange
        var client = fixture.GenerateValidClient();
        //Act
        client.SetName("Jhon Wick");    
        //Assert
        Assert.Equal("Jhon Wick", client.Name);
    }
}