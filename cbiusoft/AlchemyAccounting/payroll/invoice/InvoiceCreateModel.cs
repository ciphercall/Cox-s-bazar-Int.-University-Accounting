using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlchemyAccounting.payroll.invoice
{
    public class InvoiceCreateModel
    {

        private string companyNM;

        public string COMPANYNM
        {
            get { return companyNM; }
            set { companyNM = value; }
        }

        private DateTime billdt;

        public DateTime BILLDT
        {
            get { return billdt; }
            set { billdt = value; }
        }

        private string billmy;

        public string BILLMY
        {
            get { return billmy; }
            set { billmy = value; }
        }

        private string submitpnm;

        public string SUBMITPNM
        {
            get { return submitpnm; }
            set { submitpnm = value; }
        }

        private string submitpcno;

        public string SUBMITPCNO
        {
            get { return submitpcno; }
            set { submitpcno = value; }
        }


        private Int64 billyy;

        public Int64 BILLYY
        {
            get { return billyy; }
            set { billyy = value; }
        }


        private Int64 billno;

        public Int64 BILLNO
        {
            get { return billno; }
            set { billno = value; }

        }

        private string psid;

        public string PSID
        {
            get { return psid; }
            set { psid = value; }
        }

        private string costpid;

        public string COSTPID
        {
            get { return costpid; }
            set { costpid = value; }
        }

        private string billtp;

        public string BILLTP
        {
            get { return billtp; }
            set { billtp = value; }
        }

        private Int64 billsl;

        public Int64 BILLSL
        {
            get { return billsl; }
            set { billsl = value; }
        }

        private string billnm;

        public string BILLNM
        {
            get { return billnm; }
            set { billnm = value; }
        }

        private Int64 tworker;

        public Int64 TWORKER
        {
            get { return tworker; }
            set { tworker = value; }
        }

        private decimal ratetp;

        public decimal RATEPTP
        {
            get { return ratetp; }
            set { ratetp = value; }
        }

        private decimal totqptp;

        public decimal TOTQPTP
        {
            get { return totqptp; }
            set { totqptp = value; }
        }

        private decimal amtptp;

        public decimal AMTPTP
        {
            get { return amtptp; }
            set { amtptp = value; }
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