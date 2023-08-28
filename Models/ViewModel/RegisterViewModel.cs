using System.ComponentModel.DataAnnotations;

namespace SGF.Models.ViewModel
{
    public class RegisterViewModel
    {
      [Required(ErrorMessage = "Esse campo é obrigatório.")]
      [EmailAddress]
      public string Email { get; set; }

      [Required(ErrorMessage = "Esse campo é obrigatório.")]
      [DataType(DataType.Password)]
      public string Password { get; set; }

      [DataType(DataType.Password)]
      [Display(Name = "Confirme a senha")]
      [Compare("Password", ErrorMessage = "As senhas não conferem")]
      public string ConfirmPassword { get; set; }
    }
}
