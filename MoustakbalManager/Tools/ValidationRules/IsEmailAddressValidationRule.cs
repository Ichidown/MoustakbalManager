using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace MoustakbalManager.Tools.ValidationRules
{
    public class IsEmailAddressValidationRule : ValidationRule
	{
		public override ValidationResult Validate(object value, CultureInfo cultureInfo)
		{
			if (string.IsNullOrWhiteSpace((value ?? "").ToString()))
			{
				return new ValidationResult(false, "champ requis");
			}
			else
			{
				Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
				if (regex.IsMatch(value.ToString()))
					return ValidationResult.ValidResult;
				else
					return new ValidationResult(false, "l'adresse email est incorrecte");
			}
				
		}
	}
}
