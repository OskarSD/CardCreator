using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Card_Creator {
	public class CardElement {
		public CardElement(Card card) {
			CardI = card;
		}
		public Card CardI { get; set; }
		public Button DisplayCard() {

			//main button
			string nameEdit = Regex.Replace(CardI.Name, @"\s+", "");
			Button mainButton = new Button {
				Name = nameEdit,
				HorizontalContentAlignment = HorizontalAlignment.Stretch,
				VerticalContentAlignment = VerticalAlignment.Stretch
			};

			//background image
			ImageSource bgImage = new BitmapImage(new Uri(CardI.BackgroundImagePath));
			Image backgroundImage = new Image {
				Source = bgImage
			};

			//portrait image
			ImageSource pImage = new BitmapImage(new Uri(CardI.PortraitImagePath));
			Image portraitImage = new Image {
				Source = pImage
			};

			//name text
			TextBlock nameText = new TextBlock {
				Text = CardI.Name,
				FontSize = 16,
				FontWeight = FontWeights.Light,
				Foreground = Brushes.White,
				HorizontalAlignment = HorizontalAlignment.Center,
				VerticalAlignment = VerticalAlignment.Center
			};

			//description text
			TextBlock descriptionText = new TextBlock {
				Text = CardI.Description,
				FontSize = 14,
				FontWeight = FontWeights.Light,
				Foreground = Brushes.White,
				HorizontalAlignment = HorizontalAlignment.Center,
				TextWrapping = TextWrapping.Wrap
			};

			//type text
			TextBlock typeText = new TextBlock {
				Text = CardI.TypeName,
				FontSize = 12,
				FontWeight = FontWeights.Light,
				Foreground = Brushes.White,
				HorizontalAlignment = HorizontalAlignment.Center,
				VerticalAlignment = VerticalAlignment.Center
			};

			//type border
			Border typeBorder = new Border {
				BorderThickness = new Thickness(1),
				BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom(CardI.TypeColor)),
				CornerRadius = new CornerRadius(5, 5, 5, 5)
			};

			//life text
			TextBlock lifeText = new TextBlock {
				Text = CardI.Life.ToString(),
				FontSize = 12,
				FontWeight = FontWeights.Bold,
				Foreground = Brushes.White,
				HorizontalAlignment = HorizontalAlignment.Center,
				VerticalAlignment = VerticalAlignment.Center,
			};

			//damage text
			TextBlock damageText = new TextBlock {
				Text = CardI.Damage.ToString(),
				FontSize = 12,
				FontWeight = FontWeights.Bold,
				Foreground = Brushes.White,
				HorizontalAlignment = HorizontalAlignment.Center,
				VerticalAlignment = VerticalAlignment.Center
			};

			//mana text
			TextBlock manaText = new TextBlock {
				Text = CardI.Mana.ToString(),
				FontSize = 10,
				FontWeight = FontWeights.Bold,
				Foreground = Brushes.White,
				HorizontalAlignment = HorizontalAlignment.Center,
				VerticalAlignment = VerticalAlignment.Center
			};

			//main grid
			Grid mainGrid = new Grid();

			mainGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(2, GridUnitType.Star) });
			mainGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(14, GridUnitType.Star) });
			mainGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
			mainGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50, GridUnitType.Star) });
			mainGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
			mainGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(10, GridUnitType.Star) });
			mainGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
			mainGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(40, GridUnitType.Star) });
			mainGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
			mainGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(8, GridUnitType.Star) });
			mainGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(4, GridUnitType.Star) });
			mainGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(6, GridUnitType.Star) });

			mainGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) });
			mainGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(4, GridUnitType.Star) });
			mainGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(20, GridUnitType.Star) });
			mainGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(30, GridUnitType.Star) });
			mainGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(20, GridUnitType.Star) });
			mainGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(30, GridUnitType.Star) });
			mainGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(20, GridUnitType.Star) });
			mainGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(4, GridUnitType.Star) });
			mainGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

			Grid.SetRowSpan(backgroundImage, 12);
			Grid.SetColumnSpan(backgroundImage, 9);
			Grid.SetRow(nameText, 1);
			Grid.SetColumn(nameText, 1);
			Grid.SetColumnSpan(nameText, 7);
			Grid.SetRow(portraitImage, 3);
			Grid.SetColumn(portraitImage, 1);
			Grid.SetColumnSpan(portraitImage, 7);
			Grid.SetRow(typeBorder, 5);
			Grid.SetColumn(typeBorder, 3);
			Grid.SetColumnSpan(typeBorder, 3);
			Grid.SetRow(descriptionText, 7);
			Grid.SetColumn(descriptionText, 2);
			Grid.SetColumnSpan(descriptionText, 5);
			Grid.SetRow(lifeText, 9);
			Grid.SetRowSpan(lifeText, 2);
			Grid.SetColumn(lifeText, 1);
			Grid.SetColumnSpan(lifeText, 2);
			Grid.SetRow(damageText, 9);
			Grid.SetRowSpan(damageText, 2);
			Grid.SetColumn(damageText, 6);
			Grid.SetColumnSpan(damageText, 2);
			Grid.SetRow(manaText, 10);
			Grid.SetColumn(manaText, 4);

			Panel.SetZIndex(backgroundImage, 0);
			Panel.SetZIndex(portraitImage, 1);
			Panel.SetZIndex(nameText, 2);
			Panel.SetZIndex(typeBorder, 2);
			Panel.SetZIndex(descriptionText, 2);
			Panel.SetZIndex(lifeText, 2);
			Panel.SetZIndex(damageText, 2);
			Panel.SetZIndex(manaText, 2);

			mainButton.Content = mainGrid;

			typeBorder.Child = typeText;

			mainGrid.Children.Add(backgroundImage);
			mainGrid.Children.Add(nameText);
			mainGrid.Children.Add(portraitImage);
			mainGrid.Children.Add(typeBorder);
			mainGrid.Children.Add(descriptionText);
			mainGrid.Children.Add(lifeText);
			mainGrid.Children.Add(damageText);
			mainGrid.Children.Add(manaText);

			return mainButton;
		}
	}
}
