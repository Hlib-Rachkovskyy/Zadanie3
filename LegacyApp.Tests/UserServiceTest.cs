using System;
using JetBrains.Annotations;
using LegacyApp;
using Xunit;

namespace LegacyApp.Tests;

[TestSubject(typeof(UserService))]
public class UserServiceTest
{

    [Fact]
    public void AddUser_Should_Return_False_When_FirstName_Is_Missing()
    {
        // Arrange
        var userService = new UserService();
        // Act
        var addResult = userService.AddUser("", "Doe", "johndoe@gmail.com", DateTime.Parse("1982-03-21"), 1);
        // Assert
        Assert.False(addResult);
    }
    
    [Fact]
    public void AddUser_Should_Return_False_When_LastName_Is_Missing()
    {
        // Arrange
        var userService = new UserService();
        // Act
        var addResult = userService.AddUser("John", "", "johndoe@gmail.com", DateTime.Parse("1982-03-21"), 1);
        // Assert
        Assert.False(addResult);
    }


    [Fact]
    public void AddUser_Should_Return_False_When_Email_Is_Not_Correct()
    {
        // Arrange
        var userService = new UserService();
        // Act
        var addResult = userService.AddUser("John", "Doe", "johndoegmailcom", DateTime.Parse("1982-03-21"), 1);
        // Assert
        Assert.False(addResult);
    }

    [Fact]
    public void AddUser_Should_Return_False_When_Age_Is_Lower_Than_21()
    {
        var userService = new UserService();
        // Act
        var addResult = userService.AddUser("John", "Doe", "johndoe@gmail.com", DateTime.Parse("2016-03-21"), 1);
        // Assert
        Assert.False(addResult);
    }
    

    [Fact]
    public void AddUser_Should_Return_False_When_HasLimit_Is_True_And_CreditLimit_Lower_Than_500()
    {
        var userService = new UserService();
        // Act
        var addResult = userService.AddUser("John", "Kowalski", "kowalski@wp.pl", DateTime.Parse("1982-03-21"), 1);
        // Assert
        Assert.False(addResult);
    }
    
    [Fact]
    public void AddUser_Should_Throw_ArgumentException_When_Client_Not_In_UserCreditService_Database()
    {
        var userService = new UserService();
        // Act
        Assert.Throws<ArgumentException>(() =>
        {
            userService.AddUser("John", "Andrzejewicz", "kowalski@wp.pl", DateTime.Parse("1982-03-21"), 3);
        });
        // Assert
        
    }
    [Fact]
    public void AddUser_Should_Throw_ArgumentException_When_Client_Not_In_ClientRepository_Database()
    {
        var userService = new UserService();
        // Act
        Assert.Throws<ArgumentException>(() =>
        {
            userService.AddUser("John", "Doe", "kowalski@wp.pl", DateTime.Parse("1982-03-21"), 7);
        });
    }
    
    [Fact]
    public void AddUser_Should_Return_False_When_Age_Is_Lower_Than_21_By_Month_Difference()
    {
        // Arrange
        var userService = new UserService();
        // Act
        var addResult = userService.AddUser("John", "Doe", "johndoe@gmail.com", DateTime.Parse("2003-04-25"), 1);
        // Assert
        Assert.False(addResult);
    }
    [Fact]
    public void AddUser_Should_Return_True_For_Clients_With_Proper_Data_Without_Credit_Limit()
    {
        // Arrange
        var userService = new UserService();
        // Act
        var addResult = userService.AddUser("John", "Malewski", "johndoe@gmail.com", DateTime.Parse("1982-03-21"), 2);
        // Assert
        Assert.True(addResult);
    }
    
    [Fact]
    public void AddUser_Should_Return_True_For_Clients_With_Proper_Data_With_Credit_Limit()
    {
        // Arrange
        var userService = new UserService();
        // Act
        var addResult = userService.AddUser("John", "Smith", "johndoe@gmail.com", DateTime.Parse("1982-03-21"), 3);
        // Assert
        Assert.True(addResult);
    }
    
    

}