//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Crud.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Horario_Pessoa
    {
        public int id { get; set; }
        public Nullable<int> pessoa_id { get; set; }
        public Nullable<int> horario_id { get; set; }
    
        public virtual Trabalho_Horario Trabalho_Horario { get; set; }
        public virtual Pessoa Pessoa { get; set; }
    }
}
