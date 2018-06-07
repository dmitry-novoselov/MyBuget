#region Usings

using System;

#endregion

namespace Budget.Domain {
    [Serializable]
    public class MonthlyCashStatement : IDated, IEquatable<MonthlyCashStatement> {
        public MonthlyCashStatement(MonthlyCashStatementCategory category, YearMonth month, DateTime date, int amount, string description) {
            Category = category;
            Month = month;
            Date = date;
            Amount = amount;
            Description = description;
            isFinalPayment = false;
        }

        public MonthlyCashStatementCategory Category { get; set; }
        public YearMonth Month { get; set; }
        public DateTime Date { get; set; }
        public int Amount { get; set; }
        public string Description { get; set; }

        public bool Equals(MonthlyCashStatement other) {
            return !ReferenceEquals(other, null) &&
                ReferenceEquals(Category, other.Category) &&
                Month == other.Month &&
                Date == other.Date &&
                Amount == other.Amount &&
                Description == other.Description;
        }

        public override bool Equals(object obj) {
            return Equals(obj as MonthlyCashStatement);
        }

        public override int GetHashCode() {
            return Category.GetHashCode();
        }

        private bool? isFinalPayment;

        public bool IsFinalPayment {
            get { return isFinalPayment.GetValueOrDefault(true); }
            set { isFinalPayment = value; }
        }
    }
}
