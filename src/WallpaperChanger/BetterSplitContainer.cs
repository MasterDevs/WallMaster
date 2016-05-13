using System.Windows.Forms;

namespace WallpaperChanger
{
    public class BetterSplitContainer : SplitContainer
    {
        private Control focused;

        public BetterSplitContainer()
            : base()
        {
            if (ParentForm != null)
                focused = getFocused(ParentForm.Controls);
            MouseDown += new MouseEventHandler(BetterSplitContainer_MouseDown);
            MouseUp += new MouseEventHandler(BetterSplitContainer_MouseUp);
        }

        private void BetterSplitContainer_MouseDown(object sender, MouseEventArgs e)
        {
            //-- Save previously focused control
            focused = getFocused(ParentForm.Controls);
        }

        private void BetterSplitContainer_MouseUp(object sender, MouseEventArgs e)
        {
            if (focused != null)
            {
                focused.Focus(); //-- Restore focus to previous control
                focused = null;
            }
        }

        private Control getFocused(ControlCollection controls)
        {
            foreach (Control c in controls)
            {
                if (c.Focused)
                    return c; //-- This control has focus
                else if (c.ContainsFocus)
                    return getFocused(c.Controls); //-- Child control has focus
            }
            return null; //-- No control had focus
        }
    }
}