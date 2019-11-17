using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace MoustakbalManager.Tools.ValidationRules
{
    public class IsPhoneNumberValidationRule : ValidationRule
	{
		public override ValidationResult Validate(object value, CultureInfo cultureInfo)
		{
			if (string.IsNullOrWhiteSpace((value ?? "").ToString())) // is empty input
			{
				return new ValidationResult(false, "champ requis");
			}
			else
			{
				if (Regex.IsMatch(value.ToString(), @"^\d+$")) // is a number
				{
					if (value.ToString().Length >= 9 && value.ToString().Length <= 10) // is a phone number
						return ValidationResult.ValidResult;
					else
						return new ValidationResult(false, "le numéro de téléphone est incorrect");
				}
				else
				{
					return new ValidationResult(false, "ce n'est pas un numero de telephone");
				}
					
			}
				
		}
	}
}
