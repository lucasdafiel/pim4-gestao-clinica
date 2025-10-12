// Local: /Models/Profissional.cs

namespace WebClinicSystem.Models;

public class Profissional
{
    public int Id { get; set; } 
    public string NomeCompleto { get; set; } = string.Empty;
    public string Especialidade { get; set; } =  string.Empty;
    public string Email { get; set; }  = string.Empty;
}