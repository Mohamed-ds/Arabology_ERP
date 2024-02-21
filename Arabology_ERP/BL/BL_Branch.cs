using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Arabology_ERP.DAL;
using MyLibrary;

namespace Arabology_ERP.BL
{
    public class BL_Branch : IGeneralProcessing
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
                Branch branch = new Branch()
                {
                    BranchNameA = txtNameA,
                    BranchNameE = txtNameE,
                    InActive = chInActive
                };
                db.Branches.Add(branch);
                db.SaveChanges();
                _4Data.msgAlert("تمت الإضافة");
                return branch.BranchId;
            }
            catch (Exception ex)
            {
                _4Exception.GetException(ex);
                return 0;
            }
            
        }

        public void DeclareControls(ref string id, ref string nameA, ref string nameE, ref bool inActive, ref string find,ref DataGridView grid)
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
                    Branch branch = db.Branches.Find(id);
                    if (_4Data.msgQuestion("سوف يتم حذف الفرع هل انت متأكد أنك تريد الحذف؟") == System.Windows.Forms.DialogResult.Yes)
                    {
                        db.Branches.Remove(branch);
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

        public void Search(string findWay)
        {   
            try
            {
                var branches = db.Branches.ToList();
                switch (findWay)
                {
                    case "Contains":
                        branches = db.Branches.Where(b => b.BranchNameA.Contains(txtFind) || b.BranchNameE.Contains(txtFind)).ToList();
                        dgv.DataSource = branches.ToList();
                        break;
                    case "StartsWith":
                        branches = db.Branches.Where(b => b.BranchNameA.StartsWith(txtFind) || b.BranchNameE.StartsWith(txtFind)).ToList();
                        dgv.DataSource = branches.ToList();
                        break;
                    case "Equals":
                        branches = db.Branches.Where(b => b.BranchNameA.Equals(txtFind) || b.BranchNameE.Equals(txtFind)).ToList();
                        dgv.DataSource = branches.ToList();
                        break;
                    default:
                        SetDGV();
                        break;
                }
                
            }
            catch (Exception ex)
            {
                _4Exception.GetException(ex);
                return ;
            }
        }

        public void Update()
        {

            try
            {
                if (txtId != "")
                {
                    int id = int.Parse(txtId);
                    Branch branch = db.Branches.Find(id);
                    if (_4Data.msgQuestion("سوف يتم تعديل الفرع هل انت متأكد أنك تريد التعديل؟") == System.Windows.Forms.DialogResult.Yes)
                    {
                        branch.BranchNameA = txtNameA;
                        branch.BranchNameE = txtNameE;
                        branch.InActive = chInActive;
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

        public void FillDGV()
        {
            dgv.DataSource = db.Branches.ToList();
        }
        public void SetDGV()
        {
            dgv.Columns[0].Visible = false;
            dgv.Columns[1].HeaderText = "الاسم بالعربي";
            dgv.Columns[2].HeaderText = "الاسم بالانجليزي";
            dgv.Columns[3].HeaderText = "غير فعال";
        }
    }
}
