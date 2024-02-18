using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Windows.Forms.VisualStyles;

namespace Fusionbird.FusionToolkit
{
    /// <summary>
    /// A trackbar that supports transparency
    /// </summary>
    public class FusionTrackBar : TrackBar
    {
        #region Fields

        private Rectangle ChannelBounds;
        private TrackBarOwnerDrawParts m_OwnerDrawParts;
        private Rectangle ThumbBounds;
        private int ThumbState;

        #endregion

        #region Public Events

        public event EventHandler<TrackBarDrawItemEventArgs> DrawChannel;
        public event EventHandler<TrackBarDrawItemEventArgs> DrawThumb;
        public event EventHandler<TrackBarDrawItemEventArgs> DrawTicks;

        #endregion

        #region Constructors

        public FusionTrackBar()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Overrides WndProc to intercept custom draw messages in  the message queue
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 20)
            {
                m.Result = IntPtr.Zero;
            }
            else
            {
                base.WndProc(ref m);
                if (m.Msg == 0x204e)
                {
                    NativeMethods.NMHDR structure = (NativeMethods.NMHDR)Marshal.PtrToStructure(m.LParam, typeof(NativeMethods.NMHDR));
                    if (structure.code == -12)
                    {
                        IntPtr ptr;
                        Marshal.StructureToPtr(structure, m.LParam, false);
                        NativeMethods.NMCUSTOMDRAW nmcustomdraw = (NativeMethods.NMCUSTOMDRAW)Marshal.PtrToStructure(m.LParam, typeof(NativeMethods.NMCUSTOMDRAW));
                        if (nmcustomdraw.dwDrawStage == NativeMethods.CustomDrawDrawStage.CDDS_PREPAINT)
                        {
                            Graphics graphics = Graphics.FromHdc(nmcustomdraw.hdc);
                            PaintEventArgs e = new PaintEventArgs(graphics, Bounds);
                            e.Graphics.TranslateTransform((0 - Left), (0 - Top));
                            InvokePaintBackground(Parent, e);
                            InvokePaint(Parent, e);
                            SolidBrush brush = new SolidBrush(BackColor);
                            e.Graphics.FillRectangle(brush, Bounds);
                            brush.Dispose();
                            e.Graphics.ResetTransform();
                            e.Dispose();
                            graphics.Dispose();
                            ptr = new IntPtr(0x30);
                            m.Result = ptr;
                        }
                        else if (nmcustomdraw.dwDrawStage == NativeMethods.CustomDrawDrawStage.CDDS_POSTPAINT)
                        {
                            OnDrawTicks(nmcustomdraw.hdc);
                            OnDrawChannel(nmcustomdraw.hdc);
                            OnDrawThumb(nmcustomdraw.hdc);
                        }
                        else if (nmcustomdraw.dwDrawStage == NativeMethods.CustomDrawDrawStage.CDDS_ITEMPREPAINT)
                        {
                            if (nmcustomdraw.dwItemSpec.ToInt32() == 2)
                            {
                                ThumbBounds = nmcustomdraw.rc.ToRectangle();
                                if (Enabled)
                                {
                                    if (nmcustomdraw.uItemState == NativeMethods.CustomDrawItemState.CDIS_SELECTED)
                                    {
                                        ThumbState = 3;
                                    }
                                    else
                                    {
                                        ThumbState = 1;
                                    }
                                }
                                else
                                {
                                    ThumbState = 5;
                                }
                                OnDrawThumb(nmcustomdraw.hdc);
                            }
                            else if (nmcustomdraw.dwItemSpec.ToInt32() == 3)
                            {
                                ChannelBounds = nmcustomdraw.rc.ToRectangle();
                                OnDrawChannel(nmcustomdraw.hdc);
                            }
                            else if (nmcustomdraw.dwItemSpec.ToInt32() == 1)
                            {
                                OnDrawTicks(nmcustomdraw.hdc);
                            }
                            ptr = new IntPtr(4);
                            m.Result = ptr;
                        }
                    }
                }
            }
        }

        #endregion

        #region Private Drawing Methods

        /// <summary>
        /// Draws horizontal ticks
        /// </summary>
        /// <param name="g">The graphics context</param>
        /// <param name="color">The colour of the ticks</param>
        private void DrawHorizontalTicks(Graphics g, Color color)
        {
            RectangleF innerTickRect;
            int numofTicks = (Maximum / TickFrequency) - 1;
            Pen tickPen = new Pen(color);
            RectangleF endTickRRect = new RectangleF((ChannelBounds.Left + (ThumbBounds.Width / 2)), (ThumbBounds.Top - 5), 0f, 3f);
            RectangleF endTickLRect = new RectangleF(((ChannelBounds.Right - (ThumbBounds.Width / 2)) - 1), (ThumbBounds.Top - 5), 0f, 3f);
            float tickPitch = (endTickLRect.Right - endTickRRect.Left) / ((numofTicks + 1));
            // Draw upper (top) ticks
            if (TickStyle != TickStyle.BottomRight)
            {
                // Draw right outer tick
                g.DrawLine(tickPen, endTickRRect.Left, endTickRRect.Top, endTickRRect.Right, endTickRRect.Bottom);
                // Draw left outer tick
                g.DrawLine(tickPen, endTickLRect.Left, endTickLRect.Top, endTickLRect.Right, endTickLRect.Bottom);
                // Draw inner ticks
                innerTickRect = endTickRRect;
                innerTickRect.Height--;
                innerTickRect.Offset(tickPitch, 1f);
                int numOfInnerTicks = numofTicks - 1;
                for (int i = 0; i <= numOfInnerTicks; i++)
                {
                    g.DrawLine(tickPen, innerTickRect.Left, innerTickRect.Top, innerTickRect.Left, innerTickRect.Bottom);
                    innerTickRect.Offset(tickPitch, 0f);
                }
            }
            endTickRRect.Offset(0f, (ThumbBounds.Height + 6));
            endTickLRect.Offset(0f, (ThumbBounds.Height + 6));
            // Draw lower (bottom) ticks
            if (TickStyle != TickStyle.TopLeft)
            {
                // Draw right outer tick
                g.DrawLine(tickPen, endTickRRect.Left, endTickRRect.Top, endTickRRect.Left, endTickRRect.Bottom);
                // Draw left outer tick
                g.DrawLine(tickPen, endTickLRect.Left, endTickLRect.Top, endTickLRect.Left, endTickLRect.Bottom);
                // Draw innerticks
                innerTickRect = endTickRRect;
                innerTickRect.Height--;
                innerTickRect.Offset(tickPitch, 0f);
                int numOfInnerTicks = numofTicks - 1;
                for (int j = 0; j <= numOfInnerTicks; j++)
                {
                    g.DrawLine(tickPen, innerTickRect.Left, innerTickRect.Top, innerTickRect.Left, innerTickRect.Bottom);
                    innerTickRect.Offset(tickPitch, 0f);
                }
            }
            tickPen.Dispose();
        }

        /// <summary>
        /// Draws the ticks on a vertical track-bar
        /// </summary>
        /// <param name="g">The graphics context</param>
        /// <param name="color">The colour of which to draw the ticks</param>
        private void DrawVerticalTicks(Graphics g, Color color)
        {
            RectangleF innerTickRect;
            int numOfTicks = (Maximum / TickFrequency) - 1;
            Pen tickPen = new Pen(color);
            RectangleF endTickBottomRect = new RectangleF((ThumbBounds.Left - 5), ((ChannelBounds.Bottom - (ThumbBounds.Height / 2)) - 1), 3f, 0f);
            RectangleF endTickTopRect = new RectangleF((ThumbBounds.Left - 5), (ChannelBounds.Top + (ThumbBounds.Height / 2)), 3f, 0f);
            float y = (endTickTopRect.Bottom - endTickBottomRect.Top) / ((numOfTicks + 1));
            // Draw left-hand ticks
            if (TickStyle != TickStyle.BottomRight)
            {
                // Draw lower (bottom) outer tick
                g.DrawLine(tickPen, endTickBottomRect.Left, endTickBottomRect.Top, endTickBottomRect.Right, endTickBottomRect.Bottom);
                // Draw upper (top) outer tick
                g.DrawLine(tickPen, endTickTopRect.Left, endTickTopRect.Top, endTickTopRect.Right, endTickTopRect.Bottom);
                // Draw inner ticks
                innerTickRect = endTickBottomRect;
                innerTickRect.Width--;
                innerTickRect.Offset(1f, y);
                int numOfInnerTicks = numOfTicks - 1;
                for (int i = 0; i <= numOfInnerTicks; i++)
                {
                    g.DrawLine(tickPen, innerTickRect.Left, innerTickRect.Top, innerTickRect.Right, innerTickRect.Bottom);
                    innerTickRect.Offset(0f, y);
                }
            }
            endTickBottomRect.Offset((ThumbBounds.Width + 6), 0f);
            endTickTopRect.Offset((ThumbBounds.Width + 6), 0f);
            // Draw right-hand ticks
            if (TickStyle != TickStyle.TopLeft)
            {
                // Draw lower (bottom) tick
                g.DrawLine(tickPen, endTickBottomRect.Left, endTickBottomRect.Top, endTickBottomRect.Right, endTickBottomRect.Bottom);
                // Draw upper (top) tick
                g.DrawLine(tickPen, endTickTopRect.Left, endTickTopRect.Top, endTickTopRect.Right, endTickTopRect.Bottom);
                // Draw inner ticks
                innerTickRect = endTickBottomRect;
                innerTickRect.Width--;
                innerTickRect.Offset(0f, y);
                int numOfInnerTicks = numOfTicks - 1;
                for (int j = 0; j <= numOfInnerTicks; j++)
                {
                    g.DrawLine(tickPen, innerTickRect.Left, innerTickRect.Top, innerTickRect.Right, innerTickRect.Bottom);
                    innerTickRect.Offset(0f, y);
                }
            }
            tickPen.Dispose();
        }

        /// <summary>
        /// Draw a downward pointer
        /// </summary>
        /// <param name="g">The graphics context</param>
        private void DrawPointerDown(Graphics g)
        {
            Point[] points = new Point[6]
                                 {
                                     new Point(ThumbBounds.Left + (ThumbBounds.Width/2), ThumbBounds.Bottom - 1),
                                     new Point(ThumbBounds.Left, (ThumbBounds.Bottom - (ThumbBounds.Width/2)) - 1), ThumbBounds.Location,
                                     new Point(ThumbBounds.Right - 1, ThumbBounds.Top),
                                     new Point(ThumbBounds.Right - 1, (ThumbBounds.Bottom - (ThumbBounds.Width/2)) - 1),
                                     new Point(ThumbBounds.Left + (ThumbBounds.Width/2), ThumbBounds.Bottom - 1)
                                 };
            GraphicsPath path = new GraphicsPath();
            path.AddLines(points);
            Region region = new Region(path);
            g.Clip = region;
            if ((ThumbState == 3) || !Enabled)
            {
                ControlPaint.DrawButton(g, ThumbBounds, ButtonState.All);
            }
            else
            {
                g.Clear(SystemColors.Control);
            }
            g.ResetClip();
            region.Dispose();
            path.Dispose();
            // Draw light shadow
            Point[] shadowPoints = new Point[] { points[0], points[1], points[2], points[3] };
            g.DrawLines(SystemPens.ControlLightLight, shadowPoints);
            // Draw dark shadow
            shadowPoints = new Point[] { points[3], points[4], points[5] };
            g.DrawLines(SystemPens.ControlDarkDark, shadowPoints);
            points[0].Offset(0, -1);
            points[1].Offset(1, 0);
            points[2].Offset(1, 1);
            points[3].Offset(-1, 1);
            points[4].Offset(-1, 0);
            points[5] = points[0];
            shadowPoints = new Point[] { points[0], points[1], points[2], points[3] };
            g.DrawLines(SystemPens.ControlLight, shadowPoints);
            shadowPoints = new Point[] { points[3], points[4], points[5] };
            g.DrawLines(SystemPens.ControlDark, shadowPoints);
        }

        /// <summary>
        /// Draw left-facing pointer
        /// </summary>
        /// <param name="g">The graphics context</param>
        private void DrawPointerLeft(Graphics g)
        {
            Point[] points = new Point[6]
                                 {
                                     new Point(ThumbBounds.Left, ThumbBounds.Top + (ThumbBounds.Height/2)),
                                     new Point(ThumbBounds.Left + (ThumbBounds.Height/2), ThumbBounds.Top),
                                     new Point(ThumbBounds.Right - 1, ThumbBounds.Top),
                                     new Point(ThumbBounds.Right - 1, ThumbBounds.Bottom - 1),
                                     new Point(ThumbBounds.Left + (ThumbBounds.Height/2), ThumbBounds.Bottom - 1),
                                     new Point(ThumbBounds.Left, ThumbBounds.Top + (ThumbBounds.Height/2)),
                                 };
            GraphicsPath path = new GraphicsPath();
            path.AddLines(points);
            Region region = new Region(path);
            g.Clip = region;
            if ((ThumbState == 3) || !Enabled)
            {
                ControlPaint.DrawButton(g, ThumbBounds, ButtonState.All);
            }
            else
            {
                g.Clear(SystemColors.Control);
            }
            g.ResetClip();
            region.Dispose();
            path.Dispose();
            // Draw light shadow
            Point[] shadowPoints = new Point[] { points[0], points[1], points[2] };
            g.DrawLines(SystemPens.ControlLightLight, shadowPoints);
            // Draw dark shadow
            shadowPoints = new Point[] { points[2], points[3], points[4], points[5] };
            g.DrawLines(SystemPens.ControlDarkDark, shadowPoints);
            points[0].Offset(1, 0);
            points[1].Offset(0, 1);
            points[2].Offset(-1, 1);
            points[3].Offset(-1, -1);
            points[4].Offset(0, -1);
            points[5] = points[0];
            shadowPoints = new Point[] { points[0], points[1], points[2] };
            g.DrawLines(SystemPens.ControlLight, shadowPoints);
            shadowPoints = new Point[] { points[2], points[3], points[4], points[5] };
            g.DrawLines(SystemPens.ControlDark, shadowPoints);
        }

        /// <summary>
        /// Draws a right-facing pointer
        /// </summary>
        /// <param name="g">The graphics context</param>
        private void DrawPointerRight(Graphics g)
        {
            Point[] points = new Point[6]
                                 {
                                     new Point(ThumbBounds.Left, ThumbBounds.Bottom - 1),
                                     new Point(ThumbBounds.Left, ThumbBounds.Top),
                                     new Point((ThumbBounds.Right - (ThumbBounds.Height/2)) - 1, ThumbBounds.Top),
                                     new Point(ThumbBounds.Right - 1, ThumbBounds.Top + (ThumbBounds.Height/2)),
                                     new Point((ThumbBounds.Right - (ThumbBounds.Height/2)) - 1, ThumbBounds.Bottom - 1),
                                     new Point(ThumbBounds.Left, ThumbBounds.Bottom - 1)
                                 };
            GraphicsPath path = new GraphicsPath();
            path.AddLines(points);
            Region region = new Region(path);
            g.Clip = region;
            if ((ThumbState == 3) || !Enabled)
            {
                ControlPaint.DrawButton(g, ThumbBounds, ButtonState.All);
            }
            else
            {
                g.Clear(SystemColors.Control);
            }
            g.ResetClip();
            region.Dispose();
            path.Dispose();
            // Draw light shadow
            Point[] shadowPoints = new Point[] { points[0], points[1], points[2], points[3] };
            g.DrawLines(SystemPens.ControlLightLight, shadowPoints);
            // Draw dark shadow
            shadowPoints = new Point[] { points[3], points[4], points[5] };
            g.DrawLines(SystemPens.ControlDarkDark, shadowPoints);
            points[0].Offset(1, -1);
            points[1].Offset(1, 1);
            points[2].Offset(0, 1);
            points[3].Offset(-1, 0);
            points[4].Offset(0, -1);
            points[5] = points[0];
            shadowPoints = new Point[] { points[0], points[1], points[2], points[3] };
            g.DrawLines(SystemPens.ControlLight, shadowPoints);
            shadowPoints = new Point[] { points[3], points[4], points[5] };
            g.DrawLines(SystemPens.ControlDark, shadowPoints);
        }

        /// <summary>
        /// Draws an upwards facing pointer
        /// </summary>
        /// <param name="g">The graphics context</param>
        private void DrawPointerUp(Graphics g)
        {
            Point[] points = new Point[6]
                                 {
                                     new Point(ThumbBounds.Left, ThumbBounds.Bottom - 1),
                                     new Point(ThumbBounds.Left, ThumbBounds.Top + (ThumbBounds.Width/2)),
                                     new Point(ThumbBounds.Left + (ThumbBounds.Width/2), ThumbBounds.Top),
                                     new Point(ThumbBounds.Right - 1, ThumbBounds.Top + (ThumbBounds.Width/2)),
                                     new Point(ThumbBounds.Right - 1, ThumbBounds.Bottom - 1),
                                     new Point(ThumbBounds.Left, ThumbBounds.Bottom - 1)
                                 };
            GraphicsPath path = new GraphicsPath();
            path.AddLines(points);
            Region region = new Region(path);
            g.Clip = region;
            if ((ThumbState == 3) || !Enabled)
            {
                ControlPaint.DrawButton(g, ThumbBounds, ButtonState.All);
            }
            else
            {
                g.Clear(SystemColors.Control);
            }
            g.ResetClip();
            region.Dispose();
            path.Dispose();
            // Draw light shadow
            Point[] shadowPoints = new Point[] { points[0], points[1], points[2] };
            g.DrawLines(SystemPens.ControlLightLight, shadowPoints);
            // Draw dark shadow
            shadowPoints = new Point[] { points[2], points[3], points[4], points[5] };
            g.DrawLines(SystemPens.ControlDarkDark, shadowPoints);
            points[0].Offset(1, -1);
            points[1].Offset(1, 0);
            points[2].Offset(0, 1);
            points[3].Offset(-1, 0);
            points[4].Offset(-1, -1);
            points[5] = points[0];
            shadowPoints = new Point[] { points[0], points[1], points[2] };
            g.DrawLines(SystemPens.ControlLight, shadowPoints);
            shadowPoints = new Point[] { points[2], points[3], points[4], points[5] };
            g.DrawLines(SystemPens.ControlDark, shadowPoints);
        }

        #endregion

        #region Event Invoker Methods

        /// <summary>
        /// Fires the tick drawing events
        /// </summary>
        /// <param name="hdc">The device context that holds the graphics operations</param>
        protected virtual void OnDrawTicks(IntPtr hdc)
        {
            Graphics graphics = Graphics.FromHdc(hdc);
            if (((OwnerDrawParts & TrackBarOwnerDrawParts.Ticks) == TrackBarOwnerDrawParts.Ticks) && !DesignMode)
            {
                TrackBarDrawItemEventArgs e = new TrackBarDrawItemEventArgs(graphics, ClientRectangle, (TrackBarItemState)ThumbState);
                if (DrawTicks != null)
                {
                    DrawTicks(this, e);
                }
            }
            else
            {
                if (TickStyle == TickStyle.None)
                {
                    return;
                }
                if (ThumbBounds.Equals(Rectangle.Empty))
                {
                    return;
                }
                Color black = Color.Black;
                if (VisualStyleRenderer.IsSupported)
                {
                    VisualStyleRenderer vsr = new VisualStyleRenderer("TRACKBAR", (int)NativeMethods.TrackBarParts.TKP_TICS, ThumbState);
                    black = vsr.GetColor(ColorProperty.TextColor);
                }
                if (Orientation == Orientation.Horizontal)
                {
                    DrawHorizontalTicks(graphics, black);
                }
                else
                {
                    DrawVerticalTicks(graphics, black);
                }
            }
            graphics.Dispose();
        }

        /// <summary>
        /// Fires the DrawThumb events
        /// </summary>
        /// <param name="hdc">The device context for graphics operations</param>
        protected virtual void OnDrawThumb(IntPtr hdc)
        {
            Graphics graphics = Graphics.FromHdc(hdc);
            graphics.Clip = new Region(ThumbBounds);
            if (((OwnerDrawParts & TrackBarOwnerDrawParts.Thumb) == TrackBarOwnerDrawParts.Thumb) && !DesignMode)
            {
                TrackBarDrawItemEventArgs e = new TrackBarDrawItemEventArgs(graphics, ThumbBounds, (TrackBarItemState)ThumbState);
                if (DrawThumb != null)
                {
                    DrawThumb(this, e);
                }
            }
            else
            {
                // Determine the style of the thumb, based on the tickstyle
                NativeMethods.TrackBarParts part = NativeMethods.TrackBarParts.TKP_THUMB;
                if (ThumbBounds.Equals(Rectangle.Empty))
                {
                    return;
                }
                switch (TickStyle)
                {
                    case TickStyle.None:
                    case TickStyle.BottomRight:
                        part = (Orientation != Orientation.Horizontal) ? NativeMethods.TrackBarParts.TKP_THUMBRIGHT : NativeMethods.TrackBarParts.TKP_THUMBBOTTOM;
                        break;
                    case TickStyle.TopLeft:
                        part = (Orientation != Orientation.Horizontal) ? NativeMethods.TrackBarParts.TKP_THUMBLEFT : NativeMethods.TrackBarParts.TKP_THUMBTOP;
                        break;

                    case TickStyle.Both:
                        part = (Orientation != Orientation.Horizontal) ? NativeMethods.TrackBarParts.TKP_THUMBVERT : NativeMethods.TrackBarParts.TKP_THUMB;
                        break;
                }
                // Perform drawing
                if (VisualStyleRenderer.IsSupported)
                {
                    VisualStyleRenderer vsr = new VisualStyleRenderer("TRACKBAR", (int)part, ThumbState);
                    vsr.DrawBackground(graphics, ThumbBounds);
                    graphics.ResetClip();
                    graphics.Dispose();
                    return;
                }
                switch (part)
                {
                    case NativeMethods.TrackBarParts.TKP_THUMBBOTTOM:
                        DrawPointerDown(graphics);
                        break;
                    case NativeMethods.TrackBarParts.TKP_THUMBTOP:
                        DrawPointerUp(graphics);

                        break;
                    case NativeMethods.TrackBarParts.TKP_THUMBLEFT:
                        DrawPointerLeft(graphics);

                        break;
                    case NativeMethods.TrackBarParts.TKP_THUMBRIGHT:
                        DrawPointerRight(graphics);

                        break;
                    default:
                        if ((ThumbState == 3) || !Enabled)
                        {
                            ControlPaint.DrawButton(graphics, ThumbBounds, ButtonState.All);
                        }
                        else
                        {
                            // Tick-style is both - draw the thumb as a solid rectangle
                            graphics.FillRectangle(SystemBrushes.Control, ThumbBounds);
                        }
                        ControlPaint.DrawBorder3D(graphics, ThumbBounds, Border3DStyle.Raised);
                        break;
                }
            }
            graphics.ResetClip();
            graphics.Dispose();
        }

        /// <summary>
        /// Fires the DrawChannel events
        /// </summary>
        /// <param name="hdc">The device context for graphics operations</param>
        protected virtual void OnDrawChannel(IntPtr hdc)
        {
            Graphics graphics = Graphics.FromHdc(hdc);
            if (((OwnerDrawParts & TrackBarOwnerDrawParts.Channel) == TrackBarOwnerDrawParts.Channel) && !DesignMode)
            {
                TrackBarDrawItemEventArgs e = new TrackBarDrawItemEventArgs(graphics, ChannelBounds, (TrackBarItemState)ThumbState);
                if (DrawChannel != null)
                {
                    DrawChannel(this, e);
                }
            }
            else
            {
                if (ChannelBounds.Equals(Rectangle.Empty))
                {
                    return;
                }
                if (VisualStyleRenderer.IsSupported)
                {
                    VisualStyleRenderer vsr = new VisualStyleRenderer("TRACKBAR", (int)NativeMethods.TrackBarParts.TKP_TRACK, (int)TrackBarItemState.Normal);
                    vsr.DrawBackground(graphics, ChannelBounds);
                    graphics.ResetClip();
                    graphics.Dispose();
                    return;
                }
                ControlPaint.DrawBorder3D(graphics, ChannelBounds, Border3DStyle.Sunken);
            }
            graphics.Dispose();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets which parts of the trackbar will be owner drawn
        /// </summary>
        [DefaultValue(typeof(TrackBarOwnerDrawParts), "None")]
        [Description("Gets/sets the trackbar parts that will be OwnerDrawn.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Editor(typeof(TrackDrawModeEditor), typeof(UITypeEditor))]
        public TrackBarOwnerDrawParts OwnerDrawParts
        {
            get { return m_OwnerDrawParts; }
            set { m_OwnerDrawParts = value; }
        }

        #endregion
    }
    /// <summary>
    /// Adds some options under the OwnerDraw item in the designer
    /// </summary>
    public class TrackDrawModeEditor : UITypeEditor
    {
        #region Methods

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            TrackBarOwnerDrawParts parts = TrackBarOwnerDrawParts.None;
            if (!(value is TrackBarOwnerDrawParts) || (provider == null))
            {
                return value;
            }
            IWindowsFormsEditorService service = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
            if (service == null)
            {
                return value;
            }
            CheckedListBox control = new CheckedListBox();
            control.BorderStyle = BorderStyle.None;
            control.CheckOnClick = true;
            control.Items.Add("Ticks", (((FusionTrackBar)context.Instance).OwnerDrawParts & TrackBarOwnerDrawParts.Ticks) == TrackBarOwnerDrawParts.Ticks);
            control.Items.Add("Thumb", (((FusionTrackBar)context.Instance).OwnerDrawParts & TrackBarOwnerDrawParts.Thumb) == TrackBarOwnerDrawParts.Thumb);
            control.Items.Add("Channel", (((FusionTrackBar)context.Instance).OwnerDrawParts & TrackBarOwnerDrawParts.Channel) == TrackBarOwnerDrawParts.Channel);
            service.DropDownControl(control);
            IEnumerator enumerator = control.CheckedItems.GetEnumerator();
            while (enumerator.MoveNext())
            {
                object objectValue = RuntimeHelpers.GetObjectValue(enumerator.Current);
                parts |= (TrackBarOwnerDrawParts)Enum.Parse(typeof(TrackBarOwnerDrawParts), objectValue.ToString());
            }
            control.Dispose();
            service.CloseDropDown();
            return parts;
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        #endregion
    }

    public class TrackBarDrawItemEventArgs : EventArgs
    {
        #region Fields

        private Rectangle _bounds;
        private Graphics _graphics;
        private TrackBarItemState _state;

        #endregion

        #region Methods

        public TrackBarDrawItemEventArgs(Graphics graphics, Rectangle bounds, TrackBarItemState state)
        {
            _graphics = graphics;
            _bounds = bounds;
            _state = state;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the bounds
        /// </summary>
        public Rectangle Bounds
        {
            get
            {
                return _bounds;
            }
        }

        /// <summary>
        /// Gets the graphics context
        /// </summary>
        public Graphics Graphics
        {
            get
            {
                return _graphics;
            }
        }

        /// <summary>
        /// Gets the state of the item
        /// </summary>
        public TrackBarItemState State
        {
            get
            {
                return _state;
            }
        }

        #endregion
    }

    [Flags]
    public enum TrackBarOwnerDrawParts
    {
        Channel = 4,
        None = 0,
        Thumb = 2,
        Ticks = 1
    }

    public enum TrackBarItemState
    {
        Active = 3,
        Disabled = 5,
        Hot = 2,
        Normal = 1
    }

    internal class NativeMethods
    {
        # region Constants

        public const int NM_CUSTOMDRAW = -12;
        public const int NM_FIRST = 0;
        public const int S_OK = 0;
        public const int TMT_COLOR = 0xcc;

        #endregion

        #region Constructors

        private NativeMethods()
        {
        }

        #endregion

        #region Imports

        [DllImport("UxTheme.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int CloseThemeData(IntPtr hTheme);
        [DllImport("Comctl32.dll", EntryPoint = "DllGetVersion", CallingConvention = CallingConvention.Cdecl)]
        public static extern int CommonControlsGetVersion(ref DLLVERSIONINFO pdvi);
        [DllImport("UxTheme.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int DrawThemeBackground(IntPtr hTheme, IntPtr hdc, int iPartId, int iStateId, ref RECT pRect, ref RECT pClipRect);
        [DllImport("UxTheme.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetThemeColor(IntPtr hTheme, int iPartId, int iStateId, int iPropId, ref int pColor);
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("UxTheme.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool IsAppThemed();
        [DllImport("UxTheme.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern IntPtr OpenThemeData(IntPtr hwnd, string pszClassList);

        #endregion

        #region Enumerations

        public enum CustomDrawDrawStage
        {
            CDDS_ITEM = 0x10000,
            CDDS_ITEMPOSTERASE = 0x10004,
            CDDS_ITEMPOSTPAINT = 0x10002,
            CDDS_ITEMPREERASE = 0x10003,
            CDDS_ITEMPREPAINT = 0x10001,
            CDDS_POSTERASE = 4,
            CDDS_POSTPAINT = 2,
            CDDS_PREERASE = 3,
            CDDS_PREPAINT = 1,
            CDDS_SUBITEM = 0x20000
        }

        public enum CustomDrawItemState
        {
            CDIS_CHECKED = 8,
            CDIS_DEFAULT = 0x20,
            CDIS_DISABLED = 4,
            CDIS_FOCUS = 0x10,
            CDIS_GRAYED = 2,
            CDIS_HOT = 0x40,
            CDIS_INDETERMINATE = 0x100,
            CDIS_MARKED = 0x80,
            CDIS_SELECTED = 1,
            CDIS_SHOWKEYBOARDCUES = 0x200
        }

        public enum CustomDrawReturnFlags
        {
            CDRF_DODEFAULT = 0,
            CDRF_NEWFONT = 2,
            CDRF_NOTIFYITEMDRAW = 0x20,
            CDRF_NOTIFYPOSTERASE = 0x40,
            CDRF_NOTIFYPOSTPAINT = 0x10,
            CDRF_NOTIFYSUBITEMDRAW = 0x20,
            CDRF_SKIPDEFAULT = 4
        }

        public enum TrackBarCustomDrawPart
        {
            TBCD_CHANNEL = 3,
            TBCD_THUMB = 2,
            TBCD_TICS = 1
        }

        public enum TrackBarParts
        {
            TKP_THUMB = 3,
            TKP_THUMBBOTTOM = 4,
            TKP_THUMBLEFT = 7,
            TKP_THUMBRIGHT = 8,
            TKP_THUMBTOP = 5,
            TKP_THUMBVERT = 6,
            TKP_TICS = 9,
            TKP_TICSVERT = 10,
            TKP_TRACK = 1,
            TKP_TRACKVERT = 2
        }

        #endregion

        #region Structures

        [StructLayout(LayoutKind.Sequential)]
        public struct DLLVERSIONINFO
        {
            public int cbSize;
            public int dwMajorVersion;
            public int dwMinorVersion;
            public int dwBuildNumber;
            public int dwPlatformID;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NMCUSTOMDRAW
        {
            public NMHDR hdr;
            public CustomDrawDrawStage dwDrawStage;
            public IntPtr hdc;
            public RECT rc;
            public IntPtr dwItemSpec;
            public CustomDrawItemState uItemState;
            public IntPtr lItemlParam;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NMHDR
        {
            public IntPtr HWND;
            public int idFrom;
            public int code;
            public override string ToString()
            {
                return string.Format(CultureInfo.InvariantCulture, "Hwnd: {0}, ControlID: {1}, Code: {2}", new object[] { HWND, idFrom, code });
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
            public RECT(Rectangle rect)
            {
                this = new RECT();
                Left = rect.Left;
                Top = rect.Top;
                Right = rect.Right;
                Bottom = rect.Bottom;
            }

            public override string ToString()
            {
                return string.Format(CultureInfo.InvariantCulture, "{0}, {1}, {2}, {3}", new object[] { Left, Top, Right, Bottom });
            }

            public Rectangle ToRectangle()
            {
                return Rectangle.FromLTRB(Left, Top, Right, Bottom);
            }
        }

        #endregion
    }
}