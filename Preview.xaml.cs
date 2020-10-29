using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
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
using System.IO;

namespace Card_Creator {
	public partial class Preview : Page {
		Card card;
		public Preview(StreamReader file) {
			InitializeComponent();
			string json = file.ReadToEnd();
			card = JsonConvert.DeserializeObject<Card>(json);
			text1.Text = card.Name;
			text2.Text = card.TypeName;
			string info = "Life: " + card.Life + "\nDamage: " + card.Damage + "\nMana: " + card.Mana;
			text3.Text = info;
		}

	}
}
