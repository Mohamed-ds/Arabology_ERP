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
    public class BL_Category : IGeneralProcessing
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
                Category category = new Category()
                {
                    CategoryNameA = txtNameA,
                    CategoryNameE = txtNameE,
                    InActive = chInActive
                };
                db.Categories.Add(category);
                db.SaveChanges();
                _4Data.msgAlert("تمت الإضافة");
                return category.CategoryId;
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
                    Category category = db.Categories.Find(id);
                    if (_4Data.msgQuestion("سوف يتم حذف القطاع هل انت متأكد أنك تريد الحذف؟") == System.Windows.Forms.DialogResult.Yes)
                    {
                        db.Categories.Remove(category);
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
            dgv.DataSource = db.Categories.ToList();
        }

        public void Search(string findWay)
        {
            try
            {
                var categories = db.Categories.ToList();
                switch (findWay)
                {
                    case "Contains":
                        categories = db.Categories.Where(b => b.CategoryNameA.Contains(txtFind) || b.CategoryNameE.Contains(txtFind)).ToList();
                        dgv.DataSource = categories.ToList();
                        break;
                    case "StartsWith":
                        categories = db.Categories.Where(b => b.CategoryNameA.StartsWith(txtFind) || b.CategoryNameE.StartsWith(txtFind)).ToList();
                        dgv.DataSource = categories.ToList();
                        break;
                    case "Equals":
                        categories = db.Categories.Where(b => b.CategoryNameA.Equals(txtFind) || b.CategoryNameE.Equals(txtFind)).ToList();
                        dgv.DataSource = categories.ToList();
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
                    Category category = db.Categories.Find(id);
                    if (_4Data.msgQuestion("سوف يتم تعديل القطاع هل انت متأكد أنك تريد التعديل؟") == System.Windows.Forms.DialogResult.Yes)
                    {
                        category.CategoryNameA = txtNameA;
                        category.CategoryNameE = txtNameE;
                        category.InActive = chInActive;
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
