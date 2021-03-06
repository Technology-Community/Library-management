using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.Linq;
using System.Linq;
using DevComponents.DotNetBar;
using System.Threading;
using System.IO;
using DevComponents.DotNetBar.Rendering;
using QuanLyThuVien.Properties;
namespace QuanLyThuVien
{
    public partial class frmMain_1 : DevComponents.DotNetBar.Office2007RibbonForm
    {
        public frmMain_1()
        {
            InitializeComponent();
        }

        public void GetValue(string vl1)
        {
            buttonItem3.Text =vl1.Trim();

        }
        #region "Delegate cho form thông tin"
        public delegate void PassData(string value);
            
        #endregion
        private void buttonItem13_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ribbonControl1_Click(object sender, EventArgs e)
        {

        }
        
        private void frmMain_1_Load(object sender, EventArgs e)
        {
            frmDangNhap frm = new frmDangNhap();
            frm.Close();

        }

        private void bar1_ItemClick(object sender, EventArgs e)
        {

        }

        private void btniDX_Click(object sender, EventArgs e)
        {
            this.Hide();
            
            Thread thread = new Thread(new ThreadStart(openForm));
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            this.Close();
            // Application.Exit();
            
        }

        void openForm()
        {
            Application.Run(new frmDangNhap());
        }

        private bool m_ColorSelected = false;
        private eOffice2007ColorScheme m_BaseColorScheme = eOffice2007ColorScheme.Blue;



        private void colorPickerDropDown1_ExpandChange(object sender, EventArgs e)
        {
            if (colorPickerDropDown1.Expanded)
            {
                // Remember the starting color scheme to apply if no color is selected during live-preview
                m_ColorSelected = false;
                m_BaseColorScheme = ((Office2007Renderer)GlobalManager.Renderer).ColorTable.InitialColorScheme;
            }
            else
            {
                if (!m_ColorSelected)
                {   
                    RibbonPredefinedColorSchemes.ChangeOffice2007ColorTable(this, m_BaseColorScheme);
                }
            }
        }

        private void colorPickerDropDown1_SelectedColorChanged(object sender, EventArgs e)
        {

            m_ColorSelected = true; // Indicate that color was selected for buttonStyleCustom_ExpandChange method
            RibbonPredefinedColorSchemes.ChangeOffice2007ColorTable(this, m_BaseColorScheme, colorPickerDropDown1.SelectedColor);

        }

        private void colorPickerDropDown1_ColorPreview(object sender, ColorPreviewEventArgs e)
        {
            RibbonPredefinedColorSchemes.ChangeOffice2007ColorTable(this, m_BaseColorScheme, e.Color);
        }

        private void buttonItem11_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonItem10_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void buttonItem9_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
        }



        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ribbonControl1_Click_1(object sender, EventArgs e)
        {
            
        }

        private void buttonItem15_Click(object sender, EventArgs e)
        {
            frmThongTinTG frm = new frmThongTinTG();
           
            frm.Show();
        }

        private void buttonItem12_Click(object sender, EventArgs e)
        {
            frmThongTinTG frm = new frmThongTinTG();
            frm.Show();
        }

        private void phông1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackgroundImage = Resources.Trinity_college_library_dub;
        }

        private void phông2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackgroundImage = Resources.cathedral_library_kalocsa;
        }

        private void phông3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackgroundImage = Resources.local_library_tip_lg;
        }

        private void phông4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackgroundImage = Resources.library;
        }

        private void phông5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackgroundImage = Resources.library__1_;
        }

        private void buttonItem3_Click(object sender, EventArgs e)
        {

        }

        private void btnIThongTin_Click(object sender, EventArgs e)
        {
            frmThongTin frm = new frmThongTin();
            PassData pass = new PassData(frm.GetValue);
            pass(this.buttonItem3.Text);
            frm.Show();
        }

        private void buttonItem14_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonItem16_Click(object sender, EventArgs e)
        {
            frmSach frm = new frmSach();
            frm.Show();
        }

        private void buttonItem17_Click(object sender, EventArgs e)
        {
            frmQLDauSach frm = new frmQLDauSach();
            frm.Show();
        }

        private void buttonItem18_Click(object sender, EventArgs e)
        {
            frmQlDocGia frm = new frmQlDocGia();
            frm.Show();
        }

        private void buttonItem19_Click(object sender, EventArgs e)
        {
            frmQLThe frm = new frmQLThe();
            frm.Show();
        }

        private void buttonItem20_Click(object sender, EventArgs e)
        {
            frmQlMuonSach frm = new frmQlMuonSach();
            frm.Show();
        }

        private void buttonItem21_Click(object sender, EventArgs e)
        {
            frmQLTraSach frm = new frmQLTraSach();
            frm.Show();
        }

        private void buttonItem7_Click(object sender, EventArgs e)
        {
            frmQLUser frm = new frmQLUser();
            frm.Show();
        }

        QLTVDataContext db = new QLTVDataContext();
        private void buttonItem2_Click(object sender, EventArgs e)
        {
            try
            {
                string folder = Directory.GetCurrentDirectory() + @"\BackUp";
                Directory.CreateDirectory(folder);
                if (!File.Exists(folder))
                {
                    File.Delete(Application.StartupPath + @"\BackUp\QLTV.bak");
                    int rowAffected = db.ExecuteCommand("BACKUP DATABASE " + db.Mapping.DatabaseName + " TO DISK ='" + Application.StartupPath + @"\BackUp\QLTV.bak'");
                    if (rowAffected != 0)
                    {
                        MessageBox.Show("Sao Lưu Thành Công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        db.SubmitChanges();
                    }
                    else
                        MessageBox.Show("Sao Lưu Thất Bại", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            catch (System.IO.IOException ex)
            {
                MessageBox.Show(ex.Message, "Quản Lý Thư Viện");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Quản Lý Thư Viện");
            }
          

        }

        private void buttonItem4_Click(object sender, EventArgs e)
        {

        }


   
    }
}