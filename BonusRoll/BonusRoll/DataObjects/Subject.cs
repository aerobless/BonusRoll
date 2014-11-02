using System;

namespace BonusRoll
{
	public class Subject
	{
		public string Name{ get; set; }

		public DateTime LastInvocation{ get; set; }

		public Subject (string Name)
		{
			this.Name = Name;
		}
	}
}

