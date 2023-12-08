using NEO_SYSTEM_TECHNOLOGY.Entity.Enum;
using NEO_SYSTEM_TECHNOLOGY.Entity;
using System.ComponentModel.DataAnnotations;

namespace NEO_SYSTEM_TECHNOLOGY.ViewModels
{
    public class DogovorReceiptVM
    {
        public string Order { get; set; }
        public string OrgName { get; set; }
        public int? AccountNumber { get; set; }
        public int? AmountFaceValue { get; set; }
        public bool IsVatIncluded { get; set; }
        public Currency Currency { get; set; }
        public int? PaymentSum { get; set; }
        public DateTime? DateFaceValue { get; set; }
        public DateTime? PaymentDate { get; set; }

        //public Dogovor Dogovor { get; set; }
        public int DogovorId { get; set; }

        [Required]
        [Display(Name = "File")]
        public IFormFile FormFile { get; set; }

    }
}
