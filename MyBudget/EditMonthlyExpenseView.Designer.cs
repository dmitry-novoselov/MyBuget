namespace MyBudget {
	partial class EditMonthlyExpenseView {
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
            this.expenseItems = new System.Windows.Forms.ComboBox();
            this.yearMonth = new System.Windows.Forms.DateTimePicker();
            this.date = new System.Windows.Forms.DateTimePicker();
            this.amount = new System.Windows.Forms.TextBox();
            this.description = new System.Windows.Forms.TextBox();
            this.ok = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.isFinal = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // expenseItems
            // 
            this.expenseItems.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.expenseItems.FormattingEnabled = true;
            this.expenseItems.Location = new System.Drawing.Point(151, 24);
            this.expenseItems.MaxDropDownItems = 50;
            this.expenseItems.Name = "expenseItems";
            this.expenseItems.Size = new System.Drawing.Size(145, 21);
            this.expenseItems.TabIndex = 1;
            // 
            // yearMonth
            // 
            this.yearMonth.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.yearMonth.Location = new System.Drawing.Point(12, 24);
            this.yearMonth.Name = "yearMonth";
            this.yearMonth.Size = new System.Drawing.Size(133, 20);
            this.yearMonth.TabIndex = 0;
            // 
            // date
            // 
            this.date.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.date.Location = new System.Drawing.Point(303, 24);
            this.date.Name = "date";
            this.date.Size = new System.Drawing.Size(135, 20);
            this.date.TabIndex = 2;
            // 
            // amount
            // 
            this.amount.Location = new System.Drawing.Point(445, 24);
            this.amount.Name = "amount";
            this.amount.Size = new System.Drawing.Size(100, 20);
            this.amount.TabIndex = 3;
            // 
            // description
            // 
            this.description.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.description.Location = new System.Drawing.Point(551, 24);
            this.description.Name = "description";
            this.description.Size = new System.Drawing.Size(156, 20);
            this.description.TabIndex = 4;
            // 
            // ok
            // 
            this.ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ok.Location = new System.Drawing.Point(551, 60);
            this.ok.Name = "ok";
            this.ok.Size = new System.Drawing.Size(75, 23);
            this.ok.TabIndex = 5;
            this.ok.Text = "OK";
            this.ok.UseVisualStyleBackColor = true;
            this.ok.Click += new System.EventHandler(this.ok_Click);
            // 
            // cancel
            // 
            this.cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel.Location = new System.Drawing.Point(632, 60);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(75, 23);
            this.cancel.TabIndex = 6;
            this.cancel.Text = "Отмена";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Месяц";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(148, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Статья";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(300, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Дата";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(442, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Сумма";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(548, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Описание";
            // 
            // isFinal
            // 
            this.isFinal.AutoSize = true;
            this.isFinal.Location = new System.Drawing.Point(151, 50);
            this.isFinal.Name = "isFinal";
            this.isFinal.Size = new System.Drawing.Size(163, 17);
            this.isFinal.TabIndex = 8;
            this.isFinal.Text = "Закрыть для этого месяца";
            this.isFinal.UseVisualStyleBackColor = true;
            // 
            // EditMonthlyExpenseView
            // 
            this.AcceptButton = this.ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancel;
            this.ClientSize = new System.Drawing.Size(721, 96);
            this.Controls.Add(this.isFinal);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.ok);
            this.Controls.Add(this.description);
            this.Controls.Add(this.amount);
            this.Controls.Add(this.date);
            this.Controls.Add(this.yearMonth);
            this.Controls.Add(this.expenseItems);
            this.Name = "EditMonthlyExpenseView";
            this.Text = "EditMonthlyExpenseView";
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox expenseItems;
		private System.Windows.Forms.DateTimePicker yearMonth;
		private System.Windows.Forms.DateTimePicker date;
		private System.Windows.Forms.TextBox amount;
		private System.Windows.Forms.TextBox description;
		private System.Windows.Forms.Button ok;
		private System.Windows.Forms.Button cancel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox isFinal;
	}
}