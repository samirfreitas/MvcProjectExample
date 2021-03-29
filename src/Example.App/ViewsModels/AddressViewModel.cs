using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Example.App.ViewsModels
{
    public class AddressViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [DisplayName("Logradouro")]
        [Required(ErrorMessage = "O campo do logradouro é obrigatório.")]
        [StringLength(200, ErrorMessage = "o campo logradouro precisa ter entre {2} e {1} caracteres.", MinimumLength = 2)]
        public string PublicPlace { get; set; }

        [DisplayName("Número")]
        [Required(ErrorMessage = "O campo do número é obrigatório.")]
        [StringLength(50, ErrorMessage = "o campo número precisa ter entre {2} e {1} caracteres.", MinimumLength = 1)]
        public string Number { get; set; }

        [DisplayName("Complemento")]
        public string Complement { get; set; }

        [DisplayName("CEP")]
        [Required(ErrorMessage = "O campo do CEP é obrigatório.")]
        [StringLength(8, ErrorMessage = "o campo CEP precisa ter entre {2} e {1} caracteres.", MinimumLength = 8)]
        public string ZipCode { get; set; }

        [DisplayName("Bairro")]
        [Required(ErrorMessage = "O campo do bairro é obrigatório.")]
        [StringLength(100, ErrorMessage = "o campo bairro precisa ter entre {2} e {1} caracteres.", MinimumLength = 2)]
        public string District { get; set; }

        [DisplayName("Cidade")]
        [Required(ErrorMessage = "O campo do cidade é obrigatório.")]
        [StringLength(100, ErrorMessage = "o campo cidade precisa ter entre {2} e {1} caracteres.", MinimumLength = 2)]
        public string City { get; set; }

        [DisplayName("Estado")]
        [Required(ErrorMessage = "O campo do estado é obrigatório.")]
        [StringLength(50, ErrorMessage = "o campo estado precisa ter entre {2} e {1} caracteres.", MinimumLength = 2)]
        public string State { get; set; }
        
        [HiddenInput]
        public Guid VendorId { get; set; }



    }
}
