using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using api.Validator;

namespace api.Dtos
{
    public class ItemDetailsDto
    {
        [Required]
        required public string PartnerItemRef { get; set; }
        [Required]
        required public string Name { get; set; }
        [Range(1, 5, ErrorMessage = "Qty must be between 1 and 5.")]
        public int Qty { get; set; } //must only allow value > 1 && value <=5
        [PositiveValueValidation]
        [MaxDigit(5)]
        public long UnitPrice { get; set; } //can't exceed 5, meaning like say 100000 cant
    }
}