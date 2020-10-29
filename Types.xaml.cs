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

namespace Card_Creator
{
	/// <summary>
	/// Interaction logic for Types.xaml
	/// </summary>
	public partial class Types : Page {
		List<Type> types;
		public Types() {
			InitializeComponent();
			string json = new WebClient().DownloadString("https://my-json-server.typicode.com/OskarSD/demo/type");
			types = JsonConvert.DeserializeObject<List<Type>>(json);
			LoadTypes();
		}
		private void LoadTypes() {
			WrapPanel p = new WrapPanel {
				Orientation = Orientation.Horizontal,
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
				b.Child = typeE.DisplayType();

			}
		}
	}
}
