using Card_Creator.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Card_Creator {
	public partial class Collection : Page {
		MainWindow win = (MainWindow)Application.Current.MainWindow;
		CardCreatorContext context = new CardCreatorContext();
		List<Card> FilteredCards = new List<Card>();
		List<Card> FilteredSearch = new List<Card>();
		List<Card> FilteredLife = new List<Card>();
		List<Card> FilteredDamage = new List<Card>();
		List<Card> FilteredMana = new List<Card>();
		List<Card> FilteredType = new List<Card>();
		List<int> LifeList = new List<int>();
		List<int> DamageList = new List<int>();
		List<int> ManaList = new List<int>();
		string typeFilter = "";


		public Collection() {
			InitializeComponent();
			LoadCards();
			DisplayTypeButtons();
		}

		//update filter when the search filter is changed
		private void SearchFilter_TextChanged(object sender, TextChangedEventArgs e) {
			FilterSearch(SearchFilter.Text);
			FilterCards();
			LoadCards();
		}

		//matches all the filtered card lists and returns the final filtered list
		private void FilterCards() {
			FilteredCards = FilteredSearch.Intersect(FilteredLife).ToList();
			FilteredCards = FilteredCards.Intersect(FilteredDamage).ToList();
			FilteredCards = FilteredCards.Intersect(FilteredMana).ToList();
			FilteredCards = FilteredCards.Intersect(FilteredType).ToList();
		}

		//filters out all cards with partially matching name
		private void FilterSearch(string searchFilter) {
			FilteredSearch.Clear();
			var cards = context.Card;
			foreach (Card card in cards) {
				if (card.Name.ToLower().Contains(searchFilter.ToLower())) {
					FilteredSearch.Add(card);
				}
			}
			if (FilteredSearch.Count == 0) {
				if (!(searchFilter.Length > 0)) {
					foreach (Card card in cards) {
						FilteredSearch.Add(card);
					}
				}
			}
		}
		
		//filters out all cards with matching life
		private void FilterLife(bool isNew) {
			if (isNew) {
				List<int> TempLifeList = new List<int>();
				LifeList = TempLifeList;
			};
			FilteredLife.Clear();
			var cards = context.Card;
			foreach (Card card in cards) {
				foreach (int life in LifeList) {
					if (life == card.Life) {
						FilteredLife.Add(card);
						break;
					}
					if (life == 9 && card.Life > 9) {
						FilteredLife.Add(card);
						break;
					}
				}
			}
			if (FilteredLife.Count == 0 && LifeList.Count == 0) {
				foreach (Card card in cards) {
					FilteredLife.Add(card);
				}
			}
		}

		//filters out all cards with matching damage
		private void FilterDamage(bool isNew) {
			if (isNew) {
				List<int> TempDamageList = new List<int>();
				DamageList = TempDamageList;
			}
			FilteredDamage.Clear();
			var cards = context.Card;
			foreach (Card card in cards) {
				foreach (int damage in DamageList) {
					if (damage == card.Damage) {
						FilteredDamage.Add(card);
						break;
					}
					if (damage == 9 && card.Damage > 9) {
						FilteredDamage.Add(card);
						break;
					}
				}
			}
			if (FilteredDamage.Count == 0 && DamageList.Count == 0) {
				foreach (Card card in cards) {
					FilteredDamage.Add(card);
				}
			}
		}

		//filters out all cards with matching mana
		private void FilterMana (bool isNew) {
			if (isNew) {
				List<int> TempManaList = new List<int>();
				ManaList = TempManaList;
			};
			FilteredMana.Clear();
			var cards = context.Card;
			foreach (Card card in cards) {
				foreach (int mana in ManaList) {
					if (mana == card.Mana) {
						FilteredMana.Add(card);
						break;
					}
					if (mana == 9 && card.Mana > 9) {
						FilteredMana.Add(card);
						break;
					}
				}
			}
			if (FilteredMana.Count == 0 && ManaList.Count == 0) {
				foreach (Card card in cards) {
					FilteredMana.Add(card);
				}
			}
		}

		//loads all the card elements on the page
		private void LoadCards() {
			CardViewPanel.Children.Clear();
			WrapPanel p = new WrapPanel {
				Orientation = Orientation.Horizontal,
				HorizontalAlignment = HorizontalAlignment.Center,
				ItemWidth = 220,
				ItemHeight = 300
			};
			CardViewPanel.Children.Add(p);
			foreach (Card card in FilteredCards) {
				Border b = new Border {
					Margin = new Thickness(5, 10, 5, 10),
				};
				p.Children.Add(b);

				CardElement cardE = new CardElement(card);
				Button cardB = cardE.DisplayCard();
				cardB.Click += CardB_Click;
				b.Child = cardB;
			}
		}

		//getting the correct card when selecting
		private void CardB_Click(object sender, RoutedEventArgs e) {
			var cards = context.Card;
			Button b = sender as Button;
			foreach (Card card in cards) {
				string nameEdit = Regex.Replace(card.Name, @"\s+", "");
				if (nameEdit == b.Name) {
					PopupText.Text = card.Name;
					break;
				}
			}
			PopupBackground.Visibility = Visibility.Visible;
			CardPopup.Visibility = Visibility.Visible;
		}

		//cancel button
		private void CancelButton_Click(object sender, RoutedEventArgs e) { HidePopup(); }

		//deletes selected type from database
		private void DeleteButton_Click(object sender, RoutedEventArgs e) {
			Card card = context.Card.Find(PopupText.Text);
			context.Card.Remove(card);
			context.SaveChanges();
			RefreshPage();
		}

		//refreshes all elements on the page with new cards
		public void RefreshPage() {
			ResetLists();
			FilterSearch("");
			FilterLife(true);
			FilterDamage(true);
			FilterMana(true);
			FilterType(true);
			FilterCards();
			LoadCards();
			GenerateTypes();
			HidePopup();
		}

		//resets all filtered lists
		private void ResetLists() {
			FilteredCards.Clear();
			FilteredSearch.Clear();
			FilteredLife.Clear();
			FilteredDamage.Clear();
			FilteredMana.Clear();
		}

		//removes popup
		private void HidePopup() {
			PopupBackground.Visibility = Visibility.Hidden;
			CardPopup.Visibility = Visibility.Hidden;
		}

		//sends the selected card to CardEditor
		private void EditButton_Click(object sender, RoutedEventArgs e) {
			win.NavigateCardEditorPremade(context.Card.Find(PopupText.Text));
		}

		private void PreviewButton_Click(object sender, RoutedEventArgs e) {
			win.NavigatePreviewFromDatabase(context.Card.Find(PopupText.Text));
		}

		private void ExportButton_Click(object sender, RoutedEventArgs e) {
			win.ExportCard(context.Card.Find(PopupText.Text), "Collection");
		}

		//life buttons which changes the FilteredLife list
		private void Life_Click(object sender, RoutedEventArgs e) {
			Button b = sender as Button;
			Brush background = (SolidColorBrush)b.Background;
			Brush activeBackground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#d14b4b")); //red
			Brush inactiveBackground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#363434")); //dark
			string numberString = b.Content.ToString();
			if (background.ToString() == activeBackground.ToString()) {
				b.Background = inactiveBackground;
				if (numberString == "9+") {
					LifeList.Remove(9);
				} else {
					int number = Int32.Parse(numberString);
					LifeList.Remove(number);
				}
			} else if (background.ToString() == inactiveBackground.ToString()) {
				b.Background = activeBackground;
				if (numberString == "9+") {
					LifeList.Add(9);
				} else {
					int number = Int32.Parse(numberString);
					LifeList.Add(number);
				}
			}
			FilterLife(false);
			FilterCards();
			LoadCards();
		}

		//damage buttons which changes the FilteredDamage list
		private void Damage_Click(object sender, RoutedEventArgs e) {
			Button b = sender as Button;
			Brush background = (SolidColorBrush)b.Background;
			Brush activeBackground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#cad14b")); //yellow
			Brush inactiveBackground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#363434")); //dark
			string numberString = b.Content.ToString();
			if (background.ToString() == activeBackground.ToString()) {
				b.Background = inactiveBackground;
				if (numberString == "9+") {
					DamageList.Remove(9);
				} else {
					int number = Int32.Parse(numberString);
					DamageList.Remove(number);
				}
			} else if (background.ToString() == inactiveBackground.ToString()) {
				b.Background = activeBackground;
				if (numberString == "9+") {
					DamageList.Add(9);
				} else {
					int number = Int32.Parse(numberString);
					DamageList.Add(number);
				}
			}
			FilterDamage(false);
			FilterCards();
			LoadCards();
		}

		//mana buttons which changes the FilteredMana list
		private void Mana_Click(object sender, RoutedEventArgs e) {
			Button b = sender as Button;
			Brush background = (SolidColorBrush)b.Background;
			Brush activeBackground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#4b7cd1")); //blue
			Brush inactiveBackground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#363434")); //dark
			string numberString = b.Content.ToString();
			if (background.ToString() == activeBackground.ToString()) {
				b.Background = inactiveBackground;
				if (numberString == "9+") {
					ManaList.Remove(9);
				} else {
					int number = Int32.Parse(numberString);
					ManaList.Remove(number);
				}
			} else if (background.ToString() == inactiveBackground.ToString()) {
				b.Background = activeBackground;
				if (numberString == "9+") {
					ManaList.Add(9);
				} else {
					int number = Int32.Parse(numberString);
					ManaList.Add(number);
				}
			}
			FilterMana(false);
			FilterCards();
			LoadCards();
		}

		//filters out cards that matches the selected type
		private void FilterType(bool isNew) {
			FilteredType.Clear();
			if (isNew) {
				typeFilter = "";
			}
			var cards = context.Card;
			foreach (Card card in cards) {
				if (card.TypeName == typeFilter) {
					FilteredType.Add(card);
				}
			}
			if (FilteredType.Count == 0 && typeFilter.Length == 0) {
				foreach (Card card in cards) {
					FilteredType.Add(card);
				}
			}
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
			Button deselectButton = new Button {
				Name = "",
				Content = "Deselect Type",
				FontSize = 15,
				FontWeight = FontWeights.Light,
				Foreground = Brushes.White,
				Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#303030")),
				BorderThickness = new Thickness(0)
			};
			deselectButton.Click += DeselectClick;
			p.Children.Add(deselectButton);
			foreach (Type type in types) {
				string nameEdit = Regex.Replace(type.Name, @"\s+", "");
				Button b = new Button {
					Name = nameEdit,
					Content = type.Name,
					FontSize = 15,
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

		//button for displaying and hiding types
		public void TypeButtonClick(object sender, EventArgs e) {
			DisplayTypeButtons();
		}

		//hides type buttons if the mouse leaves the type buttons area
		private void TypeLeave(object sender, MouseEventArgs e) {
			if ((!TypeStackPanel.IsMouseOver) &&
				(!TypeButton.IsMouseOver)) {
				TypeStackPanel.Visibility = Visibility.Hidden;
				Panel.SetZIndex(TypeScrollView, 0);
			}
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

		//type selection
		private void TypeClick(object sender, RoutedEventArgs e) {
			Button b = sender as Button;
			TypeButtonText.Text = b.Content.ToString();
			typeFilter = b.Content.ToString();
			FilterType(false);
			FilterCards();
			LoadCards();
			DisplayTypeButtons();
		}

		//deselect type
		private void DeselectClick(object sender, RoutedEventArgs e) {
			TypeButtonText.Text = "Type";
			FilterType(true);
			FilterCards();
			LoadCards();
			DisplayTypeButtons();
		}

		//this is used for the custom border/textbox combination on the page
		private void TextBox_GotFocus(object sender, RoutedEventArgs e) { SearchBox.BorderThickness = new Thickness(2); }
		private void TextBox_LostFocus(object sender, RoutedEventArgs e) { SearchBox.BorderThickness = new Thickness(0); }

	}
}
