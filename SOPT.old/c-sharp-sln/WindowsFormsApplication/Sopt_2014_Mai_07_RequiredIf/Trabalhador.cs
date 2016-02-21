using System;
using System.ComponentModel.DataAnnotations;

namespace WindowsFormsApplication.Sopt_2014_Mai_07_RequiredIf
{
    public class Trabalhador
    {
        [Required(ErrorMessage = "*")]
        public int Id { get; set; }

        [Required(ErrorMessage = "*")]
        public String Nome { get; set; }

        public String Aposentado { get; set; }

        [RequiredIf("Aposentado", "S")]
        public DateTime DataAposentadoria { get; set; }
    }
}