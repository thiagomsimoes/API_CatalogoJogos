using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;

namespace Api_CatalogoJogos.InputModel
{
    public class JogoInputModel
    {
        //Princípio de Fail Fast
        [Required]
        [StringLength(100, MinimumLength =3,ErrorMessage = "O nome do jogo deve conter entre 3 e 100 caracteres")]
        public string Nome { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "O nome da produtora deve conter entre 3 e 100 caracteres")]
        public string Produtora { get; set; }
        [Required]
        [Range(1,1000,ErrorMessage ="O preço deve ser de no mínimo 1 real e de no máximo 1000 reais")] 
        public double Preco { get; set; }
    }
}
