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
    public class BL_Sector : IGeneralProcessing
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
                Sector sector = new Sector()
                {
                    SectorNameA = txtNameA,
                    SectorNameE = txtNameE,
                    InActive = chInActive
                };
                db.Sectors.Add(sector);
                db.SaveChanges();
                _4Data.msgAlert("تمت الإضافة");
                return sector.SectorId;
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
                    Sector sector = db.Sectors.Find(id);
                    if (_4Data.msgQuestion("سوف يتم حذف القطاع هل انت متأكد أنك تريد الحذف؟") == System.Windows.Forms.DialogResult.Yes)
                    {
                        db.Sectors.Remove(sector);
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
            dgv.DataSource = db.Sectors.ToList();
        }

        public void Search(string findWay)
        {
            try
            {
                var sectors = db.Sectors.ToList();
                switch (findWay)
                {
                    case "Contains":
                        sectors = db.Sectors.Where(b => b.SectorNameA.Contains(txtFind) || b.SectorNameE.Contains(txtFind)).ToList();
                        dgv.DataSource = sectors.ToList();
                        break;
                    case "StartsWith":
                        sectors = db.Sectors.Where(b => b.SectorNameA.StartsWith(txtFind) || b.SectorNameE.StartsWith(txtFind)).ToList();
                        dgv.DataSource = sectors.ToList();
                        break;
                    case "Equals":
                        sectors = db.Sectors.Where(b => b.SectorNameA.Equals(txtFind) || b.SectorNameE.Equals(txtFind)).ToList();
                        dgv.DataSource = sectors.ToList();
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
                    Sector sector = db.Sectors.Find(id);
                    if (_4Data.msgQuestion("سوف يتم تعديل القطاع هل انت متأكد أنك تريد التعديل؟") == System.Windows.Forms.DialogResult.Yes)
                    {
                        sector.SectorNameA = txtNameA;
                        sector.SectorNameE = txtNameE;
                        sector.InActive = chInActive;
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
