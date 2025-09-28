using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebClinic.Data.Context;
using WebClinic.Core.Interfaces;
using WebClinic.Core.Models;

namespace WebClinic.Data.Repositories
{
    public class PacienteRepository : IPacienteRepository
    {
        private readonly WebClinicContext _context;

        public PacienteRepository(WebClinicContext context)
        {
            _context = context;
        }

        public void Adicionar(Paciente paciente)
        {
            _context.Pacientes.Add(paciente);
            _context.SaveChanges(); // Salva as mudanças no banco de dados
        }

        public Paciente? ObterPorId(int id)
        {
            return _context.Pacientes.Find(id);
        }

        public Paciente? ObterPorCPF(string cpf)
        {
            return _context.Pacientes.FirstOrDefault(p => p.CPF == cpf);
        }

        public List<Paciente> ListarTodos()
        {
            return _context.Pacientes.ToList();
        }

        public void Atualizar(Paciente paciente)
        {
            _context.Pacientes.Update(paciente);
            _context.SaveChanges();
        }

        public void Excluir(int id)
        {
            var paciente = ObterPorId(id);
            if (paciente is not null)
            {
                _context.Pacientes.Remove(paciente);
                _context.SaveChanges();
            }
        }
    }
}