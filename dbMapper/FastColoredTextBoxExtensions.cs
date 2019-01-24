using FastColoredTextBoxNS;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DBMapper
{
    public static class FastColoredTextBoxExtensions
    {
        public static void SetFindText(this FastColoredTextBox tBox, string findText)
        {
            //if (tBox.findForm != null)
            //    tBox.findForm.tbFind.Text = findText;
            tBox.SetMarkerText(findText);
        }
        private static UnderlineStyle underlineStyle = new UnderlineStyle();
        public static void SetMarkerText(this FastColoredTextBox tBox, string markerText)
        {
            tBox.ClearMarkerText();
            if (!String.IsNullOrEmpty(markerText))
            {
                tBox.Range.SetStyle(underlineStyle, Regex.Escape(markerText), System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            }
        }
        public static void ClearMarkerText(this FastColoredTextBox tBox)
        {
            tBox.Range.ClearStyle(underlineStyle);
        }
    }
    class UnderlineStyle : Style
    {
        public override void Draw(Graphics gr, Point position, Range range)
        {
            //get size of rectangle
            Size size = GetSizeOfRange(range);
            //create rectangle
            Rectangle rect = new Rectangle(position, size);
            //inflate it
            //rect.Inflate(2, 0);
            gr.DrawLine(new Pen(Color.GreenYellow, 3), new Point(rect.Left, rect.Bottom), new Point(rect.Right, rect.Bottom));
        }
    }
}
