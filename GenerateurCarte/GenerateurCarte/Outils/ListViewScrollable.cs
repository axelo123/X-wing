using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Outils
{
    public class ListViewScrollable : ListView
    {
        #region Windows API
        [Serializable, System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
        private struct SCROLLINFO
        {
            public uint cbSize;
            public uint fMask;
            public int nMin;
            public int nMax;
            public uint nPage;
            public int nPos;
            public int nTrackPos;
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Bool)]
        private static extern bool GetScrollInfo(IntPtr hwnd, int fnBar, ref SCROLLINFO lpsi);

        private enum ScrollBarMessages : uint
        {
            WM_HSCROLL = 0x114,
            WM_VSCROLL = 0x115
        }

        private enum ScrollBarCommands : uint
        {
            SB_LINEUP = 0,
            SB_LINELEFT = 0,
            SB_LINEDOWN = 1,
            SB_LINERIGHT = 1,
            SB_PAGEUP = 2,
            SB_PAGELEFT = 2,
            SB_PAGEDOWN = 3,
            SB_PAGERIGHT = 3,
            SB_THUMBPOSITION = 4,
            SB_THUMBTRACK = 5,
            SB_TOP = 6,
            SB_LEFT = 6,
            SB_BOTTOM = 7,
            SB_RIGHT = 7,
            SB_ENDSCROLL = 8
        }

        private enum ScrollBarConstants : uint
        {
            SB_HORZ = 0,
            SB_VERT = 1,
            SB_CTL = 2,
            SB_BOTH = 3
        }

        private enum ScrollInfoMask : uint
        {
            SIF_RANGE = 0x1,
            SIF_PAGE = 0x2,
            SIF_POS = 0x4,
            SIF_DISABLENOSCROLL = 0x8,
            SIF_TRACKPOS = 0x10,
            SIF_ALL = (SIF_RANGE | SIF_PAGE | SIF_POS | SIF_TRACKPOS),
        }
        #endregion

        /// <summary>
        /// Types de changement détecté au niveau de la barre de scrolling
        /// </summary>
        public enum Queries
        {
            Unknown,
            Top,
            Bottom,
            Left = Top,
            Right = Bottom,
            LineUp,
            LineDown,
            LineLeft = LineUp,
            LineRight = LineDown,
            PageUp,
            PageDown,
            PageLeft = PageUp,
            PageRight = PageDown,
            ThumbTrack,
            ThumbPosition,
            EndScroll
        }

        /// <summary>
        /// Description d'un événement de changement au niveau de la barre de scrolling
        /// </summary>
        public class EventArgs : System.EventArgs
        {
            #region Membres pirvés
            private Queries m_Query;
            private int m_Position, m_Minimum, m_Maximum, m_PageSize;
            #endregion

            /// <summary>
            /// Type de changement détecté
            /// </summary>
            public Queries Query { get { return m_Query; } }

            /// <summary>
            /// Position de la barre de scrolling
            /// </summary>
            public int Position { get { return m_Position; } }

            /// <summary>
            /// Valeur minimale de la barre de scrolling
            /// </summary>
            public int Minimum { get { return m_Minimum; } }

            /// <summary>
            /// Valeur maximale de la barre de scrolling
            /// </summary>
            public int Maximum { get { return m_Maximum; } }

            /// <summary>
            /// Taille d'une page, c'est à dire étendue de la zône de visualisation et gérée par la barre de scrolling
            /// </summary>
            public int PageSize { get { return m_PageSize; } }

            /// <summary>
            /// Constructeur
            /// </summary>
            /// <param name="Query">Type de changement</param>
            /// <param name="Position">Position</param>
            /// <param name="Minimum">Valeur minimale</param>
            /// <param name="Maximum">Valeur maximale</param>
            /// <param name="PageSize">Taille de page</param>
            public EventArgs(Queries Query, int Position, int Minimum, int Maximum, int PageSize)
            {
                m_Query = Query;
                m_Position = Position;
                m_Minimum = Minimum;
                m_Maximum = Maximum;
                m_PageSize = PageSize;
            }
        }

        /// <summary>
        /// Sur un changement survenu au sein de la barre de scrolling vertical
        /// </summary>
        public event EventHandler<EventArgs> OnHorizontalScroll = null;

        /// <summary>
        /// Sur un changement survenu au sein de la barre de scrolling vertical
        /// </summary>
        public event EventHandler<EventArgs> OnVerticalScroll = null;

        /// <summary>
        /// Procédure de fenêtre
        /// </summary>
        /// <param name="m">Descriptif du message de fenêtre qu'a reçu ce contrôle</param>
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            for (int Direction = 0; Direction < 2; Direction++)
            {
                if (((Direction == 0) && (m.Msg == (int)ScrollBarMessages.WM_HSCROLL) && (OnHorizontalScroll != null))
                    || ((Direction == 1) && (m.Msg == (int)ScrollBarMessages.WM_VSCROLL) && (OnVerticalScroll != null)))
                {
                    Queries Query = Queries.Unknown;
                    SCROLLINFO ScrollInfo = new SCROLLINFO();
                    ScrollInfo.fMask = (uint)(ScrollInfoMask.SIF_RANGE | ScrollInfoMask.SIF_PAGE | ScrollInfoMask.SIF_POS);
                    try
                    {
                        if (!GetScrollInfo(this.Handle, (Direction == 0) ? (int)ScrollBarConstants.SB_HORZ : (int)ScrollBarConstants.SB_VERT, ref ScrollInfo)) return;
                    }
                    catch
                    {
                        return;
                    }
                    switch (m.WParam.ToInt32() & 0xffff)
                    {
                        case (int)ScrollBarCommands.SB_TOP: // ScrollBarCommands.SB_LEFT:
                            Query = Queries.Top;
                            break;
                        case (int)ScrollBarCommands.SB_BOTTOM: // ScrollBarCommands.SB_RIGHT:
                            Query = Queries.Bottom;
                            break;
                        case (int)ScrollBarCommands.SB_LINEUP: // ScrollBarCommands.SB_LINELEFT
                            Query = Queries.LineUp;
                            break;
                        case (int)ScrollBarCommands.SB_LINEDOWN: // ScrollBarCommands.SB_LINERIGHT
                            Query = Queries.LineDown;
                            break;
                        case (int)ScrollBarCommands.SB_PAGEUP: // ScrollBarCommands.SB_PAGELEFT
                            Query = Queries.PageUp;
                            break;
                        case (int)ScrollBarCommands.SB_PAGEDOWN: // ScrollBarCommands.SB_PAGERIGHT
                            Query = Queries.PageDown;
                            break;
                        case (int)ScrollBarCommands.SB_THUMBTRACK:
                            Query = Queries.ThumbTrack;
                            ScrollInfo.nPos = m.WParam.ToInt32() >> 16;
                            break;
                        case (int)ScrollBarCommands.SB_THUMBPOSITION:
                            Query = Queries.ThumbPosition;
                            ScrollInfo.nPos = m.WParam.ToInt32() >> 16;
                            break;
                        case (int)ScrollBarCommands.SB_ENDSCROLL:
                            Query = Queries.EndScroll;
                            break;
                    }
                    ((Direction == 0) ? OnHorizontalScroll : OnVerticalScroll)(this, new EventArgs(Query, ScrollInfo.nPos, ScrollInfo.nMin, ScrollInfo.nMax, (int)ScrollInfo.nPage));
                }
            }
        }
    }
}
