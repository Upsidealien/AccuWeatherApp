using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace AppAPITemplate
{
	public class InfoPage : ContentPage
	{
		/*
			Displays all the information for the API menus	
		*/
		static Label One = new Label
		{
			Text = "",
			TextColor = Color.White,
			FontSize = 50,
			BackgroundColor = Color.Aqua,
			HorizontalTextAlignment = TextAlignment.Center,
			VerticalTextAlignment = TextAlignment.Center,
			HorizontalOptions = LayoutOptions.Fill,
			FontFamily = Device.OnPlatform(
				"Oswald-Bold",
				null,
				null
			),
		};
		static Label Two = new Label
		{
			Text = "",
			HorizontalTextAlignment = TextAlignment.Center,
			VerticalTextAlignment = TextAlignment.Center,
			HorizontalOptions = LayoutOptions.FillAndExpand,
			TextColor = Color.Gray,
			FontFamily = Device.OnPlatform(
				"Oswald-Bold",
				null,
				null
			),
		};
		static Label Three = new Label
		{
			Text = "",
			WidthRequest = 0,
			HorizontalOptions = LayoutOptions.FillAndExpand,
			VerticalOptions = LayoutOptions.FillAndExpand,
			VerticalTextAlignment = TextAlignment.End,
			HorizontalTextAlignment = TextAlignment.Center,
			TextColor = Color.Gray,
			FontFamily = Device.OnPlatform(
				"Oswald-Bold",
				null,
				null
			),
		};
		static Label Four = new Label
		{
			Text = "",
			HorizontalOptions = LayoutOptions.Start,
			TextColor = Color.Gray,
			FontFamily = Device.OnPlatform(
				"Oswald-Bold",
				null,
				null
			),
		};
		static Label Five = new Label
		{
			Text = "",
			HorizontalOptions = LayoutOptions.Start,
			TextColor = Color.Gray,
			FontFamily = Device.OnPlatform(
				"Oswald-Bold",
				null,
				null
			),
		};

		public class InfoPageLayoutChildren : StackLayout
		{
			public InfoPageLayoutChildren()
			{
				Spacing = 2;
				WidthRequest = 0;
				Orientation = StackOrientation.Horizontal;
				Children.Add(new StackLayout
				{
					Spacing = 2,
					WidthRequest = 0,
					HorizontalOptions = LayoutOptions.FillAndExpand,
					Children = {

								new Label
									{
										Text = "Weather",
										HorizontalOptions = LayoutOptions.Start,
										TextColor = Color.Gray,
										FontFamily = Device.OnPlatform(
											"Oswald-Bold",
											null,
											null
										),
									},
								new Label
									{
										Text = "Sunrise",
										HorizontalOptions = LayoutOptions.Start,
										TextColor = Color.Gray,
										FontFamily = Device.OnPlatform(
											"Oswald-Bold",
											null,
											null
										),
									},
								},
				});

				Children.Add(new StackLayout
				{
					Spacing = 2,
					WidthRequest = 0,
					HorizontalOptions = LayoutOptions.FillAndExpand,
					Children = {
						Four,
						Five
					},
				});

			}
		}

		public class InfoPageLayout : StackLayout
		{
			public InfoPageLayout()
			{
				HeightRequest = 70;
				Spacing = 5;
				Orientation = StackOrientation.Vertical;
				Children.Add(
					One
				);
				Children.Add(
					Two
				);
				Children.Add(new InfoPageLayoutChildren());
				Children.Add(
					Three
				);
			}
		}


		public MenuItem currentItem;

		//This holds the Views (it will switch between saying "Loading" and showing the info)
		public InfoPage(MenuItem item)
		{
			currentItem = item;
			Content = new InfoPageLayout();
		}


		protected override async void OnAppearing()
		{
			base.OnAppearing();



			List<string> list = await CallAPI(currentItem);
			One.Text = list[0];
			Two.Text = list[1];
			Three.Text = list[2];
			Four.Text = list[3];
			Five.Text = list[4];
		}

		static async Task<List<string>> CallAPI(MenuItem menuItem)
		{
			string response = await GetResponseFromAPI(menuItem);

			List<string> list = ConstructList(response);

			return list;
		}

		static async Task<string> GetResponseFromAPI(MenuItem menuItem)
		{
			string query = ConstructQuery(menuItem);

			//string results = "[{ One : \"Thomas 2\", Two : \"Is the best 2\", Three : \"And number three \", Four : \"And is four too much\", Five : \"A a high five\"}]"; //string results = call query.
			using (var client = new HttpClient())
			{
				var response = await client.GetStringAsync(query);
				return response.ToString();
			}
		}

		static string ConstructQuery(MenuItem menuItem)
		{
			string query = "";

			switch (menuItem.Name)
			{
				case "Melbourne":
					query = "https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20weather.forecast%20where%20woeid%20in%20(select%20woeid%20from%20geo.places(1)%20where%20text%3D%22Melbourne%2C%20AUS%22)&format=json&diagnostics=true&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys&callback=";
					break;
				case "Edinburgh":
					query = "https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20weather.forecast%20where%20woeid%20in%20(select%20woeid%20from%20geo.places(1)%20where%20text%3D%22Edinburgh%2C%20UK%22)&format=json&diagnostics=true&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys&callback=";
					break;
				case "London":
					query = "https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20weather.forecast%20where%20woeid%20in%20(select%20woeid%20from%20geo.places(1)%20where%20text%3D%22London%2C%20UK%22)&format=json&diagnostics=true&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys&callback=";
					break;
				case "Sydney":
					query = "https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20weather.forecast%20where%20woeid%20in%20(select%20woeid%20from%20geo.places(1)%20where%20text%3D%22Sydney%2C%20AUS%22)&format=json&diagnostics=true&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys&callback=";
					break;
				case "Miami":
					query = "https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20weather.forecast%20where%20woeid%20in%20(select%20woeid%20from%20geo.places(1)%20where%20text%3D%22Miami%2C%20FL%22)&format=json&diagnostics=true&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys&callback=";
					break;
				case "New York":
					query = "https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20weather.forecast%20where%20woeid%20in%20(select%20woeid%20from%20geo.places(1)%20where%20text%3D%22New%20York%2C%20NY%22)&format=json&diagnostics=true&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys&callback=";
					break;
			}

			return query;
		}

		static List<string> ConstructList(string response)
		{

			List<string> items = new List<string>();

			dynamic jsonResult = JsonConvert.DeserializeObject(response); //var jsonResult = Newtonsoft.Json.Linq.JObject.Parse(results);


			string city = jsonResult["query"]["results"]["channel"]["location"]["city"].Value;
			string sunrise = jsonResult["query"]["results"]["channel"]["astronomy"]["sunrise"].Value;
			string sunset = jsonResult["query"]["results"]["channel"]["astronomy"]["sunset"].Value;
			string typeOfWeather = jsonResult["query"]["results"]["channel"]["item"]["condition"]["text"].Value;
			string time = jsonResult["query"]["results"]["channel"]["item"]["condition"]["date"].Value.ToString();

			items.Add(city);
			items.Add(time);
			items.Add(sunset);
			items.Add(typeOfWeather);
			items.Add(sunrise);

			return items;

		}

	
	}
}

/*

public class TimePage : ContentPage
	{
		readonly Label timeLabel = new Label
		{
			Text = "Loading...",
			HorizontalOptions = LayoutOptions.CenterAndExpand,
			VerticalOptions = LayoutOptions.CenterAndExpand,
		};

		public TimePage()
		{
			Content = timeLabel;
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();
			timeLabel.Text = await RequestTimeAsync();
		}

		static async Task<string> RequestTimeAsync()
		{
			using (var client = new HttpClient())
			{
				var jsonString = await client.GetStringAsync("https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20yahoo.finance.quote%20where%20symbol%20in%20(%22YHOO%22)&format=json&diagnostics=true&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys&callback=");
				var jsonObject = JObject.Parse(jsonString);
				return jsonObject["time"].Value<string>();
			}
		}
	}



*/
