using System;
using System.Collections.Generic;
using System.Text;

namespace AutoPrint
{
    public static class ConvertData
    {
        public static string Convert2Digit(string str)
        {
            string str2 = str.Substring(0, 1);
            string str3 = str.Substring(1, 1);
            string str4 = "";
            return ((str4 + ConvertChinese(str2) + "拾") + ConvertChinese(str3)).Replace("零拾", "零").Replace("零零", "零");
        }

        public static string Convert3Digit(string str)
        {
            string str2 = str.Substring(0, 1);
            string str3 = str.Substring(1, 1);
            string str4 = str.Substring(2, 1);
            string str5 = "";
            return (((str5 + ConvertChinese(str2) + "佰") + ConvertChinese(str3) + "拾") + ConvertChinese(str4)).Replace("零佰", "零").Replace("零拾", "零").Replace("零零", "零").Replace("零零", "零");
        }

        public static string Convert4Digit(string str)
        {
            string str2 = str.Substring(0, 1);
            string str3 = str.Substring(1, 1);
            string str4 = str.Substring(2, 1);
            string str5 = str.Substring(3, 1);
            string str6 = "";
            return ((((str6 + ConvertChinese(str2) + "仟") + ConvertChinese(str3) + "佰") + ConvertChinese(str4) + "拾") + ConvertChinese(str5)).Replace("零仟", "零").Replace("零佰", "零").Replace("零拾", "零").Replace("零零", "零").Replace("零零", "零").Replace("零零", "零");
        }

        public static string ConvertChinese(string str)
        {
            switch (str)
            {
                case "0":
                    return "零";

                case "1":
                    return "壹";

                case "2":
                    return "贰";

                case "3":
                    return "叁";

                case "4":
                    return "肆";

                case "5":
                    return "伍";

                case "6":
                    return "陆";

                case "7":
                    return "柒";

                case "8":
                    return "捌";

                case "9":
                    return "玖";
            }
            return "";
        }

        public static string convertData(string str)
        {
            string str3 = "";
            int length = str.Length;
            if (length <= 4)
            {
                str3 = ConvertDigit(str);
            }
            else if (length <= 8)
            {
                str3 = ConvertDigit(str.Substring(length - 4, 4));
                str3 = (ConvertDigit(str.Substring(0, length - 4)) + "万" + str3).Replace("零万", "万").Replace("零零", "零");
            }
            else if (length <= 12)
            {
                str3 = ConvertDigit(str.Substring(length - 4, 4));
                str3 = ConvertDigit(str.Substring(length - 8, 4)) + "万" + str3;
                str3 = (ConvertDigit(str.Substring(0, length - 8)) + "亿" + str3).Replace("零亿", "亿").Replace("零万", "零").Replace("零零", "零").Replace("零零", "零");
            }
            length = str3.Length;
            if (length < 2)
            {
                return str3;
            }
            string str5 = str3.Substring(length - 2, 2);
            if (str5 == null)
            {
                return str3;
            }
            if (!(str5 == "佰零"))
            {
                if (str5 != "仟零")
                {
                    if (str5 == "万零")
                    {
                        return (str3.Substring(0, length - 2) + "万");
                    }
                    if (str5 != "亿零")
                    {
                        return str3;
                    }
                    return (str3.Substring(0, length - 2) + "亿");
                }
            }
            else
            {
                return (str3.Substring(0, length - 2) + "佰");
            }
            return (str3.Substring(0, length - 2) + "仟");
        }

        public static string ConvertDigit(string str)
        {
            int length = str.Length;
            string str2 = "";
            switch (length)
            {
                case 1:
                    str2 = ConvertChinese(str);
                    break;

                case 2:
                    str2 = Convert2Digit(str);
                    break;

                case 3:
                    str2 = Convert3Digit(str);
                    break;

                case 4:
                    str2 = Convert4Digit(str);
                    break;
            }
            str2 = str2.Replace("拾零", "拾");
            length = str2.Length;
            return str2;
        }

        public static string ConvertSum(string data)
        {
            if (!IsPositveDecimal(data))
            {
                return "输入的不是正数字！";
            }
            if (double.Parse(data) > 999999999999.99)
            {
                return "数字太大，无法换算，请输入一万亿元以下的金额";
            }
            char[] chArray = new char[] { '.' };
            string[] strArray = null;
            strArray = data.Split(new char[] { chArray[0] });
            if (strArray.Length == 1)
            {
                return (convertData(data) + "圆整");
            }
            return (convertData(strArray[0]) + "圆" + ConvertXiaoShu(strArray[1]));
        }

        public static string ConvertXiaoShu(string str)
        {
            if (str.Length == 1)
            {
                return (ConvertChinese(str) + "角");
            }
            string str2 = ConvertChinese(str.Substring(0, 1)) + "角";
            string str3 = str.Substring(1, 1);
            return (str2 + ConvertChinese(str3) + "分").Replace("零分", "").Replace("零角", "");
        }

        public static bool IsPositveDecimal(string str)
        {
            decimal num;
            try
            {
                num = decimal.Parse(str);
            }
            catch (Exception)
            {
                return false;
            }
            return (num >= 0M);
        }
    }
}
