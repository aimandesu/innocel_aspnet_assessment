using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using api.Validator;

namespace api.Dtos.Transaction
{
    public record TransactionDto
    {
        [Required]
        public string PartnerKey { get; set; } = string.Empty; //allowed partner key
        [Required]
        public string PartnerRefNo { get; set; } = string.Empty; //partner referrence number
        [Required]
        public string PartnerPassword { get; set; } = string.Empty; //encode to base64

        [PositiveValueValidation]
        public long TotalAmount { get; set; }  //can't exceed 5, meaning like say 100000 cant

        public ItemDetailsDto[] Items { get; set; } = [];

        public string Timestamp { get; set; } = string.Empty;

        public string Sig { get; set; } = string.Empty;

    }
}