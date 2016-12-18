using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;


namespace AppAPITemplate
{
	public class SecondMenu : Menu
	{
		public SecondMenu(MenuItem menuItem)
		{
			Title = "API Template 2";
			Content = list;

			list.ItemsSource = CallAPI(menuItem); //Need to add an "await" into this

			list.ItemTapped += (sender, e) =>
			{
				list.SelectedItem = null;
				//Navigation.PushAsync(new SecondMenu(e.Item as MenuItem));
				ClickMenuItem(e.Item as MenuItem);
			};
		}

		public void ClickMenuItem(MenuItem itemClicked)
		{
			//Implements menu item click
			//e.g. Navigation.PushAsync(new InfoPage(e.Item as string));
			Navigation.PushAsync(new InfoPage(itemClicked));
		}

		public List<MenuItem> CallAPI(MenuItem menuItem)
		{
			List<MenuItem> menuList = ConstructMenuItemList(GetResponseFromAPI(menuItem));

			return menuList;
		}


		public string GetResponseFromAPI(MenuItem menuItem)
		{
			string result = "";
			//string query = constructQuery(menuItem);

			switch (menuItem.Name)
			{
				case "FTSE 100 Index":
					result = 
					break;
				case "NYSE Composite":
					break;
				case "NASDAQ 100":
					break;
				case "NASDAQ Composite":
					break;
				case "DOW JONES":
					break;
				case "SP 500":
					break;
				case "FTSE 100":
					break;	
			}

			string results = "[{ Name : \"Thomas 2\", Description : \"Is the best 2\"}, {Name : \"Cartwright 2\", Description : \"Is the bestest 2\"}]"; //string results = call query.

			return results;
		}

		public List<MenuItem> ConstructMenuItemList(string response)
		{

			List<MenuItem> menuItems = new List<MenuItem>();

			dynamic jsonResult = JsonConvert.DeserializeObject(response); //var jsonResult = Newtonsoft.Json.Linq.JObject.Parse(results);

			foreach (var item in jsonResult)
			{
				MenuItem tempMenuItem = new MenuItem();

				tempMenuItem.Name = item["Name"].Value;
				tempMenuItem.Description = item["Description"].Value;


				menuItems.Add(tempMenuItem);


			}


			return menuItems;

		}


		/*
			constructQuery(MenuItem menuItem) {

				return query;
	
			}
		*/
	}
}

