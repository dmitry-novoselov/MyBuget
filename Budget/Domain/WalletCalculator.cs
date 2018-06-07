using System.Text.RegularExpressions;

namespace Budget.Domain {
	public static class WalletCalculator {
		public static int OveralAmount(this string numbers) {
			var bottomLine = 0;

			var values = new Regex(@"-?\d+");
			foreach (Match match in values.Matches(numbers ?? "")) {
				bottomLine += int.Parse(match.Value);
			}

			return bottomLine;
		}
	}
}
