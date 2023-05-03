using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;


namespace AlchemyAccounting
{
    public class Global
    {
        //public static String connection = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ToString();
        // public static String connection = new SqlConnectionStringBuilder { DataSource = "sql2012.dbsqlserver.com,1288", InitialCatalog = "asl_cbiu", UserID = "cbiusoft", Password = "asl_T@deAb5@7532", MultipleActiveResultSets=true,MinPoolSize=1,MaxPoolSize=2000 }.ToString();
        public static String connection = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ToString();
        public static void dropDownAdd(DropDownList ob, String sql)
        {
            List<String> List = new List<string>();
            try
            {
                SqlConnection con = new SqlConnection(connection);
                if (con.State != ConnectionState.Open)con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader rd = cmd.ExecuteReader(); List.Clear();
                //List.Add("Select");
                string a = "Select";

                while (rd.Read())
                {

                    List.Add(rd[0].ToString());
                }
                rd.Close();
                ob.Items.Clear();
                ob.Items.Add(a);
                // ob.Text = "";
                for (int i = 0; i < List.Count; i++)
                {
                    ob.Items.Add(List[i].ToString());
                }
            }
            catch { }
        }
        public static void DropDownAddAllTextWithValue(DropDownList ob, String sql)
        {
            List<String> ListName = new List<string>();
            List<String> ListValue = new List<string>();
            try
            {
                SqlConnection con = new SqlConnection(connection);
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader rd = cmd.ExecuteReader();
                ListName.Clear();
                ListValue.Clear();
                ListName.Add("--SELECT--");
                ListValue.Add("--SELECT--");
                while (rd.Read())
                {
                    ListName.Add(rd[0].ToString());
                    ListValue.Add(rd[1].ToString());
                }
                rd.Close();
                ob.Items.Clear();

                ob.Text = "";
                for (int i = 0; i < ListName.Count; i++)
                {
                    ob.Items.Add(new ListItem(ListName[i].ToUpper(), ListValue[i]));
                }
            }
            catch { }
        }
        public static string Execute(string str)
        {
            string s = "";
            try
            {
                SqlConnection Conn = new SqlConnection(connection);
                Conn.Open();
                SqlCommand cmd = new SqlCommand(str, Conn);
                cmd.ExecuteNonQuery();
                Conn.Close();
                s = "true";
            }
            catch { }
            return s;
        }
        public static void  BindDropDown(DropDownList ob, String sql)
        {
            SqlConnection con = new SqlConnection(connection);
            if (con.State != ConnectionState.Open)con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            ob.Items.Clear();
            da.Fill(ds);
            ob.DataSource = ds;
            ob.DataTextField = "NM";
            ob.DataValueField = "ID";
            ob.DataBind();
            ob.Items.Insert(0, new ListItem("--SELECT--"));
            if (con.State != ConnectionState.Closed)con.Close();
        }
        public static void BindDropDownNM(DropDownList ob, String sql)
        {
            SqlConnection con = new SqlConnection(connection);
            if (con.State != ConnectionState.Open)con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            ob.Items.Clear();
            da.Fill(ds);
            ob.DataSource = ds;
            ob.DataTextField = "NM";
            //ob.DataValueField = "ID";
            ob.DataBind();
            ob.Items.Insert(0, new ListItem("--SELECT--"));
            if (con.State != ConnectionState.Closed)con.Close();
        }
        public static void dropDownAdd_GridEditMode(DropDownList ob, String sql, string root)
        {
            List<String> List = new List<string>();
            try
            {
                SqlConnection con = new SqlConnection(connection);
                if (con.State != ConnectionState.Open)con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader rd = cmd.ExecuteReader(); List.Clear(); 

                while (rd.Read())
                {

                    List.Add(rd[0].ToString());
                }
                rd.Close();
                ob.Items.Clear();
                ob.Items.Add(root);
                // ob.Text = "";
                for (int i = 0; i < List.Count; i++)
                {
                    ob.Items.Add(List[i].ToString());
                }
            }
            catch { }
        }
        public static void dropDownAddWithValue(DropDownList ob, String sql)
        {
            try
            {
                SqlConnection con = new SqlConnection(connection);
                if (con.State != ConnectionState.Open)con.Open();
                SqlCommand com = new SqlCommand(sql, con);
                SqlDataReader dr = com.ExecuteReader();
                ob.Items.Clear();
                string a = "Select";
                ob.Items.Add(a);
                while (dr.Read())
                {
                    ob.Items.Add(new ListItem(dr[0].ToString(), dr[1].ToString()));
                }
                if (con.State != ConnectionState.Closed)con.Close();
            }
            catch { }
        }
        public static void dropDownAddTrans(DropDownList ob, String sql)
        {
            List<String> List = new List<string>();
            try
            {
                SqlConnection con = new SqlConnection(connection);
                if (con.State != ConnectionState.Open)con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader rd = cmd.ExecuteReader(); List.Clear();
                string a = "";
                while (rd.Read())
                {

                    List.Add(rd[0].ToString());
                }
                rd.Close();
                ob.Items.Clear();
                ob.Items.Add(a);

                for (int i = 0; i < List.Count; i++)
                {
                    ob.Items.Add(List[i].ToString());
                }
            }
            catch { }
        }

        public static void dropDownAddWithSelect(DropDownList ob, String sql)
        {
            List<String> List = new List<string>();
            try
            {
                SqlConnection con = new SqlConnection(connection);
                if (con.State != ConnectionState.Open)con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader rd = cmd.ExecuteReader(); List.Clear();
                List.Add("Select");
                while (rd.Read())
                {
                    List.Add(rd[0].ToString());
                }
                rd.Close();
                ob.Items.Clear();
                ob.Text = "";
                for (int i = 0; i < List.Count; i++)
                {
                    ob.Items.Add(List[i].ToString());
                }
            }
            catch { }
        }

        public static void editableDropDownAdd(DropDownList ob, String sql)
        {
            List<String> List = new List<string>();
            try
            {
                SqlConnection con = new SqlConnection(connection);
                if (con.State != ConnectionState.Open)con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader rd = cmd.ExecuteReader(); List.Clear();
                List.Add("Select");
                while (rd.Read())
                {
                    List.Add(rd[0].ToString());
                }
                rd.Close();
                ob.Items.Clear();
                ob.Text = "";
                for (int i = 0; i < List.Count; i++)
                {
                    ob.Items.Add(List[i].ToString());
                }
            }
            catch { }
        }

        public static void listAdd(ListBox ob, String sql)
        {
            List<String> List = new List<string>();
            try
            {
                SqlConnection con = new SqlConnection(connection);
                if (con.State != ConnectionState.Open)con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader rd = cmd.ExecuteReader();
                List.Clear();
                while (rd.Read())
                {
                    List.Add(rd[0].ToString());
                }
                rd.Close();
                ob.Items.Clear();
                ob.Text = "";
                for (int i = 0; i < List.Count; i++)
                {
                    ob.Items.Add(List[i].ToString());
                }
            }
            catch { }
        }
        public static void txtAdd(String sql, TextBox txtadd)
        {
            //String mystring = "";
            try
            {
                SqlConnection con = new SqlConnection(connection);
                if (con.State != ConnectionState.Open)con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    txtadd.Text = reader[0].ToString();
                }
                if (con.State != ConnectionState.Closed)con.Close();
                reader.Close();
            }
            catch { }
            //return List;
        }

        public static void lblAdd(String sql, Label lblAdd)
        {
            try
            {
                SqlConnection con = new SqlConnection(connection);
                if (con.State != ConnectionState.Open)con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    lblAdd.Text = reader[0].ToString();
                }
                if (con.State != ConnectionState.Closed)con.Close();
                reader.Close();
            }
            catch { }
        }

        public static void StringAdd(String sql, String Str)
        {
            try
            {
                SqlConnection con = new SqlConnection(connection);
                if (con.State != ConnectionState.Open)con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Str = reader[0].ToString();
                }
                if (con.State != ConnectionState.Closed)con.Close();
                reader.Close();
            }
            catch { }
        }

        public static void gridViewAdd(GridView ob, String sql)
        {
            DataTable table = new DataTable();
            try
            {
                SqlConnection con = new SqlConnection(connection);
                if (con.State != ConnectionState.Open)con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataAdapter ad = new SqlDataAdapter(cmd);
                ad.Fill(table);
                ob.DataSource = table;
                ob.DataBind();
            }
            catch { }
            //return List;
        }
        public static DateTime DateTimeZoon(DateTime date)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            string td = PrintDate.ToString("dd-MM-yyyy HH:mm:ss");
            DateTime datetime = Convert.ToDateTime(td);
            return datetime;
        }
        public static string Dayformat(DateTime dt)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            string td = PrintDate.ToString("dd/MM/yyyy");
            string mydate = td;
            return mydate;
        }
        public static DateTime Dayformat1(DateTime dt)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            return PrintDate;
        }
        public static string DayformatHifen(DateTime dt)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            string td = PrintDate.ToString("dd-MM-yyyy");
            string mydate = td;
            return mydate;
        }
        public static string TimeFormat(DateTime Tt)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            string td = PrintDate.ToString("HH:mm:ss");
            string myTime = td;
            return myTime;
        }
        public string monformat(DateTime mm)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            string td = PrintDate.ToString("MMM");
            string mymonth = td;
            return mymonth;
        }
        public static string Slipt(string text, int position, char sumbol)
        {
            string s = "";
            string searchPar = text;
            int splitter = searchPar.IndexOf(sumbol);
            if (splitter != -1)
            {
                string[] lineSplit = searchPar.Split(sumbol);
                s = lineSplit[position];
            }
            return s;
        }
        public static string GetData(string str)
        {
            string Result = "";
            try
            {

                SqlConnection Conn = new SqlConnection(connection);
                Conn.Open();
                SqlCommand cmd = new SqlCommand(str, Conn);
                SqlDataReader DR = cmd.ExecuteReader();
                if (DR.Read())
                    Result = DR[0].ToString();
                DR.Close();
                Conn.Close();

            }
            catch { }
            return Result;
        }

        public static void FormView(FormView ob, String sql)
        {
            DataTable table = new DataTable();
            try
            {
                SqlConnection con = new SqlConnection(connection);
                if (con.State != ConnectionState.Open)con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataAdapter ad = new SqlDataAdapter(cmd);
                ad.Fill(table);
                ob.DataSource = table;
                ob.DataBind();
            }
            catch { }
            //return List;
        }
    }
}
