using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Helpers
{
    using System.Security.Cryptography;
    using System.Text;
    using System.Collections.Generic;
    using System;
    using System.Linq;
    using api.Dtos.Transaction;

    public static class SignatureHelper
    {
        public static string GenerateSignature(TransactionDto dto)
        {
            // Step 1: Prepare dictionary (excluding timestamp and sig)
            var parameters = new Dictionary<string, string>
            {
                {"PartnerKey", dto.PartnerKey },
                {"PartnerPassword", dto.PartnerPassword }, // already Base64 encoded
                {"PartnerRefNo", dto.PartnerRefNo },
                {"TotalAmount", dto.TotalAmount.ToString() }
            };

            // Step 2: Sort by key alphabetically
            var sortedValues = parameters
                .OrderBy(p => p.Key)
                .Select(p => p.Value)
                .ToList();

            // Step 3: Prepend timestamp to values
            sortedValues.Insert(0, dto.Timestamp); // timestamp should be in yyyyMMddHHmmss format

            // Step 4: Join all values
            var rawString = string.Concat(sortedValues);

            // Step 5: Compute SHA256 and convert to Base64
            using var sha256 = SHA256.Create();
            var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawString));
            return Convert.ToBase64String(hashBytes);
        }
    }

}