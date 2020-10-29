using System;
using System.Collections.Generic;
using System.Text;

namespace Card_Creator {
	public class Card {
		public Card(string name, string[] type, int life, int damage, int mana) {
			Name = name;
			TypeName = type[0];
			TypeColor = type[1];
			Life = life;
			Damage = damage;
			Mana = mana;
		}
		public string Name { get; set; }
		public string TypeName { get; set; }
		public string TypeColor { get; set; }
		public int Life { get; set; }
		public int Damage { get; set; }
		public int Mana { get; set; }
	}
}
