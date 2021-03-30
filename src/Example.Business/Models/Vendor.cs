using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Example.Business.Models
{
    public class Vendor : Entity
    {
        [DisplayName("Nome")]
        [Required(ErrorMessage = "O campo do nome é obrigatório.")]
        [StringLength(200, ErrorMessage = "o campo nome precisa ter entre {2} e {1} caracteres.", MinimumLength = 2)]
        public string Name { get; set; }

        [DisplayName("Documento")]
        [Required(ErrorMessage = "O campo do documento é obrigatório.")]
        [StringLength(14, ErrorMessage = "o campo documento precisa ter entre {2} e {1} caracteres.", MinimumLength = 11)]
        public string Document { get; set; }

        [DisplayName("Tipo Fornecedor")]
        public VendorType VendorType { get; set; }

        public Address Address { get; set; }

        [DisplayName("Ativo?")]
        public bool Active { get; set; }

        /* EF Relations */
        public IEnumerable<Product> Products { get; set; }
    }
}
