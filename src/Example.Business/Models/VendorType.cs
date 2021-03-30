using System.ComponentModel.DataAnnotations;

namespace Example.Business.Models
{
    public enum VendorType
    {
        [Display(Name ="Pessoa Fisica")]
        PhysicalPerson = 1,
        [Display(Name = "Pessoa Juridica")]
        LegalPearson = 2
    }
}
