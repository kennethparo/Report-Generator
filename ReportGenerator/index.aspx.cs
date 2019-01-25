using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace ReportGenerator
{
    public partial class index : System.Web.UI.Page
    {
        string constring = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        string dropDownList1Value = null;
        string dropDownList2Value = null;
        SqlConnection con = null;
        SqlCommand cmd = null;
        SqlDataAdapter da = null;
        DataTable dt = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack)
            {

            }
        }

        private void TabMenuChange()
        {
            if (Session["tab"].ToString() == "inbound")
            {
                list1.Attributes["class"] = "active";
                list2.Attributes["class"] = "";
                list3.Attributes["class"] = "";
                inbound.Attributes["class"] = "tab-pane fade active in";
                midbound.Attributes["class"] = "tab-pane fade";
                outbound.Attributes["class"] = "tab-pane fade";
            }
            else if (Session["tab"].ToString() == "midbound")
            {
                list1.Attributes["class"] = "";
                list2.Attributes["class"] = "active";
                list3.Attributes["class"] = "";
                inbound.Attributes["class"] = "tab-pane fade";
                midbound.Attributes["class"] = "tab-pane fade active in";
                outbound.Attributes["class"] = "tab-pane fade";
            }
            else if (Session["tab"].ToString() == "outbound")
            {
                list1.Attributes["class"] = "";
                list2.Attributes["class"] = "";
                list3.Attributes["class"] = "active";
                inbound.Attributes.Add("class", "tab-pane fade");
                midbound.Attributes.Add("class", "tab-pane fade");
                outbound.Attributes.Add("class", "tab-pane fade active in");
            }
        }

        private DataTable retrieveData()
        {
            con = new SqlConnection(constring);
            con.Open();
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SearchBintoBinTransaction";
            cmd.Parameters.Add("@txtitemcode", SqlDbType.VarChar).Value = "%" + txtCode.Text + "%";
            cmd.Parameters.Add("@txtbinloc", SqlDbType.VarChar).Value = "%" + txtBinLoc.Text + "%";
            da = new SqlDataAdapter();
            dt = new DataTable();
            da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            con.Close();
            return dt;
        }

        private DataTable GoodReceipt()
        {
            con = new SqlConnection(constring);
            con.Open();
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "GR_PLAN";
            cmd.Parameters.Add("@searchkey", SqlDbType.VarChar).Value = "%" + TextBox5.Text + "%";
            da = new SqlDataAdapter();
            dt = new DataTable();
            da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            con.Close();
            return dt;
        }

        private DataTable ItemCode()
        {
            con = new SqlConnection(constring);
            con.Open();
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Item_Code";
            cmd.Parameters.Add("@searchkey", SqlDbType.VarChar).Value = "%" + TextBox6.Text + "%";
            da = new SqlDataAdapter();
            dt = new DataTable();
            da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            con.Close();
            return dt;
        }

        private DataTable CycleCount_DB()
        {
            con = new SqlConnection(constring);
            con.Open();
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SearchCycleCountSearch";
            cmd.Parameters.Add("@txtitemcode", SqlDbType.VarChar).Value = "%" + itemCodeTextBox.Text + "%";
            cmd.Parameters.Add("@txtbincode", SqlDbType.VarChar).Value = "%" + binCodeTextBox.Text + "%";
            cmd.Parameters.Add("@txtbinlevel", SqlDbType.VarChar).Value = "%" + binLevelTextBox.Text + "%";
            da = new SqlDataAdapter();
            dt = new DataTable();
            da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            con.Close();
            return dt;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gridviewContainer.DataSource = retrieveData();
            gridviewContainer.DataBind();
            if (gridviewContainer.Rows.Count > 0)
            {
                btnPrint.Visible = true;
                btnExportToExcel.Visible = true;
            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            dropDownList1Value = DropDownList1.SelectedItem.Value;

            if (dropDownList1Value == "Bin To Bin Transactions")
            {
                Session["PRINTNAAAA"] = retrieveData();
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "OpenWindow", "window.open('print_bintobintrancs.aspx','','toolbar=no');", true);
            }
            else if (dropDownList1Value == "Item List")
            {
                Session["PrintItemList"] = ItemCode();
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "OpenWindow", "window.open('ItemListForm.aspx','','toolbar=no');", true);
            }
            else if (dropDownList1Value == "GR Plan")
            {
                Session["PrintGRPLan"] = GoodReceipt();
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "OpenWindow", "window.open('GR_Plan.aspx','','toolbar=no');", true);
            }
        }

        protected void btnPrintCycleCount(object sender, EventArgs e)
        {
            Session["PrintCycleCount"] = CycleCount_DB();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "OpenWindow", "window.open('CycleCountForm.aspx','','toolbar=no');", true);
        }

        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            if (gridviewContainer.Rows.Count > 0)
            {
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=Report.xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);

                    gridviewContainer.HeaderRow.BackColor = Color.White;
                    foreach (TableCell cell in gridviewContainer.HeaderRow.Cells)
                    {
                        cell.BackColor = gridviewContainer.HeaderStyle.BackColor;
                    }
                    foreach (GridViewRow row in gridviewContainer.Rows)
                    {
                        row.BackColor = Color.White;
                        foreach (TableCell cell in row.Cells)
                        {
                            if (row.RowIndex % 2 == 0)
                            {
                                cell.BackColor = gridviewContainer.AlternatingRowStyle.BackColor;
                            }
                            else
                            {
                                cell.BackColor = gridviewContainer.RowStyle.BackColor;
                            }
                            cell.CssClass = "textmode";
                        }
                    }

                    gridviewContainer.RenderControl(hw);
                    //style to format numbers to string
                    string style = @"<style> .textmode { } </style>";
                    Response.Write(style);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected void ActionList_TextChanged(object sender, EventArgs e)
        {
            Session["tab"] = "inbound";
            TabMenuChange();
            dropDownList1Value = DropDownList1.SelectedItem.Value;

            if (dropDownList1Value == "GR Plan")
            {
                GRPlan.Style.Add("display", "block");
                BinToBin.Style.Add("display", "none");
                ItemList.Style.Add("display", "none");
                CycleCount.Style.Add("display", "none");
            }
            else if (dropDownList1Value == "Item List")
            {
                ItemList.Style.Add("display", "block");
                BinToBin.Style.Add("display", "none");
                GRPlan.Style.Add("display", "none");
                CycleCount.Style.Add("display", "none");
            }
        }

        protected void ActionList2_TextChanged(object sender, EventArgs e)
        {
            Session["tab"] = "midbound";
            TabMenuChange();
            dropDownList2Value = DropDownList2.SelectedItem.Value;

            if (dropDownList2Value == "Bin To Bin Transactions")
            {
                BinToBin.Style.Add("display", "block");
                GRPlan.Style.Add("display", "none");
                ItemList.Style.Add("display", "none");
                CycleCount.Style.Add("display", "none");
            }
            else if (dropDownList2Value == "Cycle Count Sheet")
            {
                BinToBin.Style.Add("display", "none");
                GRPlan.Style.Add("display", "none");
                ItemList.Style.Add("display", "none");
                CycleCount.Style.Add("display", "block");
            }
            else if (dropDownList2Value == "Replenishment")
            {
                //ItemList.Style.Add("display", "block");
                //BinToBin.Style.Add("display", "none");
                //GRPlan.Style.Add("display", "none");
            }
        }

        protected void ActionList3_TextChanged(object sender, EventArgs e)
        {
            Session["tab"] = "outbound";
            TabMenuChange();
            if (DropDownList3.SelectedItem.Value == "Bin To Bin Transactions")
            {
                //BinToBin.Style.Add("display", "block");
                //GRPlan.Style.Add("display", "none");
                //ItemList.Style.Add("display", "none");
            }
            else if (DropDownList3.SelectedItem.Value == "Bin To Bin Plan")
            {
                //GRPlan.Style.Add("display", "block");
                //BinToBin.Style.Add("display", "none");
                //ItemList.Style.Add("display", "none");
            }
            else if (DropDownList3.SelectedItem.Value == "Item List")
            {
                //ItemList.Style.Add("display", "block");
                //BinToBin.Style.Add("display", "none");
                //GRPlan.Style.Add("display", "none");
            }
        }

        protected void test1_Click(object sender, EventArgs e)
        {
            Session["tab"] = "inbound";
            TabMenuChange();
            BinToBin.Style.Add("display", "none");
            GRPlan.Style.Add("display", "block");
            ItemList.Style.Add("display", "none");
            CycleCount.Style.Add("display", "none");
        }

        protected void test2_Click(object sender, EventArgs e)
        {
            Session["tab"] = "midbound";
            TabMenuChange();
            BinToBin.Style.Add("display", "block");
            GRPlan.Style.Add("display", "none");
            ItemList.Style.Add("display", "none");
            CycleCount.Style.Add("display", "none");
        }

        protected void test3_Click(object sender, EventArgs e)
        {
            Session["tab"] = "outbound";
            TabMenuChange();
        }
    }
}