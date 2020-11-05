using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using System.IO;
using Card_Creator.Data;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Card_Creator {

	public partial class CardEditor : Page {
		MainWindow win = (MainWindow)Application.Current.MainWindow;
		CardCreatorContext context = new CardCreatorContext();
		string originalPath;
		bool isEditingExistingCard = false, selectedType = false;

		public CardEditor() {
			InitializeComponent();
			win.SaveCardButton.Click += SaveCardButton_Click;
			DisplayBackground();
			GenerateTypes();
			DisplayTypeButtons();
		}

		//locks the name if you're editing an existing card
		public void LockName() {
			NameText.Text = "Name (Cannot be changed)";
			NameBoxText.IsReadOnly = true;
			NameCover.Visibility = Visibility.Visible;
		}

		//function used to edit existing cards
		public void PremadeInputs(Card card) {
			LockName();
			TypeRules(card.TypeName, false);
			TypeButtonText.Text = card.TypeName;
			isEditingExistingCard = true;
			NameBoxText.Text = card.Name;
			DescriptionBoxText.Text = card.Description;
			TypeDisplay.Text = card.TypeName;
			TypeBorderDisplay.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom(card.TypeColor));
			LifeBoxText.Text = card.Life.ToString();
			DamageBoxText.Text = card.Damage.ToString();
			ManaBoxText.Text = card.Mana.ToString();
			originalPath = card.PortraitImagePath;
			ImageSource pImage = new BitmapImage(new Uri(originalPath));
			PortraitDisplay.Source = pImage;
			DisplayBackground();
		}

		//checks if all the user inputs are valid
		private bool IsCardValid() {
			if ((String.IsNullOrWhiteSpace(NameBoxText.Text)) ||
				(StartWithNumber(NameBoxText.Text)) ||
				(String.IsNullOrWhiteSpace(DescriptionBoxText.Text)) ||
				(StartWithNumber(DescriptionBoxText.Text)) ||
				(!int.TryParse(LifeBoxText.Text, out _)) ||
				(!int.TryParse(DamageBoxText.Text, out _)) ||
				(!int.TryParse(ManaBoxText.Text, out _)) || 
				(PortraitDisplay.Source == null) ||
				(!selectedType) ||
				(!VerifyRules())) {
				return false;
			}
			return true;
		}

		//checks if name is already being used
		private bool IsNameInUse(string name) {
			MessageBox.Show(name);
			if (context.Card.Find(name) != null) {
				if (name == context.Card.Find(name).Name) {
					return true;
				} else {
					return false;
				}
			}
			return false;
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

		//displays a popup depending on situation
		private void DisplayPopup(bool isValid, string situ) {
			if (isValid) {
				PopupBackground.Visibility = Visibility.Visible;
				ValidPopup.Visibility = Visibility.Visible;
			} else if (situ == "BadName") {
				PopupBackground.Visibility = Visibility.Visible;
				InvalidNamePopup.Visibility = Visibility.Visible;
			} else {
				PopupBackground.Visibility = Visibility.Visible;
				InvalidPopup.Visibility = Visibility.Visible;
			}
		}

		//saves card to database and saves image to a folder
		private void SaveCardButton_Click(object sender, RoutedEventArgs e) {
			if (!IsCardValid()) {
				DisplayPopup(false, "");
			} else if (IsNameInUse(NameBoxText.Text)) {
				DisplayPopup(false, "BadName");
			} else {
				SaveImage(originalPath);
				string filename = System.IO.Path.GetFileName(originalPath);
				string newPath = String.Format(ProcessPath() + "\\cardimages\\" + filename);
				string backgroundPath = String.Format(ProcessPath() + "\\cardimages\\CardBackground.png");
				Card newCard = CreateCard(newPath, backgroundPath);
				if (isEditingExistingCard) {
					Card oldCard = context.Card.Find(NameBoxText.Text);
					oldCard.Description = newCard.Description;
					oldCard.TypeColor = newCard.TypeColor;
					oldCard.TypeName = newCard.TypeName;
					oldCard.Life = newCard.Life;
					oldCard.Damage = newCard.Damage;
					oldCard.Mana = newCard.Mana;
					oldCard.PortraitImagePath = newCard.PortraitImagePath;

				} else {
					context.Card.Add(newCard);
				}
				context.SaveChanges();
				DisplayPopup(true, "");
			}
		}

		//gets the path into the project
		private string ProcessPath() {
			string processPath = System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
			return processPath;
		}

		//create the card
		private Card CreateCard(string portraitImagePath, string backgroundImagePath) {
			Card card = new Card(
				NameBoxText.Text, DescriptionBoxText.Text, 
				TypeDisplay.Text, TypeBorderDisplay.BorderBrush.ToString(),
				Convert.ToInt32(LifeBoxText.Text),
				Convert.ToInt32(DamageBoxText.Text),
				Convert.ToInt32(ManaBoxText.Text),
				portraitImagePath, backgroundImagePath);
			return card;
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
			foreach (Type type in types) {
				string nameEdit = Regex.Replace(type.Name, @"\s+", "");
				Button b = new Button {
					Name = nameEdit,
					Content = type.Name,
					FontSize = 14,
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

		//hides type buttons if the mouse leaves the type buttons area
		private void TypeLeave(object sender, MouseEventArgs e) {
			if ((!TypeStackPanel.IsMouseOver) &&
				(!TypeButton.IsMouseOver)) {
				TypeStackPanel.Visibility = Visibility.Hidden;
				Panel.SetZIndex(TypeScrollView, 0);
			}
		}

		//button for displaying and hiding types
		public void TypeButtonClick(object sender, EventArgs e) {
			DisplayTypeButtons();
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
			LifeCover.Visibility = Visibility.Hidden;
			DamageCover.Visibility = Visibility.Hidden;
			ManaCover.Visibility = Visibility.Hidden;
			selectedType = true;
			TypeButtonText.Text = b.Content.ToString();
			string typeColor = GetTypeColor(b.Content.ToString());
			TypeDisplay.Text = b.Content.ToString();
			TypeBorderDisplay.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom(typeColor));
			TypeRules(b.Content.ToString(), true);
			DisplayTypeButtons();
		}

		//gets the typeColor
		private string GetTypeColor(string nameType) {
			Type type = context.Type.Find(nameType);
			return type.ColorCode;
		}

		//image button
		private void ImageSelector_Click(object sender, RoutedEventArgs e) {
			DisplayImage(SelectImage());
		}

		//displays selected image
		private void DisplayImage(string filepath) {
			if (ImageCheck(filepath) == "Good") {
				ImageSource imgSource = new BitmapImage(new Uri(filepath));
				PortraitDisplay.Source = imgSource;
			} else if (ImageCheck(filepath) == "Bad") {
				BadImagePopup.Visibility = Visibility.Visible;
			} else if (ImageCheck(filepath) == "Canceled") {
				//dead end
			}
		}

		//checks if the selected image is valid
		private string ImageCheck(string filepath) {
			if (filepath == "Canceled") {
				return filepath;
			} else {
				int width, height;
				using (var stream = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.Read)) {
					var bitmapFrame = BitmapFrame.Create(stream, BitmapCreateOptions.DelayCreation, BitmapCacheOption.OnLoad);
					width = bitmapFrame.PixelWidth;
					height = bitmapFrame.PixelHeight;
					bitmapFrame = null;
				}
				if (!(width - height > 500) && !(width - height < -30)) {
					return "Good";
				} else {
					return "Bad";
				}
			}
		}

		//opens file explorer and returns the filepath of the selected image
		public string SelectImage() {
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "Image Files (*.png;*.jpg)|*.png;*.jpg";
			if (openFileDialog.ShowDialog() == true) {
				originalPath = openFileDialog.FileName;
				return openFileDialog.FileName;
			} else {
				return "Canceled";
			}
		}

		//saves the image to a folder inside the project
		public void SaveImage(string filepath) {
			string dirPath = ProcessPath() + "\\cardimages\\";
			string name = Path.GetFileName(filepath);
			string destinationPath = GetDestinationPath(name);
			if (!DoesImageExist(filepath, dirPath)) {
				File.Copy(filepath, destinationPath, true);
			}
		}

		//gets the new destination path from inside the project
		private string GetDestinationPath(string filename) {
			string appStartPath = String.Format(ProcessPath() + "\\cardimages\\" + filename);
			return appStartPath;
		}

		//checks if image already exists inside the directory
		private bool DoesImageExist(string filepath, string dirPath) {
			string name = System.IO.Path.GetFileName(filepath);
			DirectoryInfo directory = new DirectoryInfo(dirPath);
			FileInfo[] files = directory.GetFiles();
			foreach (FileInfo file in files) {
				if (String.Compare(file.Name, name) == 0) {
					return true;
				}
			}
			return false;
		}

		//reset button
		public void ResetButton(object sender, EventArgs e) {
			Refresh(false);
			RemovePopup();
		}

		//removes popups
		private void RemovePopup() {
			BadImagePopup.Visibility = Visibility.Hidden;
			PopupBackground.Visibility = Visibility.Hidden;
			ValidPopup.Visibility = Visibility.Hidden;
			InvalidPopup.Visibility = Visibility.Hidden;
			InvalidNamePopup.Visibility = Visibility.Hidden;
		}

		//displays the appropriate rules for the selected type
		private void TypeRules(string typeName, bool resetInputs) {
			var types = context.Type;
			foreach (Type type in types) {
				if (type.Name == typeName) {
					LifeText.Text = "Life (" + type.LifeMin + " - " + type.LifeMax + ")";
					DamageText.Text = "Damage (" + type.DamageMin + " - " + type.DamageMax + ")";
					ManaText.Text = "Mana (" + type.ManaMin + " - " + type.ManaMax + ")";
					if (resetInputs) {
						LifeBoxText.Text = "";
						DamageBoxText.Text = "";
						ManaBoxText.Text = "";
					}
					break;
				}
			}
		}

		//resets the displayed rules
		private void ResetRules() {
			LifeText.Text = "Life ()";
			DamageText.Text = "Damage ()";
			ManaText.Text = "Mana ()";
			LifeBoxText.Text = "";
			DamageBoxText.Text = "";
			ManaBoxText.Text = "";
		}

		//verifies that the rules are being followed
		private bool VerifyRules() {
			Type type = context.Type.Find(TypeDisplay.Text);
			if ((CompareNumbers(type.LifeMin.ToString(), LifeBoxText.Text)) &&
				(CompareNumbers(LifeBoxText.Text, type.LifeMax.ToString())) &&
				(CompareNumbers(type.DamageMin.ToString(), DamageBoxText.Text)) &&
				(CompareNumbers(DamageBoxText.Text, type.DamageMax.ToString())) &&
				(CompareNumbers(type.ManaMin.ToString(), ManaBoxText.Text)) &&
				(CompareNumbers(ManaBoxText.Text, type.ManaMax.ToString()))) {
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

		//defaults all the input values
		public void Refresh(bool selected) {
			selectedType = selected;
			NameCover.Visibility = Visibility.Hidden;
			NameBoxText.IsReadOnly = false;
			NameText.Text = "Name";
			TypeStackPanel.Children.Clear();
			GenerateTypes();
			RemovePopup();
			ResetRules();
			isEditingExistingCard = false;
			originalPath = "";
			TypeBorderDisplay.BorderBrush = Brushes.Transparent;
			TypeDisplay.Text = "";
			PortraitDisplay.Source = null;
			NameBoxText.Text = "";
			DescriptionBoxText.Text = "";
			if (!selected) {
				TypeButtonText.Text = "Type";
				LifeCover.Visibility = Visibility.Visible;
				DamageCover.Visibility = Visibility.Visible;
				ManaCover.Visibility = Visibility.Visible;
			} else {
				LifeCover.Visibility = Visibility.Hidden;
				DamageCover.Visibility = Visibility.Hidden;
				ManaCover.Visibility = Visibility.Hidden;
			}
		}

		//navigates to the preview page with the current card
		private void PreviewButton_Click(object sender, RoutedEventArgs e) {
			win.NavigatePreviewFromDatabase(context.Card.Find(NameDisplay.Text));
		}

		//displays the background image on the card displayer
		private void DisplayBackground() {
			string backgroundPath = String.Format(ProcessPath() + "\\cardimages\\CardBackground.png");
			ImageSource bgImage = new BitmapImage(new Uri(backgroundPath));
			BackgroundDisplay.Source = bgImage;
		}

		//navigates to the collection page
		private void CollectionButton_Click(object sender, RoutedEventArgs e) { win.NavigateCollection(); }

		//restart button
		private void RestartButton_Click(object sender, RoutedEventArgs e) { Refresh(false); }

		//ok button
		private void OKButton_Click(object sender, RoutedEventArgs e) { RemovePopup(); }

		//updates the displayed card info when input is changed
		private void NameBoxText_TextChanged(object sender, TextChangedEventArgs e) { NameDisplay.Text = NameBoxText.Text; }
		private void DescriptionBoxText_TextChanged(object sender, TextChangedEventArgs e) { DescriptionDisplay.Text = DescriptionBoxText.Text; }
		private void LifeBoxText_TextChanged(object sender, TextChangedEventArgs e) { LifeDisplay.Text = LifeBoxText.Text; }
		private void DamageBoxText_TextChanged(object sender, TextChangedEventArgs e) { DamageDisplay.Text = DamageBoxText.Text; }
		private void ManaBoxText_TextChanged(object sender, TextChangedEventArgs e) { ManaDisplay.Text = ManaBoxText.Text; }

		//this is used for the custom border/textbox combination on the page
		private void TextBox_LostFocus(object sender, RoutedEventArgs e) { NameBox.BorderThickness = new Thickness(0); }
		private void TextBox_GotFocus(object sender, RoutedEventArgs e) { NameBox.BorderThickness = new Thickness(2); }
		private void TextBox_LostFocus_1(object sender, RoutedEventArgs e) { DescriptionBox.BorderThickness = new Thickness(0); }
		private void TextBox_GotFocus_1(object sender, RoutedEventArgs e) { DescriptionBox.BorderThickness = new Thickness(2); }
		private void TextBox_LostFocus_2(object sender, RoutedEventArgs e) { LifeBox.BorderThickness = new Thickness(0); }
		private void TextBox_GotFocus_2(object sender, RoutedEventArgs e) { LifeBox.BorderThickness = new Thickness(2); }
		private void TextBox_LostFocus_3(object sender, RoutedEventArgs e) { DamageBox.BorderThickness = new Thickness(0); }
		private void TextBox_GotFocus_3(object sender, RoutedEventArgs e) { DamageBox.BorderThickness = new Thickness(2); }
		private void TextBox_LostFocus_4(object sender, RoutedEventArgs e) { ManaBox.BorderThickness = new Thickness(0); }
		private void TextBox_GotFocus_4(object sender, RoutedEventArgs e) { ManaBox.BorderThickness = new Thickness(2); }
	}
}
