﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlchemyAccounting.Accounts.Interface
{
    public class SingleVoucher
    {
        private string transtp;

        public string Transtp
        {
            get { return transtp; }
            set { transtp = value; }
        }
        private DateTime transdt;

        public DateTime Transdt
        {
            get { return transdt; }
            set { transdt = value; }
        }
        private string monyear;

        public string Monyear
        {
            get { return monyear; }
            set { monyear = value; }
        }
        private int voucher;

        public int Voucher
        {
            get { return voucher; }
            set { voucher = value; }
        }
        private string transfor;

        public string Transfor
        {
            get { return transfor; }
            set { transfor = value; }
        }
        private string costpid;

        public string Costpid
        {
            get { return costpid; }
            set { costpid = value; }
        }
        private string transmode;

        public string Transmode
        {
            get { return transmode; }
            set { transmode = value; }
        }
        private string debitcd;

        public string Debitcd
        {
            get { return debitcd; }
            set { debitcd = value; }
        }
        private string creditcd;

        public string Creditcd
        {
            get { return creditcd; }
            set { creditcd = value; }
        }
        private string chequeno;

        public string Chequeno
        {
            get { return chequeno; }
            set { chequeno = value; }
        }
        private DateTime chequedt;

        public DateTime Chequedt
        {
            get { return chequedt; }
            set { chequedt = value; }
        }
        private string remarks;

        public string Remarks
        {
            get { return remarks; }
            set { remarks = value; }
        }
        private decimal amount;

        public decimal Amount
        {
            get { return amount; }
            set { amount = value; }
        }
        private string inword;

        public string Inword
        {
            get { return inword; }
            set { inword = value; }
        }
        private string username;

        public string Username
        {
            get { return username; }
            set { username = value; }
        }
        private decimal debitAmt;

        public decimal DebitAmt
        {
            get { return debitAmt; }
            set { debitAmt = value; }
        }
        private decimal creditAmt;

        public decimal CreditAmt
        {
            get { return creditAmt; }
            set { creditAmt = value; }
        }

        private string serialNo_MREC;

        public string SerialNo_MREC
        {
            get { return serialNo_MREC; }
            set { serialNo_MREC = value; }
        }

        private string serialNo_MPAY;

        public string SerialNo_MPAY
        {
            get { return serialNo_MPAY; }
            set { serialNo_MPAY = value; }
        }
        private string serialNo_JOUR;

        public string SerialNo_JOUR
        {
            get { return serialNo_JOUR; }
            set { serialNo_JOUR = value; }
        }

        private string serial_Cont;

        public string Serial_Cont
        {
            get { return serial_Cont; }
            set { serial_Cont = value; }
        }
        string transNo;

        public string TransNo
        {
            get { return transNo; }
            set { transNo = value; }
        }

        private string storeTo;

        public string StoreTo
        {
            get { return storeTo; }
            set { storeTo = value; }
        }

        private string storeFrom;

        public string StoreFrom
        {
            get { return storeFrom; }
            set { storeFrom = value; }
        }

        private string psID;

        public string PsID
        {
            get { return psID; }
            set { psID = value; }
        }

        private string serial_BUY;

        public string Serial_BUY
        {
            get { return serial_BUY; }
            set { serial_BUY = value; }
        }

        private string serial_SALE;

        public string Serial_SALE
        {
            get { return serial_SALE; }
            set { serial_SALE = value; }
        }

        private string sl_Sale_dis;

        public string Sl_Sale_dis
        {
            get { return sl_Sale_dis; }
            set { sl_Sale_dis = value; }
        }

        private string userpc;

        public string Userpc
        {
            get { return userpc; }
            set { userpc = value; }
        }

        private string ipaddress;

        public string Ipaddress
        {
            get { return ipaddress; }
            set { ipaddress = value; }
        }
        private string pcd;

        public string Pcd
        {
            get { return pcd; }
            set { pcd = value; }
        }
        private string psid;

        public string Psid
        {
            get { return psid; }
            set { psid = value; }
        }
        private string commission;

        public string Commission
        {
            get { return commission; }
            set { commission = value; }
        }
        private string carrent;

        public string Carrent
        {
            get { return carrent; }
            set { carrent = value; }
        }
        private decimal billamt;

        public decimal Billamt
        {
            get { return billamt; }
            set { billamt = value; }
        }
        private decimal commamt;

        public decimal Commamt
        {
            get { return commamt; }
            set { commamt = value; }
        }
        private decimal carrentamt;

        public decimal Carrentamt
        {
            get { return carrentamt; }
            set { carrentamt = value; }
        }

        public decimal VatAmount { get; set; }
    }
}