using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Card_Creator {
	public class Card {
		public Card(string name, string typeName, string typeColor, int life, int damage, int mana) {
			Name = name;
			TypeName = typeName;
			TypeColor = typeColor;
			Life = life;
			Damage = damage;
			Mana = mana;
		}

		[Key]
		public string Name { get; set; }

		public string TypeName { get; set; }
		public string TypeColor { get; set; }
		public int Life { get; set; }
		public int Damage { get; set; }
		public int Mana { get; set; }
	}
}
