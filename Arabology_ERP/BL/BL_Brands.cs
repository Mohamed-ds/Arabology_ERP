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
    public class BL_Brands : IGeneralProcessing
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
                Brand brand = new Brand()
                {
                    BrandNameA = txtNameA,
                    BrandNameE = txtNameE,
                    InActive = chInActive
                };
                db.Brands.Add(brand);
                db.SaveChanges();
                _4Data.msgAlert("تمت الإضافة");
                return brand.BrandId;
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
                    Brand brand = db.Brands.Find(id);
                    if (_4Data.msgQuestion("سوف يتم حذف الشركة هل انت متأكد أنك تريد الحذف؟") == System.Windows.Forms.DialogResult.Yes)
                    {
                        db.Brands.Remove(brand);
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
            dgv.DataSource = db.Brands.ToList();
        }

        public void Search(string findWay)
        {
            try
            {
                var brands = db.Brands.ToList();
                switch (findWay)
                {
                    case "Contains":
                        brands = db.Brands.Where(b => b.BrandNameA.Contains(txtFind) || b.BrandNameE.Contains(txtFind)).ToList();
                        dgv.DataSource = brands.ToList();
                        break;
                    case "StartsWith":
                        brands = db.Brands.Where(b => b.BrandNameA.StartsWith(txtFind) || b.BrandNameE.StartsWith(txtFind)).ToList();
                        dgv.DataSource = brands.ToList();
                        break;
                    case "Equals":
                        brands = db.Brands.Where(b => b.BrandNameA.Equals(txtFind) || b.BrandNameE.Equals(txtFind)).ToList();
                        dgv.DataSource = brands.ToList();
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
                    Brand brand = db.Brands.Find(id);
                    if (_4Data.msgQuestion("سوف يتم تعديل الشركة هل انت متأكد أنك تريد التعديل؟") == System.Windows.Forms.DialogResult.Yes)
                    {
                        brand.BrandNameA = txtNameA;
                        brand.BrandNameE = txtNameE;
                        brand.InActive = chInActive;
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
