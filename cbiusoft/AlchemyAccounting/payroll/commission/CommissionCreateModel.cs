using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlchemyAccounting.payroll.commission
{
    public class CommissionCreateModel
    {
        private DateTime transdt;

        public DateTime TRANSDT
        {
            get { return transdt; }
            set { transdt = value; }
        }


        private string transmy;

        public string TRANSMY
        {
            get { return transmy; }
            set { transmy = value; }
        }

        private Int64 transno;

        public Int64 TRANSNO
        {
            get { return transno; }
            set { transno = value; }

        }


        private string psid;

        public string PSID
        {
            get { return psid; }
            set { psid = value; }
        }

        private string payable;

        public string Payable
        {
            get { return payable; }
            set { payable = value; }
        }

        private string costpid;

        public string COSTPID
        {
            get { return costpid; }
            set { costpid = value; }
        }

        private decimal billamt;

        public decimal BILLAMT
        {
            get { return billamt; }
            set { billamt = value; }
        }

        private decimal compcnt;

        public decimal COMPCNT
        {
            get { return compcnt; }
            set { compcnt = value; }
        }

        private decimal commamt;

        public decimal COMMAMT
        {
            get { return commamt; }
            set { commamt = value; }
        }

        private decimal carrent;

        public decimal CARRENT
        {
            get { return carrent; }
            set { carrent = value; }
        }

        private decimal advamtp;

        public decimal ADVAMTP
        {
            get { return advamtp; }
            set { advamtp = value; }
        }

        private decimal totamt;

        public decimal TOTAMT
        {
            get { return totamt; }
            set { totamt = value; }
        }

        private decimal advamtc;

        public decimal ADVAMTC
        {
            get { return advamtc; }
            set { advamtc = value; }
        }

        private decimal netamt;

        public decimal NETAMT
        {
            get { return netamt; }
            set { netamt = value; }
        }

        private string remarks;

        public string REMARKS
        {
            get { return remarks; }
            set { remarks = value; }
        }

        private DateTime inTm;

        public DateTime InTm
        {
            get { return inTm; }
            set { inTm = value; }
        }

        private string userPc;

        public string UserPc
        {
            get { return userPc; }
            set { userPc = value; }
        }

        private string ip;

        public string Ip
        {
            get { return ip; }
            set { ip = value; }
        }

    }
}