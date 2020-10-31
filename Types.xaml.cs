using Card_Creator.Data;
using System.Windows;
using System.Windows.Controls;

namespace Card_Creator {
	public partial class Types : Page {
		CardCreatorContext context = new CardCreatorContext();

		public Types() {
			InitializeComponent();
			LoadTypes();
		}

		//loads all the type elements on the page
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

		//getting the correct type when selecting
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

		//cancel button
		private void CancelButton_Click(object sender, RoutedEventArgs e) { HidePopup(); }

		//deletes selected type from database
		private void DeleteButton_Click(object sender, RoutedEventArgs e) {
			var types = context.Type;
			Type type = context.Type.Find(PopupText.Text);
			context.Type.Remove(type);
			context.SaveChanges();
			RefreshPage();
		}

		//refreshes all elements on the page with new types
		public void RefreshPage() {
			TypeViewPanel.Children.Clear();
			LoadTypes();
			HidePopup();
		}

		//removes popup
		private void HidePopup() {
			PopupBackground.Visibility = Visibility.Hidden;
			TypePopup.Visibility = Visibility.Hidden;
		}
	}
}
