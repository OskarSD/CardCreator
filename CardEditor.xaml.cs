using System;
using System.Collections.Generic;
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
using Microsoft.Win32;
using System.IO;
using System.Windows.Automation;
using System.Net;
using Newtonsoft.Json;

namespace Card_Creator {

	public partial class CardEditor : Page {

		List<Type> types;
		string name, description, typeName, typeColor, imagePath;
		int life, damage, mana;

		public CardEditor() {
			InitializeComponent();
			MainWindow win = (MainWindow)Application.Current.MainWindow;
			win.SaveCardButton.Click += SaveCardButton_Click;

			string json = new WebClient().DownloadString("https://my-json-server.typicode.com/OskarSD/demo/type");
			types = JsonConvert.DeserializeObject<List<Type>>(json);
			GenerateTypes();
			DisplayTypeButtons();
		}

		private void SaveCardButton_Click(object sender, RoutedEventArgs e) {

		}

		private void NameBoxText_TextChanged(object sender, TextChangedEventArgs e) {
			name = NameBoxText.Text;
			testName.Text = name; 
		}

		private void DescriptionBoxText_TextChanged(object sender, TextChangedEventArgs e) {
			description = DescriptionBoxText.Text;
			testDescription.Text = description;
		}

		private void LifeBoxText_TextChanged(object sender, TextChangedEventArgs e) {
			if (!String.IsNullOrWhiteSpace(LifeBoxText.Text)) {
				life = Convert.ToInt32(LifeBoxText.Text);
			} else {
				life = 0;
			}
			testLife.Text = LifeBoxText.Text;
		}

		private void DamageBoxText_TextChanged(object sender, TextChangedEventArgs e) {
			//CURRENTLY WON'T UPDATE IF ONE NUMBER IS LEFT
			if (!String.IsNullOrWhiteSpace(DamageBoxText.Text)) {
				damage = Convert.ToInt32(DamageBoxText.Text);
			} else {
				damage = 0;
			}
			testDamage.Text = DamageBoxText.Text;
		}

		private void ManaBoxText_TextChanged(object sender, TextChangedEventArgs e) {
			if (!String.IsNullOrWhiteSpace(ManaBoxText.Text)) {
				mana = Convert.ToInt32(ManaBoxText.Text);
			} else {
				mana = 0;
			}
			testMana.Text = ManaBoxText.Text;
		}

		private string ToBase64(string path) {
			byte[] imageArray = File.ReadAllBytes(path);
			string imageBase64 = Convert.ToBase64String(imageArray);
			return imageBase64;
		}

		private void TextBox_LostFocus(object sender, RoutedEventArgs e) {
			NameBox.BorderThickness = new Thickness(0);
		}

		private void TextBox_GotFocus(object sender, RoutedEventArgs e) {
			NameBox.BorderThickness = new Thickness(2);
		}

		private void TextBox_LostFocus_1(object sender, RoutedEventArgs e) {
			DescriptionBox.BorderThickness = new Thickness(0);
		}

		private void TextBox_GotFocus_1(object sender, RoutedEventArgs e) {
			DescriptionBox.BorderThickness = new Thickness(2);
		}

		private void TextBox_LostFocus_2(object sender, RoutedEventArgs e) {
			LifeBox.BorderThickness = new Thickness(0);
			if (LifeBoxText.Text == typeName) {

			}
		}

		private void TextBox_GotFocus_2(object sender, RoutedEventArgs e) {
			LifeBox.BorderThickness = new Thickness(2);
		}

		private void TextBox_LostFocus_3(object sender, RoutedEventArgs e) {
			DamageBox.BorderThickness = new Thickness(0);
		}

		private void TextBox_GotFocus_3(object sender, RoutedEventArgs e) {
			DamageBox.BorderThickness = new Thickness(2);
		}

		private void TextBox_LostFocus_4(object sender, RoutedEventArgs e) {
			ManaBox.BorderThickness = new Thickness(0);
		}

		private void TextBox_GotFocus_4(object sender, RoutedEventArgs e) {
			ManaBox.BorderThickness = new Thickness(2);
		}

		public void GenerateTypes() {
			WrapPanel p = new WrapPanel {
				Orientation = Orientation.Vertical,
				HorizontalAlignment = HorizontalAlignment.Center,
				ItemHeight = 42,
				ItemWidth = 400
			};
			TypeStackPanel.Children.Add(p);
			foreach (Type type in types) {
				Button b = new Button {
					Name = type.Name,
					Content = type.Name,
					FontSize = 14,
					FontWeight = FontWeights.Light,
					Foreground = Brushes.White,
					Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#3b3b3b")),
					BorderThickness = new Thickness(0)
				};
				b.MouseLeave += TypeLeave;
				b.Click += TypeClick;
				Panel.SetZIndex(b, 3);
				p.Children.Add(b);
			}
		}

		private void TypeLeave(object sender, MouseEventArgs e) {
			if ((!TypeStackPanel.IsMouseOver) &&
				(!TypeButton.IsMouseOver)) {
				TypeStackPanel.Visibility = Visibility.Hidden;
				Panel.SetZIndex(TypeScrollView, 0);
			}
		}

		private void TypeClick(object sender, RoutedEventArgs e) {
			Button b = sender as Button;
			typeName = b.Name;
			TypeRules(typeName);
			testType.Text = typeName;
			DisplayTypeButtons();
		}

		private void TypeRules(string typeName) {
			foreach (Type type in types) {
				if (type.Name == typeName) {
					LifeText.Text = "Life (" + type.LifeMin + " - " + type.LifeMax + ")";
					DamageText.Text = "Damage (" + type.DamageMin + " - " + type.DamageMax + ")";
					ManaText.Text = "Mana (" + type.ManaMin + " - " + type.ManaMax + ")";
					LifeBoxText.Text = "";
					DamageBoxText.Text = "";
					ManaBoxText.Text = "";
					break;
				}
			}
		}

		public void TypeButtonClick(object sender, EventArgs e) {
			DisplayTypeButtons();
		}

		private void DisplayTypeButtons() {
			if (TypeStackPanel.Visibility == Visibility.Hidden) {
				TypeStackPanel.Visibility = Visibility.Visible;
				Panel.SetZIndex(TypeScrollView, 2);
			} else {
				TypeStackPanel.Visibility = Visibility.Hidden;
				Panel.SetZIndex(TypeScrollView, 0);
			}
		}

		//private Card CreateCard(string name, string typeName, string typeColor, int life, int damage, int mana) {
		//	string[] type = { typeName, typeColor };
		//	Card card = new Card(name, type, life, damage, mana);
		//	return card;
		//}

		//Saves card to database
		//public void SaveCard() {
		//	Card card = CreateCard(name, typeName, typeColor, life, damage, mana);
		//}

		//FINISH THIS
		public bool IsCardValid(string typeName) {
			if ((!String.IsNullOrWhiteSpace(NameBoxText.Text)) &&
				(!String.IsNullOrWhiteSpace(DescriptionBoxText.Text))) {
				return true;
			} else {
				return false;
			}
		}

		public void SelectImage(object sender, EventArgs e) {
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "Image Files (*.png;*.jpeg)|*.png;*.jpeg";
			if (openFileDialog.ShowDialog() == true) {
				string fullPath = openFileDialog.FileName;
				string fileName = openFileDialog.SafeFileName;
				imagePath = fullPath.Replace(fileName, "");
			}
		}

		public void ResetButton(object sender, EventArgs e) {
			//resets all input fields
		}

	}
}
