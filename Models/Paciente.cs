// Local: /Models/Paciente.cs

namespace WebClinicSystem.Models; // Garante que esta classe pertence ao nosso projeto

public class Paciente
{
    public int Id { get; set; }
    public string NomeCompleto { get; set; }
    public string CPF { get; set; }
    public string Telefone { get; set; }
    public DateTime DataNascimento { get; set; }
}