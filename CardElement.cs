using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Card_Creator {
	public class CardElement {
		public CardElement(Card card) {
			CardI = card;
		}
		public Card CardI { get; set; }
		public Border DisplayCard() {
			Border b = new Border {
				BorderThickness = new Thickness(2),
				BorderBrush = Brushes.White
			};
			Grid g = new Grid();
			Border nb = new Border {
				BorderThickness = new Thickness(1),
				BorderBrush = Brushes.White
			};
			Border ib = new Border {
				BorderThickness = new Thickness(1),
				BorderBrush = Brushes.White
			};
			Border tb = new Border {
				Background = (SolidColorBrush)(new BrushConverter().ConvertFrom(CardI.TypeColor)),
				BorderThickness = new Thickness(1),
				BorderBrush = Brushes.White,
				CornerRadius = new CornerRadius(5, 5, 5, 5)
			};
			Border ab = new Border {
				Margin = new Thickness(-1, 0, 0, -1),
				BorderThickness = new Thickness(1),
				BorderBrush = Brushes.White,
				CornerRadius = new CornerRadius(0, 15, 0, 0)
			};
			Border lb = new Border {
				Margin = new Thickness(0, 0, -1, -1),
				BorderThickness = new Thickness(1),
				BorderBrush = Brushes.White,
				CornerRadius = new CornerRadius(15, 0, 0, 0)
			};
			Border mb = new Border {
				Margin = new Thickness(0, 0, 0, -1),
				BorderThickness = new Thickness(1),
				BorderBrush = Brushes.White,
				CornerRadius = new CornerRadius(5, 5, 0, 0)
			};
			TextBlock nbt = new TextBlock {
				Text = CardI.Name,
				FontSize = 14,
				FontWeight = FontWeights.Light,
				Foreground = Brushes.White,
				HorizontalAlignment = HorizontalAlignment.Center,
				VerticalAlignment = VerticalAlignment.Center
			};
			TextBlock tbt = new TextBlock {
				Text = CardI.TypeName,
				FontSize = 14,
				FontWeight = FontWeights.Light,
				Foreground = Brushes.White,
				HorizontalAlignment = HorizontalAlignment.Center,
				VerticalAlignment = VerticalAlignment.Center
			};
			TextBlock abt = new TextBlock {
				Text = CardI.Damage.ToString(),
				FontSize = 18,
				FontWeight = FontWeights.Bold,
				Foreground = Brushes.White,
				HorizontalAlignment = HorizontalAlignment.Center,
				VerticalAlignment = VerticalAlignment.Center,
			};
			TextBlock lbt = new TextBlock {
				Text = CardI.Life.ToString(),
				FontSize = 18,
				FontWeight = FontWeights.Bold,
				Foreground = Brushes.White,
				HorizontalAlignment = HorizontalAlignment.Center,
				VerticalAlignment = VerticalAlignment.Center
			};
			TextBlock mbt = new TextBlock {
				Text = CardI.Mana.ToString(),
				FontSize = 16,
				FontWeight = FontWeights.Bold,
				Foreground = Brushes.White,
				HorizontalAlignment = HorizontalAlignment.Center,
				VerticalAlignment = VerticalAlignment.Center
			};

			g.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
			g.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) });
			g.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) });
			g.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
			g.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(3, GridUnitType.Star) });
			g.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
			g.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) });
			g.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) });
			g.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
			g.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
			g.RowDefinitions.Add(new RowDefinition { Height = new GridLength(3, GridUnitType.Star) });
			g.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
			g.RowDefinitions.Add(new RowDefinition { Height = new GridLength(12, GridUnitType.Star) });
			g.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
			g.RowDefinitions.Add(new RowDefinition { Height = new GridLength(3, GridUnitType.Star) });
			g.RowDefinitions.Add(new RowDefinition { Height = new GridLength(14, GridUnitType.Star) });
			g.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
			g.RowDefinitions.Add(new RowDefinition { Height = new GridLength(4, GridUnitType.Star) });
			Grid.SetColumn(nb, 1);
			Grid.SetColumnSpan(nb, 7);
			Grid.SetRow(nb, 1);
			Grid.SetColumn(ib, 1);
			Grid.SetColumnSpan(ib, 7);
			Grid.SetRow(ib, 3);
			Grid.SetColumn(tb, 3);
			Grid.SetColumnSpan(tb, 3);
			Grid.SetRow(tb, 5);
			//Grid.SetRow(d, 6);
			Grid.SetColumn(ab, 0);
			Grid.SetColumnSpan(ab, 2);
			Grid.SetRow(ab, 7);
			Grid.SetRowSpan(ab, 2);
			Grid.SetColumn(lb, 7);
			Grid.SetColumnSpan(lb, 2);
			Grid.SetRow(lb, 7);
			Grid.SetRowSpan(lb, 2);
			Grid.SetColumn(mb, 4);
			Grid.SetRow(mb, 8);

			b.Child = g;

			g.Children.Add(nb); //name border
			g.Children.Add(ib); //image border
			g.Children.Add(tb); //type border
			//g.Children.Add(d); //description
			g.Children.Add(ab); //attack border
			g.Children.Add(lb); //life border
			g.Children.Add(mb); //mana border

			nb.Child = nbt;
			//ib.Child = Image;
			tb.Child = tbt;
			ab.Child = abt;
			lb.Child = lbt;
			mb.Child = mbt;

			return b;
		}

	}
}
