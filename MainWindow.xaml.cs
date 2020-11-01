using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Card_Creator {

	public partial class MainWindow : Window {
		CardEditor cardEditor;
		Collection collection;
		TypeEditor typeEditor;
		Types types;
		public MainWindow() {
			InitializeComponent();
			cardEditor = new CardEditor();
			collection = new Collection();
			typeEditor = new TypeEditor();
			types = new Types();
			Main.Content = cardEditor;
			DisplayCardButtons();
			DisplayTypeButtons();
		}

		public void NavigateCardEditor() {
			cardEditor.Refresh();
			Main.Content = cardEditor; 
		}
		public void NavigateTypeEditor() {
			typeEditor.Refresh();
			Main.Content = typeEditor;
		}
		public void NavigateCollection() {
			collection.RefreshPage();
			Main.Content = collection; 
		}
		public void NavigateTypes() {
			types.RefreshPage();
			Main.Content = types;
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
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "JSON Files (*.json)|*.json";
			if (openFileDialog.ShowDialog() == true) {
				StreamReader file = new StreamReader(openFileDialog.FileName);
				Main.Content = new Preview(file);
			}
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
