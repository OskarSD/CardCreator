using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Card_Creator.Migrations;
using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Drawing;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Net.Http;
using System.Buffers.Text;
using Card_Creator.Data;
using System.Diagnostics;

namespace Card_Creator {

	public partial class MainWindow : Window {
		CardCreatorContext context = new CardCreatorContext();
		CardEditor cardEditor;
		TypeEditor typeEditor;
		Types types;
		Preview preview;
		public MainWindow() {
			InitializeComponent();
			cardEditor = new CardEditor();
			typeEditor = new TypeEditor();
			types = new Types();
			preview = new Preview();
			NavigateCardEditor();
			DisplayCardButtons();
			DisplayTypeButtons();
			DeleteOnStartup();
		}

		//deleted images which are not being used
		private void DeleteOnStartup() {
			var cards = context.Card;
			string path = String.Format(System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + "\\cardimages");
			string[] files = Directory.GetFiles(path);
			if (files.Length > 1) {
				foreach (string file in files) {
					bool delete = true;
					foreach (Card card in cards) {
						if (card.PortraitImagePath == System.IO.Path.GetFullPath(file) && System.IO.Path.GetFileName(file) != "CardBackground.png") {
							delete = false;
						}
					}
					if (delete && System.IO.Path.GetFileName(file) != "CardBackground.png") {
						File.Delete(file);
					}
				}
			}
		}

		public void NavigateCardEditor() {
			cardEditor.Refresh(false);
			Main.Content = cardEditor;
		}
		public void NavigateCardEditorPremade(Card card) {
			cardEditor.Refresh(true);
			cardEditor.PremadeInputs(card);
			Main.Content = cardEditor;
		}

		public void NavigateTypeEditor() {
			typeEditor.Refresh();
			Main.Content = typeEditor;
		}
		public void NavigateCollection() {
			Collection collection = new Collection();
			collection.RefreshPage();
			Main.Content = collection;
		}
		public void NavigateTypes() {
			types.RefreshPage();
			Main.Content = types;
		}

		//navigation function for imported cards
		public void NavigatePreviewFromImport() {
			Card importedCard = ImportCard();
			if (importedCard != null) {
				Main.Content = preview;
				preview.ImportSetup();
				preview.GetCard(importedCard, true);
			} 
		}

		//converts base64 to BitmapImage
		public BitmapImage ConvertFromBase64(string bgImage64) {
			byte[] binaryData = Convert.FromBase64String(bgImage64);
			BitmapImage bi = new BitmapImage();
			bi.BeginInit();
			bi.StreamSource = new MemoryStream(binaryData);
			bi.EndInit();
			return bi;
		}

		//navigation function for cards already in database
		public void NavigatePreviewFromDatabase(Card card) {
			Main.Content = preview;
			preview.DatabaseSetup();
			preview.GetCard(card, false);
		}

		public void TBDisplayCardClick(object sender, EventArgs e) { DisplayCardButtons(); }

		public void TBDisplayTypeClick(object sender, EventArgs e) { DisplayTypeButtons(); }

		public void TBTypesClick(object sender, EventArgs e) { NavigateTypes(); }

		public void TBCollectionClick(object sender, EventArgs e) { NavigateCollection(); }

		public void RemoveFocus(object sender, EventArgs e) { MainGrid.Focus(); }

		public void SaveCardClick(object sender, EventArgs e) { DisplayCardButtons(); }

		public void NewCardClick(object sender, EventArgs e) {
			NavigateCardEditor();
			DisplayCardButtons();
		}

		public void OpenCardClick(object sender, EventArgs e) {
			NavigatePreviewFromImport();
		}

		public void SaveImage(string filepath) {
			cardEditor.SaveImage(filepath);
		}

		//import card
		private Card ImportCard() {
			OpenFileDialog openFileDialog = new OpenFileDialog {
				Filter = "JSON Files (*.json)|*.json"
			};
			if (openFileDialog.ShowDialog() == true) {
				StreamReader file = new StreamReader(openFileDialog.FileName);
				string json = file.ReadToEnd();
				return JsonConvert.DeserializeObject<Card>(json);
			} else {
				return null;
			}
		}

		//export card
		public void ExportCard(Card card, string nav) {
			Card newCard = ConvertCard(card);
			string nameEdit = Regex.Replace(newCard.Name, @"\s+", "");
			string content = JsonConvert.SerializeObject(newCard);
			SaveJsonFile(content, nameEdit, nav);
		}

		//save file
		private void SaveJsonFile(string content, string name, string nav) {
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.Filter = "JSON File (*.json)|*.json";
			saveFileDialog.FileName = name;
			saveFileDialog.DefaultExt = "json";
			saveFileDialog.RestoreDirectory = true;
			if (saveFileDialog.ShowDialog() == true) {
				File.WriteAllText(saveFileDialog.FileName, content);
			} else if (nav == "Collection") {
				NavigateCollection();
			}
		}

		//convert card so you can export it
		private Card ConvertCard(Card card) {
			Card newCard = card;
			newCard.PortraitImagePath = ToBase64(card.PortraitImagePath);
			newCard.BackgroundImagePath = ToBase64(card.BackgroundImagePath);
			return newCard;
		}

		//base64 converter
		private string ToBase64(string path) {
			string filename = System.IO.Path.GetFileName(path);
			string name = System.IO.Path.GetFileName(filename);
			filename = Directory.GetParent(filename).ToString() + "\\cardimages\\" + name;
			byte[] imageBytes = File.ReadAllBytes(filename);
			string base64String = Convert.ToBase64String(imageBytes);
			return base64String;
		}

		public void TBCardLeave(object sender, EventArgs e) {
			if ((!CardButton.IsMouseOver) &&
				(!NewCardButton.IsMouseOver) &&
				(!SaveCardButton.IsMouseOver) &&
				(!OpenCardButton.IsMouseOver)) {
				NewCardButton.Visibility = Visibility.Hidden;
				SaveCardButton.Visibility = Visibility.Hidden;
				OpenCardButton.Visibility = Visibility.Hidden;
			}
		}

		private void DisplayCardButtons() {
			if (NewCardButton.Visibility == Visibility.Hidden) {
				NewCardButton.Visibility = Visibility.Visible;
				SaveCardButton.Visibility = Visibility.Visible;
				OpenCardButton.Visibility = Visibility.Visible;
			} else {
				NewCardButton.Visibility = Visibility.Hidden;
				SaveCardButton.Visibility = Visibility.Hidden;
				OpenCardButton.Visibility = Visibility.Hidden;
			}
		}

		public void NewTypeClick(object sender, EventArgs e) {
			NavigateTypeEditor();
			DisplayTypeButtons();
		}

		public void SaveTypeClick(object sender, EventArgs e) {
			DisplayTypeButtons();
		}

		public void TBTypeLeave(object sender, EventArgs e) {
			if ((!TypeButton.IsMouseOver) &&
				(!NewTypeButton.IsMouseOver) &&
				(!SaveTypeButton.IsMouseOver)) {
				NewTypeButton.Visibility = Visibility.Hidden;
				SaveTypeButton.Visibility = Visibility.Hidden;
			}
		}

		private void DisplayTypeButtons() {
			if (NewTypeButton.Visibility == Visibility.Hidden) {
				NewTypeButton.Visibility = Visibility.Visible;
				SaveTypeButton.Visibility = Visibility.Visible;
			} else {
				NewTypeButton.Visibility = Visibility.Hidden;
				SaveTypeButton.Visibility = Visibility.Hidden;
			}
		}

	}
}
