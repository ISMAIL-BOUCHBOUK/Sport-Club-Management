using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;

namespace برنامج_تسيير_قاعة_الرياضة.reports
{
    public partial class dépenses : DevExpress.XtraReports.UI.XtraReport
    {
        int index , rowspan , row ;
        double sum = 0;
        DAL.DAL DAL1 = new DAL.DAL();
        DAL.DAL DAL2 = new DAL.DAL();
        SqlParameter[] para1;
        SqlParameter[] para2;
        SqlDataReader dr1;
        SqlDataReader dr2;


        public double summ()
        {
            return sum;
        }

        public dépenses(int anne, int mois1)
        {
            InitializeComponent();

            DAL1.Open();
            para1 = new SqlParameter[2];
            para1[0] = new SqlParameter("anne", anne);
            para1[1] = new SqlParameter("mois", mois1);
            dr1 = DAL1.Read("CHA_affichage", para1);

            if (dr1.Read())
            {
                index = dr1.GetInt32(0);
                table1.Rows[0].Cells[0].Text = dr1.GetDouble(2).ToString();
                table1.Rows[0].Cells[4].Text = dr1.GetString(3).ToString();
                sum = sum + dr1.GetDouble(2);

                DAL2.Open();
                para2 = new SqlParameter[1];
                para2[0] = new SqlParameter("id", index);
                dr2 = DAL2.Read("CHA_DET_affichage", para2);
                if (dr2.Read())
                {
                    rowspan = 1;
                    table1.Rows[0].Cells[1].Text = dr2.GetDouble(2).ToString();
                    table1.Rows[0].Cells[2].Text = dr2.GetInt32(1).ToString()+"/"+mois1.ToString()+"/"+anne.ToString();
                    table1.Rows[0].Cells[3].Text = dr2.GetString(3).ToString();
                }
                while (dr2.Read())
                {
                    rowspan++;
                    table1.InsertRowBelow(table1.Rows[table1.Rows.Count - 1]);
                    table1.Rows[table1.Rows.Count - 1].Cells[1].Text = dr2.GetDouble(2).ToString();
                    table1.Rows[table1.Rows.Count - 1].Cells[2].Text = dr2.GetInt32(1).ToString() + "/" + mois1.ToString() + "/" + anne.ToString();
                    table1.Rows[table1.Rows.Count - 1].Cells[3].Text = dr2.GetString(3).ToString();
                }
                table1.Rows[0].Cells[0].RowSpan = rowspan;
                table1.Rows[0].Cells[4].RowSpan = rowspan;
                dr2.Close();
                DAL2.Close();
            }
            else
            {
                table1.Rows.RemoveAt(0);
            }
            while (dr1.Read())
            {
                table1.InsertRowBelow(table1.Rows[table1.Rows.Count - 1]);
                row = table1.Rows.Count - 1;
                index = dr1.GetInt32(0);
                table1.Rows[row].Cells[0].Text = dr1.GetDouble(2).ToString();
                table1.Rows[row].Cells[4].Text = dr1.GetString(3).ToString();
                sum = sum + dr1.GetDouble(2);

                DAL2.Open();
                para2 = new SqlParameter[1];
                para2[0] = new SqlParameter("id", index);
                dr2 = DAL2.Read("CHA_DET_affichage", para2);
                if (dr2.Read())
                {
                    rowspan = 1;
                    table1.Rows[row].Cells[1].Text = dr2.GetDouble(2).ToString();
                    table1.Rows[row].Cells[2].Text = dr2.GetInt32(1).ToString() + "/" + mois1.ToString() + "/" + anne.ToString();
                    table1.Rows[row].Cells[3].Text = dr2.GetString(3).ToString();
                }
                while (dr2.Read())
                {
                    rowspan++;
                    table1.InsertRowBelow(table1.Rows[table1.Rows.Count - 1]);
                    table1.Rows[table1.Rows.Count - 1].Cells[1].Text = dr2.GetDouble(2).ToString();
                    table1.Rows[table1.Rows.Count - 1].Cells[2].Text = dr2.GetInt32(1).ToString() + "/" + mois1.ToString() + "/" + anne.ToString();
                    table1.Rows[table1.Rows.Count - 1].Cells[3].Text = dr2.GetString(3).ToString();
                }
                table1.Rows[row].Cells[0].RowSpan = rowspan;
                table1.Rows[row].Cells[4].RowSpan = rowspan;
                dr2.Close();
                DAL2.Close();
            }

            xrLabel2.Text = sum.ToString();
            dr1.Close();
            DAL1.Close();
        }

    }
}
