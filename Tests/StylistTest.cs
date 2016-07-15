using Xunit;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace HairSalon
{
  public class StylistTest : IDisposable
  {
    public StylistTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=hair_salon_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
      //Arrange, Act
      int result = Stylist.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Equal_ReturnTrueIfNamesAreTheSame()
    {
      //Arrange
      Stylist firstStylist = new Stylist("Bob");
      Stylist otherFirstStylist = new Stylist("Bob");

      //Act Assert
      Assert.Equal(firstStylist, otherFirstStylist);
    }

    [Fact]
    public void Test_Save_SavesToDatabase()
    {
      //Arrange
      Stylist newStylist = new Stylist("Linda");
      List<Stylist> expectedResult = new List<Stylist> {newStylist};

      //Act
      newStylist.Save();
      List<Stylist> savedStylists = Stylist.GetAll();

      //Assert
      Assert.Equal(expectedResult, savedStylists);
    }

    [Fact]
    public void Test_Find_FindsStylistInDatabase()
    {
      //Arrange
      Stylist testStylist = new Stylist("Bob");
      testStylist.Save();

      //Act
      Stylist foundStylist = Stylist.Find(testStylist.GetId());

      //Assert
      Assert.Equal(testStylist, foundStylist);
    }

    public void Dispose()
    {
      Stylist.DeleteAll();
    }
  }
}
