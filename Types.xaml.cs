using Card_Creator.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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
	public partial class Types : Page {
		MainWindow win = (MainWindow)Application.Current.MainWindow;
		CardCreatorContext context = new CardCreatorContext();

		public Types() {
			InitializeComponent();
			//string json = new WebClient().DownloadString("https://my-json-server.typicode.com/OskarSD/demo/type");
			//types = JsonConvert.DeserializeObject<List<Type>>(json);
			
			LoadTypes();
		}

		//private DbSet<Type> GetDbContext()
  //      {

		//	var types = context.Type;



		//	return types;
  //      }
		private void LoadTypes() {

			var types = context.Type;
			WrapPanel p = new WrapPanel {
				Orientation = Orientation.Horizontal,
				HorizontalAlignment = HorizontalAlignment.Center,
				ItemWidth = 260,
				ItemHeight = 130
			};
			TypeViewPanel.Children.Add(p);
			foreach (Type type in types) {
				Border b = new Border {
					Margin = new Thickness(5, 10, 5, 10),
				};
				p.Children.Add(b);

				TypeElement typeE = new TypeElement(type);
				Button typeB = typeE.DisplayType();
				typeB.Click += TypeB_Click;
				b.Child = typeB;

			}
		}

		private void TypeB_Click(object sender, RoutedEventArgs e) {
			
			var types = context.Type;

			Button b = sender as Button;
			foreach (Type type in types) {
				if (type.Name == b.Name) {
					PopupText.Text = type.Name;
					break;
				}
			}
			PopupBackground.Visibility = Visibility.Visible;
			TypePopup.Visibility = Visibility.Visible;
		}

		private void CancelButton_Click(object sender, RoutedEventArgs e) {
			HidePopup();
		}

		private void DeleteButton_Click(object sender, RoutedEventArgs e) {
			
			var types = context.Type;

			Button b = sender as Button;
			Type t;
			foreach (Type type in types) {
				if (type.Name == b.Name) {
					t = type;
				}
			}
			//delete t (selected type) from database
			win.Types_Restart();
		}

		private void HidePopup() {
			PopupBackground.Visibility = Visibility.Hidden;
			TypePopup.Visibility = Visibility.Hidden;
		}
	}
}
