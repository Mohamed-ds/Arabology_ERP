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
    public class BL_ItemType : IGeneralProcessing
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
                ItemType itemType = new ItemType()
                {
                    ItemTypeNameA = txtNameA,
                    ItemTypeNameE = txtNameE,
                    InActive = chInActive
                };
                db.ItemTypes.Add(itemType);
                db.SaveChanges();
                _4Data.msgAlert("تمت الإضافة");
                return itemType.ItemTypeId;
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
                    ItemType itemType = db.ItemTypes.Find(id);
                    if (_4Data.msgQuestion("سوف يتم حذف النوع هل انت متأكد أنك تريد الحذف؟") == System.Windows.Forms.DialogResult.Yes)
                    {
                        db.ItemTypes.Remove(itemType);
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
            dgv.DataSource = db.ItemTypes.ToList();
        }

        public void Search(string findWay)
        {
            try
            {
                var itemTypes = db.ItemTypes.ToList();
                switch (findWay)
                {
                    case "Contains":
                        itemTypes = db.ItemTypes.Where(b => b.ItemTypeNameA.Contains(txtFind) || b.ItemTypeNameE.Contains(txtFind)).ToList();
                        dgv.DataSource = itemTypes.ToList();
                        break;
                    case "StartsWith":
                        itemTypes = db.ItemTypes.Where(b => b.ItemTypeNameA.StartsWith(txtFind) || b.ItemTypeNameE.StartsWith(txtFind)).ToList();
                        dgv.DataSource = itemTypes.ToList();
                        break;
                    case "Equals":
                        itemTypes = db.ItemTypes.Where(b => b.ItemTypeNameA.Equals(txtFind) || b.ItemTypeNameE.Equals(txtFind)).ToList();
                        dgv.DataSource = itemTypes.ToList();
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
                    ItemType itemType = db.ItemTypes.Find(id);
                    if (_4Data.msgQuestion("سوف يتم تعديل النوع هل انت متأكد أنك تريد التعديل؟") == System.Windows.Forms.DialogResult.Yes)
                    {
                        itemType.ItemTypeNameA = txtNameA;
                        itemType.ItemTypeNameE = txtNameE;
                        itemType.InActive = chInActive;
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
