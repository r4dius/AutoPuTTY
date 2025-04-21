using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AutoPuTTY
{
    public enum PopupAlignment
    {
        TopLeft,
        TopCenter,
        TopRight,
        BottomLeft,
        BottomCenter,
        BottomRight,
        LeftTop,
        LeftCenter,
        LeftBottom,
        RightTop,
        RightCenter,
        RightBottom
    }

    internal class InfoPopupForm : Form
    {
        private readonly string _content;
        private const int CornerRadius = 4;  // tweak to your taste

        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        public InfoPopupForm(string content)
        {
            _content = content;

            AutoSize = true;
            BackColor = Color.Black;
            FormBorderStyle = FormBorderStyle.None;
            Opacity = 0.85;
            Padding = new Padding(10, 10, 10, 12);
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            TopMost = true;

            var infoLabel = new Label
            {
                AutoSize = true,
                ForeColor = Color.White,
                Font = new Font("Microsoft Sans Serif", 9),
                Location = new Point(Padding.Left, Padding.Top),
                Text = _content,
                //MaximumSize = new Size(400, 0), // Wrap long text
            };

            Controls.Add(infoLabel);

            // Resize form to fit label + padding
            int clientWidth = infoLabel.Width + Padding.Left + Padding.Right;
            int clientHeight = infoLabel.Height + Padding.Top + Padding.Bottom;
            ClientSize = new Size(clientWidth, clientHeight);

            // Rounded corners
            IntPtr roundedRegion = CreateRoundRectRgn(0, 0, clientWidth, clientHeight, CornerRadius, CornerRadius);
            Region = Region.FromHrgn(roundedRegion);

            // Close on focus loss or click
            Deactivate += (s, e) => Close();
            Click += (s, e) => Close();
            infoLabel.Click += (s, e) => Close();
        }

        public void ShowNear(Control anchorControl, PopupAlignment alignment)
        {
            var parentForm = anchorControl.FindForm();

            Point screenPoint = anchorControl.PointToScreen(Point.Empty);
            Size aSize = anchorControl.Size;
            Size pSize = Size;
            Rectangle screen = Screen.FromPoint(screenPoint).WorkingArea;

            int x = 0, y = 0;

            // Horizontal calculation
            switch (alignment)
            {
                case PopupAlignment.TopLeft:
                case PopupAlignment.BottomLeft:
                    x = screenPoint.X;
                    break;
                case PopupAlignment.TopCenter:
                case PopupAlignment.BottomCenter:
                    x = screenPoint.X + (aSize.Width - pSize.Width) / 2;
                    break;
                case PopupAlignment.TopRight:
                case PopupAlignment.BottomRight:
                    x = screenPoint.X + aSize.Width - pSize.Width;
                    break;
                case PopupAlignment.LeftTop:
                case PopupAlignment.LeftCenter:
                case PopupAlignment.LeftBottom:
                    x = screenPoint.X - pSize.Width;
                    break;
                case PopupAlignment.RightTop:
                case PopupAlignment.RightCenter:
                case PopupAlignment.RightBottom:
                    x = screenPoint.X + aSize.Width;
                    break;
            }

            // Vertical calculation
            switch (alignment)
            {
                case PopupAlignment.TopLeft:
                case PopupAlignment.TopCenter:
                case PopupAlignment.TopRight:
                    y = screenPoint.Y - pSize.Height;
                    break;
                case PopupAlignment.BottomLeft:
                case PopupAlignment.BottomCenter:
                case PopupAlignment.BottomRight:
                    y = screenPoint.Y + aSize.Height;
                    break;
                case PopupAlignment.LeftTop:
                case PopupAlignment.RightTop:
                    y = screenPoint.Y;
                    break;
                case PopupAlignment.LeftCenter:
                case PopupAlignment.RightCenter:
                    y = screenPoint.Y + (aSize.Height - pSize.Height) / 2;
                    break;
                case PopupAlignment.LeftBottom:
                case PopupAlignment.RightBottom:
                    y = screenPoint.Y + aSize.Height - pSize.Height;
                    break;
            }

            // Clamp within screen
            x = Math.Max(screen.Left, Math.Min(x, screen.Right - pSize.Width));
            y = Math.Max(screen.Top, Math.Min(y, screen.Bottom - pSize.Height));

            Location = new Point(x, y);
            Show();

            // Use BeginInvoke to activate the parent form immediately after the popup is closed
            this.Closed += (s, e) =>
            {
                parentForm?.BeginInvoke((Action)(() =>
                {
                    parentForm?.Activate();  // Activate the parent form
                }));
            };
        }
    }
}
