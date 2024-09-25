using System;
namespace to_do_api.Modules
{
	public class Card
	{
		public Guid Id { get; set; }
		public string Text { get; set; }
		public bool IsSelected { get; set; }

		public Card(string Text)
		{
			this.Text = Text;
            
		}

	}
}