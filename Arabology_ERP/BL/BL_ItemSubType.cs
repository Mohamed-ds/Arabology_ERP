﻿using Arabology_ERP.DAL;
using MyLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Arabology_ERP.BL
{
    public class BL_ItemSubType : IGeneralProcessing
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
                ItemSubType subType = new ItemSubType()
                {
                    ItemSubTypeNameA = txtNameA,
                    ItemSubTypeNameE = txtNameE,
                    InActive = chInActive
                };
                db.ItemSubTypes.Add(subType);
                db.SaveChanges();
                _4Data.msgAlert("تمت الإضافة");
                return subType.ItemSubTypeId;
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
                    ItemSubType subType = db.ItemSubTypes.Find(id);
                    if (_4Data.msgQuestion("سوف يتم حذف النوع هل انت متأكد أنك تريد الحذف؟") == System.Windows.Forms.DialogResult.Yes)
                    {
                        db.ItemSubTypes.Remove(subType);
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
            dgv.DataSource = db.ItemSubTypes.ToList();
        }

        public void Search(string findWay)
        {
            try
            {
                var subTypes = db.ItemSubTypes.ToList();
                switch (findWay)
                {
                    case "Contains":
                        subTypes = db.ItemSubTypes.Where(b => b.ItemSubTypeNameA.Contains(txtFind) || b.ItemSubTypeNameE.Contains(txtFind)).ToList();
                        dgv.DataSource = subTypes.ToList();
                        break;
                    case "StartsWith":
                        subTypes = db.ItemSubTypes.Where(b => b.ItemSubTypeNameA.StartsWith(txtFind) || b.ItemSubTypeNameE.StartsWith(txtFind)).ToList();
                        dgv.DataSource = subTypes.ToList();
                        break;
                    case "Equals":
                        subTypes = db.ItemSubTypes.Where(b => b.ItemSubTypeNameA.Equals(txtFind) || b.ItemSubTypeNameE.Equals(txtFind)).ToList();
                        dgv.DataSource = subTypes.ToList();
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
                    ItemSubType subType = db.ItemSubTypes.Find(id);
                    if (_4Data.msgQuestion("سوف يتم تعديل النوع هل انت متأكد أنك تريد التعديل؟") == System.Windows.Forms.DialogResult.Yes)
                    {
                        subType.ItemSubTypeNameA = txtNameA;
                        subType.ItemSubTypeNameE = txtNameE;
                        subType.InActive = chInActive;
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
