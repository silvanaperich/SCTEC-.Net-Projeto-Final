using DeskFlow.Api.Models.Entities;
using DeskFlow.Api.Repositories.Interfaces;
using DeskFlow.Api.Services.Interfaces;

namespace DeskFlow.Api.Services
{
    public class ChamadosServices : IChamadosServices
    {
        private IChamadosRepository _chamadosRepository;

        public ChamadosServices(IChamadosRepository chamadosRepository)
        {
            _chamadosRepository = chamadosRepository;
        }

        public async Task Atualizar(int id, Chamado chamadoAtualizado)
        {
            Chamado chamado = await _chamadosRepository.ObterChamadoPorId(id);
            
            if (chamado == null)
            {
                //todo: criar exception personalizada
                throw new Exception("Chamado não encontrado");
            }
            chamado.Atualizar(chamadoAtualizado);
            await _chamadosRepository.Atualizar(chamado);
        }

        public async Task Cadastrar(Chamado chamado)
        {
            await _chamadosRepository.Cadastrar(chamado);
        }

        public async Task Excluir(int id)
        {
            Chamado chamado = await _chamadosRepository.ObterChamadoPorId(id);
            
            if (chamado != null)
            {
                await _chamadosRepository.Excluir(chamado);   
            }
        }

        public async Task<Chamado> ObterChamadoPorId(int id) => await _chamadosRepository.ObterChamadoPorId(id);

        public async Task<List<Chamado>> ObterTodos() => await _chamadosRepository.ObterTodos();
    }
}