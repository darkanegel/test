using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SimpleSEOAnalyser
{
    public partial class SEO : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string _KeyText = TextBox1.Text;
                bool _isChecked = CheckBox1.Checked;
                Uri uriResult;
                if (Common.CheckURLValid(_KeyText, out uriResult))
                {
                    if (Common.RemoteFileExists(uriResult?.AbsoluteUri) == false)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Unhandled')", true);
                    }
                    else
                    {
                        if (CheckBox2.Checked)
                        {
                            DataTable dt = Common.GridGetSource(uriResult?.AbsoluteUri, _isChecked, Util.eOptions.GetAllWords);
                            GridView1.DataSource = dt;
                            GridView1.DataBind();
                            ViewState["dtGridView1"] = dt;
                            ViewState["sortGridView1"] = "ASC";
                        }
                        else
                        {
                            GridView1.DataSource = null;
                            GridView1.DataBind();
                        }

                        if (CheckBox3.Checked)
                        {
                            DataTable dt = Common.GridGetSource(uriResult?.AbsoluteUri, _isChecked, Util.eOptions.GetAllMetas);
                            GridView2.DataSource = dt;
                            GridView2.DataBind();
                            ViewState["dtGridView2"] = dt;
                            ViewState["sortGridView2"] = "ASC";
                        }
                        else
                        {
                            GridView2.DataSource = null;
                            GridView2.DataBind();
                        }

                        if (CheckBox3.Checked)
                        {
                            DataTable dt = Common.GridGetSource(uriResult?.AbsoluteUri, _isChecked, Util.eOptions.GetAllExternalLink);
                            GridView3.DataSource = dt;
                            GridView3.DataBind();
                            ViewState["dtGridView3"] = dt;
                            ViewState["sortGridView3"] = "ASC";
                        }
                        else
                        {
                            GridView3.DataSource = null;
                            GridView3.DataBind();
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter valid URL')", true);
                }
            }
        }

        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dataTable = (DataTable)ViewState["dtGridView1"];
            if (dataTable != null)
            {
                DataView _dataView = new DataView(dataTable);

                if (Convert.ToString(ViewState["sortGridView1"]) == "ASC")
                {
                    _dataView.Sort = e.SortExpression + " " + Common.ConvertSortDirectionToSql(SortDirection.Descending);
                    ViewState["sortGridView1"] = "DESC";
                }
                else
                {
                    _dataView.Sort = e.SortExpression + " " + Common.ConvertSortDirectionToSql(SortDirection.Ascending);
                    ViewState["sortGridView1"] = "ASC";
                }

                dataTable = _dataView.ToTable();
                GridView1.DataSource = dataTable;
                GridView1.DataBind();
            }
        }

        protected void GridView2_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dataTable = (DataTable)ViewState["dtGridView2"];
            if (dataTable != null)
            {
                DataView _dataView = new DataView(dataTable);

                if (Convert.ToString(ViewState["sortGridView2"]) == "ASC")
                {
                    _dataView.Sort = e.SortExpression + " " + Common.ConvertSortDirectionToSql(SortDirection.Descending);
                    ViewState["sortGridView2"] = "DESC";
                }
                else
                {
                    _dataView.Sort = e.SortExpression + " " + Common.ConvertSortDirectionToSql(SortDirection.Ascending);
                    ViewState["sortGridView2"] = "ASC";
                }

                dataTable = _dataView.ToTable();
                GridView2.DataSource = dataTable;
                GridView2.DataBind();
            }
        }

        protected void GridView3_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dataTable = (DataTable)ViewState["dtGridView3"];
            if (dataTable != null)
            {
                DataView _dataView = new DataView(dataTable);

                if (Convert.ToString(ViewState["sortGridView3"]) == "ASC")
                {
                    _dataView.Sort = e.SortExpression + " " + Common.ConvertSortDirectionToSql(SortDirection.Descending);
                    ViewState["sortGridView3"] = "DESC";
                }
                else
                {
                    _dataView.Sort = e.SortExpression + " " + Common.ConvertSortDirectionToSql(SortDirection.Ascending);
                    ViewState["sortGridView3"] = "ASC";
                }

                dataTable = _dataView.ToTable();
                GridView3.DataSource = dataTable;
                GridView3.DataBind();
            }
        }
    }
}