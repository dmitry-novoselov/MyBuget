using System;
using System.Collections;
using System.Collections.Generic;

namespace Budget {
	public static class EnumerableExtentions {
		public static IEnumerable<TOutput> Convert<TInput, TOutput>(this IEnumerable<TInput> input, Converter<TInput, TOutput> cast) {
			foreach (var item in input) {
				yield return cast(item);
			}
		}

		public static IEnumerable<TOutput> Convert<TInput, TOutput>(this IEnumerable input, Converter<TInput, TOutput> cast) {
			foreach (TInput item in input) {
				yield return cast(item);
			}
		}
	}
}
