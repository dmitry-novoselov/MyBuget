using System;
using System.Collections.Generic;
using Budget.Presentation;

namespace Tests.Presentation.Fakes {
	internal class EditTransferViewFake : IEditTransferView {
		public PETransfer Transfer { get; set; }
		public Action OnOK { get; set; }

		public string Text { get; set; }
		public void Show() { IsShown = true; }

		public bool IsShown { get; set; }
	}
}
