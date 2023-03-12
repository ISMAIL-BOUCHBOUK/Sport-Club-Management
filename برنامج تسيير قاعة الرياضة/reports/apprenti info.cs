using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data.SqlClient;
using System.Data;

namespace برنامج_تسيير_قاعة_الرياضة.reports
{
    public partial class apprenti_info : DevExpress.XtraReports.UI.XtraReport
    {
        SqlParameter[] para;
        SqlDataReader dr;
        DAL.DAL DAL = new DAL.DAL();
        string month;

        private string mois(int mois)
        {
            switch (mois)
            {
                case 1:
                    month = "يناير";
                    break;
                case 2:
                    month = "فبراير";
                    break;
                case 3:
                    month = "مارس";
                    break;
                case 4:
                    month = "أبريل";
                    break;
                case 5:
                    month = "ماي";
                    break;
                case 6:
                    month = "يونيو";
                    break;
                case 7:
                    month = "يوليوز";
                    break;
                case 8:
                    month = "غشت";
                    break;
                case 9:
                    month = "شتنبر";
                    break;
                case 10:
                    month = "أكتوبر";
                    break;
                case 11:
                    month = "نونبر";
                    break;
                case 12:
                    month = "دجنبر";
                    break;
                default:
                    month = "";
                    break;
            }
            return month;
        }

        public apprenti_info(int index)
        {
            InitializeComponent();

            para = new SqlParameter[1];
            para[0] = new SqlParameter("id", index);
            DAL.Open();
            dr = DAL.Read("AP_affichage det", para);
            if (dr.Read())
            {
                xrLabel17.Text = dr.GetString(0);
                xrLabel18.Text = dr.GetString(1);
                xrLabel19.Text = dr.GetDateTime(2).ToShortDateString();
                xrLabel20.Text = dr.GetString(3);
                xrLabel21.Text = dr.GetInt32(4).ToString();
                xrLabel22.Text = mois(dr.GetDateTime(5).Month) + " " + dr.GetDateTime(5).Year.ToString();
                xrLabel23.Text = dr.GetString(6);
                xrLabel24.Text = dr.GetString(7);
                xrLabel25.Text = dr.GetString(8) + " 212+";
                xrLabel26.Text = dr.GetInt32(9).ToString();
                xrLabel27.Text = dr.GetString(10);
                xrLabel28.Text = dr.GetString(11);
                xrLabel29.Text = dr.GetString(12);
                xrLabel30.Text = dr.GetString(13) + " 212+";
            }
            
            DAL.Close();
        }


    }
}
