using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Card_Creator {
	public partial class TypeEditor : Page {
		MainWindow win = (MainWindow)Application.Current.MainWindow;
		public TypeEditor() {
			InitializeComponent();
			win.SaveTypeButton.Click += SaveTypeButton_Click;
		}

		//saves type to database
		private void SaveTypeButton_Click(object sender, RoutedEventArgs e) {
			if (!IsTypeValid()) {
				DisplayPopup(false);
			} else {
				//send CreateType() to database
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

		//opens Types page
		private void ToTypes(object sender, RoutedEventArgs e) {
			win.TypeEditor_ToTypes();
		}

		//removes popup
		private void OKButton_Click(object sender, RoutedEventArgs e) {
			RemovePopup();
		}

		//restarts page
		private void RestartButton_Click(object sender, RoutedEventArgs e) {
			win.TypeEditor_Restart();
		}

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
				(String.IsNullOrWhiteSpace(DescBoxText.Text)) ||
				(String.IsNullOrWhiteSpace(ColorBoxText.Text)) ||
				(!IsColorValid(ColorBoxText.Text)) ||
				(!int.TryParse(MinLifeBoxText.Text, out _)) ||
				(!int.TryParse(MaxLifeBoxText.Text, out _)) ||
				(!int.TryParse(MinDamageBoxText.Text, out _)) ||
				(!int.TryParse(MaxDamageBoxText.Text, out _)) ||
				(!int.TryParse(MinManaBoxText.Text, out _)) ||
				(!int.TryParse(MaxManaBoxText.Text, out _)) ||
				(!CompareNumbers(MinLifeBoxText.Text, MaxLifeBoxText.Text)) ||
				(!CompareNumbers(MinDamageBoxText.Text, MaxDamageBoxText.Text)) ||
				(!CompareNumbers(MinManaBoxText.Text, MaxManaBoxText.Text))) {
				return false;
			}
			return true;
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
			if (f < s) {
				return true;
			} else {
				return false;
			}
		}

		//this is used for the custom border/textbox combination on the page
		private void TextBox_LostFocus(object sender, RoutedEventArgs e) {
			NameBox.BorderThickness = new Thickness(0);
		}
		private void TextBox_GotFocus(object sender, RoutedEventArgs e) {
			NameBox.BorderThickness = new Thickness(2);
		}

		private void TextBox_LostFocus_1(object sender, RoutedEventArgs e) {
			DescBox.BorderThickness = new Thickness(0);
		}
		private void TextBox_GotFocus_1(object sender, RoutedEventArgs e) {
			DescBox.BorderThickness = new Thickness(2);
		}

		private void TextBox_LostFocus_8(object sender, RoutedEventArgs e) {
			ColorBox.BorderThickness = new Thickness(0);
		}
		private void TextBox_GotFocus_8(object sender, RoutedEventArgs e) {
			ColorBox.BorderThickness = new Thickness(2);
		}

		private void TextBox_LostFocus_2(object sender, RoutedEventArgs e) {
			MinLifeBox.BorderThickness = new Thickness(0);
		}
		private void TextBox_GotFocus_2(object sender, RoutedEventArgs e) {
			MinLifeBox.BorderThickness = new Thickness(2);
		}

		private void TextBox_LostFocus_3(object sender, RoutedEventArgs e) {
			MaxLifeBox.BorderThickness = new Thickness(0);
		}
		private void TextBox_GotFocus_3(object sender, RoutedEventArgs e) {
			MaxLifeBox.BorderThickness = new Thickness(2);
		}

		private void TextBox_LostFocus_4(object sender, RoutedEventArgs e) {
			MinDamageBox.BorderThickness = new Thickness(0);
		}
		private void TextBox_GotFocus_4(object sender, RoutedEventArgs e) {
			MinDamageBox.BorderThickness = new Thickness(2);
		}

		private void TextBox_LostFocus_5(object sender, RoutedEventArgs e) {
			MaxDamageBox.BorderThickness = new Thickness(0);
		}
		private void TextBox_GotFocus_5(object sender, RoutedEventArgs e) {
			MaxDamageBox.BorderThickness = new Thickness(2);
		}

		private void TextBox_LostFocus_6(object sender, RoutedEventArgs e) {
			MinManaBox.BorderThickness = new Thickness(0);
		}
		private void TextBox_GotFocus_6(object sender, RoutedEventArgs e) {
			MinManaBox.BorderThickness = new Thickness(2);
		}

		private void TextBox_LostFocus_7(object sender, RoutedEventArgs e) {
			MaxManaBox.BorderThickness = new Thickness(0);
		}
		private void TextBox_GotFocus_7(object sender, RoutedEventArgs e) {
			MaxManaBox.BorderThickness = new Thickness(2);
		}
	}
}
