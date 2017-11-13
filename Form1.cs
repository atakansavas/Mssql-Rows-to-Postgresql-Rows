using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace sqlToPGSQL
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            var cString = "#";
            var tableName = textBox1.Text;

            using (var conn = new SqlConnection(cString))
            using (var cmd = new SqlCommand())
            { 
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = "select * from " + tableName;
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                    {
                        var vl = "INSERT INTO \"public\".\"prm_" + tableName + "\" (";
                        var columns = "";
                        var values = "";
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            var clName = reader.GetName(i);
                            var clResult = "";
                            if (clName == "Id")
                                clResult = "gid";
                            else if (clName == "InsertDate" || clName == "InsertDateTime")
                                clResult = "created";
                            else if (clName == "LastUpdated" || clName == "UpdateDateTime")
                                clResult = "modified";
                            else if (clName == "InsertUserId" || clName == "InsertUser")
                                clResult = "created_by";
                            else if (clName == "UpdateUserId")
                                clResult = "modified_by";
                            else
                                clResult = clName;

                            columns += "\"" + clResult + "\",";

                            var clType = reader.GetDataTypeName(i);
                            var vlType = "";
                            if (string.IsNullOrEmpty(reader.GetValue(i).ToString().Trim()))
                            {
                                vlType = "null";
                            }
                            else
                            {
                                if (clType.Contains("varchar") || clType == "datetime")
                                    vlType = "'" + reader.GetValue(i).ToString().Replace("'", "''") + "'";
                                else if (clType == "bit")
                                    vlType = ((bool)reader.GetValue(i) ? "'1'" : "'0'");
                                else if (clType == "decimal")
                                    vlType = "'" + reader.GetValue(i).ToString().Replace(",", ".") + "'";
                                else
                                    vlType = reader.GetValue(i).ToString();
                            }

                            values += vlType + ",";
                        }

                        vl += columns.TrimEnd(',') + ") VALUES (" + values.TrimEnd(',') + ");";
                        richTextBox1.AppendText(Environment.NewLine + "" + vl);
                    }
            }
        }
    }
}