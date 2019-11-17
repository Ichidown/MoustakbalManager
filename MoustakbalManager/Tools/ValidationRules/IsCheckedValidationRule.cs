using System.Globalization;
using System.Windows.Controls;

namespace MoustakbalManager.Tools.ValidationRules
{
	public class IsCheckedValidationRule : ValidationRule
	{
		public override ValidationResult Validate(object value, CultureInfo cultureInfo)
		{
			if (value is bool && (bool)value)
			{
				return ValidationResult.ValidResult;
			}
			return new ValidationResult(false, "l'option doit être cochée");
		}
	}
}
