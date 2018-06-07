using System.Collections.Generic;
using System.Linq;

namespace Budget.Domain {
	public static class TransferExtentions {
		public static IEnumerable<T> Within<T>(this IEnumerable<T> dated, Period period) where T : IDated {
			return dated.Where(d => period.From <= d.Date && d.Date < period.To);
		}
	}
}
