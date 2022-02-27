using Learn.Adventure.Models.Entities;
using Learn.Adventure.Repository.Abstractions;
using Learn.Adventure.Services.Implementation;
using MongoDB.Bson;
using Moq;
using Xunit;

namespace Learn.Adventure.Test;

public class OptionServiceTests
{
    private readonly OptionService _sut;
    
    private readonly Mock<IRepository<Option>> _optionRepositoryMock = new Mock<IRepository<Option>>();

    public OptionServiceTests()
    {
        _sut = new OptionService(_optionRepositoryMock.Object);
    }

    [Fact]
    public void GetByAdventureIdAndId_ShouldReturnOption_WhenOptionExits()
    {
        var optionId = ObjectId.GenerateNewId();
        var adventureId = ObjectId.GenerateNewId();
        var parentId = ObjectId.Empty;
        var optionText = "Root Choice";

        var option = new Option()
        {
            Id = optionId,
            AdventureId = adventureId,
            ParentId = parentId,
            Text = optionText
        };

        _optionRepositoryMock
            .Setup(x => x.FindOne(o => o.Id == optionId && o.AdventureId == adventureId))
            .Returns(option);

        var optionResult = _sut.Get(adventureId.ToString(), optionId.ToString());
        
        Assert.Equal(optionId, ObjectId.Parse(optionResult.Id));
    }
}