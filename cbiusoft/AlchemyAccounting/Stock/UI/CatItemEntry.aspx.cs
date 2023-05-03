using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using AlchemyAccounting;
using System.Data.SqlClient;
using System.Web.Services;
using System.Text;
using System.Collections.Specialized;

namespace AlchemyAccounting.Stock.UI
{
    public partial class CatItemEntry : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                if (!Page.IsPostBack)
                {
                    txtCategoryNM.Focus();
                    lblCatID.Text = "";
                }
            }
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT CATNM FROM STK_ITEMMST WHERE CATNM like '" + prefixText + "%'", conn);
            SqlDataReader oReader;
            conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["CATNM"].ToString());
            return CompletionSet.ToArray();
        }

        protected void txtCategoryNM_TextChanged(object sender, EventArgs e)
        {
            lblCatID.Text = "";
            lblMaxCatID.Text = "";
            Global.lblAdd(@"select CATID from STK_ITEMMST where CATNM='" + txtCategoryNM.Text + "'", lblCatID);
            Global.lblAdd(@"select max(CATID) from STK_ITEMMST", lblMaxCatID);
            if (lblCatID.Text == "")
            {
                if (lblMaxCatID.Text == "")
                {
                    lblCatID.Text = "I001";
                }
                else
                {
                    string MaxCatId = lblMaxCatID.Text;
                    string CatId = MaxCatId.Substring(1, 3);
                    string mid, C_ID;
                    int ID = int.Parse(CatId);
                    int CID = ID + 1;
                    if (CID < 10)
                    {
                        mid = CID.ToString();
                        C_ID = "00" + mid;
                    }
                    else if (CID < 100)
                    {
                        mid = CID.ToString();
                        C_ID = "0" + mid;
                    }
                    else
                        C_ID = CID.ToString();
                    string FID = "I" + C_ID.ToString();
                    lblCatID.Text = FID;
                }
            }
            else
            {
                
            }
            Search.Focus();
            //BindEmployeeDetails();
        }

        protected void BindEmployeeDetails()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();
            SqlCommand cmd = new SqlCommand("Select * from STK_ITEM  where CATID='" + lblCatID.Text + "'", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvDetails.DataSource = ds;
                gvDetails.DataBind();
                TextBox txtItemNM = (TextBox)gvDetails.FooterRow.FindControl("txtItemNM");
                txtItemNM.Focus();
            }
            else
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                gvDetails.DataSource = ds;
                gvDetails.DataBind();
                int columncount = gvDetails.Rows[0].Cells.Count;
                gvDetails.Rows[0].Cells.Clear();
                gvDetails.Rows[0].Cells.Add(new TableCell());
                gvDetails.Rows[0].Cells[0].ColumnSpan = columncount;
                gvDetails.Rows[0].Cells[0].Text = "No Records Found";
                TextBox txtItemNM = (TextBox)gvDetails.FooterRow.FindControl("txtItemNM");
                txtItemNM.Focus();
            }
        }

        protected void Search_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();
            SqlCommand cmd = new SqlCommand("Select CATID from STK_ITEMMST  where CATID='" + lblCatID.Text + "'", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                BindEmployeeDetails();
            }
            else
            {
                string userName = HttpContext.Current.Session["UserName"].ToString();

                string query = "";
                SqlCommand comm = new SqlCommand(query, conn);

                query = ("insert into STK_ITEMMST (CATID, CATNM, USERPC, USERID,IPADDRESS) " +
                               "values('" + lblCatID.Text + "','" + txtCategoryNM.Text + "','',@USERID,'')");

                comm = new SqlCommand(query, conn);
                comm.Parameters.AddWithValue("@USERID", userName);

                conn.Open();
                int result = comm.ExecuteNonQuery();
                conn.Close();
                BindEmployeeDetails();
            }
        }

        protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Global.lblAdd(@"select MAX(ITEMID) from STK_ITEM where CATID = '"+ lblCatID.Text +"'", lblIMaxItemID);
                string ItemCD;
                string mxCD,OItemCD,mid,subItemCD;
                int subCD, incrItCD;
                if (lblIMaxItemID.Text == "")
                {
                    ItemCD = lblCatID.Text + "0001";
                }
                else
                {
                    mxCD = lblIMaxItemID.Text;
                    OItemCD = mxCD.Substring(4,4);
                    subCD = int.Parse(OItemCD);
                    incrItCD = subCD + 1;
                    if (incrItCD < 10)
                    {
                        mid = incrItCD.ToString();
                        subItemCD = "000" + mid;
                    }
                    else if (incrItCD < 100)
                    {
                        mid = incrItCD.ToString();
                        subItemCD = "00" + mid;
                    }
                    else if (incrItCD < 1000)
                    {
                        mid = incrItCD.ToString();
                        subItemCD = "0" + mid;
                    }
                    else
                        subItemCD = incrItCD.ToString();

                    ItemCD = lblCatID.Text + subItemCD;
                }
                e.Row.Cells[0].Text = lblCatID.Text;
                e.Row.Cells[1].Text = ItemCD;
            }
        }

        protected void gvDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string userName = HttpContext.Current.Session["UserName"].ToString();
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            string query = "";
            SqlCommand comm = new SqlCommand(query, conn);

           

            if (e.CommandName.Equals("AddNew"))
            {
                string CatID, ItemID;
                CatID = gvDetails.FooterRow.Cells[0].Text;
                ItemID = gvDetails.FooterRow.Cells[1].Text;
                TextBox txtItemNM = (TextBox)gvDetails.FooterRow.FindControl("txtItemNM");
                TextBox txtBrand = (TextBox)gvDetails.FooterRow.FindControl("txtBrand");
                TextBox txtUnit = (TextBox)gvDetails.FooterRow.FindControl("txtUnit");
                TextBox txtBuyRT = (TextBox)gvDetails.FooterRow.FindControl("txtBuyRT");
                TextBox txtSaleRT = (TextBox)gvDetails.FooterRow.FindControl("txtSaleRT");
                TextBox txtPack = (TextBox)gvDetails.FooterRow.FindControl("txtPack");
                Decimal PQty;
                if(txtPack.Text=="")
                    PQty = 0;
                else
                    PQty = Convert.ToDecimal(txtPack.Text);
                TextBox txtMinsQty = (TextBox)gvDetails.FooterRow.FindControl("txtMinsQty");


                query = ("insert into STK_ITEM ( CATID, ITEMID, ITEMNM, BRAND, UNIT, BUYRT, SALERT, PQTY, MINSQTY, USERPC, USERID,IPADDRESS) " +
                         "values(@CatID,@ItemID,'" + txtItemNM.Text + "','" + txtBrand.Text + "','" + txtUnit.Text + "','" + txtBuyRT.Text + "','" + txtSaleRT.Text + "'," + PQty + ",'" + txtMinsQty.Text + "','',@USERID,'')");

                comm = new SqlCommand(query, conn);
                comm.Parameters.AddWithValue("@CatID", CatID);
                comm.Parameters.AddWithValue("@ItemID", ItemID);
                comm.Parameters.AddWithValue("@USERID", userName);

                conn.Open();
                int result = comm.ExecuteNonQuery();
                conn.Close();
                BindEmployeeDetails();
            }
        }

        protected void gvDetails_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvDetails.EditIndex = e.NewEditIndex;
            BindEmployeeDetails();
        }

        protected void gvDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            string userName = HttpContext.Current.Session["UserName"].ToString();

            Label lblCatGID = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblCatGID");
            Label lblItemID = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblItemID");
            TextBox txtItemNMEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtItemNMEdit");
            TextBox txtBrandEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtBrandEdit");
            TextBox txtUnitEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtUnitEdit");
            TextBox txtBuyRTEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtBuyRTEdit");
            TextBox txtSaleRTEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtSaleRTEdit");
            TextBox txtPackEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtPackEdit");
            Decimal PQty;
                if(txtPackEdit.Text=="")
                    PQty = 0;
                else
                    PQty = Convert.ToDecimal(txtPackEdit.Text);
            TextBox txtMinsQtyEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtMinsQtyEdit");

            conn.Open();
            SqlCommand cmd = new SqlCommand("update STK_ITEM set ITEMNM='" + txtItemNMEdit.Text + "', BRAND='" + txtBrandEdit.Text + "', UNIT='" + txtUnitEdit.Text + "', BUYRT = '" + txtBuyRTEdit.Text + "', SALERT = '" + txtSaleRTEdit.Text + "',PQTY=" + PQty + ", MINSQTY = '" + txtMinsQtyEdit.Text + "', USERID = '" + userName + "' where CATID = '" + lblCatGID.Text + "' and ITEMID = '" + lblItemID.Text + "'", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            gvDetails.EditIndex = -1;
            BindEmployeeDetails();
        }

        protected void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            Label lblCatGID = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblCatGID");
            Label lblItemID = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblItemID");

            Global.lblAdd(@"select ITEMID from STK_TRANS where ITEMID = '" + lblItemID.Text + "'", lblChkItemID);
            
            int result = 0;

            if (lblChkItemID.Text == "")
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("delete FROM STK_ITEM where CATID = '" + lblCatGID.Text + "' and ITEMID = '" + lblItemID.Text + "'", conn);
                result = cmd.ExecuteNonQuery();
                conn.Close();
            }

            else
            {
                Response.Write("<script>alert('This Item has a Transaction.');</script>");
            }

            if (result == 1)
            {
              BindEmployeeDetails();
            }
        }

        protected void gvDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvDetails.EditIndex = -1;
            BindEmployeeDetails();
        }

        protected void txtItemNM_TextChanged(object sender, EventArgs e)
        {
            
        }

        protected void txtBrandEdit_TextChanged(object sender, EventArgs e)
        {

        }

    }
}