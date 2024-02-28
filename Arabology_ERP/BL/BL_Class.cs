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
    public class BL_Class : IGeneralProcessing
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
                Class clss = new Class()
                {
                    ClassNameA = txtNameA,
                    ClassNameE = txtNameE,
                    InActive = chInActive
                };
                db.Classes.Add(clss);
                db.SaveChanges();
                _4Data.msgAlert("تمت الإضافة");
                return clss.ClassId;
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
                    Class clss = db.Classes.Find(id);
                    if (_4Data.msgQuestion("سوف يتم حذف الفئة هل انت متأكد أنك تريد الحذف؟") == System.Windows.Forms.DialogResult.Yes)
                    {
                        db.Classes.Remove(clss);
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
            dgv.DataSource = db.Classes.ToList();
        }

        public void Search(string findWay)
        {
            try
            {
                var classes = db.Classes.ToList();
                switch (findWay)
                {
                    case "Contains":
                        classes = db.Classes.Where(b => b.ClassNameA.Contains(txtFind) || b.ClassNameE.Contains(txtFind)).ToList();
                        dgv.DataSource = classes.ToList();
                        break;
                    case "StartsWith":
                        classes = db.Classes.Where(b => b.ClassNameA.StartsWith(txtFind) || b.ClassNameE.StartsWith(txtFind)).ToList();
                        dgv.DataSource = classes.ToList();
                        break;
                    case "Equals":
                        classes = db.Classes.Where(b => b.ClassNameA.Equals(txtFind) || b.ClassNameE.Equals(txtFind)).ToList();
                        dgv.DataSource = classes.ToList();
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
                    Class clss = db.Classes.Find(id);
                    if (_4Data.msgQuestion("سوف يتم تعديل الفئة هل انت متأكد أنك تريد التعديل؟") == System.Windows.Forms.DialogResult.Yes)
                    {
                        clss.ClassNameA = txtNameA;
                        clss.ClassNameE = txtNameE;
                        clss.InActive = chInActive;
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
