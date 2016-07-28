using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace Dinning
{
  public class Dish
  {
    private int _id;
    private string _name;
    private int _price;
    private int _storeId;
    public Dish(string name, int price, int storeId, int Id = 0)
    {
      _name = name;
      _price = price;
      _storeId = storeId;
      _id = Id;
    }

    public override bool Equals(System.Object otherDish)
    {
      if (!(otherDish is Dish))
      {
      return false;
      }
        else
      {
    Dish newDish = (Dish) otherDish;
    bool idEquality = (this.GetId()== newDish.GetId());
    bool nameEquality = (this.GetName() == newDish.GetName());
    bool storeIdEqulity = (this.GetStoreId() == newDish.GetStoreId());
    bool priceEquality = (this.GetPrice() == newDish.GetPrice());
    return (idEquality && nameEquality && priceEquality && storeIdEqulity);
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

    public int GetPrice()
    {
      return _price;
    }
    public int GetStoreId()
    {
      return _storeId;
    }

    public static List<Dish> GetAll()
    {
      List<Dish> allDishes = new List<Dish>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM dish", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int dishId = rdr.GetInt32(0);
        string dishName=rdr.GetString(1);
        int dishPrice= rdr.GetInt32(2);
        int dishStoreId=rdr.GetInt32(3);
        Dish oneDish=new Dish(dishName,dishPrice,dishStoreId,dishId);
        allDishes.Add(oneDish);
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

     public static void DeleteAll()
     {
       SqlConnection conn = DB.Connection();
       conn.Open();
       SqlCommand cmd = new SqlCommand("DELETE FROM dish;",conn);
       cmd.ExecuteNonQuery();
       conn.Close();
     }

     public void Save()
     {
       SqlConnection conn =DB.Connection();
       conn.Open();


       SqlCommand cmd = new SqlCommand("INSERT INTO dish(name,price,store_id) OUTPUT INSERTED.id values(@name,@price,@storeId);", conn);

       SqlParameter nameParameter = new SqlParameter();
       nameParameter.ParameterName = "@name";
       nameParameter.Value = this.GetName();

       SqlParameter priceParameter = new SqlParameter();
       priceParameter.ParameterName = "@price";
       priceParameter.Value = this.GetPrice();

       SqlParameter storeParameter = new SqlParameter();
       storeParameter.ParameterName = "@storeId";
       storeParameter.Value = this.GetStoreId();
       cmd.Parameters.Add(nameParameter);
       cmd.Parameters.Add(priceParameter);
       cmd.Parameters.Add(storeParameter);
       SqlDataReader rdr = cmd.ExecuteReader();
       while(rdr.Read())
       {
         this._id = rdr.GetInt32(0);
       }

        if (rdr != null)
        {
          rdr.Close();
        }

        if (conn != null)
          conn.Close();

     }
     public static Dish Find(int Id)
     {
       SqlConnection conn = DB.Connection();
       conn.Open();

       SqlCommand cmd = new SqlCommand("SELECT * FROM dish WHERE id = @dishId;", conn);
       SqlParameter DishParameter = new SqlParameter();
       DishParameter.ParameterName = "@dishId";
       DishParameter.Value = Id;
       cmd.Parameters.Add(DishParameter);
       SqlDataReader rdr = cmd.ExecuteReader();

       string foundName=null;
       int foundPrice=0;
       int foundStoreId=0;
       int foundId=0;

       while(rdr.Read())
       {
         foundId=rdr.GetInt32(0);
         foundName=rdr.GetString(1);
         foundPrice=rdr.GetInt32(2);
         foundStoreId=rdr.GetInt32(3);
       }
       Dish foundDish= new Dish(foundName, foundPrice, foundStoreId, foundId);

       if (rdr!= null)
       {
         rdr.Close();
       }
       if (conn != null)
       {
         conn.Close();
       }
       return foundDish;
     }
     //end of find method
     public void Update(string name)
     {
       SqlConnection conn = DB.Connection();
       conn.Open();

       SqlCommand cmd = new SqlCommand("UPDATE dish SET name = @name output inserted.name WHERE id =@dishId;", conn);
       SqlParameter DishNameParameter = new SqlParameter();
       DishNameParameter.ParameterName = "@name";
       DishNameParameter.Value = name;

       SqlParameter DishIdParameter = new SqlParameter();
       DishIdParameter.ParameterName = "@dishId";
       DishIdParameter.Value = this.GetId();

       cmd.Parameters.Add(DishNameParameter);
       cmd.Parameters.Add(DishIdParameter);

       SqlDataReader rdr = cmd.ExecuteReader();

       while(rdr.Read())
       {
         this._name = rdr.GetString(0);
       }

       if(rdr !=null)
       {
         rdr.Close();
       }
       if(conn!=null)
       {
         conn.Close();
       }
     }

     public void Delete()
     {
       SqlConnection conn = DB.Connection();
       conn.Open();
       SqlCommand cmd = new SqlCommand (" DELETE FROM dish WHERE id =@DishId;", conn);

       SqlParameter dishIdParameter = new SqlParameter();
       dishIdParameter.ParameterName = "@DishId";
       dishIdParameter.Value=this.GetId();
       Console.WriteLine(this.GetId());
       cmd.Parameters.Add(dishIdParameter);
       cmd.ExecuteNonQuery();
       if (conn !=null)
       {
         conn.Close();
       }
     }

}

  }
