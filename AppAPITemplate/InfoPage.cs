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
		static Label City = new Label
		{
			Text = "-",
			TextColor = Color.Black,
			FontSize = 35,
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
		static Label WeatherTitle = new Label
		{
			Text = "Weather",
			FontSize = 25,
			HorizontalTextAlignment = TextAlignment.Center,
			VerticalTextAlignment = TextAlignment.Center,
			HorizontalOptions = LayoutOptions.FillAndExpand,
			TextColor = Color.Black,
			FontFamily = Device.OnPlatform(
				"Oswald-Bold",
				null,
				null
			),
		};

		static Label SunriseTitle = new Label
		{
			Text = "Sunrise",
			FontSize = 25,
			HorizontalTextAlignment = TextAlignment.Center,
			VerticalTextAlignment = TextAlignment.Center,
			HorizontalOptions = LayoutOptions.FillAndExpand,
			TextColor = Color.Black,
			FontFamily = Device.OnPlatform(
				"Oswald-Bold",
				null,
				null
			),
		};

		static Label SunsetTitle = new Label
		{
			Text = "Sunset",
			FontSize = 25,
			HorizontalTextAlignment = TextAlignment.Center,
			VerticalTextAlignment = TextAlignment.Center,
			HorizontalOptions = LayoutOptions.FillAndExpand,
			TextColor = Color.Black,
			FontFamily = Device.OnPlatform(
				"Oswald-Bold",
				null,
				null
			),
		};

		static Label Time = new Label
		{
			Text = "",
			FontSize = 25,
			HorizontalOptions = LayoutOptions.FillAndExpand,
			HorizontalTextAlignment = TextAlignment.Center,
			TextColor = Color.Gray,
			FontFamily = Device.OnPlatform(
				"Oswald-Bold",
				null,
				null
			),
		};
		static Label Weather = new Label
		{
			Text = "-",
			FontSize = 20,
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

		static Label Sunrise = new Label
		{
			Text = "-",
			FontSize = 20,
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

		static Label Sunset = new Label
		{
			Text = "-",
			FontSize = 20,
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

		static Label TemperatureTitle = new Label
		{
			Text = "Temperature",
			FontSize = 25,
			HorizontalTextAlignment = TextAlignment.Center,
			VerticalTextAlignment = TextAlignment.Center,
			HorizontalOptions = LayoutOptions.FillAndExpand,
			TextColor = Color.Black,
			FontFamily = Device.OnPlatform(
				"Oswald-Bold",
				null,
				null
			),
		};

		static Label Temperature = new Label
		{
			Text = "-",
			FontSize = 20,
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
						SunriseTitle,
						Sunrise
					}
				});

				Children.Add(new StackLayout
				{
					Spacing = 2,
					WidthRequest = 0,
					HorizontalOptions = LayoutOptions.FillAndExpand,
					Children = {
								SunsetTitle,
								Sunset
					}
				});

			}
		}

		public class InfoPageLayout : StackLayout
		{
			public InfoPageLayout()
			{
				//HeightRequest = 140;
				Spacing = 5;
				Orientation = StackOrientation.Vertical;
				Padding = new Thickness(30, 10, 30, 10);
				Children.Add(
					City
				);
				Children.Add(
					Time
				);
				Children.Add(new StackLayout
					{
						Spacing = 2,
						WidthRequest = 0,
						HorizontalOptions = LayoutOptions.FillAndExpand,
						Children = {
									WeatherTitle,
									Weather
					}
				});
				Children.Add(new StackLayout
				{
					Spacing = 2,
					WidthRequest = 0,
					HorizontalOptions = LayoutOptions.FillAndExpand,
					Children = {
						TemperatureTitle,
						Temperature
					}
				});
				Children.Add(new InfoPageLayoutChildren());
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
			City.Text = list[0];
			Sunrise.Text = list[4];
			Sunset.Text = list[2];
			Weather.Text = list[3];
			Time.Text = list[1];
			Temperature.Text = list[5];
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
			string temp = jsonResult["query"]["results"]["channel"]["item"]["condition"]["temp"].Value;


			DateTime dateTime = Convert.ToDateTime(jsonResult["query"]["results"]["channel"]["item"]["condition"]["date"].Value.ToString().Substring(0, 25));
			string time = dateTime.ToString("HH:mm") + ", " + dateTime.ToString("d MMM");

			items.Add(city);
			items.Add(time);
			items.Add(sunset);
			items.Add(typeOfWeather);
			items.Add(sunrise);
			items.Add(temp);

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
