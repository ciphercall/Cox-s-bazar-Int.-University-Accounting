using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlchemyAccounting.payroll.model
{
    public class payroll_model
    {
        private string empID;

        public string EmpID
        {
            get { return empID; }
            set { empID = value; }
        }
        private string empNM;

        public string EmpNM
        {
            get { return empNM; }
            set { empNM = value; }
        }
        private string fatherNM;

        public string FatherNM
        {
            get { return fatherNM; }
            set { fatherNM = value; }
        }
        private string qaterID;

        public string QaterID
        {
            get { return qaterID; }
            set { qaterID = value; }
        }
        private DateTime idExpDt;

        public DateTime IdExpDt
        {
            get { return idExpDt; }
            set { idExpDt = value; }
        }
        private string ppNo;

        public string PpNo
        {
            get { return ppNo; }
            set { ppNo = value; }
        }
        private DateTime ppExpDt;

        public DateTime PpExpDt
        {
            get { return ppExpDt; }
            set { ppExpDt = value; }
        }
        private string nationality;

        public string Nationality
        {
            get { return nationality; }
            set { nationality = value; }
        }
        private string occupation;

        public string Occupation
        {
            get { return occupation; }
            set { occupation = value; }
        }
        private string fileNo;

        public string FileNo
        {
            get { return fileNo; }
            set { fileNo = value; }
        }
        private decimal basicSal;

        public decimal BasicSal
        {
            get { return basicSal; }
            set { basicSal = value; }
        }
        private decimal foods;

        public decimal Foods
        {
            get { return foods; }
            set { foods = value; }
        }
        private string address;

        public string Address
        {
            get { return address; }
            set { address = value; }
        }
        private string contactNo;

        public string ContactNo
        {
            get { return contactNo; }
            set { contactNo = value; }
        }
        private DateTime entryDT;

        public DateTime EntryDT
        {
            get { return entryDT; }
            set { entryDT = value; }
        }
        private string comNM;

        public string ComNM
        {
            get { return comNM; }
            set { comNM = value; }
        }
        private string reference;

        public string Reference
        {
            get { return reference; }
            set { reference = value; }
        }
        private DateTime vacFr;

        public DateTime VacFr
        {
            get { return vacFr; }
            set { vacFr = value; }
        }
        private DateTime vacTo;

        public DateTime VacTo
        {
            get { return vacTo; }
            set { vacTo = value; }
        }
        private string status;

        public string Status
        {
            get { return status; }
            set { status = value; }
        }
        private string note;

        public string Note
        {
            get { return note; }
            set { note = value; }
        }
        private DateTime holDt;

        public DateTime HolDt
        {
            get { return holDt; }
            set { holDt = value; }
        }
        private string holSt;

        public string HolSt
        {
            get { return holSt; }
            set { holSt = value; }
        }
        private string remarks;

        public string Remarks
        {
            get { return remarks; }
            set { remarks = value; }
        }
        private DateTime transDT;

        public DateTime TransDT
        {
            get { return transDT; }
            set { transDT = value; }
        }
        private string transMY;

        public string TransMY
        {
            get { return transMY; }
            set { transMY = value; }
        }
        private string siteNM;

        public string SiteNM
        {
            get { return siteNM; }
            set { siteNM = value; }
        }
        private string siteID;

        public string SiteID
        {
            get { return siteID; }
            set { siteID = value; }
        }
        private string trade;

        public string Trade
        {
            get { return trade; }
            set { trade = value; }
        }
        private decimal norHR;

        public decimal NorHR
        {
            get { return norHR; }
            set { norHR = value; }
        }
        private decimal norOT;

        public decimal NorOT
        {
            get { return norOT; }
            set { norOT = value; }
        }
        private decimal fOT;

        public decimal FOT
        {
            get { return fOT; }
            set { fOT = value; }
        }
        private decimal hOT;

        public decimal HOT
        {
            get { return hOT; }
            set { hOT = value; }
        }
        private Int64 sl;

        public Int64 Sl
        {
            get { return sl; }
            set { sl = value; }
        }
        private DateTime inTm;

        public DateTime InTm
        {
            get { return inTm; }
            set { inTm = value; }
        }

        private string userNm;

        public string UserNm
        {
            get { return userNm; }
            set { userNm = value; }
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

        private decimal bouns;

        public decimal Bouns
        {
            get { return bouns; }
            set { bouns = value; }
        }
        private decimal othAdd;

        public decimal OthAdd
        {
            get { return othAdd; }
            set { othAdd = value; }
        }
        private decimal advance;

        public decimal Advance
        {
            get { return advance; }
            set { advance = value; }
        }
        private decimal penalty;

        public decimal Penalty
        {
            get { return penalty; }
            set { penalty = value; }
        }
        private decimal othDed;

        public decimal OthDed
        {
            get { return othDed; }
            set { othDed = value; }
        }
        private int mmDays;

        public int MmDays
        {
            get { return mmDays; }
            set { mmDays = value; }
        }
        private int nmDays;

        public int NmDays
        {
            get { return nmDays; }
            set { nmDays = value; }
        }
        private int otDays;

        public int OtDays
        {
            get { return otDays; }
            set { otDays = value; }
        }
        private decimal ratePD;

        public decimal RatePD
        {
            get { return ratePD; }
            set { ratePD = value; }
        }
        private decimal ratePH;

        public decimal RatePH
        {
            get { return ratePH; }
            set { ratePH = value; }
        }
        private int otHour;

        public int OtHour
        {
            get { return otHour; }
            set { otHour = value; }
        }
        private decimal otcAdd;

        public decimal OtcAdd
        {
            get { return otcAdd; }
            set { otcAdd = value; }
        }
        private decimal grossAmt;

        public decimal GrossAmt
        {
            get { return grossAmt; }
            set { grossAmt = value; }
        }
        private decimal otcDED;

        public decimal OtcDED
        {
            get { return otcDED; }
            set { otcDED = value; }
        }
        private decimal netAmt;

        public decimal NetAmt
        {
            get { return netAmt; }
            set { netAmt = value; }
        }
        private decimal otAmt;

        public decimal OtAmt
        {
            get { return otAmt; }
            set { otAmt = value; }
        }

        //// Quotation

        private DateTime qDt;

        public DateTime QDt
        {
            get { return qDt; }
            set { qDt = value; }
        }
        private int year;

        public int Year
        {
            get { return year; }
            set { year = value; }
        }
        private Int64 trNo;

        public Int64 TrNo
        {
            get { return trNo; }
            set { trNo = value; }
        }
        private string quoteNo;

        public string QuoteNo
        {
            get { return quoteNo; }
            set { quoteNo = value; }
        }
        private string compNM;

        public string CompNM
        {
            get { return compNM; }
            set { compNM = value; }
        }
        private string compADD;

        public string CompADD
        {
            get { return compADD; }
            set { compADD = value; }
        }
        private string compContact;

        public string CompContact
        {
            get { return compContact; }
            set { compContact = value; }
        }
        private string attnPerNm;

        public string AttnPerNm
        {
            get { return attnPerNm; }
            set { attnPerNm = value; }
        }
        private string attPerDesig;

        public string AttPerDesig
        {
            get { return attPerDesig; }
            set { attPerDesig = value; }
        }
        private string subject;

        public string Subject
        {
            get { return subject; }
            set { subject = value; }
        }
        private string prepNM;

        public string PrepNM
        {
            get { return prepNM; }
            set { prepNM = value; }
        }
        private string prepDesig;

        public string PrepDesig
        {
            get { return prepDesig; }
            set { prepDesig = value; }
        }
        private string prepContact;

        public string PrepContact
        {
            get { return prepContact; }
            set { prepContact = value; }
        }
        private string qtTp;

        public string QtTp
        {
            get { return qtTp; }
            set { qtTp = value; }
        }
        private Int64 qSL;

        public Int64 QSL
        {
            get { return qSL; }
            set { qSL = value; }
        }
        private string desc;

        public string Desc
        {
            get { return desc; }
            set { desc = value; }
        }
        private decimal qRate;

        public decimal QRate
        {
            get { return qRate; }
            set { qRate = value; }
        }
        private decimal qQty;

        public decimal QQty
        {
            get { return qQty; }
            set { qQty = value; }
        }
        private decimal qTotal;

        public decimal QTotal
        {
            get { return qTotal; }
            set { qTotal = value; }
        }
        private string prepCompNM;

        public string PrepCompNM
        {
            get { return prepCompNM; }
            set { prepCompNM = value; }
        }

        private string unit;

        public string Unit
        {
            get { return unit; }
            set { unit = value; }
        }
    }
}