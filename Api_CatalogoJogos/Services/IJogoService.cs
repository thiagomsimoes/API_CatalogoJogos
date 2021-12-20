using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using Api_CatalogoJogos.InputModel;
using Api_CatalogoJogos.ViewModel;

namespace Api_CatalogoJogos.Services
{
    public interface IJogoService:IDisposable
    {
        Task<List<JogoViewModel>> Obter(int pagina, int quantidade);
        Task<JogoViewModel> Obter(Guid id);
        Task<JogoViewModel> Inserir(JogoInputModel jogo);
        Task Atualizar (Guid id, JogoInputModel jogo);
        Task Atualizar (Guid id, double preco);
        Task Remover (Guid id);
        




    }
}
