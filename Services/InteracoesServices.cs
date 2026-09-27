using DeskFlow.Api.Models.Entities;
using DeskFlow.Api.Repositories.Interfaces;
using DeskFlow.Api.Services.Interfaces;

namespace DeskFlow.Api.Services
{
    public class InteracoesServices : IInteracoesServices
    {
        private IInteracoesRepository _interacoesRepository;

        public InteracoesServices(IInteracoesRepository interacoesRepository)
        {
            _interacoesRepository = interacoesRepository;
        }

        public async Task Atualizar(int id, Interacao interacaoAtualizada)
        {
            Interacao interacao = await _interacoesRepository.ObterInteracaoPorId(id);

            if (interacao == null)
            {
                //todo: criar exception personalizada
                throw new Exception("Interacao não encontrada");
            }

            interacao.Atualizar(interacaoAtualizada);
            await _interacoesRepository.Atualizar(interacao);
        }

        public async Task Cadastrar(Interacao interacao)
        {
            await _interacoesRepository.Cadastrar(interacao);
        }

        public async Task Excluir(int id)
        {
            Interacao interacao = await _interacoesRepository.ObterInteracaoPorId(id);

            if (interacao != null)
            {
                await _interacoesRepository.Excluir(interacao);
            }
        }

        public async Task<Interacao> ObterInteracaoPorId(int id) => await _interacoesRepository.ObterInteracaoPorId(id);

        public async Task<List<Interacao>> ObterTodas() => await _interacoesRepository.ObterTodas();
    }
}