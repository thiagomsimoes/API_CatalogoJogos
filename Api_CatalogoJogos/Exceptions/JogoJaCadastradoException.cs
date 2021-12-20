using System;


namespace Api_CatalogoJogos.Exceptions
{
    public class JogoJaCadastradoException : Exception
    {
        public JogoJaCadastradoException()
            :base("Jogo já cadastrado")
        { }
    }
}
