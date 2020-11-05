using Newtonsoft.Json;
using System.Windows.Controls;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System;
using System.Windows;
using Card_Creator.Data;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;

namespace Card_Creator {
	public partial class Preview : Page {
		MainWindow win = (MainWindow)Application.Current.MainWindow;
		CardCreatorContext context = new CardCreatorContext();
		BitmapImage bitmapImage;

		public Preview() {
			InitializeComponent();
		}

		//display import related elements
		public void ImportSetup() {
			ImportGrid.Visibility = Visibility.Visible;
			DatabaseGrid.Visibility = Visibility.Hidden;
		}

		//display database related elements
		public void DatabaseSetup() {
			DatabaseGrid.Visibility = Visibility.Visible;
			ImportGrid.Visibility = Visibility.Hidden;
		}

		//creates the card object
		private Card CreateCard(string path) {
			string backgroundPath = String.Format(System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + "\\cardimages\\CardBackground.png");
			Card card = new Card(
				NameDisplay.Text, DescriptionDisplay.Text,
				TypeDisplay.Text, TypeBorderDisplay.BorderBrush.ToString(),
				Convert.ToInt32(LifeDisplay.Text),
				Convert.ToInt32(DamageDisplay.Text),
				Convert.ToInt32(ManaDisplay.Text),
				path, backgroundPath);
			return card;
		}

		//gets card information
		public void GetCard(Card card, bool isImported) {
			if (isImported) {
				bitmapImage = win.ConvertFromBase64(card.PortraitImagePath);
			}
			FillDisplay(card, isImported);
		}

		//fills up the displayed card with information
		private void FillDisplay(Card card, bool isImported) {
			NameDisplay.Text = card.Name;
			DescriptionDisplay.Text = card.Description;
			TypeDisplay.Text = card.TypeName;
			LifeDisplay.Text = card.Life.ToString();
			DamageDisplay.Text = card.Damage.ToString();
			ManaDisplay.Text = card.Mana.ToString();
			TypeBorderDisplay.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom(card.TypeColor));
			Image img = new Image();
			if (isImported) {
				PortraitDisplay.Source = win.ConvertFromBase64(card.PortraitImagePath);
				BackgroundDisplay.Source = win.ConvertFromBase64(card.BackgroundImagePath);
			} else {
				PortraitDisplay.Source = new BitmapImage(new Uri(card.PortraitImagePath));
				BackgroundDisplay.Source = new BitmapImage(new Uri(card.BackgroundImagePath));
			}
		}

		private void ExportButton_Click(object sender, RoutedEventArgs e) {
			win.ExportCard(CreateCard(PortraitDisplay.Source.ToString()), "Preview");
		}

		//checks if name is already being used
		private bool IsNameInUse(string name) {
			if (context.Card.Find(name) != null) {
				if (name == context.Card.Find(name).Name) {
					return true;
				} else {
					return false;
				}
			}
			return false;
		}

		private void EditButton_Click(object sender, RoutedEventArgs e) {
			win.NavigateCardEditorPremade(context.Card.Find(NameDisplay.Text));
		}

		private void DeleteButton_Click(object sender, RoutedEventArgs e) {
			context.Card.Remove(context.Card.Find(NameDisplay.Text));
			context.SaveChanges();
			DeletionPopup();
		}

		private void CollectionButton_Click(object sender, RoutedEventArgs e) {
			RemovePopups();
			win.NavigateCollection();
		}

		private void SaveButton_Click(object sender, RoutedEventArgs e) {
			if (IsNameInUse(NameDisplay.Text)) {
				BadNamePopup();
			} else {
				string nameEdit = Regex.Replace(NameDisplay.Text, @"\s+", "");
				string path = String.Format(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + "\\cardimages\\" + nameEdit + ".png");
				int i = 0;
				while (File.Exists(path)) {
					path = String.Format(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + "\\cardimages\\" + nameEdit + "(" + i + ").png");
					i++;
				}

				using (FileStream stream = new FileStream(path, FileMode.Create)) {
					PngBitmapEncoder encoder = new PngBitmapEncoder();
					encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
					encoder.Save(stream);
				}

				Card card = CreateCard(path);
				context.Card.Add(card);
				context.SaveChanges();
				SavedPopup();
			}
		}

		private void CancelButton_Click(object sender, RoutedEventArgs e) {
			win.NavigateCollection();
		}

		private void BadNamePopup() {
			InvalidNamePopup.Visibility = Visibility.Visible;
		}

		private void SavedPopup() {
			SavePopup.Visibility = Visibility.Visible;
		}

		private void DeletionPopup() {
			DeletePopup.Visibility = Visibility.Visible;
		}

		public void OKButton_Click(object sender, RoutedEventArgs e) {
			RemovePopups();
		}

		private void RemovePopups() {
			InvalidNamePopup.Visibility = Visibility.Hidden;
			SavePopup.Visibility = Visibility.Hidden;
			DeletePopup.Visibility = Visibility.Hidden;
		}
	}
}
