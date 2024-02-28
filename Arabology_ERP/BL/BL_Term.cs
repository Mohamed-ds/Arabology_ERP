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
    public class BL_Term : IGeneralProcessing
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
                Term term = new Term()
                {
                    TermNameA = txtNameA,
                    TermNameE = txtNameE,
                    InActive = chInActive
                };
                db.Terms.Add(term);
                db.SaveChanges();
                _4Data.msgAlert("تمت الإضافة");
                return term.TermId;
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
                    Term term = db.Terms.Find(id);
                    if (_4Data.msgQuestion("سوف يتم حذف شرط الدفع هل انت متأكد أنك تريد الحذف؟") == System.Windows.Forms.DialogResult.Yes)
                    {
                        db.Terms.Remove(term);
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
            dgv.DataSource = db.Terms.ToList();
        }

        public void Search(string findWay)
        {
            try
            {
                var terms = db.Terms.ToList();
                switch (findWay)
                {
                    case "Contains":
                        terms = db.Terms.Where(b => b.TermNameA.Contains(txtFind) || b.TermNameE.Contains(txtFind)).ToList();
                        dgv.DataSource = terms.ToList();
                        break;
                    case "StartsWith":
                        terms = db.Terms.Where(b => b.TermNameA.StartsWith(txtFind) || b.TermNameE.StartsWith(txtFind)).ToList();
                        dgv.DataSource = terms.ToList();
                        break;
                    case "Equals":
                        terms = db.Terms.Where(b => b.TermNameA.Equals(txtFind) || b.TermNameE.Equals(txtFind)).ToList();
                        dgv.DataSource = terms.ToList();
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
                    Term term = db.Terms.Find(id);
                    if (_4Data.msgQuestion("سوف يتم تعديل شرط الدفع هل انت متأكد أنك تريد التعديل؟") == System.Windows.Forms.DialogResult.Yes)
                    {
                        term.TermNameA = txtNameA;
                        term.TermNameE = txtNameE;
                        term.InActive = chInActive;
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
