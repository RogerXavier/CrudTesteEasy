using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Crud.Models.ViewModels
{
    public class PessoaViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Preencha o campo")]
        [StringLength(50)]
        [EmailAddress(ErrorMessage = "Insira um email válido")]
        [Display(Name = "* Endereço de e-mail:")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Preencha o campo")]
        [StringLength(50)]
        [Display(Name = "* Nome/Name:")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Preencha o campo")]
        [StringLength(50)]
        [Display(Name = "* Skype ( please create an account if you don't have yet / bom criar caso não tenha):")]
        public string Skype { get; set; }

        [Required(ErrorMessage = "Preencha o campo")]
        [StringLength(15)]
        [RegularExpression("([0-9]+)", ErrorMessage = "Somente números")]
        [Display(Name = "* Telefone/Phone (Whatsapp):")]
        public string Telefone { get; set; }

        [StringLength(15)]
        [Display(Name = "Linkedin:")]
        public string Linkedin { get; set; }

        [Required(ErrorMessage = "Preencha o campo")]
        [StringLength(50)]
        [Display(Name = "* Cidade/City:")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "Preencha o campo")]
        [StringLength(30)]
        [Display(Name = "* Estado/State:")]
        public string Estado { get; set; }

        [Display(Name = "Portfolio:")]
        public string Portfolio { get; set; }

        [Required(ErrorMessage = "Preencha o campo")]
        [StringLength(10)]
        [Display(Name = "* What is your hourly salary requirements? / Qual sua pretensão salarial por hora?")]
        public string SalarioPorHora { get; set; }

        [Display(Name = "Do you know any other language or framework that was not listed above?"
                + "Tell us and evaluate yourself from 0 to 5. / Conhece mais alguma linguagem ou"
                + "framework que não foi listado aqui em cima? Conte para nos e se auto avalie ainda de 0 a 5.")]
        public string ExperienciaTecnologia { get; set; }

        [Display(Name = "Link CRUD:")]
        public string LinkCrud { get; set; }
        public List<ConhecimentoModel> ConhecimentoList { get; set; }
        [Display(Name = "*What is your willingness to work today? / Qual é sua disponibilidade para trabalhar hoje?")]
        public List<TrabalhoTempoModel> TrabalhoTempoList { get; set; }
        [Display(Name = "What's the best time to work for you? / Pra você qual é o melhor horário para trabalhar?")]
        public List<TrabalhoHorarioModel> TrabalhoHorarioList { get; set; }

        //public List<Nullable<int>> ConhecimentoIds { get; set; }
        //public List<Nullable<int>> TempoIds { get; set; }
        //public List<Nullable<int>> HorariosIds { get; set; }

    }

    public class ConhecimentoModel
    {
        public int Id { get; set; }
        public string Tecnologia { get; set; }
        public string Nivel { get; set; }
    }

    public class TrabalhoTempoModel
    {
        public int Id { get; set; }
        public string Tempo { get; set; }
        public bool IsSelected { get; set; }
    }

    public class TrabalhoHorarioModel
    {
        public int Id { get; set; }
        public string Horario { get; set; }
        public bool IsSelected { get; set; }
    }

}