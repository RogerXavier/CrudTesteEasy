﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Crud.Models.ViewModels
{
    public class ListPessoaViewModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
    }
}