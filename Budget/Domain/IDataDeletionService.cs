
namespace Budget.Domain {
	public interface IDataDeletionService {
		void DeleteRemainder(CashStatement remainder);
		void DeleteCashMovement(CashStatement movement);
		void DeleteMonthlyCashMovement(MonthlyCashStatement expense);
	}
}
