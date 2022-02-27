using System;
using Learn.Adventure.Models.Entities;
using Learn.Adventure.Repository.Abstractions;
using Learn.Adventure.Repository.Implementation;
using Learn.Adventure.Services.Implementation;
using MongoDB.Bson;
using Moq;
using Xunit;

namespace Learn.Adventure.Test;

public class UserServiceTests
{
    private readonly UserService _sut;

    private readonly Mock<IRepository<User>> _userRepositoryMock = new Mock<IRepository<User>>();

    public UserServiceTests()
    {
        _sut = new UserService(_userRepositoryMock.Object);
    }

    [Fact]
    public void GetByUserId_ShouldReturnUser_WhenUserExists()
    {
        var userId = ObjectId.GenerateNewId();
        var firstName = "First";
        var lastName = "Last";
        var email = "first@last.com";

        var user = new User()
        {
            Id = userId,
            FirstName = firstName,
            LastName = lastName,
            Email = email
        };

        _userRepositoryMock.Setup(x => x.FindById(userId.ToString()))
            .Returns(user);

        var userResult = _sut.Get(userId.ToString());
        
        Assert.Equal(userId, ObjectId.Parse(userResult.Id));

    }
    
    
    [Fact]
    public void GetByUserId_ShouldReturnNull_WhenUserDoesNotExist()
    {
        var userId = ObjectId.GenerateNewId();
        

        var user = new User()
        {
            Id = userId
        };

        _userRepositoryMock.Setup(x => x.FindById(It.IsAny<ObjectId>().ToString()))
            .Returns((User) null);

        var userResult = _sut.Get(userId.ToString());
        
        Assert.Null(userResult.FirstName);

    }
}