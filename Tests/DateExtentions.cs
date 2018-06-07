using Tests.Dsl;
using Budget.Domain;

namespace Tests {
	public static class DateExtentions {
		public static PeriodBuilder of2009(this double ddmm) {
			return ddmm.of(2009);
		}

		public static PeriodBuilder of2010(this double ddmm) {
			return ddmm.of(2010);
		}

		private static PeriodBuilder of(this double date, int year) {
			var day = (int)date;
			var month = (int)(date * 100 - day * 100 + 0.001);

			return new PeriodBuilder(day, month, year);
		}
	}

	public static class jan {
		public static YearMonth of2009 {
			get { return new YearMonth(1, 2009); }
		}
	}

	public static class feb {
		public static YearMonth of2009 {
			get { return new YearMonth(2, 2009); }
		}
	}
}
