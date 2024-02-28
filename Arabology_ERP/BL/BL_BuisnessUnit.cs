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
    public class BL_BuisnessUnit : IGeneralProcessing
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
                BuisnessUnit buisnessUnit = new BuisnessUnit()
                {
                    BUNameA = txtNameA,
                    BUNameE = txtNameE,
                    InActive = chInActive
                };
                db.BuisnessUnits.Add(buisnessUnit);
                db.SaveChanges();
                _4Data.msgAlert("تمت الإضافة");

                int id = 0;
                if (!int.TryParse(buisnessUnit.BUID, out id))
                {
                    throw new Exception("لم يتم استطاعة تحويل الكود الي رقم");
                }
                
                
                return id ;
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
                    BuisnessUnit buisness = db.BuisnessUnits.Find(txtId);
                    if (_4Data.msgQuestion("سوف يتم حذف وحدة العمل هل انت متأكد أنك تريد الحذف؟") == System.Windows.Forms.DialogResult.Yes)
                    {
                        db.BuisnessUnits.Remove(buisness);
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
            dgv.DataSource = db.BuisnessUnits.ToList();
        }

        public void Search(string findWay)
        {
            try
            {
                var buisnessUnits = db.BuisnessUnits.ToList();
                switch (findWay)
                {
                    case "Contains":
                        buisnessUnits = db.BuisnessUnits.Where(b => b.BUNameA.Contains(txtFind) || b.BUNameE.Contains(txtFind)).ToList();
                        dgv.DataSource = buisnessUnits.ToList();
                        break;
                    case "StartsWith":
                        buisnessUnits = db.BuisnessUnits.Where(b => b.BUNameA.StartsWith(txtFind) || b.BUNameE.StartsWith(txtFind)).ToList();
                        dgv.DataSource = buisnessUnits.ToList();
                        break;
                    case "Equals":
                        buisnessUnits = db.BuisnessUnits.Where(b => b.BUNameA.Equals(txtFind) || b.BUNameE.Equals(txtFind)).ToList();
                        dgv.DataSource = buisnessUnits.ToList();
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
                    
                    BuisnessUnit buisnessUnit = db.BuisnessUnits.Find(txtId);
                    if (_4Data.msgQuestion("سوف يتم تعديل وحدة العمل هل انت متأكد أنك تريد التعديل؟") == System.Windows.Forms.DialogResult.Yes)
                    {
                        buisnessUnit.BUNameA = txtNameA;
                        buisnessUnit.BUNameE = txtNameE;
                        buisnessUnit.InActive = chInActive;
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
