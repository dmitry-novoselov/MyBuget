using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Budget.Domain {
	public class DatedSet<T> : IEnumerable<T> where T : IDated {
		private static readonly DatedComparer comparer = new DatedComparer();

		private readonly T[] items;
		private readonly int left;
		private readonly int right;

		public DatedSet(IEnumerable<T> items)
			: this(items.ToArray(), false) {}

		private DatedSet(T[] items, bool sorted, int left = 0, int right = 0) {
			if (sorted) {
				this.items = items;

				this.left = left;
				this.right = right;
			} else {
				this.items = items;
				Array.Sort(items, comparer);

				this.left = 0;
				this.right = items.Length;
			}
		}

		public DatedSet<T> this[Period period] {
			get {
				var newLeft = FindLeftBorder(items, period.From, left, right);
				var newRight = FindLeftBorder(items, period.To, left, right);

				return new DatedSet<T>(items, true, newLeft, newRight);
			}
		}

		private T[] enumerable;

		public IEnumerator<T> GetEnumerator() {
			if (enumerable == null) {
				enumerable = new T[right - left];
				Array.Copy(items, left, enumerable, 0, right - left);
			}

			return ((IEnumerable<T>) enumerable).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}

		private static int FindLeftBorder(T[] array, DateTime date, int left, int right) {
			while (true) {
				if (left == right) return left;

				var middle = (left + right)/2;
				if (array[middle].Date < date) {
					left = left == middle ? middle + 1 : middle;
				} else {
					right = right == middle ? middle - 1 : middle;
				}
			}
		}

		private class DatedComparer : IComparer<T> {
			public int Compare(T x, T y) {
				if (ReferenceEquals(x, y)) return 0;
				if (ReferenceEquals(x, null)) return -1;
				if (ReferenceEquals(y, null)) return 1;

				return x.Date.CompareTo(y.Date);
			}
		}
	}
}