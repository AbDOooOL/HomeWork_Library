using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_Library.models
{
	public class Book
	{
		public int Id { get; set; }
		public string? TitleBook { get; set; }
		public string? AuthorName { get; set; }
		public int NumberCopies { get; set; }
		public DateTime DatePublication { get; set; } = DateTime.Now;
		public double price { get; set; }

    }
}
