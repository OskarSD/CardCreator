using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Card_Creator {
	public class TypeElement {
		public TypeElement(Type type) {
			TypeI = type;
		}
		public Type TypeI { get; set; }
		public Button DisplayType() {

			//main button
			string nameEdit = Regex.Replace(TypeI.Name, @"\s+", "");
			Button button = new Button {
				Name = nameEdit,
				HorizontalContentAlignment = HorizontalAlignment.Stretch,
				VerticalContentAlignment = VerticalAlignment.Stretch
			};
			Border border = new Border {
				BorderThickness = new Thickness(1),
				BorderBrush = Brushes.White,
				Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#202020"))
			};
			TextBlock name = new TextBlock {
				Text = TypeI.Name,
				FontSize = 14,
				FontWeight = FontWeights.Light,
				Foreground = Brushes.White,
				HorizontalAlignment = HorizontalAlignment.Center,
				VerticalAlignment = VerticalAlignment.Center
			};
			Border descBorder = new Border {
				BorderThickness = new Thickness(),
				Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#2f2f2f"))
			};
			TextBlock desc = new TextBlock {
				Text = TypeI.Description,
				FontSize = 11,
				FontWeight = FontWeights.Light,
				Foreground = Brushes.White,
				HorizontalAlignment = HorizontalAlignment.Center,
				VerticalAlignment = VerticalAlignment.Center,
				TextWrapping = TextWrapping.Wrap
			};
			TextBlock life = new TextBlock {
				Text = "life",
				FontSize = 13,
				FontWeight = FontWeights.Light,
				Foreground = Brushes.White,
				HorizontalAlignment = HorizontalAlignment.Center,
				VerticalAlignment = VerticalAlignment.Center
			};
			TextBlock damage = new TextBlock {
				Text = "damage",
				FontSize = 13,
				FontWeight = FontWeights.Light,
				Foreground = Brushes.White,
				HorizontalAlignment = HorizontalAlignment.Center,
				VerticalAlignment = VerticalAlignment.Center
			};
			TextBlock mana = new TextBlock {
				Text = "mana",
				FontSize = 13,
				FontWeight = FontWeights.Light,
				Foreground = Brushes.White,
				HorizontalAlignment = HorizontalAlignment.Center,
				VerticalAlignment = VerticalAlignment.Center
			};
			TextBlock lifeRange = new TextBlock {
				Text = TypeI.LifeMin.ToString() + " - " + TypeI.LifeMax.ToString(),
				FontSize = 12,
				FontWeight = FontWeights.Light,
				Foreground = Brushes.White,
				HorizontalAlignment = HorizontalAlignment.Center,
				VerticalAlignment = VerticalAlignment.Center
			};
			TextBlock damageRange = new TextBlock {
				Text = TypeI.DamageMin.ToString() + " - " + TypeI.DamageMax.ToString(),
				FontSize = 12,
				FontWeight = FontWeights.Light,
				Foreground = Brushes.White,
				HorizontalAlignment = HorizontalAlignment.Center,
				VerticalAlignment = VerticalAlignment.Center
			};
			TextBlock manaRange = new TextBlock {
				Text = TypeI.ManaMin.ToString() + " - " + TypeI.ManaMax.ToString(),
				FontSize = 12,
				FontWeight = FontWeights.Light,
				Foreground = Brushes.White,
				HorizontalAlignment = HorizontalAlignment.Center,
				VerticalAlignment = VerticalAlignment.Center
			};
			Border colorBorder = new Border {
				BorderThickness = new Thickness(0),
				Background = (SolidColorBrush)(new BrushConverter().ConvertFrom(TypeI.ColorCode)),
			};

			Grid grid = new Grid();

			grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(4, GridUnitType.Star) });

			grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(12, GridUnitType.Star) });

			grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(4, GridUnitType.Star) });

			grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(8, GridUnitType.Star) });

			grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(3, GridUnitType.Star) });

			grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(9, GridUnitType.Star) });

			grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

			grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(8, GridUnitType.Star) });

			grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(4, GridUnitType.Star) });

			grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

			grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(7, GridUnitType.Star) });

			grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

			grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(7, GridUnitType.Star) });

			grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

			grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(7, GridUnitType.Star) });

			grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

			Grid.SetRow(name, 1);
			Grid.SetColumn(name, 1);
			Grid.SetRow(descBorder, 1);
			Grid.SetRowSpan(descBorder, 3);
			Grid.SetColumn(descBorder, 3);
			Grid.SetColumnSpan(descBorder, 3);
			Grid.SetRow(life, 5);
			Grid.SetColumn(life, 1);
			Grid.SetRow(damage, 5);
			Grid.SetColumn(damage, 3);
			Grid.SetRow(mana, 5);
			Grid.SetColumn(mana, 5);
			Grid.SetRow(lifeRange, 7);
			Grid.SetColumn(lifeRange, 1);
			Grid.SetRow(damageRange, 7);
			Grid.SetColumn(damageRange, 3);
			Grid.SetRow(manaRange, 7);
			Grid.SetColumn(manaRange, 5);

			Grid.SetRow(colorBorder, 3);
			Grid.SetColumn(colorBorder, 1);

			grid.Children.Add(name);
			grid.Children.Add(descBorder);
			descBorder.Child = desc;
			grid.Children.Add(life);
			grid.Children.Add(damage);
			grid.Children.Add(mana);
			grid.Children.Add(lifeRange);
			grid.Children.Add(damageRange);
			grid.Children.Add(manaRange);
			grid.Children.Add(colorBorder);

			button.Content = border;
			border.Child = grid;
			return button;
		}
	}
}
