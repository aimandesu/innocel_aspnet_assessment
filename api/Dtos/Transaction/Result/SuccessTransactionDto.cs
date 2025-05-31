using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Validator;
using api.Enum;

namespace api.Dtos.Transaction.Result
{

    public class SuccessTransactionDto
    {
        public TransactionResult Result { get; set; }
        [PositiveValueValidation]
        public long TotalAmount { get; set; }
        [PositiveValueValidation]
        public long TotalDiscount { get; set; }
        [PositiveValueValidation]
        public long FinalAmount { get; set; }
        public string ResultMessage { get; set; } = string.Empty;
    }
}