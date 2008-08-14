using System;
using System.Windows.Forms;
using System.Drawing;

namespace ScreenMonitor {
	/// <summary>
	/// Monitors the number of screens on the computer as well as their sizes
	/// Raises an event when they change
	/// </summary>
	public class ScreenMonitor {
		private Screen[] _screens;
		private System.Timers.Timer _timer;

		public double Interval {
			get { return _timer.Interval; }
			set { _timer.Interval = value; }
		}


		public ScreenMonitor(double interval) {
			_screens = Screen.AllScreens;
			_timer = new System.Timers.Timer(interval);
			_timer.AutoReset = false;
			_timer.Enabled = false;
			_timer.Elapsed += new System.Timers.ElapsedEventHandler(_timer_Elapsed);
		}


		public void Start() {
			_screens = Screen.AllScreens;
			_timer.Start();
		}

		public void Stop() {
			_timer.Stop();
		}



		void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e) {
			bool isChanged = checkChanges();
			if (isChanged) {
				raiseChanged();
			}

			_timer.Start();
		}



		private void raiseChanged() {
			if (ScreenChanged != null) {
				ScreenChanged(this, new EventArgs());
			}
		}


		/// <summary>
		/// Checks if the screens have changed since the last time we checked
		/// </summary>
		/// <returns>True if something has changed</returns>
		private bool checkChanges() {
			Screen[] curr = Screen.AllScreens;

			/* If we have different number of screens, 
			 * then stop checking, enough has changed
			 */
			if (_screens.Length != curr.Length) {
				_screens = curr;
				return true;
			}

			int length = _screens.Length;

			for (int i = 0; i < length; i++) {
				//if (doScreensDiffer(curr[i], _screens[i])) {
				if (curr[i] != _screens[i]) {
					_screens = curr;
					return true;
				}
			}

			return false;
		}

		private bool doScreensDiffer(Screen s1, Screen s2) {
			if (s1.BitsPerPixel != s2.BitsPerPixel) {
				return false;
			}

			if (s1.Bounds != s2.Bounds) {
				return false;
			}

			return true;
		}

		

		public event EventHandler ScreenChanged;
	}
}
