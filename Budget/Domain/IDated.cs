﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Budget.Domain {
	public interface IDated {
		DateTime Date { get; }
	}
}
