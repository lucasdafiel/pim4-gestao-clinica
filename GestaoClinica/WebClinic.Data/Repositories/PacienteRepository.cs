using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebClinic.Data.Context;
using WebClinic.Core.Interfaces;
using WebClinic.Core.Models;
using Microsoft.EntityFrameworkCore; // Adicione esta referência para DbUpdateException

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
            try
            {
                _context.Pacientes.Add(paciente);
                _context.SaveChanges(); // Salva as mudanças no banco de dados
            }
            catch (DbUpdateException ex)
            {
                // Esta é a parte mais importante. Vamos capturar o erro detalhado.
                // Coloque um breakpoint na linha abaixo para inspecionar a exceção 'ex'
                // ou lance uma nova exceção para ver a mensagem completa no console.
                throw new Exception($"Ocorreu um erro ao salvar no banco de dados. Veja a exceção interna para detalhes.", ex);
            }
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

        public void Atualizar(Paciente pacienteAtualizado)
        {
            var pacienteExistente = ObterPorId(pacienteAtualizado.PacienteId);

            if (pacienteExistente != null)
            {
                pacienteExistente.NomeCompleto = pacienteAtualizado.NomeCompleto;
                pacienteExistente.CPF = pacienteAtualizado.CPF;
                pacienteExistente.DataNascimento = pacienteAtualizado.DataNascimento;
                pacienteExistente.TelefoneContato = pacienteAtualizado.TelefoneContato;
                pacienteExistente.Email = pacienteAtualizado.Email;

                _context.SaveChanges();
            }
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