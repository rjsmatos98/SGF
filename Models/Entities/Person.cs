using System.ComponentModel.DataAnnotations;

namespace SGF.Models.Entities
{
  public class Person
  {
    public int PersonId { get; set; }

    [Required, MaxLength(80, ErrorMessage = "Nome não pode exceder 80 caracteres")]
    public string Name { get; set; }

    [EmailAddress]
    [Required, MaxLength(120)]
    public string Email { get; set; }

    [Required, MaxLength(14)]
    public string CPF { get; set; }
  }
}