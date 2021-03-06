using System.Windows.Forms;
using System.Data.Linq;
using System.Linq;
using System.Data;

namespace QuanLyThuVien
{
    partial class QLTVDataContext
    {
       
    }

    partial class tbSach
    {
        QLTVDataContext db = new QLTVDataContext();
        public void CBXLoadTheoMaDS(ComboBox cbx, string s)
        {
            cbx.Items.Clear();
            var query = (from tbs in db.tbSaches
                         where tbs.MaDauSach == s
                         select tbs.MaSach);
            foreach (var item in query)
            {
                cbx.Items.Add(item.Trim());
            }
        }
    }

    public partial class tbTG
    {
        QLTVDataContext db = new QLTVDataContext();
        // LoadComboBox
        public void LoadComBoBox(ComboBox cbx)
        {
            cbx.Items.Clear();
            var tacgia = from tg in db.tbTGs
                         select tg;
            foreach (var item in tacgia)
            {
                cbx.Items.Add(item.TenTG.Trim());
            }               
        }

        public string GetMaTG(string s)
        {
           var result=from nxb in db.tbTGs
                    where nxb.TenTG == s
                    select nxb.MaTG;
           string i = "";
           foreach (var item in result)
           {
               i = item.ToString().Trim();
           }
           return i;
        }

        // LoadGridView

        
    }

    public partial class tbNXB
    {
        QLTVDataContext db = new QLTVDataContext();
        public void LoadCBNXB(ComboBox cbx)
        {
            cbx.Items.Clear();
            var nxb = (from n in db.tbNXBs select n);
            foreach (var item in nxb)
            {
                cbx.Items.Add(item.TenNXB.Trim());
            }
        }

        public string GetMaNXB(string s)
        {
         
            var result=   (from n in db.tbNXBs
                                      where n.TenNXB == s
                                  select n.MaNXB);
            string i = "";
            foreach (string item in result)
            {
                i = item;
            }
            return i;
            
        }
        // Return String
        public string GetMaNXB2(string s)
        {
            return (from n in db.tbNXBs
                          where n.TenNXB == s
                          select n.MaNXB).SingleOrDefault<System.String>();
        }
    }

    public partial class tbDauSach
    {
        QLTVDataContext db = new QLTVDataContext();

        public void LoadComBoBox(ComboBox cbx, ComboBox cbx2, int i)
        {
//
            cbx2.Items.Clear();
            var query = (from tbds in db.tbDauSaches
                         select tbds);
            var query2 = (from tbds in db.tbDauSaches
                          where tbds.TenSach == cbx.Text
                          group tbds by tbds.ChuyenNganh into gb
                          select new { Name = gb.Key });
            var query3 = (from tbds in db.tbDauSaches
                          where tbds.TenSach == cbx.Text
                          select tbds.MaDauSach);

            var query4 = (from tbds in db.tbDauSaches
                          group tbds by tbds.ChuyenNganh into gb
                          select new { Name = gb.Key });
            if (i == 1)
            {
                foreach (var item in query)
                {
                    cbx.Items.Add(item.TenSach);
                }
            }
            if (i == 2)
            {
                foreach (var item in query3)
                {
                    cbx2.Items.Add(item.Trim());
                }
            }
            if (i == 3)
            {
                foreach (var item in query2)
                {
                    cbx2.Items.Add(item.Name.Trim());
                }
            }
            if (i==4)
            {
                foreach (var item   in query4)
                {
                    cbx.Items.Add(item.Name.Trim());
                }
            }
        }

           
        public void LoadGridView(DataGridView gv)
        {
            IQueryable query = from tbds in db.tbDauSaches
                               join tbtg in db.tbTGs on tbds.MaTG equals tbtg.MaTG
                               join tbnxb in db.tbNXBs on tbds.MaNXB equals tbnxb.MaNXB
                               select new
                               {
                                   tbds.MaDauSach,
                                   tbds.TenSach,
                                   tbnxb.TenNXB,
                                   tbtg.TenTG,
                                   tbds.NamXuatBan,
                                   tbds.ChuyenNganh,
                                   tbds.SoLuong,
                                   tbds.GhiChu
                               };
            gv.DataSource = query;
        }

        public int DemSach()
        {
            return (from ds in db.tbDauSaches select ds).Count();
        }
    }

    public partial class tbThe
    {
        QLTVDataContext db = new QLTVDataContext();
        public void LoadCBX(ComboBox cbx)
        {
            cbx.Items.Clear();
            var query = (from tb in db.tbThes
                         select tb);
            foreach (var item in query)
            {
                cbx.Items.Add(item.MaThe);
            }
        }

        public void LoadGridView(DataGridView gv)
        {
            var query = (from tbTHE in db.tbThes
                         join tbDOCGIA in db.tbDGs
                         on tbTHE.MaDG equals tbDOCGIA.MaDG
                         select new
                         {
                             tbDOCGIA.MaDG,
                             tbDOCGIA.TenDG,
                             tbDOCGIA.ChucVu,
                             tbTHE.MaThe,
                             tbTHE.NgayCap,
                             tbTHE.Han
                         });
            gv.DataSource = query;
        }
    }

    public partial class tbDG
    {
        QLTVDataContext db = new QLTVDataContext();
        public string GetTenDG(string s)
        {
            return (from tb in db.tbDGs 
                    join tbthe in db.tbThes on tb.MaDG equals tbthe.MaDG
                        where tbthe.MaThe==s
                        select tb.TenDG).SingleOrDefault<System.String>();
        }

        public string GetTenDG2(string s)
        {
            return (from tb in db.tbDGs
                    where tb.MaDG == s
                    select tb.TenDG).SingleOrDefault<System.String>();
        }

        public void LoadComBoBox(ComboBox cbx, int i)
        {
            cbx.Items.Clear();
            var query = (from tb in db.tbDGs
                         group tb by tb.ChucVu into gb
                         select gb.Key);
            var query2 = (from tb in db.tbDGs
                          select tb.MaDG);

            if (i == 0)
            {
                foreach (var item in query)
                {
                    cbx.Items.Add(item.Trim());
                }
            }
            if (i==1)
            {
                foreach (var item in query2)
                {
                    cbx.Items.Add(item.Trim());
                }
            }
        }

        public void LoadComBoBoxTheoMa(ComboBox cbx, string s,int i)
        {
            cbx.Items.Clear();
            var query = (from tb in db.tbDGs
                         where tb.MaDG==s
                             select tb.ChucVu);
            if (i==0)
            {
                foreach (var item in query)
                {
                    cbx.Items.Add(item.Trim());
                }
            }
        }
        public void LoadGridView(DataGridView gv)
        {
            var query = (from tb in db.tbDGs
                         select tb);
            gv.DataSource = query;
        }
    }

    public partial class tbUser
    {
        QLTVDataContext db = new QLTVDataContext();
        public void LoadGridView(DataGridView gv)
        {
            var query = (from tb in db.tbUsers
                         select tb);
            gv.DataSource = query;
        }
    }
}
