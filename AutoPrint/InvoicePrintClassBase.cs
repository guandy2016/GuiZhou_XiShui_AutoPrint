using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace AutoPrint
{
    public class InvoicePrintClassBase
    {
        protected decimal m_decX = 0.5M;
        protected decimal m_decX_0 = 0.8M;
        protected decimal m_decY = 0.3M;
        protected decimal m_First_Column_W = 3.0M;
        protected decimal m_LineHeight = 0.95M;
        protected decimal m_Rect_x = 3.4M;
        protected decimal m_Rect_y = 3.0M;
        protected decimal m_Second_Column_W = 3.6M;
        protected decimal m_Third_Column_W = 3.0M;
        protected decimal Margins_Left = 0.5M;
        protected decimal Margins_Top = 0.5M;
        protected decimal rect_H = 7.5M;
        protected decimal rect_W = 15.7M;

        public int DanToPex(decimal x)
        {
            return (int)((x * 0.3937M) * 100M);
        }

        public Point GetPoint(decimal x, decimal y)
        {
            return new Point((int)((x * 0.3937M) * 100M), (int)((y * 0.3937M) * 100M));
        }
     
        public virtual void LoadWeightPaintDocument(Graphics g, PrintDataClass pdc, string PrintTitle)
        {
        }
    }
}
