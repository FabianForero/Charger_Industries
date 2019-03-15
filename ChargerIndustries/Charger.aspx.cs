using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.IO;
using System.Text;

namespace ChargerIndustries
{
    public partial class Charger : System.Web.UI.Page
    {
        DataTable dt = new DataTable();

        public static SqlConnectionStringBuilder con
        {
            get
            {
                SqlConnectionStringBuilder con = new SqlConnectionStringBuilder();
                con.DataSource = "charger1.database.windows.net";
                con.UserID = "Fabian@charger1";
                con.Password = "Trabajo1";
                con.InitialCatalog = "charger_industries";
                return con;
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            FileUpload1.SaveAs(Server.MapPath("\\TempFile\\" + FileUpload1.FileName));
            string cs = "Data Source=" + Server.MapPath("\\TempFile\\" + FileUpload1.FileName) + ";";
            using (SQLiteConnection con = new SQLiteConnection(cs))
            {
                con.Open();
                string[] tables = { "Tool", "CalibrationResult" };
                foreach (string name in tables)
                {
                    DropDownList1.Items.Add(new System.Web.UI.WebControls.ListItem(name));
                }
                foreach (string value in tables)
                {
                    string stm = "SELECT * FROM " + value;
                    var da = new SQLiteDataAdapter(stm, con);
                    dt.Reset();
                    da.Fill(dt);
                    saveToAzure(value);
                    da.Dispose();
                }
                con.Close();
            }
            Server.MapPath("\\TempFile\\");
            string[] files = Directory.GetFiles(Server.MapPath("\\TempFile\\"));
            foreach (string file in files)
            {
                File.Delete(file);
            }
        }

        public void saveToAzure(string table)
        {
            using (SqlConnection connection = new SqlConnection(con.ConnectionString))
            {
                connection.Open();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("INSERT INTO " + table + " ");
                    sb.Append("VALUES('");
                    sb.Append(dt.Rows[i][0].ToString() + "'");
                    for (int j = 1; j < dt.Columns.Count; j++)
                    {
                        sb.Append(", '" + dt.Rows[i][j].ToString() + "'");
                    }
                    sb.Append(")");
                    String sql = sb.ToString();
                    SqlCommand cmd = new SqlCommand(sql, connection);
                    cmd.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public void table_selected(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM " + DropDownList1.SelectedValue;
            using (SqlConnection connection = new SqlConnection(con.ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(sql, connection);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                connection.Close();
                da.Dispose();
            }
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
    }
}