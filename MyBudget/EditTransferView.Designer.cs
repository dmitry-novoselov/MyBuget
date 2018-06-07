namespace MyBudget {
	partial class EditTransferView {
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
			this.amount = new System.Windows.Forms.TextBox();
			this.description = new System.Windows.Forms.TextBox();
			this.ok = new System.Windows.Forms.Button();
			this.cancel = new System.Windows.Forms.Button();
			this.date = new System.Windows.Forms.DateTimePicker();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// amount
			// 
			this.amount.Location = new System.Drawing.Point(154, 24);
			this.amount.Name = "amount";
			this.amount.Size = new System.Drawing.Size(103, 20);
			this.amount.TabIndex = 2;
			// 
			// description
			// 
			this.description.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.description.Location = new System.Drawing.Point(263, 24);
			this.description.Name = "description";
			this.description.Size = new System.Drawing.Size(193, 20);
			this.description.TabIndex = 3;
			// 
			// ok
			// 
			this.ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ok.Location = new System.Drawing.Point(298, 57);
			this.ok.Name = "ok";
			this.ok.Size = new System.Drawing.Size(75, 23);
			this.ok.TabIndex = 4;
			this.ok.Text = "OK";
			this.ok.UseVisualStyleBackColor = true;
			this.ok.Click += new System.EventHandler(this.ok_Click);
			// 
			// cancel
			// 
			this.cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancel.Location = new System.Drawing.Point(381, 57);
			this.cancel.Name = "cancel";
			this.cancel.Size = new System.Drawing.Size(75, 23);
			this.cancel.TabIndex = 5;
			this.cancel.Text = "Отмена";
			this.cancel.UseVisualStyleBackColor = true;
			this.cancel.Click += new System.EventHandler(this.cancel_Click);
			// 
			// date
			// 
			this.date.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.date.Location = new System.Drawing.Point(12, 24);
			this.date.Name = "date";
			this.date.Size = new System.Drawing.Size(136, 20);
			this.date.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(9, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(33, 13);
			this.label1.TabIndex = 6;
			this.label1.Text = "Дата";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(151, 9);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(41, 13);
			this.label2.TabIndex = 6;
			this.label2.Text = "Сумма";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(260, 9);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(57, 13);
			this.label3.TabIndex = 6;
			this.label3.Text = "Описание";
			// 
			// EditTransferView
			// 
			this.AcceptButton = this.ok;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancel;
			this.ClientSize = new System.Drawing.Size(470, 92);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.date);
			this.Controls.Add(this.cancel);
			this.Controls.Add(this.ok);
			this.Controls.Add(this.description);
			this.Controls.Add(this.amount);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "EditTransferView";
			this.Text = "EditTransferView";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox amount;
		private System.Windows.Forms.TextBox description;
		private System.Windows.Forms.Button ok;
		private System.Windows.Forms.Button cancel;
		private System.Windows.Forms.DateTimePicker date;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
	}
}