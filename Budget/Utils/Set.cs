using System;
using System.Collections;
using System.Collections.Generic;

namespace Budget.Utils {
	public class Set<T> : IEnumerable<T> {
		private readonly Dictionary<T, object> set = new Dictionary<T, object>();
		
		public Set() { }

		public void Add(T item) {
			set[item] = null;
		}

		public void AddRange(IEnumerable<T> items) {
			foreach (var item in items) {
				Add(item);
			}
		}

		public IEnumerator<T> GetEnumerator() {
			return set.Keys.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}
	}
}
