using Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class clsCryptocurrencyM : clsModelBase, IValidatableObject
    {
        public int AssetID { get; set; }

        public int CryptoID { get; set; }

        public string Name { get; set; }

        public string Ticker { get; set; }

        #region ErrorHandling
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Name == null || Name == "")
            {
                yield return new ValidationResult("It is obligated to give a name to the cryptocurrency.", new[] { nameof(Name) });
            }
            if (Ticker.ToUpper() != Ticker)
            {
                yield return new ValidationResult("The ticker can only contain uppercase characters.", new[] { nameof(Ticker) });
            }
            yield break;
        }
        #endregion
    }
}
