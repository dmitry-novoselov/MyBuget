using System;

namespace Budget.Presentation {
	public interface IEditTransferView : IView {
		PETransfer Transfer { set; }

		Action OnOK { set; }
	}
}
