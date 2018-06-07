
namespace Budget.Presentation {
	public static class MoneyExtentions {
		public static int NegateIf(this int value, bool negate) {
			return negate ? -value : value;
		}
	}
}
