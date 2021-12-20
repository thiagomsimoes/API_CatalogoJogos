using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Api_CatalogoJogos.Repositories;
using Api_CatalogoJogos.Exceptions;
using Api_CatalogoJogos.ViewModel;
using Api_CatalogoJogos.InputModel;
using Api_CatalogoJogos.Entities;

namespace Api_CatalogoJogos.Services
{
    public class JogoService : IJogoService
    {

        //DI: Injeção de dependência (desacoplamento do código)

        private readonly IJogoRepository _jogoRepository;

        public JogoService(IJogoRepository jogoRepository)
        {
            var _jogoRepository = jogoRepository;
        }

        public async Task<List<JogoViewModel>> Obter(int pagina, int quantidade)
        {
            var jogos = await _jogoRepository.Obter(pagina, quantidade);


            // Linq => Para cada jogo será criado uma JogoViewModel
            return jogos.Select(jogo => new JogoViewModel
                                {
                                    Id = jogo.Id,
                                    Nome = jogo.Nome,
                                    Produtora = jogo.Produtora,
                                    Preco = jogo.Preco

                                })
                                .ToList();
        }
        public async Task<JogoViewModel> Obter(Guid id)
        {
            var jogo = await _jogoRepository.Obter(id);

            if (jogo == null)
            {
                return null;
            }

            return new JogoViewModel
            {
                Id = jogo.Id,
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco

            };

        }

        public async Task<JogoViewModel> Inserir(JogoInputModel jogo)
        {
            var entidadejogo = await _jogoRepository.Obter(jogo.Nome, jogo.Produtora);

            if (entidadejogo.Count > 0)
            {
                throw new JogoJaCadastradoException();
            }

            var jogoInsert = new Jogo
            {
                Id = Guid.NewGuid(),
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco

            };

            await _jogoRepository.Inserir(jogoInsert);

            return new JogoViewModel
            {
                Id = jogoInsert.Id,
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco

            };
        }

        public async Task Atualizar(Guid id, JogoInputModel jogo)
        {
            var entidadejogo = await _jogoRepository.Obter(id);
            if (entidadejogo == null)
            {
                throw new JogoNaoCadastradoException();
            }

            entidadejogo.Nome = jogo.Nome;
            entidadejogo.Produtora = jogo.Produtora;
            entidadejogo.Preco = jogo.Preco;

            await _jogoRepository.Atualizar(entidadejogo);
        }

        public async Task Atualizar(Guid id, double preco)
        {
            var entidadejogo = await _jogoRepository.Obter(id);
            if (entidadejogo == null)
            {
                throw new JogoNaoCadastradoException();
            }

            entidadejogo.Preco = preco;

            await _jogoRepository.Atualizar(entidadejogo);

        }
        public async Task Remover(Guid id)
        {
            var jogo = await _jogoRepository.Obter(id);
            if (jogo == null)
            {
                throw new JogoNaoCadastradoException();
            }

            await _jogoRepository.Remover(id);
        }

        public void Dispose()
        {
            //_jogoRepository?.Dispose();
        }

    }
}
