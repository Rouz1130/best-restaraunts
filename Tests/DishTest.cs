using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Dinning
{
  public class DishTest : IDisposable
  {
    public DishTest()
    {
      DBConfiguration.ConnectionString="Data Source=(localdb)\\mssqllocaldb;Initial Catalog=dinning_test;Integrated Security =SSPI;";
    }

      public void Dispose()
      {
        Dish.DeleteAll();
      }

      [Fact]
      public void T1_EmptyDataBase_True()
    {
      //Arrange
      List<Dish> allDishes = Dish.GetAll();
      int result = allDishes.Count;

      Assert.Equal(0,result);
    }
      [Fact]
      public void T2_Return_DishObjectsOverride()
      {
        Dish firstdish = new Dish("Pizza",13,3);
        Dish seconddish = new Dish("Pizza",13,3);

        Assert.Equal(firstdish, seconddish);
      }
      [Fact]
      public void Test_Save_DishesToObjects()
      {
        List<Dish> onedish = new List<Dish>{};
        Dish firstDish = new Dish("Pizza", 13,3);
        onedish.Add(firstDish);
        firstDish.Save();
        List<Dish> allDishes=Dish.GetAll();
        // Console.WriteLine(allDishes[0].GetName());
        // Console.WriteLine(allDishes[0].GetPrice());
        // Console.WriteLine(allDishes[0].GetStoreId());
        // Console.WriteLine(allDishes[0].GetId());
        Assert.Equal(onedish,allDishes);
      }
      [Fact]
      public void Test_Save_AssignsIdToObjects()
      {
        Dish oneDish = new Dish("Pizza",13,3);
        oneDish.Save();
        Dish savedDish = Dish.GetAll()[0];

        Assert.Equal(oneDish, savedDish);
      }


      [Fact]
    public void Test_Find_FindsDishInDatabase()
    {
      //Arrange
      Dish oneDish = new Dish("Pizza",13,3);
      oneDish.Save();

      //Act
      Dish foundDish  = Dish.Find(oneDish.GetId());
      Console.WriteLine(foundDish.GetName());
      Console.WriteLine(foundDish.GetPrice());
      Console.WriteLine(foundDish.GetStoreId());
      Console.WriteLine(foundDish.GetId());
      Console.WriteLine(oneDish.GetName());
      Console.WriteLine(oneDish.GetPrice());
      Console.WriteLine(oneDish.GetStoreId());
      Console.WriteLine(oneDish.GetId());
      //Assert
      Assert.Equal(oneDish, foundDish);
    }

    [Fact]
    public void Test_UpdateDishesInDatabase()
    {
      Dish myDish = new Dish("cfdfgdgd", 2 ,5);
      myDish.Save();
      myDish.Update("q");
      string result = myDish.GetName();

      Assert.Equal("q", result);
    }

    [Fact]
    public void Test_DeleteOneStore_true()
    {


      Dish firstStoreDish = new Dish("burger",87,2);
      firstStoreDish.Save();

      Dish secondStoreDsih= new Dish("toco",56,9);
      secondStoreDsih.Save();

      firstStoreDish.Delete();


      List<Dish> resultDish=Dish.GetAll();
      List<Dish> afterDeleteOneStoreDish= new List<Dish> {secondStoreDsih};

      Assert.Equal(afterDeleteOneStoreDish,resultDish);
    }
  }
}
