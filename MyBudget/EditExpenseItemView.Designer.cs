namespace MyBudget {
	partial class EditExpenseItemView {
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
			this.dayOfMonth = new System.Windows.Forms.TextBox();
			this.amount = new System.Windows.Forms.TextBox();
			this.name = new System.Windows.Forms.TextBox();
			this.ok = new System.Windows.Forms.Button();
			this.cancel = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.from = new System.Windows.Forms.DateTimePicker();
			this.to = new System.Windows.Forms.DateTimePicker();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// dayOfMonth
			// 
			this.dayOfMonth.Location = new System.Drawing.Point(12, 24);
			this.dayOfMonth.Name = "dayOfMonth";
			this.dayOfMonth.Size = new System.Drawing.Size(100, 20);
			this.dayOfMonth.TabIndex = 0;
			// 
			// amount
			// 
			this.amount.Location = new System.Drawing.Point(119, 24);
			this.amount.Name = "amount";
			this.amount.Size = new System.Drawing.Size(100, 20);
			this.amount.TabIndex = 1;
			// 
			// name
			// 
			this.name.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.name.Location = new System.Drawing.Point(226, 24);
			this.name.Name = "name";
			this.name.Size = new System.Drawing.Size(227, 20);
			this.name.TabIndex = 2;
			// 
			// ok
			// 
			this.ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ok.Location = new System.Drawing.Point(579, 58);
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
			this.cancel.Location = new System.Drawing.Point(660, 58);
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
			this.label1.Location = new System.Drawing.Point(9, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(75, 13);
			this.label1.TabIndex = 5;
			this.label1.Text = "День месяца";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(116, 9);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(41, 13);
			this.label2.TabIndex = 5;
			this.label2.Text = "Сумма";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(223, 9);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(57, 13);
			this.label3.TabIndex = 5;
			this.label3.Text = "Название";
			// 
			// from
			// 
			this.from.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.from.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.from.Location = new System.Drawing.Point(459, 24);
			this.from.Name = "from";
			this.from.Size = new System.Drawing.Size(135, 20);
			this.from.TabIndex = 3;
			// 
			// to
			// 
			this.to.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.to.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.to.Location = new System.Drawing.Point(600, 24);
			this.to.Name = "to";
			this.to.Size = new System.Drawing.Size(135, 20);
			this.to.TabIndex = 4;
			// 
			// label4
			// 
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(456, 9);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(70, 13);
			this.label4.TabIndex = 5;
			this.label4.Text = "Действует с";
			// 
			// label5
			// 
			this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(600, 9);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(21, 13);
			this.label5.TabIndex = 5;
			this.label5.Text = "По";
			// 
			// EditExpenseItemView
			// 
			this.AcceptButton = this.ok;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancel;
			this.ClientSize = new System.Drawing.Size(747, 93);
			this.Controls.Add(this.to);
			this.Controls.Add(this.from);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.cancel);
			this.Controls.Add(this.ok);
			this.Controls.Add(this.name);
			this.Controls.Add(this.amount);
			this.Controls.Add(this.dayOfMonth);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "EditExpenseItemView";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "EditMonthlyExpenseView";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox dayOfMonth;
		private System.Windows.Forms.TextBox amount;
		private System.Windows.Forms.TextBox name;
		private System.Windows.Forms.Button ok;
		private System.Windows.Forms.Button cancel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.DateTimePicker from;
		private System.Windows.Forms.DateTimePicker to;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
	}
}