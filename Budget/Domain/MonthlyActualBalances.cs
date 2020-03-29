using System;
using System.Collections.Generic;

namespace Budget.Domain
{
    public class MonthlyActualBalances {
        private readonly Dictionary<string, int> balances;

        public MonthlyActualBalances(Dictionary<string, int> balances) {
            this.balances = balances;
        }

        public int GetFor(DateTime date)
        {
            var key = $"{date.Year}-{date.Month}";

            balances.TryGetValue(key, out var balance);

            return balance;
        }
    }
}