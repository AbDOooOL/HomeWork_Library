using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HomeWork_Library.models
{
	public class User
	{
		public int Id { get; set; }

		[MaxLength(30)]
		public string? UserName { get; set; }

		[MaxLength(30)]
		[PasswordPropertyText]
        public string? Password{ get; set; }

    }
}
