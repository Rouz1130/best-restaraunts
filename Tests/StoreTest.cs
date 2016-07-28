using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Dinning
{
  public class StoreTest : IDisposable
  {
    public StoreTest()
    {
      DBConfiguration.ConnectionString="Data Source=(localdb)\\mssqllocaldb;Initial Catalog=dinning_test;Integrated Security =SSPI;";
    }

      public void Dispose()
      {
        Dish.DeleteAll();
        Store.DeleteAll();

      }

    //   [Fact]
    //   public void Test_Store_Equal_returnTrue()
    //   {
    //     //Arrange, Act
    //     Store myStore= new Store("wer",5);
    //     Store myOtherStore= new Store("wer",5);
    //     //Assert
    //     Assert.Equal(myStore, myOtherStore);
    //   }
    //   [Fact]
    //   public void Test_Save_dish_true()
    //   {
    //     //Arrange, Act
    //     Store myStore= new Store("wer",5,1);
    //   myStore.Save();
    //
    //   //Act
    //   List<Store> result = Store.GetAll();
    //   List<Store> testList = new List<Store>{myStore};
    //
    //   //Assert
    //   Assert.Equal(testList, result);
    //   }
    //
    //   [Fact]
    //   public void Test_Save_ID_true()
    //   {
    //   Store myStore = new Store ("sdojofdkoedf",3);
    //   myStore.Save();
    //   //List return list of stores
    //   List<Store> allstores= Store.GetAll();
    //   //list of store in index o should be my store
    //   Store oneStore = allstores[0];
    //
    //   int resultId= oneStore.GetId();
    //   int testId=myStore.GetId();
    //
    //   Assert.Equal(resultId, testId);
    //
    // }
    //
    //
    // [Fact]
    // public void Test_FindId()
    // {
    //   Store myStore = new Store("kjoio",2);
    //   myStore.Save();
    //   //Save data to data base and assign id depending on table order
    //   Store findStore = Store.Find(myStore.GetId());
    //
    //   Assert.Equal(findStore, myStore);
    // }
    //
    // public void Test_GetAllDishesInStore_true()
    // {
    //   Store myStore = new Store("lo-main",5);
    //   myStore.Save();
    //
    //   Dish firstDish = new Dish("Pizza",13,myStore.GetId());
    //   firstDish.Save();
    //   Dish secondDish = new Dish("tacos",13,myStore.GetId());
    //   secondDish.Save();
    //
    //   List<Dish> testDishList = new List<Dish> {firstDish,secondDish};
    //   List<Dish> resultDishList = myStore.GetDish();
    //
    //   Assert.Equal(testDishList, resultDishList);
    //
    // }
    //   [Fact]
    //   public void Test_UpdateStoreInDatabase()
    //   {
    //     Store myStore = new Store("burger king", 2);
    //     myStore.Save();
    //     myStore.Update("burger Queen");
    //     string result = myStore.GetName();
    //
    //     Assert.Equal("burger Queen", result);
    //
    //   }

      [Fact]
      public void Test_DeleteOneStore_true()
      {
        Store firstStore = new Store("A",3);
        firstStore.Save();
        Store secondStore = new Store("B",2);
        secondStore.Save();

        Dish firstStoreDish = new Dish("burger",87,firstStore.GetId());
        firstStoreDish.Save();

        Dish secondStoreDsih= new Dish("toco",56,secondStore.GetId());
        secondStoreDsih.Save();

        firstStore.Delete();
        List<Store> allStores = Store.GetAll();
        List<Store> afterDeleteOneStore= new List<Store> {secondStore};

        List<Dish> resultDish=Dish.GetAll();
        List<Dish> afterDeleteOneStoreDish= new List<Dish> {secondStoreDsih};


        Assert.Equal(afterDeleteOneStore,allStores);
        Assert.Equal(afterDeleteOneStoreDish,resultDish);
      }
  }
}
