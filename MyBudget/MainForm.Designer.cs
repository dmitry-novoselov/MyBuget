namespace MyBudget {
	partial class MainForm {
		/// <summary>
		/// Требуется переменная конструктора.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Освободить все используемые ресурсы.
		/// </summary>
		/// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Код, автоматически созданный конструктором форм Windows

		/// <summary>
		/// Обязательный метод для поддержки конструктора - не изменяйте
		/// содержимое данного метода при помощи редактора кода.
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
			System.Windows.Forms.Panel panel1;
			this.calculation = new System.Windows.Forms.DataGridView();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.delete = new System.Windows.Forms.Button();
			this.setRemainder = new System.Windows.Forms.Button();
			this.addExpense = new System.Windows.Forms.Button();
			this.addInvestment = new System.Windows.Forms.Button();
			this.addMothlyExpense = new System.Windows.Forms.Button();
			this.edit = new System.Windows.Forms.Button();
			this.monthlyBalance = new System.Windows.Forms.GroupBox();
			this.addExpenseItem = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.editExpenseItem = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.envelopeSize = new System.Windows.Forms.TextBox();
			this.planningSettings = new System.Windows.Forms.BindingSource(this.components);
			this.dateOfLeave = new System.Windows.Forms.DateTimePicker();
			this.dateOfTicketsPurchase = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.bottomLine = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.walletRemainders = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.initialRemainder = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.to = new System.Windows.Forms.DateTimePicker();
			this.label2 = new System.Windows.Forms.Label();
			this.from = new System.Windows.Forms.DateTimePicker();
			tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			panel1 = new System.Windows.Forms.Panel();
			tableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.calculation)).BeginInit();
			panel1.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.monthlyBalance.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.planningSettings)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			tableLayoutPanel1.ColumnCount = 2;
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 170F));
			tableLayoutPanel1.Controls.Add(this.calculation, 0, 0);
			tableLayoutPanel1.Controls.Add(panel1, 1, 0);
			tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			tableLayoutPanel1.Name = "tableLayoutPanel1";
			tableLayoutPanel1.RowCount = 1;
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			tableLayoutPanel1.Size = new System.Drawing.Size(887, 762);
			tableLayoutPanel1.TabIndex = 0;
			// 
			// calculation
			// 
			this.calculation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.calculation.Dock = System.Windows.Forms.DockStyle.Fill;
			this.calculation.Location = new System.Drawing.Point(3, 3);
			this.calculation.Name = "calculation";
			this.calculation.Size = new System.Drawing.Size(711, 756);
			this.calculation.TabIndex = 0;
			// 
			// panel1
			// 
			panel1.Controls.Add(this.groupBox3);
			panel1.Controls.Add(this.monthlyBalance);
			panel1.Controls.Add(this.dateOfLeave);
			panel1.Controls.Add(this.dateOfTicketsPurchase);
			panel1.Controls.Add(this.label8);
			panel1.Controls.Add(this.label7);
			panel1.Controls.Add(this.bottomLine);
			panel1.Controls.Add(this.label6);
			panel1.Controls.Add(this.walletRemainders);
			panel1.Controls.Add(this.label5);
			panel1.Controls.Add(this.groupBox1);
			panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			panel1.Location = new System.Drawing.Point(720, 3);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(164, 756);
			panel1.TabIndex = 1;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.delete);
			this.groupBox3.Controls.Add(this.setRemainder);
			this.groupBox3.Controls.Add(this.addExpense);
			this.groupBox3.Controls.Add(this.addInvestment);
			this.groupBox3.Controls.Add(this.addMothlyExpense);
			this.groupBox3.Controls.Add(this.edit);
			this.groupBox3.Location = new System.Drawing.Point(0, 0);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(161, 203);
			this.groupBox3.TabIndex = 22;
			this.groupBox3.TabStop = false;
			// 
			// delete
			// 
			this.delete.Location = new System.Drawing.Point(6, 173);
			this.delete.Name = "delete";
			this.delete.Size = new System.Drawing.Size(111, 23);
			this.delete.TabIndex = 5;
			this.delete.Text = "Удалить";
			this.delete.UseVisualStyleBackColor = true;
			this.delete.Click += new System.EventHandler(this.delete_Click);
			// 
			// setRemainder
			// 
			this.setRemainder.Location = new System.Drawing.Point(6, 12);
			this.setRemainder.Name = "setRemainder";
			this.setRemainder.Size = new System.Drawing.Size(111, 23);
			this.setRemainder.TabIndex = 0;
			this.setRemainder.Text = "Задать остаток";
			this.setRemainder.UseVisualStyleBackColor = true;
			this.setRemainder.Click += new System.EventHandler(this.setRemainder_Click);
			// 
			// addExpense
			// 
			this.addExpense.Location = new System.Drawing.Point(6, 77);
			this.addExpense.Name = "addExpense";
			this.addExpense.Size = new System.Drawing.Size(111, 23);
			this.addExpense.TabIndex = 2;
			this.addExpense.Text = "Добавить трату";
			this.addExpense.UseVisualStyleBackColor = true;
			this.addExpense.Click += new System.EventHandler(this.addExpense_Click);
			// 
			// addInvestment
			// 
			this.addInvestment.Location = new System.Drawing.Point(6, 48);
			this.addInvestment.Name = "addInvestment";
			this.addInvestment.Size = new System.Drawing.Size(111, 23);
			this.addInvestment.TabIndex = 1;
			this.addInvestment.Text = "Добавить доход";
			this.addInvestment.UseVisualStyleBackColor = true;
			this.addInvestment.Click += new System.EventHandler(this.addInvestment_Click);
			// 
			// addMothlyExpense
			// 
			this.addMothlyExpense.Location = new System.Drawing.Point(6, 106);
			this.addMothlyExpense.Name = "addMothlyExpense";
			this.addMothlyExpense.Size = new System.Drawing.Size(149, 23);
			this.addMothlyExpense.TabIndex = 3;
			this.addMothlyExpense.Text = "Добавить движ. по статье";
			this.addMothlyExpense.UseVisualStyleBackColor = true;
			this.addMothlyExpense.Click += new System.EventHandler(this.addMothlyExpense_Click);
			// 
			// edit
			// 
			this.edit.Location = new System.Drawing.Point(6, 144);
			this.edit.Name = "edit";
			this.edit.Size = new System.Drawing.Size(111, 23);
			this.edit.TabIndex = 4;
			this.edit.Text = "Редактировать";
			this.edit.UseVisualStyleBackColor = true;
			this.edit.Click += new System.EventHandler(this.edit_Click);
			// 
			// monthlyBalance
			// 
			this.monthlyBalance.Controls.Add(this.addExpenseItem);
			this.monthlyBalance.Controls.Add(this.button1);
			this.monthlyBalance.Controls.Add(this.editExpenseItem);
			this.monthlyBalance.Controls.Add(this.label3);
			this.monthlyBalance.Controls.Add(this.envelopeSize);
			this.monthlyBalance.Location = new System.Drawing.Point(0, 209);
			this.monthlyBalance.Name = "monthlyBalance";
			this.monthlyBalance.Size = new System.Drawing.Size(161, 148);
			this.monthlyBalance.TabIndex = 21;
			this.monthlyBalance.TabStop = false;
			this.monthlyBalance.Text = "Месячный баланс: ?";
			// 
			// addExpenseItem
			// 
			this.addExpenseItem.Location = new System.Drawing.Point(21, 19);
			this.addExpenseItem.Name = "addExpenseItem";
			this.addExpenseItem.Size = new System.Drawing.Size(119, 23);
			this.addExpenseItem.TabIndex = 6;
			this.addExpenseItem.Text = "Добавить статью";
			this.addExpenseItem.UseVisualStyleBackColor = true;
			this.addExpenseItem.Click += new System.EventHandler(this.addExpenseItem_Click);
			// 
			// button1
			// 
			this.button1.Enabled = false;
			this.button1.Location = new System.Drawing.Point(21, 77);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(119, 23);
			this.button1.TabIndex = 6;
			this.button1.Text = "Удалить статью";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.editExpenseItem_Click);
			// 
			// editExpenseItem
			// 
			this.editExpenseItem.Location = new System.Drawing.Point(21, 48);
			this.editExpenseItem.Name = "editExpenseItem";
			this.editExpenseItem.Size = new System.Drawing.Size(119, 23);
			this.editExpenseItem.TabIndex = 6;
			this.editExpenseItem.Text = "Править статью";
			this.editExpenseItem.UseVisualStyleBackColor = true;
			this.editExpenseItem.Click += new System.EventHandler(this.editExpenseItem_Click);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(27, 103);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(107, 13);
			this.label3.TabIndex = 12;
			this.label3.Text = "недельный конверт";
			// 
			// envelopeSize
			// 
			this.envelopeSize.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.planningSettings, "EnvelopeSize", true));
			this.envelopeSize.Location = new System.Drawing.Point(21, 116);
			this.envelopeSize.Name = "envelopeSize";
			this.envelopeSize.Size = new System.Drawing.Size(119, 20);
			this.envelopeSize.TabIndex = 7;
			// 
			// planningSettings
			// 
			this.planningSettings.DataSource = typeof(Budget.Presentation.EditPlanningSettingsUseCase.EditPlanningSettingsViewModel);
			// 
			// dateOfLeave
			// 
			this.dateOfLeave.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.planningSettings, "DateOfLeave", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.dateOfLeave.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dateOfLeave.Location = new System.Drawing.Point(0, 587);
			this.dateOfLeave.Name = "dateOfLeave";
			this.dateOfLeave.Size = new System.Drawing.Size(95, 20);
			this.dateOfLeave.TabIndex = 18;
			// 
			// dateOfTicketsPurchase
			// 
			this.dateOfTicketsPurchase.AutoSize = true;
			this.dateOfTicketsPurchase.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.planningSettings, "DateOfTicketsPurchase", true));
			this.dateOfTicketsPurchase.Location = new System.Drawing.Point(55, 610);
			this.dateOfTicketsPurchase.Name = "dateOfTicketsPurchase";
			this.dateOfTicketsPurchase.Size = new System.Drawing.Size(13, 13);
			this.dateOfTicketsPurchase.TabIndex = 17;
			this.dateOfTicketsPurchase.Text = "?";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(4, 610);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(53, 13);
			this.label8.TabIndex = 17;
			this.label8.Text = "Покупка:";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(4, 571);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(74, 13);
			this.label7.TabIndex = 17;
			this.label7.Text = "Отправление";
			// 
			// bottomLine
			// 
			this.bottomLine.AutoSize = true;
			this.bottomLine.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.planningSettings, "BottomLine", true));
			this.bottomLine.Location = new System.Drawing.Point(41, 538);
			this.bottomLine.Name = "bottomLine";
			this.bottomLine.Size = new System.Drawing.Size(13, 13);
			this.bottomLine.TabIndex = 16;
			this.bottomLine.Text = "0";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(4, 538);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(40, 13);
			this.label6.TabIndex = 15;
			this.label6.Text = "Итого:";
			// 
			// walletRemainders
			// 
			this.walletRemainders.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.planningSettings, "WalletRemainders", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.walletRemainders.Location = new System.Drawing.Point(0, 383);
			this.walletRemainders.Multiline = true;
			this.walletRemainders.Name = "walletRemainders";
			this.walletRemainders.Size = new System.Drawing.Size(158, 152);
			this.walletRemainders.TabIndex = 14;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(-1, 367);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(49, 13);
			this.label5.TabIndex = 13;
			this.label5.Text = "Остатки";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.initialRemainder);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.to);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.from);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.groupBox1.Location = new System.Drawing.Point(0, 658);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(164, 98);
			this.groupBox1.TabIndex = 10;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Период планирования";
			// 
			// initialRemainder
			// 
			this.initialRemainder.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.planningSettings, "InitialRemainder", true));
			this.initialRemainder.Location = new System.Drawing.Point(58, 71);
			this.initialRemainder.Name = "initialRemainder";
			this.initialRemainder.Size = new System.Drawing.Size(96, 20);
			this.initialRemainder.TabIndex = 2;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(5, 74);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(49, 13);
			this.label4.TabIndex = 10;
			this.label4.Text = "Остаток";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(39, 23);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(14, 13);
			this.label1.TabIndex = 8;
			this.label1.Text = "С";
			// 
			// to
			// 
			this.to.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.planningSettings, "CalculationPeriodTo", true));
			this.to.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.to.Location = new System.Drawing.Point(59, 45);
			this.to.Name = "to";
			this.to.Size = new System.Drawing.Size(95, 20);
			this.to.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(32, 49);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(21, 13);
			this.label2.TabIndex = 9;
			this.label2.Text = "По";
			// 
			// from
			// 
			this.from.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.planningSettings, "CalculationPeriodFrom", true));
			this.from.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.from.Location = new System.Drawing.Point(59, 19);
			this.from.Name = "from";
			this.from.Size = new System.Drawing.Size(95, 20);
			this.from.TabIndex = 0;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(887, 762);
			this.Controls.Add(tableLayoutPanel1);
			this.Name = "MainForm";
			this.Text = "Мой Бюджет";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Load += new System.EventHandler(this.MainForm_Load);
			tableLayoutPanel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.calculation)).EndInit();
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.monthlyBalance.ResumeLayout(false);
			this.monthlyBalance.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.planningSettings)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView calculation;
		private System.Windows.Forms.Button edit;
		private System.Windows.Forms.Button delete;
		private System.Windows.Forms.Button addMothlyExpense;
		private System.Windows.Forms.Button addExpenseItem;
		private System.Windows.Forms.Button addExpense;
		private System.Windows.Forms.Button setRemainder;
		private System.Windows.Forms.Button addInvestment;
		private System.Windows.Forms.DateTimePicker to;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.DateTimePicker from;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox envelopeSize;
		private System.Windows.Forms.BindingSource planningSettings;
		private System.Windows.Forms.TextBox initialRemainder;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox walletRemainders;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label bottomLine;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.DateTimePicker dateOfLeave;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label dateOfTicketsPurchase;
		private System.Windows.Forms.Button editExpenseItem;
		private System.Windows.Forms.GroupBox monthlyBalance;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Button button1;



	}
}

