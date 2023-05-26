using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoAppServer.Library.Models;
public class RefreshToken
{
	[Key]
	public long Id { get; set; }

	[Required]
	[MaxLength(88)]
	[Column(TypeName = "varchar(88)")] //88 is the length of a Base64String generated from 66 bytes of data.
	public string Token { get; set; } = string.Empty;

	[Required]
	public DateTime Created { get; set; }

	[Required]
	public DateTime Expires { get; set; }


	[Required]
	public long DeviceId { get; set; }


	public override int GetHashCode()
		=> this.Token.GetHashCode();

	public override bool Equals(object? obj)
		=> (obj is RefreshToken token) &&
			Equals(token);

	public bool Equals(RefreshToken token)
		=> token.Token.Equals(this.Token) &&
			token.Created.Equals(this.Created) &&
			token.Expires.Equals(this.Expires);
}
