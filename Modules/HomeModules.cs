using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;

namespace Dinning
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
    Get["/"] = _ => {
      return View["index.cshtml"];
    };

    Get["/dishes"] = _ => {
    List<Dish> AllDish = Dish.GetAll();
    return View["dishes.cshtml", AllDish];
    };
    Get["/stores"] = _ => {
      List<Store> AllStores = Store.GetAll();
      return View["stores.cshtml", AllStores];
    };

    Get["/stores/new"] = _ => {
    return View["stores_form.cshtml"];
    };
    Post["/stores/new"] = _ => {
      Store newStore = new Store(Request.Form["store-name"],Request.Form["store-review"]);
      newStore.Save();
      return View["success.cshtml"];
    };
    Get["/dishes/new"] = _ => {
      List<Store> AllStores = Store.GetAll();
      return View["dishes_form.cshtml", AllStores];
    };
    Post["/dishes/new"] = _ => {
      Dish newDish = new Dish(Request.Form["dish-name"],Request.Form["dish-price"],Request.Form["store-id"]);
      newDish.Save();
      return View["success.cshtml"];
    };

    Post["/dishes/delete"] = _ => {
      Dish.DeleteAll();
      return View["cleared.cshtml"];
    };

    Get["/stores/{id}"] = parameters => {
      Dictionary<string, object> model = new Dictionary<string, object>();
      var SelectedStore = Store.Find(parameters.id);
      var StoreDish = SelectedStore.GetDish();
      model.Add("category", SelectedStore);
      model.Add("dishes", StoreDish);
      return View["store.cshtml", model];
    };

    Get["/store/edit/{id}"] = parameters => {
      Dish selectDishes = Dish.Find(parameters.id);
      return View["stores_edit.cshtml", selectDishes];
    };

    Patch["/store/edit/{id}"] = parameters => {
      Store selectedStore = Store.Find(parameters.id);
      selectedStore.Update(Request.Form["store-name"]);
      return View["success.cshtml"];
    };

    Get["store/delete/{id}"] = parameters => {
      Store SelectedStore = Store.Find(parameters.id);
      return View["store_delete.cshtml", SelectedStore];
    };

    Delete["/store/delete/{id}"] = parameters => {
      Store selectedStore = Store.Find(parameters.id);
      return View["success.cshtml"];
    };

    Get["/dishes/edit/{id}"] = parameters => {
      Dish selectDishes = Dish.Find(parameters.id);
      return View["dishes_edit.cshtml", selectDishes];
    };

    Patch["/dishes/edit/{id}"] = parameters => {
      Dish selectedStore = Dish.Find(parameters.id);
      selectedStore.Update(Request.Form["dish-name"]);
      return View["success.cshtml"];
    };


        Get["/dish/delete/{id}"] = parameters => {
          Dish SelectedStore = Dish.Find(parameters.id);
          return View["dishes_delete.cshtml", SelectedStore];
        };

        Delete["/dish/delete/{id}"] = parameters => {
          Dish selectedDish = Dish.Find(parameters.id);
          return View["success.cshtml"];
        };
  }
  }
}
