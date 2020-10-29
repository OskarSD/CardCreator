using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Card_Creator {
	class TypeElement {
		public TypeElement(Type type) {
			TypeI = type;
		}
		public Type TypeI { get; set; }
		public Border DisplayType() {
			Border border = new Border {
				BorderThickness = new Thickness(2),
				BorderBrush = Brushes.White
			};
			TextBlock name = new TextBlock {
				Text = TypeI.Name,
				FontSize = 14,
				FontWeight = FontWeights.Light,
				Foreground = Brushes.White,
				HorizontalAlignment = HorizontalAlignment.Center,
				VerticalAlignment = VerticalAlignment.Center
			};
			TextBlock desc = new TextBlock {
				Text = TypeI.Description,
				FontSize = 10,
				FontWeight = FontWeights.Light,
				Foreground = Brushes.White,
				HorizontalAlignment = HorizontalAlignment.Center,
				VerticalAlignment = VerticalAlignment.Center
			};
			TextBlock life = new TextBlock {
				Text = "life",
				FontSize = 10,
				FontWeight = FontWeights.Light,
				Foreground = Brushes.White,
				HorizontalAlignment = HorizontalAlignment.Center,
				VerticalAlignment = VerticalAlignment.Center
			};
			TextBlock damage = new TextBlock {
				Text = "damage",
				FontSize = 10,
				FontWeight = FontWeights.Light,
				Foreground = Brushes.White,
				HorizontalAlignment = HorizontalAlignment.Center,
				VerticalAlignment = VerticalAlignment.Center
			};
			TextBlock mana = new TextBlock {
				Text = "mana",
				FontSize = 10,
				FontWeight = FontWeights.Light,
				Foreground = Brushes.White,
				HorizontalAlignment = HorizontalAlignment.Center,
				VerticalAlignment = VerticalAlignment.Center
			};
			TextBlock lifeMin = new TextBlock {
				Text = TypeI.LifeMin.ToString(),
				FontSize = 10,
				FontWeight = FontWeights.Light,
				Foreground = Brushes.White,
				HorizontalAlignment = HorizontalAlignment.Center,
				VerticalAlignment = VerticalAlignment.Center
			};
			TextBlock lifeMax = new TextBlock {
				Text = TypeI.LifeMax.ToString(),
				FontSize = 10,
				FontWeight = FontWeights.Light,
				Foreground = Brushes.White,
				HorizontalAlignment = HorizontalAlignment.Center,
				VerticalAlignment = VerticalAlignment.Center
			};
			TextBlock damageMin = new TextBlock {
				Text = TypeI.DamageMin.ToString(),
				FontSize = 10,
				FontWeight = FontWeights.Light,
				Foreground = Brushes.White,
				HorizontalAlignment = HorizontalAlignment.Center,
				VerticalAlignment = VerticalAlignment.Center
			};
			TextBlock damageMax = new TextBlock {
				Text = TypeI.DamageMax.ToString(),
				FontSize = 10,
				FontWeight = FontWeights.Light,
				Foreground = Brushes.White,
				HorizontalAlignment = HorizontalAlignment.Center,
				VerticalAlignment = VerticalAlignment.Center
			};
			TextBlock manaMin = new TextBlock {
				Text = TypeI.ManaMin.ToString(),
				FontSize = 10,
				FontWeight = FontWeights.Light,
				Foreground = Brushes.White,
				HorizontalAlignment = HorizontalAlignment.Center,
				VerticalAlignment = VerticalAlignment.Center
			};
			TextBlock manaMax = new TextBlock {
				Text = TypeI.ManaMax.ToString(),
				FontSize = 10,
				FontWeight = FontWeights.Light,
				Foreground = Brushes.White,
				HorizontalAlignment = HorizontalAlignment.Center,
				VerticalAlignment = VerticalAlignment.Center
			};
			Grid grid = new Grid();

			grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

			grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(3, GridUnitType.Star) });

			grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

			grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(2, GridUnitType.Star) });

			grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

			grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(2, GridUnitType.Star) });

			grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

			grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(2, GridUnitType.Star) });

			grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

			grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

			grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) });
			grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(3, GridUnitType.Star) });
			grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) });

			grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

			grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) });
			grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(3, GridUnitType.Star) });
			grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) });

			grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

			grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) });
			grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(3, GridUnitType.Star) });
			grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) });

			grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

			Grid.SetRow(name, 1);
			Grid.SetColumn(name, 1);
			Grid.SetColumnSpan(name, 3);
			Grid.SetRow(desc, 1);
			Grid.SetRowSpan(desc, 3);
			Grid.SetColumn(desc, 5);
			Grid.SetColumnSpan(desc, 7);

			grid.Children.Add(name);
			grid.Children.Add(desc);

			border.Child = grid;
			return border;
		}
	}
}
