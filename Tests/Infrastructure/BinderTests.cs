using System;
using System.Windows.Forms;
using Budget.Infrastructure;
using NUnit.Framework;
using DataObject = Tests.Infrastructure.Fakes.DataObject;

namespace Tests.Infrastructure {
	[TestFixture]
	public class BinderTests {
		private Form form;
		private TextBox textBox;
		private DateTimePicker dateTimePicker;

		private Binder<DataObject> binder;

		[SetUp]
		public void SetUp() {
			form = new Form();

			textBox = new TextBox();
			form.Controls.Add(textBox);

			dateTimePicker = new DateTimePicker();
			form.Controls.Add(dateTimePicker);

			binder = new Binder<DataObject>();
		}

		[Test]
		public void BindStringToTextBox() {
			binder.Bind(textBox, x => x.TheString);
			binder.DataSource = new DataObject { TheString = "string" };

			form.Show();

			Assert.AreEqual("string", textBox.Text);
		}

		[Test]
		public void BindIntToTextBox() {
			binder.Bind(textBox, x => x.TheInt);
			binder.DataSource = new DataObject { TheInt = 10 };

			form.Show();

			Assert.AreEqual("10", textBox.Text);
		}

		[Test]
		public void BindDateTimeToDateTimePicker() {
			binder.Bind(dateTimePicker, x => x.TheDateTime);
			binder.DataSource = new DataObject { TheDateTime = new DateTime(2009, 1, 2) };

			form.Show();

			Assert.AreEqual(new DateTime(2009, 1, 2), dateTimePicker.Value);
		}
	}
}
