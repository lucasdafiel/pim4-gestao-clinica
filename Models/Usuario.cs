// Local: /Models/Usuario.cs
namespace WebClinicSystem.Models;

public class Usuario
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Perfil { get; set; } = string.Empty; // Ex: Administrador, Recepcionista
}