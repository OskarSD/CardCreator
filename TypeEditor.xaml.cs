using Card_Creator.Data;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Card_Creator {
	public partial class TypeEditor : Page {
		MainWindow win = (MainWindow)Application.Current.MainWindow;
		CardCreatorContext context = new CardCreatorContext();
		public TypeEditor() {
			InitializeComponent();
			win.SaveTypeButton.Click += SaveTypeButton_Click;
		}

		//removes popup
		private void OKButton_Click(object sender, RoutedEventArgs e) { RemovePopup(); }

		//opens Types page
		private void TypesButton_Click(object sender, RoutedEventArgs e) { win.NavigateTypes(); }

		//restarts page
		private void RestartButton_Click(object sender, RoutedEventArgs e) { Refresh(); }

		//saves type to database
		private void SaveTypeButton_Click(object sender, RoutedEventArgs e) {
			if (!IsTypeValid()) {
				DisplayPopup(false);
			} else {
				context.Type.Add(CreateType());
				context.SaveChanges();
				DisplayPopup(true);
			}
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

		//defaults all the input values
		public void Refresh() {
			RemovePopup();
			NameBoxText.Text = "";
			DescBoxText.Text = "";
			ColorBoxText.Text = "#ffffff";
			MinLifeBoxText.Text = "";
			MaxLifeBoxText.Text = "";
			MinDamageBoxText.Text = "";
			MaxDamageBoxText.Text = "";
			MinManaBoxText.Text = "";
			MaxManaBoxText.Text = "";
		}

		//removes popups
		private void RemovePopup() {
			PopupBackground.Visibility = Visibility.Hidden;
			ValidPopup.Visibility = Visibility.Hidden;
			InvalidPopup.Visibility = Visibility.Hidden;
		}

		//creates the type
		private Type CreateType() {
			Type type = new Type(
				NameBoxText.Text, DescBoxText.Text, ColorBoxText.Text,
				Convert.ToInt32(MinLifeBoxText.Text), Convert.ToInt32(MaxLifeBoxText.Text),
				Convert.ToInt32(MinDamageBoxText.Text), Convert.ToInt32(MaxDamageBoxText.Text),
				Convert.ToInt32(MinManaBoxText.Text), Convert.ToInt32(MaxManaBoxText.Text));
			return type;
		}

		//checks if all the user inputs are valid
		private bool IsTypeValid() {
			if ((String.IsNullOrWhiteSpace(NameBoxText.Text)) ||
				(StartWithNumber(NameBoxText.Text)) ||
				(String.IsNullOrWhiteSpace(DescBoxText.Text)) ||
				(StartWithNumber(DescBoxText.Text)) ||
				(!IsColorValid(ColorBoxText.Text)) ||
				((!int.TryParse(MinLifeBoxText.Text, out _)) || (IsNegative(MinLifeBoxText.Text))) ||
				((!int.TryParse(MinDamageBoxText.Text, out _)) || (IsNegative(MinDamageBoxText.Text))) ||
				((!int.TryParse(MinManaBoxText.Text, out _)) || (IsNegative(MinManaBoxText.Text))) ||
				(!int.TryParse(MaxLifeBoxText.Text, out _)) ||
				(!int.TryParse(MaxDamageBoxText.Text, out _)) ||
				(!int.TryParse(MaxManaBoxText.Text, out _)) ||
				(!CompareNumbers(MinLifeBoxText.Text, MaxLifeBoxText.Text)) ||
				(!CompareNumbers(MinDamageBoxText.Text, MaxDamageBoxText.Text)) ||
				(!CompareNumbers(MinManaBoxText.Text, MaxManaBoxText.Text))) {
				return false;
			}
			return true;
		}

		//checks if string starts with a number
		private bool StartWithNumber(string text) {
			char firstChar = text[0];
			if (int.TryParse(firstChar.ToString(), out _)) {
				return true;
			} else {
				return false;
			}
		}

		//casts string to int and then checks if it's is negative
		private bool IsNegative(string text) {
			int number = Int32.Parse(text);
			if (number < 0) {
				return true;
			} else {
				return false;
			}
		}

		//displays color
		private void ColorText_TextChanged(object sender, TextChangedEventArgs e) {
			if (IsColorValid(ColorBoxText.Text)) {
				Border b = new Border {
					Background = (SolidColorBrush)(new BrushConverter().ConvertFrom(ColorBoxText.Text)),
					BorderBrush = Brushes.White,
					BorderThickness = new Thickness(1)
				};
				Grid.SetRow(b, 11);
				Grid.SetRowSpan(b, 3);
				Grid.SetColumn(b, 6);
				Grid.SetColumnSpan(b, 3);
				TypeEditorGridL.Children.Add(b);
			}
		}

		//checks if the hex for color is valid
		private bool IsColorValid(string hexcode) {
			var regex = @"(^#[0-9A-F]{6}$)";
			var match = Regex.Match(hexcode, regex, RegexOptions.IgnoreCase);
			if (match.Success) {
				return true;
			} else {
				return false;
			}
		}

		//compares two numbers
		private bool CompareNumbers(string first, string second) {
			int f = Convert.ToInt32(first);
			int s = Convert.ToInt32(second);
			if (f <= s) {
				return true;
			} else {
				return false;
			}
		}

		//this is used for the custom border/textbox combination on the page
		private void TextBox_LostFocus(object sender, RoutedEventArgs e) { NameBox.BorderThickness = new Thickness(0); }
		private void TextBox_GotFocus(object sender, RoutedEventArgs e) { NameBox.BorderThickness = new Thickness(2); }
		private void TextBox_LostFocus_1(object sender, RoutedEventArgs e) { DescBox.BorderThickness = new Thickness(0); }
		private void TextBox_GotFocus_1(object sender, RoutedEventArgs e) { DescBox.BorderThickness = new Thickness(2); }
		private void TextBox_LostFocus_2(object sender, RoutedEventArgs e) { MinLifeBox.BorderThickness = new Thickness(0); }
		private void TextBox_GotFocus_2(object sender, RoutedEventArgs e) { MinLifeBox.BorderThickness = new Thickness(2); }
		private void TextBox_LostFocus_3(object sender, RoutedEventArgs e) { MaxLifeBox.BorderThickness = new Thickness(0); }
		private void TextBox_GotFocus_3(object sender, RoutedEventArgs e) { MaxLifeBox.BorderThickness = new Thickness(2); }
		private void TextBox_LostFocus_4(object sender, RoutedEventArgs e) { MinDamageBox.BorderThickness = new Thickness(0); }
		private void TextBox_GotFocus_4(object sender, RoutedEventArgs e) { MinDamageBox.BorderThickness = new Thickness(2);}
		private void TextBox_LostFocus_5(object sender, RoutedEventArgs e) { MaxDamageBox.BorderThickness = new Thickness(0); }
		private void TextBox_GotFocus_5(object sender, RoutedEventArgs e) { MaxDamageBox.BorderThickness = new Thickness(2); }
		private void TextBox_LostFocus_6(object sender, RoutedEventArgs e) { MinManaBox.BorderThickness = new Thickness(0); }
		private void TextBox_GotFocus_6(object sender, RoutedEventArgs e) { MinManaBox.BorderThickness = new Thickness(2);  }
		private void TextBox_LostFocus_7(object sender, RoutedEventArgs e) { MaxManaBox.BorderThickness = new Thickness(0); }
		private void TextBox_GotFocus_7(object sender, RoutedEventArgs e) { MaxManaBox.BorderThickness = new Thickness(2); }
		private void TextBox_LostFocus_8(object sender, RoutedEventArgs e) { ColorBox.BorderThickness = new Thickness(0); }
		private void TextBox_GotFocus_8(object sender, RoutedEventArgs e) { ColorBox.BorderThickness = new Thickness(2); }
	}
}
