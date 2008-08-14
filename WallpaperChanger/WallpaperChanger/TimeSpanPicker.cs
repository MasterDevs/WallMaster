using System;
using System.Windows.Forms;

namespace WallpaperChanger {
	public partial class TimeSpanPicker : UserControl {
		public TimeSpanPicker() {
			InitializeComponent();
		}

		public TimeSpanPicker(TimeSpan ts) {
			setValues(ts);
		}

		public TimeSpan TimeSpan {
			get { return getTimespan(); }
			set { setValues(value); }
		}

		private TimeSpan getTimespan() {
			int days = (int)_days.Value;
			int hours = (int)_hours.Value;
			int mins = (int)_mins.Value;
			int secs = (int)_secs.Value;
			TimeSpan ts = new TimeSpan(days, hours, mins, secs);

			return ts;
		}

		private void setValues(TimeSpan ts) {
			_days.Value = ts.Days;
			_hours.Value = ts.Hours;
			_mins.Value = ts.Minutes;
			_secs.Value = ts.Seconds;
		}

		public string Heading {
			get { return groupBox1.Text; }
			set { groupBox1.Text = value; }
		}


		public event EventHandler<TimeSpanPickerValueChangedEventArgs> TimeSpanPickerValueChanged;

		private void _timespan_ValueChanged(object sender, EventArgs e)
		{
			if (TimeSpanPickerValueChanged != null) {
				TimeSpan ts = getTimespan();
				TimeSpanPickerValueChangedEventArgs tspvcea= new TimeSpanPickerValueChangedEventArgs(ts);
				TimeSpanPickerValueChanged(this, tspvcea);
			}
		}

	}

	public class TimeSpanPickerValueChangedEventArgs:EventArgs {
		private TimeSpan _ts;

		public TimeSpan TimeSpan {
			get { return _ts; }
			set { _ts = value; }
		}
		public TimeSpanPickerValueChangedEventArgs(TimeSpan ts) {
			_ts = ts;
		}
	}
}
