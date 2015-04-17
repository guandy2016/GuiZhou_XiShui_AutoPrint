using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace AutoPrint
{
    public class InvoPrintClass : InvoicePrintClassBase
    {
        public InvoPrintClass()
        {
            base.m_decX = 0.7M;
            base.m_decY = 0.2M;
            base.m_decX_0 = 0.8M;
            base.m_Rect_x = 3.1M;
            base.m_Rect_y = 2.3M;//3.5m
            base.rect_H = 9.0M;
            base.rect_W = 17.9M;
            base.m_LineHeight = 0.82M;
            base.Margins_Top = 0.5M;
            base.Margins_Left = 0.5M;
            base.m_First_Column_W = 3.3M;
            base.m_Second_Column_W = 5.6M;
            base.m_Third_Column_W = 3.3M;
        }

        public override void LoadWeightPaintDocument(Graphics g, PrintDataClass pdc, string PrintTitle)
        {
            FontStyle bold = FontStyle.Bold;
            Font font = new Font("黑体", 22f, bold);
            FontStyle regular = FontStyle.Regular;
            Font font2 = new Font("宋体", 11f, regular, GraphicsUnit.Point);
            FontStyle style = FontStyle.Regular;
            Font font3 = new Font("宋体", 14f, style, GraphicsUnit.Point);
            FontStyle style4 = FontStyle.Bold;
            Font font4 = new Font("宋体", 11f, style4, GraphicsUnit.Point);
            FontStyle italic = FontStyle.Italic;
            italic |= FontStyle.Bold;
            Font font5 = new Font("宋体", 10f, italic, GraphicsUnit.Point);
            FontStyle style6 = FontStyle.Regular;
            Font font6 = new Font("宋体", 11f, style6, GraphicsUnit.Point);
            FontStyle style7 = FontStyle.Regular;
            Font font7 = new Font("宋体", 10f, style7, GraphicsUnit.Point);
            StringFormat format = new StringFormat
            {
                LineAlignment = StringAlignment.Center
            };
            g.DrawString(pdc.CollName, font2, Brushes.Black, (PointF)base.GetPoint(base.m_Rect_x + 3.2M, base.m_Rect_y + 0.1m));
            g.DrawString(pdc.TradeID, font4, Brushes.Black, (PointF)base.GetPoint(base.m_Rect_x + 11.8M, base.m_Rect_y++));
            //g.DrawString(pdc.CollName, font2, Brushes.Black, (PointF)base.GetPoint(base.m_Rect_x + 3.2M, decimal.op_Decrement(base.m_Rect_y)));
            //g.DrawString(pdc.TradeID, font4, Brushes.Black, (PointF)base.GetPoint(base.m_Rect_x + 11.8M, decimal.op_Decrement(base.m_Rect_y)));
            decimal num = 0.0M;
            g.DrawString(pdc.CollName, font2, Brushes.Black, (PointF)base.GetPoint((base.m_Rect_x + base.m_decX) + base.m_First_Column_W, (base.m_Rect_y + num) + base.m_decY));
            g.DrawString(pdc.CarNo, font2, Brushes.Black, (PointF)base.GetPoint((((base.m_Rect_x + base.m_First_Column_W) + base.m_Second_Column_W) + base.m_Third_Column_W) + base.m_decX, (base.m_Rect_y + num) + base.m_decY));
            num = base.m_LineHeight * 1M;
            g.DrawString(pdc.NavicertCode, font2, Brushes.Black, (PointF)base.GetPoint((base.m_Rect_x + base.m_decX) + base.m_First_Column_W, (base.m_Rect_y + num) + base.m_decY));
            g.DrawString(pdc.LoadWeight.ToString(), font2, Brushes.Black, (PointF)base.GetPoint((((base.m_Rect_x + base.m_First_Column_W) + base.m_Second_Column_W) + base.m_Third_Column_W) + base.m_decX, (base.m_Rect_y + num) + base.m_decY));
            num = base.m_LineHeight * 2M;
            g.DrawString(pdc.MarkedCardCode, font2, Brushes.Black, (PointF)base.GetPoint((base.m_Rect_x + base.m_decX) + base.m_First_Column_W, (base.m_Rect_y + num) + base.m_decY));
            g.DrawString(pdc.EmptyWeight.ToString(), font2, Brushes.Black, (PointF)base.GetPoint((((base.m_Rect_x + base.m_First_Column_W) + base.m_Second_Column_W) + base.m_Third_Column_W) + base.m_decX, (base.m_Rect_y + num) + base.m_decY));
            num = base.m_LineHeight * 3M;
            g.DrawString(pdc.CoalKindName, font2, Brushes.Black, (PointF)base.GetPoint((base.m_Rect_x + base.m_decX) + base.m_First_Column_W, (base.m_Rect_y + num) + base.m_decY));
            g.DrawString(pdc.NetWeight.ToString(), font2, Brushes.Black, (PointF)base.GetPoint((((base.m_Rect_x + base.m_First_Column_W) + base.m_Second_Column_W) + base.m_Third_Column_W) + base.m_decX, (base.m_Rect_y + num) + base.m_decY));
            num = base.m_LineHeight * 4M;
            g.DrawString(pdc.TaxCanBaoPayMoney.ToString("0.00"), font2, Brushes.Black, (PointF)base.GetPoint((base.m_Rect_x + base.m_decX) + base.m_First_Column_W, (base.m_Rect_y + num) + base.m_decY));
            g.DrawString(pdc.TaxSuoDeShuiPayMoney.ToString("0.00"), font2, Brushes.Black, (PointF)base.GetPoint((((base.m_Rect_x + base.m_First_Column_W) + base.m_Second_Column_W) + base.m_Third_Column_W) + base.m_decX, (base.m_Rect_y + num) + base.m_decY));
            num = base.m_LineHeight * 5M;
            g.DrawString(pdc.TaxSewagePayMoney.ToString("0.00"), font2, Brushes.Black, (PointF)base.GetPoint((base.m_Rect_x + base.m_decX) + base.m_First_Column_W, (base.m_Rect_y + num) + base.m_decY));
            g.DrawString(pdc.TaxLocalPayMoney.ToString("0.00"), font2, Brushes.Black, (PointF)base.GetPoint((((base.m_Rect_x + base.m_First_Column_W) + base.m_Second_Column_W) + base.m_Third_Column_W) + base.m_decX, (base.m_Rect_y + num) + base.m_decY));
            num = base.m_LineHeight * 6M;
            g.DrawString(pdc.TaxYuLinPayMoney.ToString("0.00"), font2, Brushes.Black, (PointF)base.GetPoint((base.m_Rect_x + base.m_decX) + base.m_First_Column_W, (base.m_Rect_y + num) + base.m_decY));
            g.DrawString(pdc.TaxSoilWaterPayMoney.ToString("0.00"), font2, Brushes.Black, (PointF)base.GetPoint((((base.m_Rect_x + base.m_First_Column_W) + base.m_Second_Column_W) + base.m_Third_Column_W) + base.m_decX, (base.m_Rect_y + num) + base.m_decY));
            num = base.m_LineHeight * 7M;
            g.DrawString(pdc.CoalSaleUnitPrice.ToString("0.00"), font2, Brushes.Black, (PointF)base.GetPoint((base.m_Rect_x + base.m_decX) + base.m_First_Column_W, (base.m_Rect_y + num) + base.m_decY));
            g.DrawString(pdc.TaxYunShuPayMoney.ToString("0.00"), font2, Brushes.Black, (PointF)base.GetPoint((((base.m_Rect_x + base.m_First_Column_W) + base.m_Second_Column_W) + base.m_Third_Column_W) + base.m_decX, (base.m_Rect_y + num) + base.m_decY));
            num = base.m_LineHeight * 8M;
            g.DrawString(pdc.PayCity, font2, Brushes.Black, (PointF)base.GetPoint((base.m_Rect_x + base.m_decX) + base.m_First_Column_W, (base.m_Rect_y + num) + base.m_decY));
            g.DrawString((pdc.CoalSaleUnitPrice * Convert.ToDecimal(pdc.NetWeight.ToString())).ToString("0.00"), font2, Brushes.Black, (PointF)base.GetPoint((((base.m_Rect_x + base.m_First_Column_W) + base.m_Second_Column_W) + base.m_Third_Column_W) + base.m_decX, (base.m_Rect_y + num) + base.m_decY));
            num = base.m_LineHeight * 9M;
            string s = ConvertData.ConvertSum(pdc.PayMoney.ToString("0.00")) + " (￥" + pdc.PayMoney.ToString("0.00") + ")";
            g.DrawString(s, font2, Brushes.Black, (PointF)base.GetPoint((base.m_Rect_x + base.m_First_Column_W) + 0.1M, (base.m_Rect_y + num) + 0.7M));
            string str2 = "";
            if (pdc.PrintTimes > 1)
            {
                str2 = "此单据为发票补打票据";
            }
            g.DrawString(str2, font7, Brushes.Black, (PointF)base.GetPoint(base.m_Rect_x + 3.8M, (base.m_Rect_y + base.rect_H) + 0.2M));
            g.DrawString(pdc.LoadWeightTime, font7, Brushes.Black, (PointF)base.GetPoint(base.m_Rect_x + 12.3M, (base.m_Rect_y + base.rect_H) + 0.2M));
        }
       
    }
}
