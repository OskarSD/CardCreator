using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Card_Creator {
	public class Card {
		public Card(string name, string description, string typeName, string typeColor, int life, int damage, int mana, string portraitImagePath, string backgroundImagePath) {
			Name = name;
			Description = description;
			TypeName = typeName;
			TypeColor = typeColor;
			Life = life;
			Damage = damage;
			Mana = mana;
			PortraitImagePath = portraitImagePath;
			BackgroundImagePath = backgroundImagePath;
		}

		[Key]
		public string Name { get; set; }

		public string Description { get; set; }
		public string TypeName { get; set; }
		public string TypeColor { get; set; }
		public int Life { get; set; }
		public int Damage { get; set; }
		public int Mana { get; set; }
		public string PortraitImagePath { get; set; }
		public string BackgroundImagePath { get; set; }
	}
}
