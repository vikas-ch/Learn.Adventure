using Learn.Adventure.Models.DTO;
using Learn.Adventure.Models.Entities;
using Learn.Adventure.Repository.Abstractions;
using Learn.Adventure.Services.Implementation;
using MongoDB.Bson;
using Moq;
using Xunit;

namespace Learn.Adventure.Test;

public class AdventureServiceTests
{
    private readonly AdventureService _sut;

    private readonly Mock<IRepository<Models.Entities.Adventure>> _adventureRepositoryMock
        = new Mock<IRepository<Models.Entities.Adventure>>();

    private readonly Mock<IRepository<Option>> _optionRepositoryMock = new Mock<IRepository<Option>>();

    public AdventureServiceTests()
    {
        _sut = new AdventureService(_adventureRepositoryMock.Object, _optionRepositoryMock.Object);
    }
    
    [Fact]
    public void GetById_ShouldReturnAdventure_WhenAdventureExits()
    {
        var adventureId = ObjectId.GenerateNewId();
        var adventureName = "Adventure 1";
        var adventure = new Models.Entities.Adventure()
        {
            Id = adventureId,
            AdventureName = adventureName
        };
        
        _adventureRepositoryMock.Setup(x => x.FindById(adventureId.ToString()))
            .Returns(adventure);

        var adventureResult = _sut.Get(adventureId.ToString());
        
        Assert.Equal(adventureId, ObjectId.Parse(adventureResult.Id));

    }
    
    [Fact]
    public void GetById_ShouldReturnEmptyAdventureObject_WhenAdventureDoesNotExits()
    {
        var adventureId = ObjectId.GenerateNewId();
        var adventure = new Models.Entities.Adventure()
        {
            Id = adventureId,
            AdventureName = null
        };
        _adventureRepositoryMock.Setup(x => x.FindById(It.IsAny<ObjectId>().ToString()))
            .Returns((Models.Entities.Adventure) null);

        var adventureResult = _sut.Get(adventureId.ToString());
        
        Assert.Null(adventureResult.Name);

    }
}