using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace برنامج_تسيير_قاعة_الرياضة.PL.PAGES.PAGE1
{
    public partial class page1 : DevExpress.XtraEditors.XtraUserControl
    {
        DAL.DAL DAL = new DAL.DAL();
        DataTable dt = new DataTable();
        SqlParameter[] para;
        Form Previous;
        XlsxExportOptions excel = new XlsxExportOptions(new TextExportMode(), true, true, true);

        public page1(Form Previous)
        {
            InitializeComponent();
            show_data();
            this.Previous = Previous;

            if (user.acce.Contains("-2-"))
            {
                panel1.Visible = true;
                simpleButton2.Enabled = true;
            }
            if (user.acce.Contains("-3-"))
            {
                panel1.Visible = true;
                simpleButton3.Enabled = true;
                simpleButton5.Enabled = true;
            }
            if (user.acce.Contains("-4-"))
            {
                panel1.Visible = true;
                simpleButton1.Enabled = true;
            }
            if (user.acce.Contains("-5-"))
            {
                panel1.Visible = true;
                simpleButton4.Enabled = true;
            }
            if (user.acce.Contains("-17-"))
            {
                save.Visible = true;
            }
        }

        void show_data()
        {
            para = new SqlParameter[3];
            para[0] = new SqlParameter("nom", textBox1.Text);
            para[1] = new SqlParameter("ceinture", imageComboBoxEdit1.Text);
            para[2] = new SqlParameter("group", comboBoxEdit1.Text);
            DAL.Open();
            dataGridView1.DataSource = DAL.Read_data("AP_affichage act", para);
            DAL.Close();
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Dubai", 14, FontStyle.Bold);
            dataGridView1.DefaultCellStyle.Font = new Font("Dubai", 13, FontStyle.Regular);
        }

        private void imageComboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            show_data();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            new PL.PAGES.PAGE1.out_of_training(Previous).Show();
            Previous.Visible = false;
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count != 0)
            {
                new PL.PAGES.PAGE1.add_edit_trainee(Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value), Previous).Show();
                Previous.Visible = false;
            }
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            new PL.PAGES.PAGE1.add_edit_trainee(-1, Previous).Show();
            Previous.Visible = false;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count != 0)
            {
                new PL.PAGES.PAGE1.details_trainee(Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value), Previous).Show();
                Previous.Visible = false;
            }

        }

        private void page1_VisibleChanged(object sender, EventArgs e)
        {
            show_data();
        }

        private void save_MouseEnter(object sender, EventArgs e)
        {
            save_mess.Visible = true;
            save.Location = new Point(save.Location.X - 2, save.Location.Y - 2);
            save.Size = new Size(save.Size.Width + 4, save.Size.Height + 4);
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
                new ReportPrintTool(new reports.affichage_act()).ShowPreview();
            }
            catch
            {

            }

        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            new PL.PAGES.PAGE1.register(Previous).Show();
            Previous.Visible = false;
        }

        private void save2_Click(object sender, EventArgs e)
        {
            try
            {
                reports.apprenti_info report1 = new reports.apprenti_info(-1);
                report1.CreateDocument(); ;

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    reports.apprenti_info report2 = new reports.apprenti_info(Convert.ToInt32(row.Cells[0].Value));
                    report2.CreateDocument();
                    report1.Pages.AddRange(report2.Pages);
                }
                report1.Pages.RemoveAt(0);
                report1.PrintingSystem.ContinuousPageNumbering = true;
                new ReportPrintTool(report1).ShowPreview();
            }
            catch
            {

            }
        }

        private void save2_MouseEnter(object sender, EventArgs e)
        {
            save_mess2.Visible = true;
            save2.Location = new Point(save2.Location.X - 2, save2.Location.Y - 2);
            save2.Size = new Size(save2.Size.Width + 4, save2.Size.Height + 4);
        }

        private void save2_MouseLeave(object sender, EventArgs e)
        {
            save_mess2.Visible = false;
            save2.Location = new Point(save2.Location.X + 2, save2.Location.Y + 2);
            save2.Size = new Size(save2.Size.Width - 4, save2.Size.Height - 4);
        }
    }
}
