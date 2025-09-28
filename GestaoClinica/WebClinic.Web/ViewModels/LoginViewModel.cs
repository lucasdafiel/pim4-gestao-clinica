using System.ComponentModel.DataAnnotations;

namespace WebClinic.Web.ViewModels
{
    /// <summary>
    /// Representa o objeto de transferência de dados (DTO) para a requisição de login.
    /// Esta classe define os dados que o cliente deve enviar para a API de autenticação.
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// O endereço de email do usuário que está tentando fazer login.
        /// [Required] garante que este campo não pode ser nulo ou vazio.
        /// [EmailAddress] valida se o formato do texto corresponde a um email válido.
        /// </summary>
        [Required(ErrorMessage = "O campo Email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O formato do email é inválido.")]
        public string Email { get; set; }

        /// <summary>
        /// A senha do usuário.
        /// [Required] garante que o campo de senha não pode ser nulo ou vazio.
        /// </summary>
        [Required(ErrorMessage = "O campo Senha é obrigatório.")]
        public string Senha { get; set; }
    }
}