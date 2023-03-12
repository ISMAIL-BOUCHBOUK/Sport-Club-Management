using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;

namespace برنامج_تسيير_قاعة_الرياضة.PL.PAGES.PAGE1
{
    public partial class details_trainee : DevExpress.XtraEditors.XtraForm
    {
        Form Previous;
        int index;
        SqlParameter[] para;
        SqlDataReader dr;
        DAL.DAL DAL = new DAL.DAL();
        string month;
        PL.PAGES.PAGE1.apprenti_abs_record abs;
        XlsxExportOptions excel = new XlsxExportOptions(new TextExportMode(), true, true, true);

        public details_trainee(int index , Form Previous)
        {
            InitializeComponent();

            if (user.acce.Contains("-17-"))
            {
                save.Visible = true;
            }

            this.Previous = Previous;
            this.index = index;

            get_data();
        }

        private void details_trainee_FormClosing(object sender, FormClosingEventArgs e)
        {
            Previous.Visible = true;
        }
        
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

        public void get_data()
        {
            para = new SqlParameter[1];
            para[0] = new SqlParameter("id", index);
            DAL.Open();
            dr = DAL.Read("AP_affichage det", para);
            dr.Read();
            label23.Text = dr.GetString(0);
            label22.Text = dr.GetString(1);
            label21.Text = dr.GetDateTime(2).ToShortDateString();
            label20.Text = dr.GetString(3);
            label14.Text = dr.GetInt32(4).ToString();
            label28.Text = mois(dr.GetDateTime(5).Month);
            label19.Text = dr.GetDateTime(5).Year.ToString();
            label18.Text = dr.GetString(6);
            label17.Text = dr.GetString(7);
            label16.Text = dr.GetString(8);
            label29.Text = dr.GetInt32(9).ToString();
            label27.Text = dr.GetString(10);
            label26.Text = dr.GetString(11);
            label25.Text = dr.GetString(12);
            label24.Text = dr.GetString(13);
            DAL.Close();
        }

        private void save_MouseEnter(object sender, EventArgs e)
        {
            save_mess.Visible = true;
            save.Location = new Point(save.Location.X -2, save.Location.Y -2);
            save.Size = new Size(save.Size.Width +4, save.Size.Height +4);
        }

        private void save_MouseLeave(object sender, EventArgs e)
        {
            save_mess.Visible = false;
            save.Location = new Point(save.Location.X + 2, save.Location.Y + 2);
            save.Size = new Size(save.Size.Width - 4, save.Size.Height - 4);
        }

        private void save_Click(object sender, EventArgs e)
        {
            try
            {
                new ReportPrintTool(new reports.apprenti_info(index)).ShowPreview();
            }
            catch
            {

            }
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            pictureBox1.BackColor = Color.FromArgb(220, 233, 252);
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.BackColor = Color.White;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pictureBox1.BackColor = Color.FromArgb(206, 221, 245);
            abs = new PL.PAGES.PAGE1.apprenti_abs_record(this, index, label22.Text + " " + label23.Text, 0);
            abs.Show();
            abs.get_data();
            this.Hide();
        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            pictureBox2.BackColor = Color.FromArgb(220, 233, 252);
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.BackColor = Color.White;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            pictureBox2.BackColor = Color.FromArgb(206, 221, 245);
            abs = new PL.PAGES.PAGE1.apprenti_abs_record(this, index,label22.Text + " "+label23.Text, 1);
            abs.Show();
            abs.get_data();
            this.Hide();
        }

        private void save_mess_Click(object sender, EventArgs e)
        {

        }
    }
}