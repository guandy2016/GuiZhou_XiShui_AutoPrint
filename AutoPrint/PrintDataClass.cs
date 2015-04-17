using System;
using System.Collections.Generic;
using System.Text;

namespace AutoPrint
{
    using System;

    public class PrintDataClass
    {
        private string _randomNum = "";
        private string _titleTip = "";
        private string m_CarNo;
        private string m_CarOwnerName;
        private string m_CarType;
        private string m_CoalKindName;
        private decimal m_CoalSaleUnitPrice = 0M;
        private string m_CollManager;
        private string m_CollName;
        private string m_EmptyWeight;
        private bool m_IsBeatUp = false;
        private int m_IsDisplayTax = 0;
        private bool m_IsPrintTick = false;
        private string m_LoadWeight;
        private string m_LoadWeightTime;
        private string m_MarkedCardCode;
        private string m_NavicertCode;
        private string m_NetWeight;
        private string m_Operator;
        private string m_PayCity;
        private decimal m_PayMoney;
        private int m_PrintTimes;
        private string m_RoomName;
        private string m_strNote;
        private decimal m_TaxCanBaoPayMoney = 0M;
        private decimal m_TaxCounttryPayMoney = 0M;
        private decimal m_TaxesPrice;
        private decimal m_TaxFundPayMoney = 0M;
        private decimal m_TaxLocalPayMoney = 0M;
        private decimal m_TaxSewagePayMoney = 0M;
        private decimal m_TaxSoilWaterPayMoney = 0M;
        private decimal m_TaxYuLinPayMoney = 0M;
        private decimal m_TaxYunShuPayMoney = 0M;
        private decimal m_TaxSuoDeShuiPayMoney = 0M;
        private string m_TradeID;
        private string m_TradeID0;

        /// <summary>
        /// 所得税
        /// </summary>
        public decimal TaxSuoDeShuiPayMoney
        {
            get
            {
                return this.m_TaxSuoDeShuiPayMoney;
            }
            set
            {
                this.m_TaxSuoDeShuiPayMoney = value;
            }
        }
        public string CarNo
        {
            get
            {
                return this.m_CarNo;
            }
            set
            {
                this.m_CarNo = value;
            }
        }

        public string CarOwnerName
        {
            get
            {
                return this.m_CarOwnerName;
            }
            set
            {
                this.m_CarOwnerName = value;
            }
        }

        public string CarType
        {
            get
            {
                return this.m_CarType;
            }
            set
            {
                this.m_CarType = value;
            }
        }

        public string CoalKindName
        {
            get
            {
                return this.m_CoalKindName;
            }
            set
            {
                this.m_CoalKindName = value;
            }
        }
        /// <summary>
        /// 销售单价
        /// </summary>
        public decimal CoalSaleUnitPrice
        {
            get
            {
                return this.m_CoalSaleUnitPrice;
            }
            set
            {
                this.m_CoalSaleUnitPrice = value;
            }
        }

        public string CollManager
        {
            get
            {
                return this.m_CollManager;
            }
            set
            {
                this.m_CollManager = value;
            }
        }

        public string CollName
        {
            get
            {
                return this.m_CollName;
            }
            set
            {
                this.m_CollName = value;
            }
        }

        public string EmptyWeight
        {
            get
            {
                return this.m_EmptyWeight;
            }
            set
            {
                this.m_EmptyWeight = value;
            }
        }

        public bool IsBeatUp
        {
            get
            {
                return this.m_IsBeatUp;
            }
            set
            {
                this.m_IsBeatUp = value;
            }
        }

        public int IsDisplayTax
        {
            get
            {
                return this.m_IsDisplayTax;
            }
            set
            {
                this.m_IsDisplayTax = value;
            }
        }

        public bool IsPrintTick
        {
            get
            {
                return this.m_IsPrintTick;
            }
            set
            {
                this.m_IsPrintTick = value;
            }
        }

        public string LoadWeight
        {
            get
            {
                return this.m_LoadWeight;
            }
            set
            {
                this.m_LoadWeight = value;
            }
        }

        public string LoadWeightTime
        {
            get
            {
                return this.m_LoadWeightTime;
            }
            set
            {
                this.m_LoadWeightTime = value;
            }
        }

        public string MarkedCardCode
        {
            get
            {
                return this.m_MarkedCardCode;
            }
            set
            {
                this.m_MarkedCardCode = value;
            }
        }

        public string NavicertCode
        {
            get
            {
                return this.m_NavicertCode;
            }
            set
            {
                this.m_NavicertCode = value;
            }
        }

        public string NetWeight
        {
            get
            {
                return this.m_NetWeight;
            }
            set
            {
                this.m_NetWeight = value;
            }
        }

        public string Operator
        {
            get
            {
                return this.m_Operator;
            }
            set
            {
                this.m_Operator = value;
            }
        }

        public string PayCity
        {
            get
            {
                return this.m_PayCity;
            }
            set
            {
                this.m_PayCity = value;
            }
        }
        /// <summary>
        /// 合计
        /// </summary>
        public decimal PayMoney
        {
            get
            {
                return this.m_PayMoney;
            }
            set
            {
                this.m_PayMoney = value;
            }
        }

        public int PrintTimes
        {
            get
            {
                return this.m_PrintTimes;
            }
            set
            {
                this.m_PrintTimes = value;
            }
        }

        public string RandomNum
        {
            get
            {
                return this._randomNum;
            }
            set
            {
                this._randomNum = value;
            }
        }

        public string RoomName
        {
            get
            {
                return this.m_RoomName;
            }
            set
            {
                this.m_RoomName = value;
            }
        }

        public string StrNote
        {
            get
            {
                return this.m_strNote;
            }
            set
            {
                this.m_strNote = value;
            }
        }
        /// <summary>
        /// 残保金
        /// </summary>
        public decimal TaxCanBaoPayMoney
        {
            get
            {
                return this.m_TaxCanBaoPayMoney;
            }
            set
            {
                this.m_TaxCanBaoPayMoney = value;
            }
        }

        public decimal TaxCounttryPayMoney
        {
            get
            {
                return this.m_TaxCounttryPayMoney;
            }
            set
            {
                this.m_TaxCounttryPayMoney = value;
            }
        }

        public decimal TaxesPrice
        {
            get
            {
                return this.m_TaxesPrice;
            }
            set
            {
                this.m_TaxesPrice = value;
            }
        }

        public decimal TaxFundPayMoney
        {
            get
            {
                return this.m_TaxFundPayMoney;
            }
            set
            {
                this.m_TaxFundPayMoney = value;
            }
        }
        /// <summary>
        /// 资源税
        /// </summary>
        public decimal TaxLocalPayMoney
        {
            get
            {
                return this.m_TaxLocalPayMoney;
            }
            set
            {
                this.m_TaxLocalPayMoney = value;
            }
        }
        /// <summary>
        /// 排污费
        /// </summary>
        public decimal TaxSewagePayMoney
        {
            get
            {
                return this.m_TaxSewagePayMoney;
            }
            set
            {
                this.m_TaxSewagePayMoney = value;
            }
        }
        /// <summary>
        /// 水土保持费
        /// </summary>
        public decimal TaxSoilWaterPayMoney
        {
            get
            {
                return this.m_TaxSoilWaterPayMoney;
            }
            set
            {
                this.m_TaxSoilWaterPayMoney = value;
            }
        }
        /// <summary>
        /// 育林基金
        /// </summary>
        public decimal TaxYuLinPayMoney
        {
            get
            {
                return this.m_TaxYuLinPayMoney;
            }
            set
            {
                this.m_TaxYuLinPayMoney = value;
            }
        }
        /// <summary>
        /// 运输费
        /// </summary>
        public decimal TaxYunShuPayMoney
        {
            get
            {
                return this.m_TaxYunShuPayMoney;
            }
            set
            {
                this.m_TaxYunShuPayMoney = value;
            }
        }

        public string TitleTip
        {
            get
            {
                return this._titleTip;
            }
            set
            {
                this._titleTip = value;
            }
        }

        public string TradeID
        {
            get
            {
                return this.m_TradeID;
            }
            set
            {
                this.m_TradeID = value;
            }
        }

        public string TradeID0
        {
            get
            {
                return this.m_TradeID0;
            }
            set
            {
                this.m_TradeID0 = value;
            }
        }

        public PrintDataClass()
        {
            this.m_TaxCounttryPayMoney = 0M;
            this.m_TaxLocalPayMoney = 0M;
            this.m_TaxSewagePayMoney = 0M;
            this.m_TaxSoilWaterPayMoney = 0M;
            this.m_CoalSaleUnitPrice = 0M;
            this.m_TaxFundPayMoney = 0M;
            this.m_TaxCanBaoPayMoney = 0M;
            this.m_TaxYuLinPayMoney = 0M;
            this.m_TaxYunShuPayMoney = 0M;
            this.m_IsPrintTick = false;
            this.m_IsBeatUp = false;
            this._randomNum = "";
            this._titleTip = "";
            this.m_IsDisplayTax = 0;
        }

    }
}
