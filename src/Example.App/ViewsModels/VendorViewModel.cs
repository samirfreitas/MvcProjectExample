using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Example.App.ViewsModels
{
    public class VendorViewModel
    {
        [Key]
        public Guid Id { get; set; }
        [DisplayName("Nome")]
        [Required(ErrorMessage = "O campo do nome é obrigatório.")]
        [StringLength(200, ErrorMessage = "o campo nome precisa ter entre {2} e {1} caracteres.", MinimumLength = 2)]
        public string Name { get; set; }

        [DisplayName("Documento")]
        [Required(ErrorMessage = "O campo do documento é obrigatório.")]
        [StringLength(14, ErrorMessage = "o campo documento precisa ter entre {2} e {1} caracteres.", MinimumLength = 11)]
        public string Document { get; set; }

        [DisplayName("Tipo Fornecedor")]
        public int VendorType { get; set; }

        public AddressViewModel Address { get; set; }

        [DisplayName("Ativo?")]
        public bool Active { get; set; }

        /* EF Relations */
        public IEnumerable<ProductViewModel> Products { get; set; }
    }
}
