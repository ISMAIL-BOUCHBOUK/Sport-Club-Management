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
    public partial class out_of_training : DevExpress.XtraEditors.XtraForm
    {
        Form Previous;
        DAL.DAL DAL = new DAL.DAL();
        DataTable dt = new DataTable();
        SqlParameter[] para;
        XlsxExportOptions excel = new XlsxExportOptions(new TextExportMode(), true, true, true);

        public out_of_training(Form Previous)
        {
            InitializeComponent();

            this.Previous = Previous;

            show_data();

            if (user.acce.Contains("-2-"))
            {
                panel1.Visible = true;
                simpleButton5.Enabled = true;
            }
            if (user.acce.Contains("-3-"))
            {
                panel1.Visible = true;
                simpleButton3.Enabled = true;
            }
            if (user.acce.Contains("-5-"))
            {
                panel1.Visible = true;
                simpleButton1.Enabled = true;
            }
            if (user.acce.Contains("-17-"))
            {
                save.Visible = true;
            }
        }
        void show_data()
        {
            DAL.Open();
            para = new SqlParameter[3];
            para[0] = new SqlParameter("nom", textBox1.Text);
            para[1] = new SqlParameter("ceinture", imageComboBoxEdit1.Text);
            para[2] = new SqlParameter("group", comboBoxEdit1.Text);
            dt = DAL.Read_data("AP_affichage pas", para);
            dataGridView1.DataSource = dt;
            DAL.Close();
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Dubai", 14, FontStyle.Bold);
            dataGridView1.DefaultCellStyle.Font = new Font("Dubai", 13, FontStyle.Regular);
            color();
        }

        void color()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[6].Value.ToString().Contains("r"))
                {
                    row.Cells[5].Style.BackColor = Color.Pink;
                    row.Cells[5].Style.SelectionBackColor = Color.DeepPink;
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            show_data();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count != 0)
            {
                new PL.PAGES.PAGE1.add_edit_trainee(Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value), this).Show();
                this.Visible = false;
            }
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count != 0)
            {
                new PL.PAGES.PAGE1.details_trainee(Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value), this).Show();
                this.Visible = false;
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            new PL.PAGES.PAGE1.register(this).Show();
            this.Visible = false;
        }

        private void out_of_training_FormClosing(object sender, FormClosingEventArgs e)
        {
            Previous.Visible = true;
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count != 0)
            {
                this.Hide();
                new PAGES.MESSAGES.confirmation(this, Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value), "AP_supression").Show();
            }
        }

        private void out_of_training_VisibleChanged(object sender, EventArgs e)
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
                new ReportPrintTool(new reports.affichage_pass()).ShowPreview();
            }
            catch
            {

            }
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

        private void dataGridView1_Sorted(object sender, EventArgs e)
        {
            color();
        }
    }
}