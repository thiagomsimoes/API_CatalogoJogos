using System;

namespace Api_CatalogoJogos.Exceptions
{
    public class JogoNaoCadastradoException : Exception
    {
        public JogoNaoCadastradoException()
            :base("Jogo não cadastrado") 
        { }
    }
}
