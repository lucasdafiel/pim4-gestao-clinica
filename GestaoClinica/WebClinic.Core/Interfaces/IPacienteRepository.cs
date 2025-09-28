using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebClinic.Core.Models;

namespace WebClinic.Core.Interfaces
{
    public interface IPacienteRepository
    {
        void Adicionar(Paciente paciente);
        Paciente? ObterPorId(int id);
        Paciente? ObterPorCPF(string cpf);
        List<Paciente> ListarTodos();
        void Atualizar(Paciente paciente);
        void Excluir(int id);
    }
}
