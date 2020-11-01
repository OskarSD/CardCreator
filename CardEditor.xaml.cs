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
using Card_Creator.Data;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace Card_Creator {

	public partial class CardEditor : Page {
		MainWindow win = (MainWindow)Application.Current.MainWindow;
		CardCreatorContext context = new CardCreatorContext();
		string originalPath;

		public CardEditor() {
			InitializeComponent();
			win.SaveCardButton.Click += SaveCardButton_Click;
			GenerateTypes();
			DisplayTypeButtons();
		}

		//checks if all the user inputs are valid
		private bool IsCardValid() {
			if ((String.IsNullOrWhiteSpace(NameBoxText.Text)) ||
				(IsOnlyNumbers(NameBoxText.Text)) ||
				(String.IsNullOrWhiteSpace(DescriptionBoxText.Text)) ||
				(IsOnlyNumbers(DescriptionBoxText.Text)) ||
				(!int.TryParse(LifeBoxText.Text, out _)) ||
				(!int.TryParse(DamageBoxText.Text, out _)) ||
				(!int.TryParse(ManaBoxText.Text, out _))) {
				return false;
			}
			return true;
		}

		//checks is string only contains numbers
		private bool IsOnlyNumbers(string text) {
			foreach (char c in text) {
				if (c < '0' || c > '9') {
					return false;
				}
			}
			return true;
		}

		//displays a popup depending on situation
		private void DisplayPopup(bool isValid) {
			if (isValid) {
				PopupBackground.Visibility = Visibility.Visible;
				ValidPopup.Visibility = Visibility.Visible;
			} else {
				PopupBackground.Visibility = Visibility.Visible;
				InvalidPopup.Visibility = Visibility.Visible;
			}
		}

		//saves card to database and saves image to a folder
		private void SaveCardButton_Click(object sender, RoutedEventArgs e) {
			if (!IsCardValid()) {
				DisplayPopup(false);
			} else {
				SaveImage(originalPath);
				string filename = System.IO.Path.GetFileName(originalPath);
				string newPath = String.Format(ProcessPath() + "\\cardimages\\" + filename);
				string backgroundPath = String.Format(ProcessPath() + "\\cardimages\\CardBackground.png");
				NameTest.Text = newPath;

				context.Card.Add(CreateCard(newPath, backgroundPath));
				context.SaveChanges();
				DisplayPopup(true);
			}
		}

		private string ProcessPath() {
			string processPath = System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
			return processPath;
		}

		//create the card
		private Card CreateCard(string portraitImagePath, string backgroundImagePath) {
			Card card = new Card(
				NameBoxText.Text, DescriptionBoxText.Text, 
				"typeName", "typeColor",
				Convert.ToInt32(LifeBoxText.Text),
				Convert.ToInt32(DamageBoxText.Text),
				Convert.ToInt32(ManaBoxText.Text),
				portraitImagePath, backgroundImagePath);
			return card;
		}

		private string ToBase64(string path) {
			byte[] imageArray = File.ReadAllBytes(path);
			string imageBase64 = Convert.ToBase64String(imageArray);
			return imageBase64;
		}

		//dynamically creates a custom dropdown menu for types
		public void GenerateTypes() {
			var types = context.Type;
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

		//hides type buttons if the mouse leaves the type buttons area
		private void TypeLeave(object sender, MouseEventArgs e) {
			if ((!TypeStackPanel.IsMouseOver) &&
				(!TypeButton.IsMouseOver)) {
				TypeStackPanel.Visibility = Visibility.Hidden;
				Panel.SetZIndex(TypeScrollView, 0);
			}
		}

		//type selection
		private void TypeClick(object sender, RoutedEventArgs e) {
			Button b = sender as Button;
			TypeRules(b.Name);
			DisplayTypeButtons();
		}

		//displays the appropriate rules for the selected type
		private void TypeRules(string typeName) {
			var types = context.Type;
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

		//button for displaying and hiding types
		public void TypeButtonClick(object sender, EventArgs e) {
			DisplayTypeButtons();
		}

		//displays and hides the type buttons
		private void DisplayTypeButtons() {
			if (TypeStackPanel.Visibility == Visibility.Hidden) {
				TypeStackPanel.Visibility = Visibility.Visible;
				Panel.SetZIndex(TypeScrollView, 2);
			} else {
				TypeStackPanel.Visibility = Visibility.Hidden;
				Panel.SetZIndex(TypeScrollView, 0);
			}
		}

		//image button
		private void ImageSelector_Click(object sender, RoutedEventArgs e) {
			DisplayImage(SelectImage());
		}

		//displays selected image
		private void DisplayImage(string filepath) {
			if (ImageCheck(filepath) == "Good") {
				ImageSource imgSource = new BitmapImage(new Uri(filepath));
				ImageTest.Source = imgSource;
				//player clicked on good image (no popup needed)
			} else if (ImageCheck(filepath) == "Bad") {
				//player clicked on bad image (popup)
			} else if (ImageCheck(filepath) == "Canceled") {
				//player click x or cancel (no popup needed)
			} else {
				//unknown error (popup?)
			}
		}

		//checks if the selected image is valid
		private string ImageCheck(string filepath) {
			if (filepath == "Canceled") {
				return filepath;
			} else {
				int width, height;
				using (var stream = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.Read)) {
					var bitmapFrame = BitmapFrame.Create(stream, BitmapCreateOptions.DelayCreation, BitmapCacheOption.None);
					width = bitmapFrame.PixelWidth;
					height = bitmapFrame.PixelHeight;
				}
				if (width == 657 && height == 687) {
					return "Good";
				} else {
					return "Bad";
				}
			}
		}

		//opens file explorer and returns the filepath of the selected image
		public string SelectImage() {
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "Image Files (*.png;*.jpg)|*.png;*.jpg";
			if (openFileDialog.ShowDialog() == true) {
				originalPath = openFileDialog.FileName;
				return openFileDialog.FileName;
			} else {
				return "Canceled";
			}
		}

		//saves the image to a folder inside the project
		private void SaveImage(string filepath) {
			string name = System.IO.Path.GetFileName(filepath);
			string destinationPath = GetDestinationPath(name);
			File.Copy(filepath, destinationPath, true);
		}

		//gets the new destination path from inside the project
		private string GetDestinationPath(string filename) {
			string appStartPath = String.Format(ProcessPath() + "\\cardimages\\" + filename);
			return appStartPath;
		}

		//reset button
		public void ResetButton(object sender, EventArgs e) {
			Refresh();
			RemovePopup();
		}

		//removes popups
		private void RemovePopup() {
			PopupBackground.Visibility = Visibility.Hidden;
			ValidPopup.Visibility = Visibility.Hidden;
			InvalidPopup.Visibility = Visibility.Hidden;
		}

		//defaults all the input values
		public void Refresh() {
			RemovePopup();
			NameBoxText.Text = "";
			DescriptionBoxText.Text = "";
			LifeBoxText.Text = "";
			DamageBoxText.Text = "";
			ManaBoxText.Text = "";
		}

		private void PreviewButton_Click(object sender, RoutedEventArgs e) {
			//send user to preview with the finished card object
			//win.DisplayCard(card, db);
			//win.NavigatePreview();
		}

		private void CollectionButton_Click(object sender, RoutedEventArgs e) { win.NavigateCollection(); }

		private void RestartButton_Click(object sender, RoutedEventArgs e) { Refresh(); }

		private void OKButton_Click(object sender, RoutedEventArgs e) { RemovePopup(); }

		private void NameBoxText_TextChanged(object sender, TextChangedEventArgs e) {
			//testName.Text = name; 
		}

		//updates the displayed card info when input is changed
		private void DescriptionBoxText_TextChanged(object sender, TextChangedEventArgs e) {
			//DescriptionTest.Text = DescriptionBoxText.Text;
		}
		private void LifeBoxText_TextChanged(object sender, TextChangedEventArgs e) {
			//LifeTest.Text = LifeBoxText.Text;
		}
		private void DamageBoxText_TextChanged(object sender, TextChangedEventArgs e) {
			//DamageTest.Text = DamageBoxText.Text;
		}
		private void ManaBoxText_TextChanged(object sender, TextChangedEventArgs e) {
			//ManaTest.Text = ManaBoxText.Text;
		}

		//this is used for the custom border/textbox combination on the page
		private void TextBox_LostFocus(object sender, RoutedEventArgs e) { NameBox.BorderThickness = new Thickness(0); }
		private void TextBox_GotFocus(object sender, RoutedEventArgs e) { NameBox.BorderThickness = new Thickness(2); }
		private void TextBox_LostFocus_1(object sender, RoutedEventArgs e) { DescriptionBox.BorderThickness = new Thickness(0); }
		private void TextBox_GotFocus_1(object sender, RoutedEventArgs e) { DescriptionBox.BorderThickness = new Thickness(2); }
		private void TextBox_LostFocus_2(object sender, RoutedEventArgs e) { LifeBox.BorderThickness = new Thickness(0); }
		private void TextBox_GotFocus_2(object sender, RoutedEventArgs e) { LifeBox.BorderThickness = new Thickness(2); }
		private void TextBox_LostFocus_3(object sender, RoutedEventArgs e) { DamageBox.BorderThickness = new Thickness(0); }
		private void TextBox_GotFocus_3(object sender, RoutedEventArgs e) { DamageBox.BorderThickness = new Thickness(2); }
		private void TextBox_LostFocus_4(object sender, RoutedEventArgs e) { ManaBox.BorderThickness = new Thickness(0); }
		private void TextBox_GotFocus_4(object sender, RoutedEventArgs e) { ManaBox.BorderThickness = new Thickness(2); }
	}
}
