using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Card_Creator {
	public partial class Collection : Page {
		List<Card> cards;
		//public Collection() {
		//	InitializeComponent();
		//	string json = new WebClient().DownloadString("https://my-json-server.typicode.com/OskarSD/demo/card");
		//	cards = JsonConvert.DeserializeObject<List<Card>>(json);
		//	//LoadCards();
		//}

		private void TextBox_GotFocus(object sender, RoutedEventArgs e) {
			SearchBox.BorderThickness = new Thickness(2);
		}

		private void TextBox_LostFocus(object sender, RoutedEventArgs e) {
			SearchBox.BorderThickness = new Thickness(0);
		}

		//private void LoadCards() {
		//	WrapPanel p = new WrapPanel {
		//		Orientation = Orientation.Horizontal,
		//		HorizontalAlignment = HorizontalAlignment.Center,
		//		ItemWidth = 220,
		//		ItemHeight = 300
		//	};
		//	CardViewPanel.Children.Add(p);
		//	foreach (Card card in cards) {
		//		Border b = new Border {
		//			Margin = new Thickness(5, 10, 5, 10),
		//		};
		//		p.Children.Add(b);

		//		CardElement cardE = new CardElement(card);
		//		b.Child = cardE.DisplayCard();

		//	}
		//}
	}
}
