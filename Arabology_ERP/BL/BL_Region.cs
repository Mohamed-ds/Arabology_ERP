using Arabology_ERP.DAL;
using MyLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Arabology_ERP.BL
{
    public class BL_Region : IGeneralProcessing
    {
        private Arabology_ERPEntities db = new Arabology_ERPEntities();
        public string txtId { get; set; }
        public string txtNameA { get; set; }
        public string txtNameE { get; set; }
        public bool chInActive { get; set; }
        public string txtFind { get; set; }
        public DataGridView dgv { get; set; }

        public int Add()
        {
            try
            {
                Region region = new Region()
                {
                    RegionNameA = txtNameA,
                    RegionNameE = txtNameE,
                    InActive = chInActive
                };
                db.Regions.Add(region);
                db.SaveChanges();
                _4Data.msgAlert("تمت الإضافة");
                return region.RegionId;
            }
            catch (Exception ex)
            {
                _4Exception.GetException(ex);
                return 0;
            }
        }

        public void DeclareControls(ref string id, ref string nameA, ref string nameE, ref bool inActive, ref string find, ref DataGridView grid)
        {
            try
            {
                txtId = id;
                txtNameA = nameA;
                txtNameE = nameE;
                chInActive = inActive;
                txtFind = find;
                dgv = grid;
                FillDGV();
                SetDGV();
            }
            catch (Exception ex)
            {
                _4Exception.GetException(ex);
                return;
            }
        }

        public void Delete()
        {
            try
            {
                if (txtId != "")
                {
                    int id = int.Parse(txtId);
                    Region region = db.Regions.Find(id);
                    if (_4Data.msgQuestion("سوف يتم حذف المنطقة هل انت متأكد أنك تريد الحذف؟") == System.Windows.Forms.DialogResult.Yes)
                    {
                        db.Regions.Remove(region);
                        db.SaveChanges();
                        _4Data.msgAlert("تم الحذف بنجاح");
                    }
                }
            }
            catch (Exception ex)
            {
                _4Exception.GetException(ex);
                return;
            }
        }

        public void FillDGV()
        {
            dgv.DataSource = db.Regions.ToList();
        }

        public void Search(string findWay)
        {
            try
            {
                var regions = db.Regions.ToList();
                switch (findWay)
                {
                    case "Contains":
                        regions = db.Regions.Where(b => b.RegionNameA.Contains(txtFind) || b.RegionNameE.Contains(txtFind)).ToList();
                        dgv.DataSource = regions.ToList();
                        break;
                    case "StartsWith":
                        regions = db.Regions.Where(b => b.RegionNameA.StartsWith(txtFind) || b.RegionNameE.StartsWith(txtFind)).ToList();
                        dgv.DataSource = regions.ToList();
                        break;
                    case "Equals":
                        regions = db.Regions.Where(b => b.RegionNameA.Equals(txtFind) || b.RegionNameE.Equals(txtFind)).ToList();
                        dgv.DataSource = regions.ToList();
                        break;
                    default:
                        SetDGV();
                        break;
                }

            }
            catch (Exception ex)
            {
                _4Exception.GetException(ex);
                return;
            }
        }

        public void SetDGV()
        {
            dgv.Columns[0].Visible = false;
            dgv.Columns[1].HeaderText = "الاسم بالعربي";
            dgv.Columns[2].HeaderText = "الاسم بالانجليزي";
            dgv.Columns[3].HeaderText = "غير فعال";
        }

        public void Update()
        {
            try
            {
                if (txtId != "")
                {
                    int id = int.Parse(txtId);
                    Region region = db.Regions.Find(id);
                    if (_4Data.msgQuestion("سوف يتم تعديل المنطقة هل انت متأكد أنك تريد التعديل؟") == System.Windows.Forms.DialogResult.Yes)
                    {
                        region.RegionNameA = txtNameA;
                        region.RegionNameE = txtNameE;
                        region.InActive = chInActive;
                        db.SaveChanges();
                        _4Data.msgAlert("تم التعديل بنجاح");
                    }
                }
            }
            catch (Exception ex)
            {
                _4Exception.GetException(ex);
                return;
            }
        }
    }
}
