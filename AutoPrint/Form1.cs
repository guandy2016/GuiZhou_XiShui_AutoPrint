using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace AutoPrint
{
    public partial class print : Form
    {
        private PrintDataClass printData = null;
        private decimal PageSizeW;
        private decimal PageSizeH;
        //string filePath = "D:\\PrintInfo.txt";
        private string filePath = AppConfig.ReadValue("setting", "FilePath");

        public print()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetSqlHelp();
            GetDBNew();
            timerSpan.Interval = 1000 * Convert.ToInt32(AppConfig.ReadValue("setting", "timeSpan"));
        }

        #region 设置sqlhelper的连接字符串
        /// <summary>
        /// 设置sqlhelper的连接字符串
        /// </summary>
        private void SetSqlHelp()
        {
            StringBuilder sbBuilder = new StringBuilder();
            sbBuilder.AppendFormat("Server={0}", AppConfig.ReadValue("setting", "server"));
            sbBuilder.AppendFormat(";database={0}", AppConfig.ReadValue("setting", "DataBase"));
            sbBuilder.Append(";uid=sa");
            sbBuilder.AppendFormat(";pwd={0}", AppConfig.ReadValue("setting", "password"));
            SQLHelper.connStr = sbBuilder.ToString();
        }

        #endregion


        #region 获取数据库中指定的时间间隔（配置文件）过磅数据

        /// <summary>
        /// 获取数据库中指定的时间间隔（配置文件）过磅数据
        /// </summary>
        private void GetDB()
        {
            #region 获取数据库中数据

            int dataBaseTime = Convert.ToInt32(AppConfig.ReadValue("setting", "DataBaseTime"));
            string sql =
                "select top 1 * FROM [IndustryPlatform].[dbo].[TT_LoadWeight] where RoomCode=@RoomCode and WeightTime between @WeightTimeBegin and @WeightTimeEnd order by WeightTime desc";
            string weightTimeBegin = DateTime.Now.AddSeconds(dataBaseTime).ToString("yyyy-MM-dd HH:mm:ss");
            string weightTimeEnd = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            SqlParameter[] sqlParameters =
                {
                    new SqlParameter("@WeightTimeBegin", weightTimeBegin),
                    new SqlParameter("@WeightTimeEnd", weightTimeEnd),
                    new SqlParameter("@RoomCode", AppConfig.ReadValue("setting", "RoomCode"))
                };
            DataTable dtTable = null;
            try
            {
                dtTable = SQLHelper.ExcuteDataTable(sql, sqlParameters);
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(LogHelper.GetCurSourceFileName() + "~" + LogHelper.GetLineNum() + "获取过磅数据异常！", e);
            }

            #endregion

            if (dtTable != null && dtTable.Rows.Count > 0)
            {
                #region 过磅数据处理

                try
                {
                    printData = new PrintDataClass();
                    this.printData.RoomName = dtTable.Rows[0]["RoomName"].ToString();
                    this.printData.TradeID = dtTable.Rows[0]["WeightCode"].ToString();
                    this.printData.CoalKindName = dtTable.Rows[0]["CoalKindName"].ToString();
                    this.printData.CollName = dtTable.Rows[0]["CollName"].ToString();
                    this.printData.MarkedCardCode = dtTable.Rows[0]["MarkedCardCode"].ToString();
                    this.printData.NavicertCode = dtTable.Rows[0]["NavicertCode"].ToString();
                    this.printData.CarNo = dtTable.Rows[0]["CarNo"].ToString();
                    this.printData.CarOwnerName = dtTable.Rows[0]["CarOwnerName"].ToString();
                    this.printData.CarType = dtTable.Rows[0]["CarType"].ToString();
                    this.printData.CollManager = dtTable.Rows[0]["Operator"].ToString();
                    this.printData.LoadWeight = dtTable.Rows[0]["LoadWeight"].ToString();
                    this.printData.NetWeight = dtTable.Rows[0]["NetWeight"].ToString();
                    this.printData.EmptyWeight = dtTable.Rows[0]["EmptyWeight"].ToString();
                    this.printData.IsBeatUp = true;
                    this.printData.CoalSaleUnitPrice = Convert.ToDecimal(dtTable.Rows[0]["PayUnitPirce"].ToString());
                    this.printData.LoadWeightTime = dtTable.Rows[0]["WeightTime"].ToString();
                    this.printData.Operator = dtTable.Rows[0]["Operator"].ToString();
                }
                catch (Exception e)
                {
                    LogHelper.WriteLog(LogHelper.GetCurSourceFileName() + "~" + LogHelper.GetLineNum() + "过磅数加工据异常！", e);
                }

                #endregion

                #region 税费

                try
                {
                    SqlParameter sqlP_weightCode = new SqlParameter("@WeightCode", this.printData.TradeID);
                    DataTable dtTableMouney = SQLHelper.ExecSPDataSet("PT_LoadWeightTaxTwoPrint", sqlP_weightCode);

                    this.printData.TaxCounttryPayMoney = Convert.ToDecimal(dtTableMouney.Rows[0]["TaxCounttryPayMoney"]);
                    this.printData.TaxLocalPayMoney = Convert.ToDecimal(dtTableMouney.Rows[0]["TaxLocalPayMoney"]);
                    this.printData.TaxSewagePayMoney = Convert.ToDecimal(dtTableMouney.Rows[0]["TaxSewagePayMoney"]);
                    this.printData.TaxSoilWaterPayMoney = Convert.ToDecimal(dtTableMouney.Rows[0]["TaxSoilWaterPayMoney"]);
                    this.printData.TaxesPrice = Convert.ToDecimal(dtTableMouney.Rows[0]["CoalSaleUnitPrice"]);
                    this.printData.TaxFundPayMoney = Convert.ToDecimal(dtTableMouney.Rows[0]["TaxFundPayMoney"]);
                    this.printData.TaxCanBaoPayMoney = Convert.ToDecimal(dtTableMouney.Rows[0]["TaxCanBaoPayMoney"]);
                    this.printData.TaxYuLinPayMoney = Convert.ToDecimal(dtTableMouney.Rows[0]["TaxYuLinPayMoney"]);
                    this.printData.TaxYunShuPayMoney = Convert.ToDecimal(dtTableMouney.Rows[0]["TaxYunShuPayMoney"]);
                    this.printData.PayMoney = Convert.ToDecimal(dtTableMouney.Rows[0]["TaxPayMoney"]);
                    this.printData.PrintTimes = 1;
                }
                catch (Exception e)
                {
                    LogHelper.WriteLog(LogHelper.GetCurSourceFileName() + "~" + LogHelper.GetLineNum() + "税费获取或加工异常！", e);
                }


                //string sql_TaxItemDetail =
                //    "select * FROM [IndustryPlatform].[dbo].[VT_TaxItemDetail] where RoomCode=@RoomCode and CoalKindCode=@CoalKindCode";
                //SqlParameter[] sqlp_TaxItemDetail =
                //{
                //    new SqlParameter("@RoomCode", dtTable.Rows[0]["RoomCode"].ToString()),
                //    new SqlParameter("@CoalKindCode", dtTable.Rows[0]["CoalKindCode"].ToString()),
                //};
                //DataTable dtVT_TaxItemDetail = SQLHelper.ExcuteDataTable(sql_TaxItemDetail, sqlp_TaxItemDetail);
                //foreach (DataRow taxRow in dtVT_TaxItemDetail.Rows)
                //{
                //    switch (taxRow[1].ToString())
                //    {
                //        case "所得税":
                //            this.printData.TaxSuoDeShuiPayMoney = (Convert.ToDecimal(taxRow[2].ToString())*
                //                                                   Convert.ToDecimal(this.printData.NetWeight));
                //            break;
                //        case "资源税":
                //            this.printData.TaxLocalPayMoney = (Convert.ToDecimal(taxRow[2].ToString())*
                //                                               Convert.ToDecimal(this.printData.NetWeight));
                //            break;
                //        case "水土保持费":
                //            this.printData.TaxSoilWaterPayMoney = (Convert.ToDecimal(taxRow[2].ToString())*
                //                                                   Convert.ToDecimal(this.printData.NetWeight));
                //            break;
                //        case "育林基金":
                //            this.printData.TaxYuLinPayMoney = (Convert.ToDecimal(taxRow[2].ToString())*
                //                                               Convert.ToDecimal(this.printData.NetWeight));
                //            break;
                //        case "残保金":
                //            this.printData.TaxCanBaoPayMoney = (Convert.ToDecimal(taxRow[2].ToString())*
                //                                                Convert.ToDecimal(this.printData.NetWeight));
                //            break;
                //        case "排污费":
                //            this.printData.TaxSewagePayMoney = (Convert.ToDecimal(taxRow[2].ToString())*
                //                                                Convert.ToDecimal(this.printData.NetWeight));
                //            break;
                //        default:
                //            break;
                //    }
                //}
                //this.printData.PayMoney = Convert.ToDecimal(dtTable.Rows[0]["TaxAmount"].ToString());

                #endregion

                #region 销往目的地

                try
                {
                    string cityDataset = "select * from [TT_SalesCustomer] where SaleCode=@SaleCode";
                    SqlParameter sqlp_salecode = new SqlParameter("@SaleCode", dtTable.Rows[0]["CustomerName"].ToString());
                    DataTable saleCityName = SQLHelper.ExcuteDataTable(cityDataset, sqlp_salecode);
                    if (saleCityName.Rows.Count > 0)
                    {
                        this.printData.PayCity = saleCityName.Rows[0]["SaleCustomer"].ToString();
                    }
                    else
                    {
                        this.printData.PayCity = "";
                    }
                }
                catch (Exception e)
                {
                    LogHelper.WriteLog(LogHelper.GetCurSourceFileName() + "~" + LogHelper.GetLineNum() + "销往数据获取或加工异常！", e);
                }

                #endregion

                //this.printData.IsPrintTick = CarrierAdapter.Instance.Config.GetIsPrintTick();这个是从配置文件中获取数据，我没有找到
                InvoicePrint(this.printData);
            }
        }

        #endregion

        #region 根据文本获取过磅编号

        /// <summary>
        /// 获取过磅编号
        /// </summary>
        /// <returns></returns>
        private string GetTextDate()
        {
            //try
            //{
            //    string str;
            //    StreamReader reader = new StreamReader(filePath);
            //    reader.BaseStream.Seek(0L, SeekOrigin.Begin);
            //    string a = "";
            //    string b = "";
            //    string c = "";
            //    while ((str = reader.ReadLine()) != null)
            //    {
            //        a = b;
            //        b = c;
            //        c = str;
            //        //if (a == c)
            //        //{
            //        //    if (b.Length == 18)
            //        //    {
            //        //        return b;
            //        //    }
            //        //}
            //        //else
            //        {

            //            if (b.Length == 18 && isnumeric(b))
            //            {
            //                reader.Close();
            //                return b;
            //            }
            //        }
            //    }

            //    reader.Close();


            //    return "";

            //}
            //catch (IOException ioExce)
            //{
            //    return "";
            //}
            if (File.Exists(filePath))
            {
                String line;
                using (StreamReader sr = new StreamReader(filePath, Encoding.Default))
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line.Length == 18 && isnumeric(line))
                        {
                            return line;
                        }
                    }
                }
            }
            else
            {
                LogHelper.WriteLog(LogHelper.GetCurSourceFileName() + "~" + LogHelper.GetLineNum() + "数据文本不存在！");
            }
            return "";
        }

        #endregion

        #region 根据文本中的过磅编号获取过磅数据并打印

        /// <summary>
        /// 根据文本中的过磅编号获取过磅数据并打印
        /// </summary>
        private void GetDBNew()
        {
            #region 根据过磅编号获取数据库中数据

            string NO = GetTextDate();
            if (string.IsNullOrEmpty(NO))
            {
                LogHelper.WriteLog(LogHelper.GetCurSourceFileName() + "~" + LogHelper.GetLineNum() + "过磅编号为空");
                return;
            }
            int dataBaseTime = Convert.ToInt32(AppConfig.ReadValue("setting", "DataBaseTime"));
            string sql =
                "select top 1 * FROM [IndustryPlatform].[dbo].[TT_LoadWeight] where [WeightCode]='" + NO +
                "' order by WeightTime desc";
            string weightTimeBegin = DateTime.Now.AddSeconds(dataBaseTime).ToString("yyyy-MM-dd HH:mm:ss");
            string weightTimeEnd = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            DataTable dtTable = null;
            try
            {
                dtTable = SQLHelper.ExcuteDataTable(sql);
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(LogHelper.GetCurSourceFileName() + "~" + LogHelper.GetLineNum() + "获取过磅数据失败！", e);
            }

            #endregion

            if (dtTable != null && dtTable.Rows.Count > 0)
            {

                #region 处理过磅数据

                try
                {
                    printData = new PrintDataClass();
                    this.printData.RoomName = dtTable.Rows[0]["RoomName"].ToString();
                    this.printData.TradeID = dtTable.Rows[0]["WeightCode"].ToString();
                    this.printData.CoalKindName = dtTable.Rows[0]["CoalKindName"].ToString();
                    this.printData.CollName = dtTable.Rows[0]["CollName"].ToString();
                    this.printData.MarkedCardCode = dtTable.Rows[0]["MarkedCardCode"].ToString();
                    this.printData.NavicertCode = dtTable.Rows[0]["NavicertCode"].ToString();
                    this.printData.CarNo = dtTable.Rows[0]["CarNo"].ToString();
                    this.printData.CarOwnerName = dtTable.Rows[0]["CarOwnerName"].ToString();
                    this.printData.CarType = dtTable.Rows[0]["CarType"].ToString();
                    this.printData.CollManager = dtTable.Rows[0]["Operator"].ToString();
                    this.printData.LoadWeight = dtTable.Rows[0]["LoadWeight"].ToString();
                    this.printData.NetWeight = dtTable.Rows[0]["NetWeight"].ToString();
                    this.printData.EmptyWeight = dtTable.Rows[0]["EmptyWeight"].ToString();
                    this.printData.IsBeatUp = true;
                    try
                    {
                        this.printData.CoalSaleUnitPrice = Convert.ToDecimal(dtTable.Rows[0]["PayUnitPirce"].ToString());
                    }
                    catch
                    {
                        this.printData.CoalSaleUnitPrice = 0;
                    }
                    this.printData.LoadWeightTime = dtTable.Rows[0]["WeightTime"].ToString();
                    this.printData.Operator = dtTable.Rows[0]["Operator"].ToString();
                }
                catch (Exception e)
                {
                    LogHelper.WriteLog(LogHelper.GetCurSourceFileName() + "~" + LogHelper.GetLineNum() + "处理过磅数据异常！", e);
                }

                #endregion

                #region 税费

                try
                {
                    SqlParameter sqlP_weightCode = new SqlParameter("@WeightCode", this.printData.TradeID);
                    DataTable dtTableMouney = SQLHelper.ExecSPDataSet("PT_LoadWeightTaxTwoPrint", sqlP_weightCode);

                    this.printData.TaxCounttryPayMoney = Convert.ToDecimal(dtTableMouney.Rows[0]["TaxCounttryPayMoney"]);
                    this.printData.TaxLocalPayMoney = Convert.ToDecimal(dtTableMouney.Rows[0]["TaxLocalPayMoney"]);
                    this.printData.TaxSewagePayMoney = Convert.ToDecimal(dtTableMouney.Rows[0]["TaxSewagePayMoney"]);
                    this.printData.TaxSoilWaterPayMoney =
                        Convert.ToDecimal(dtTableMouney.Rows[0]["TaxSoilWaterPayMoney"]);
                    this.printData.TaxesPrice = Convert.ToDecimal(dtTableMouney.Rows[0]["CoalSaleUnitPrice"]);
                    this.printData.TaxFundPayMoney = Convert.ToDecimal(dtTableMouney.Rows[0]["TaxFundPayMoney"]);
                    this.printData.TaxCanBaoPayMoney = Convert.ToDecimal(dtTableMouney.Rows[0]["TaxCanBaoPayMoney"]);
                    this.printData.TaxYuLinPayMoney = Convert.ToDecimal(dtTableMouney.Rows[0]["TaxYuLinPayMoney"]);
                    this.printData.TaxYunShuPayMoney = Convert.ToDecimal(dtTableMouney.Rows[0]["TaxYunShuPayMoney"]);
                    this.printData.PayMoney = Convert.ToDecimal(dtTableMouney.Rows[0]["TaxPayMoney"]);
                    this.printData.PrintTimes = 1;
                }
                catch (Exception e)
                {
                    LogHelper.WriteLog(LogHelper.GetCurSourceFileName() + "~" + LogHelper.GetLineNum() + "获取税费并加工异常！", e);
                }

                #endregion

                #region 销往目的地

                try
                {
                    string cityDataset = "select * from [TT_SalesCustomer] where SaleCode=@SaleCode";
                    SqlParameter sqlp_salecode = new SqlParameter("@SaleCode",
                        dtTable.Rows[0]["CustomerName"].ToString());
                    DataTable saleCityName = SQLHelper.ExcuteDataTable(cityDataset, sqlp_salecode);
                    if (saleCityName.Rows.Count > 0)
                    {
                        this.printData.PayCity = saleCityName.Rows[0]["SaleCustomer"].ToString();
                    }
                    else
                    {
                        this.printData.PayCity = "";
                    }
                }
                catch (Exception e)
                {
                    LogHelper.WriteLog(
                        LogHelper.GetCurSourceFileName() + "~" + LogHelper.GetLineNum() + "获取销往目的地数据异常！", e);
                }

                #endregion

                InvoicePrint(this.printData); //打印
                try
                {
                    if (File.Exists(filePath)) //打印完成后删除文件
                    {

                        FileInfo fi = new FileInfo(filePath);
                        if (fi.Attributes.ToString().IndexOf("ReadOnly") != -1)
                        {
                            fi.Attributes = FileAttributes.Normal;
                        }
                        File.Delete(filePath);
                    }
                }
                catch (Exception e)
                {
                    LogHelper.WriteLog(LogHelper.GetCurSourceFileName() + "~" + LogHelper.GetLineNum() + "文本删除失败！", e);
                }


            }
        }

        #endregion

        #region 判断是否全是数字

        /// <summary>
        /// 判断是否全是数字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public bool isnumeric(string str)
        {
            Regex regex = new Regex("^[0-9]*$");
            Match m = regex.Match(str);
            if (!m.Success)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        #endregion

        #region 指定时间间隔

        /// <summary>
        /// 指定时间间隔
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerSpan_Tick(object sender, EventArgs e)
        {

            GetDBNew();

        }

        #endregion

        #region 数据打印

        /// <summary>
        /// 数据打印
        /// </summary>
        /// <param name="printData"></param>
        public virtual void InvoicePrint(PrintDataClass printData)
        {
            PrintDocument document = new PrintDocument();
            //this.PrintData = printData;
            Margins margins = new Margins(this.DanToPex(0.5M), this.DanToPex(0.5M), this.DanToPex(0.5M),
                this.DanToPex(0.5M));
            document.DefaultPageSettings.Margins = margins;
            PaperSize size = new PaperSize("First custom size", this.DanToPex(PageSizeW), this.DanToPex(PageSizeH));
            document.DefaultPageSettings.PaperSize = size;
            document.PrintController = new StandardPrintController();
            document.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);
            try
            {
                document.Print();
                LogHelper.WriteLog("过磅编号为：" + GetTextDate() + "。数据打印成功！");
            }
            catch (Exception e)
            {
                document.PrintController.OnEndPrint(document, new PrintEventArgs());
                LogHelper.WriteLog(LogHelper.GetCurSourceFileName() + "~" + LogHelper.GetLineNum() + "打印失败！", e);
            }
            finally
            {
                this.printData = null;
            }
        }

        #endregion


        #region 打印相关

        public int DanToPex(decimal x)
        {
            return (int)((x * 0.3937M) * 100M);
        }

        public virtual void pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            InvoPrintClass class2 = new InvoPrintClass();
            if (this.printData != null)
            {
                class2.LoadWeightPaintDocument(g, this.printData, this.printData.TitleTip);
            }
        }

        #endregion

        #region 二次打印

        /// <summary>
        /// 二次打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            #region 获取过磅数据

            int dataBaseTime = Convert.ToInt32(AppConfig.ReadValue("setting", "DataBaseTime"));
            string sql =
                "select top 1 * FROM [IndustryPlatform].[dbo].[TT_LoadWeight] where [WeightCode]='" +
                textBox1.Text.ToString().Trim() + "' order by WeightTime desc";
            string weightTimeBegin = DateTime.Now.AddSeconds(dataBaseTime).ToString("yyyy-MM-dd HH:mm:ss");
            string weightTimeEnd = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            DataTable dtTable = null;
            try
            {
                dtTable = SQLHelper.ExcuteDataTable(sql);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogHelper.GetCurSourceFileName() + "~" + LogHelper.GetLineNum() + "获取过磅数据失败！", ex);
            }

            #endregion



            if (dtTable != null && dtTable.Rows.Count > 0)
            {
                #region 过磅数据

                try
                {
                    printData = new PrintDataClass();
                    this.printData.RoomName = dtTable.Rows[0]["RoomName"].ToString();
                    this.printData.TradeID = dtTable.Rows[0]["WeightCode"].ToString();
                    this.printData.CoalKindName = dtTable.Rows[0]["CoalKindName"].ToString();
                    this.printData.CollName = dtTable.Rows[0]["CollName"].ToString();
                    this.printData.MarkedCardCode = dtTable.Rows[0]["MarkedCardCode"].ToString();
                    this.printData.NavicertCode = dtTable.Rows[0]["NavicertCode"].ToString();
                    this.printData.CarNo = dtTable.Rows[0]["CarNo"].ToString();
                    this.printData.CarOwnerName = dtTable.Rows[0]["CarOwnerName"].ToString();
                    this.printData.CarType = dtTable.Rows[0]["CarType"].ToString();
                    this.printData.CollManager = dtTable.Rows[0]["Operator"].ToString();
                    this.printData.LoadWeight = dtTable.Rows[0]["LoadWeight"].ToString();
                    this.printData.NetWeight = dtTable.Rows[0]["NetWeight"].ToString();
                    this.printData.EmptyWeight = dtTable.Rows[0]["EmptyWeight"].ToString();
                    this.printData.IsBeatUp = true;
                    this.printData.CoalSaleUnitPrice = Convert.ToDecimal(dtTable.Rows[0]["PayUnitPirce"].ToString());
                    this.printData.LoadWeightTime = dtTable.Rows[0]["WeightTime"].ToString();
                    this.printData.Operator = dtTable.Rows[0]["Operator"].ToString();
                }
                catch (Exception exception)
                {
                    LogHelper.WriteLog(LogHelper.GetCurSourceFileName() + "~" + LogHelper.GetLineNum() + "过磅数据加工异常！", exception);
                }

                #endregion

                #region 税费

                try
                {
                    SqlParameter sqlP_weightCode = new SqlParameter("@WeightCode", this.printData.TradeID);
                    DataTable dtTableMouney = SQLHelper.ExecSPDataSet("PT_LoadWeightTaxTwoPrint", sqlP_weightCode);

                    this.printData.TaxCounttryPayMoney = Convert.ToDecimal(dtTableMouney.Rows[0]["TaxCounttryPayMoney"]);
                    this.printData.TaxLocalPayMoney = Convert.ToDecimal(dtTableMouney.Rows[0]["TaxLocalPayMoney"]);
                    this.printData.TaxSewagePayMoney = Convert.ToDecimal(dtTableMouney.Rows[0]["TaxSewagePayMoney"]);
                    this.printData.TaxSoilWaterPayMoney = Convert.ToDecimal(dtTableMouney.Rows[0]["TaxSoilWaterPayMoney"]);
                    this.printData.TaxesPrice = Convert.ToDecimal(dtTableMouney.Rows[0]["CoalSaleUnitPrice"]);
                    this.printData.TaxFundPayMoney = Convert.ToDecimal(dtTableMouney.Rows[0]["TaxFundPayMoney"]);
                    this.printData.TaxCanBaoPayMoney = Convert.ToDecimal(dtTableMouney.Rows[0]["TaxCanBaoPayMoney"]);
                    this.printData.TaxYuLinPayMoney = Convert.ToDecimal(dtTableMouney.Rows[0]["TaxYuLinPayMoney"]);
                    this.printData.TaxYunShuPayMoney = Convert.ToDecimal(dtTableMouney.Rows[0]["TaxYunShuPayMoney"]);
                    this.printData.PayMoney = Convert.ToDecimal(dtTableMouney.Rows[0]["TaxPayMoney"]);
                    this.printData.PrintTimes = 1;
                }
                catch (Exception exception)
                {
                    LogHelper.WriteLog(LogHelper.GetCurSourceFileName() + "~" + LogHelper.GetLineNum() + "税费数据获取或加工异常！", exception);
                }


                //string sql_TaxItemDetail =
                //    "select * FROM [IndustryPlatform].[dbo].[VT_TaxItemDetail] where RoomCode=@RoomCode and CoalKindCode=@CoalKindCode";
                //SqlParameter[] sqlp_TaxItemDetail =
                //{
                //    new SqlParameter("@RoomCode", dtTable.Rows[0]["RoomCode"].ToString()),
                //    new SqlParameter("@CoalKindCode", dtTable.Rows[0]["CoalKindCode"].ToString()),
                //};
                //DataTable dtVT_TaxItemDetail = SQLHelper.ExcuteDataTable(sql_TaxItemDetail, sqlp_TaxItemDetail);
                //foreach (DataRow taxRow in dtVT_TaxItemDetail.Rows)
                //{
                //    switch (taxRow[1].ToString())
                //    {
                //        case "所得税":
                //            this.printData.TaxSuoDeShuiPayMoney = (Convert.ToDecimal(taxRow[2].ToString())*
                //                                                   Convert.ToDecimal(this.printData.NetWeight));
                //            break;
                //        case "资源税":
                //            this.printData.TaxLocalPayMoney = (Convert.ToDecimal(taxRow[2].ToString())*
                //                                               Convert.ToDecimal(this.printData.NetWeight));
                //            break;
                //        case "水土保持费":
                //            this.printData.TaxSoilWaterPayMoney = (Convert.ToDecimal(taxRow[2].ToString())*
                //                                                   Convert.ToDecimal(this.printData.NetWeight));
                //            break;
                //        case "育林基金":
                //            this.printData.TaxYuLinPayMoney = (Convert.ToDecimal(taxRow[2].ToString())*
                //                                               Convert.ToDecimal(this.printData.NetWeight));
                //            break;
                //        case "残保金":
                //            this.printData.TaxCanBaoPayMoney = (Convert.ToDecimal(taxRow[2].ToString())*
                //                                                Convert.ToDecimal(this.printData.NetWeight));
                //            break;
                //        case "排污费":
                //            this.printData.TaxSewagePayMoney = (Convert.ToDecimal(taxRow[2].ToString())*
                //                                                Convert.ToDecimal(this.printData.NetWeight));
                //            break;
                //        default:
                //            break;
                //    }
                //}
                //this.printData.PayMoney = Convert.ToDecimal(dtTable.Rows[0]["TaxAmount"].ToString());

                #endregion

                #region 销往目的地

                try
                {
                    string cityDataset = "select * from [TT_SalesCustomer] where SaleCode=@SaleCode";
                    SqlParameter sqlp_salecode = new SqlParameter("@SaleCode", dtTable.Rows[0]["CustomerName"].ToString());
                    DataTable saleCityName = SQLHelper.ExcuteDataTable(cityDataset, sqlp_salecode);
                    if (saleCityName.Rows.Count > 0)
                    {
                        this.printData.PayCity = saleCityName.Rows[0]["SaleCustomer"].ToString();
                    }
                    else
                    {
                        this.printData.PayCity = "";
                    }
                }
                catch (Exception exception)
                {
                    LogHelper.WriteLog(LogHelper.GetCurSourceFileName() + "~" + LogHelper.GetLineNum() + "销往数据获取或加工异常！", exception);
                }

                #endregion

                //this.printData.IsPrintTick = CarrierAdapter.Instance.Config.GetIsPrintTick();这个是从配置文件中获取数据，我没有找到
                InvoicePrint(this.printData);
                textBox1.Clear();
            }
        }

        #endregion

    }
}
