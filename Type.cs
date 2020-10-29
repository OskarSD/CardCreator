using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;

namespace Card_Creator {
	public class Type {
		public Type(string name, string desc, string color, int lifeMin, int lifeMax, int damageMin, int damageMax, int manaMin, int manaMax) {
			Name = name;
			Description = desc;
			ColorCode = color;
			LifeMin = lifeMin;
			LifeMax = lifeMax;
			DamageMin = damageMin;
			DamageMax = damageMax;
			ManaMin = manaMin;
			ManaMax = manaMax;
		}
		public string Name { get; set; }
		public string Description { get; set; }
		public string ColorCode { get; set; }
		public int LifeMin { get; set; }
		public int LifeMax { get; set; }
		public int DamageMin { get; set; }
		public int DamageMax { get; set; }
		public int ManaMin { get; set; }
		public int ManaMax { get; set; }
	}
}
