using System;
using System.Linq.Expressions;
using System.Windows.Forms;
using System.Collections;

namespace Budget.Infrastructure {
	public class Binder<T> {
		private BindingSource binding = new BindingSource { DataSource = typeof(T) };

		public T DataSource {
			get { return (T)binding.DataSource; }
			set { binding.DataSource = value; }
		}

        public void Bind(CheckBox checkBox, Expression<Func<T, bool>> bind) {
            checkBox.DataBindings.Add("Checked", binding, PropertyName(bind));
        }

        public void Bind<TResult>(TextBox textBox, Expression<Func<T, TResult>> bind) {
            textBox.DataBindings.Add("Text", binding, PropertyName(bind));
        }

		public void Bind<TResult>(DateTimePicker dateTimePicker, Expression<Func<T, TResult>> bind) {
			dateTimePicker.DataBindings.Add("Value", binding, PropertyName(bind));
		}

		public void Bind<TResult>(ComboBox comboBox, Expression<Func<T, TResult>> bind) {
			var sourceProperty = PropertyName(bind);
			var controlProperty = IsList(sourceProperty) ? "DataSource" : "SelectedItem";
			comboBox.DataBindings.Add(controlProperty, binding, sourceProperty);
		}

		private bool IsList(string dataSourcePropertyName) {
			return typeof(IList).IsAssignableFrom(typeof(T).GetProperty(dataSourcePropertyName).PropertyType);
		}

		private static string PropertyName<TResult>(Expression<Func<T, TResult>> bind) {
			return bind.Body
				.ToString()
				.Substring(bind.Parameters[0].Name.Length + 1);
		}
	}
}
