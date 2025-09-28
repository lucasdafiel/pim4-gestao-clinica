using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebClinic.Core.Interfaces;
using WebClinic.Web.Services;
using WebClinic.Web.ViewModels; // Supondo que você tem o LoginViewModel da resposta anterior

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ITokenService _tokenService;

    public AuthController(IUsuarioRepository usuarioRepository, ITokenService tokenService)
    {
        _usuarioRepository = usuarioRepository;
        _tokenService = tokenService;
    }

    /// <summary>
    /// Endpoint para autenticar um usuário e retornar um token JWT.
    /// </summary>
    [HttpPost("login")]
    [AllowAnonymous] // Permite que este endpoint seja acessado sem autenticação.
    public async Task<IActionResult> Login([FromBody] LoginViewModel loginViewModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Busca o usuário pelo email (já incluindo o perfil).
        var usuario = await _usuarioRepository.GetByEmailAsync(loginViewModel.Email);

        // Verifica se o usuário existe e se está ativo.
        if (usuario == null || !usuario.Ativo)
        {
            return Unauthorized(new { message = "Email ou senha inválidos." });
        }

        // Compara a senha enviada com o hash salvo no banco usando BCrypt.
        // Esta é a forma correta e segura de verificar a senha.
        var isPasswordValid = BCrypt.Net.BCrypt.Verify(loginViewModel.Senha, usuario.SenhaHash);

        if (!isPasswordValid)
        {
            return Unauthorized(new { message = "Email ou senha inválidos." });
        }

        // Se a autenticação for bem-sucedida, gera o token.
        var token = _tokenService.GenerateToken(usuario);

        // Retorna o token para o cliente.
        return Ok(new { token = token });
    }
}