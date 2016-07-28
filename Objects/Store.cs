using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace Dinning
{
  public class Store
  {
    private int _id;
    private string _name;
    private int _review;

    public Store(string Name,int review, int Id = 0)
    {
      _id = Id;
      _name = Name;
      _review = review;
    }

    public override bool Equals(System.Object otherStore)
    {
        if (!(otherStore is Store))
        {
          return false;
        }
        else
        {
          Store newStore = (Store) otherStore;
          bool idEquality = this.GetId() == newStore.GetId();
          bool nameEquality = this.GetName() == newStore.GetName();
          bool reviewEquality = this.GetReview() == newStore.GetReview();
          return (idEquality && nameEquality && reviewEquality);
        }
    }

    public int GetId()
    {
      return _id;
    }
    public string GetName()
    {
      return _name;
    }
    public int GetReview()
    {
      return _review;
    }
    public void SetName(string newName)
    {
      _name = newName;
    }
    public static List<Store> GetAll()
    {
      List<Store> allStores = new List<Store>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM store;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int storeId = rdr.GetInt32(0);
        string storeName = rdr.GetString(1);
        int storeReview = rdr.GetInt32(2);
        Store newStore = new Store(storeName,storeReview,storeId);
        allStores.Add(newStore);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return allStores;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO store (name ,review) OUTPUT INSERTED.id VALUES (@StoreName,@StoreReview);", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@StoreName";
      nameParameter.Value = this.GetName();


      SqlParameter ReviewParameter = new SqlParameter();
      ReviewParameter.ParameterName = "@StoreReview";
      ReviewParameter.Value = this.GetReview();


      cmd.Parameters.Add(nameParameter);
      cmd.Parameters.Add(ReviewParameter);

      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }

      if(conn != null)
      {
        conn.Close();
      }
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM store;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }

    public static Store Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM store WHERE id = @StoreId;", conn);
      SqlParameter storeIdParameter = new SqlParameter();
      storeIdParameter.ParameterName = "@StoreId";
      storeIdParameter.Value = id.ToString();
      cmd.Parameters.Add(storeIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundStoreId = 0;
      string foundStoreName = null;
      int foundStoreReview = 0;
      while(rdr.Read())
      {
        foundStoreId = rdr.GetInt32(0);
        foundStoreName = rdr.GetString(1);
        foundStoreReview =rdr.GetInt32(2);
      }
      Store foundStore = new Store(foundStoreName,foundStoreReview,foundStoreId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundStore;
    }
    //newStore.GetDish();
    //id = this.GetId()
    public List<Dish> GetDish()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM dish WHERE store_id = @StoreId;", conn);
      SqlParameter storeIdParameter = new SqlParameter();
      storeIdParameter.ParameterName = "@StoreId";
      storeIdParameter.Value = this.GetId();
      cmd.Parameters.Add(storeIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      List<Dish> allDishes = new List<Dish> {};
      while(rdr.Read())
      {
        int dishId = rdr.GetInt32(0);
        string dishName = rdr.GetString(1);
        int dishPrice = rdr.GetInt32(2);
        int dishStoreId = rdr.GetInt32(3);

        Dish newDish = new Dish(dishName, dishPrice,dishStoreId,dishId);
        allDishes.Add(newDish);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allDishes;
    }

    public void Update(string Name)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE store SET name =@StoreName output inserted.name WHERE id =@StoreId ;",conn);
      SqlParameter StoreNameParameter = new SqlParameter();
      StoreNameParameter.ParameterName = "@StoreName";
      StoreNameParameter.Value = Name;

      SqlParameter StoreIDParameter = new SqlParameter();
      StoreIDParameter.ParameterName = "@StoreId";
      StoreIDParameter.Value = this.GetId();

      cmd.Parameters.Add(StoreNameParameter);
      cmd.Parameters.Add(StoreIDParameter);

      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._name = rdr.GetString(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }

      if(conn != null)
      {
        conn.Close();
      }
    }

    public void Delete()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
       SqlCommand cmd = new SqlCommand ("DELETE FROM store WHERE id =@StoreId; DELETE FROM dish WHERE store_id =@StoreId;", conn);

       SqlParameter storeIdParameter = new SqlParameter();
       storeIdParameter.ParameterName = "@StoreId";
       storeIdParameter.Value=this.GetId();
       Console.WriteLine(this.GetId());
       cmd.Parameters.Add(storeIdParameter);
       cmd.ExecuteNonQuery();
       if (conn !=null)
       {
         conn.Close();
       }

    }



  }
}
