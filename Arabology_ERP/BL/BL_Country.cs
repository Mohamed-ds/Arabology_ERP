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
    public class BL_Country : IGeneralProcessing
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
                Country country = new Country()
                {
                    CountryNameA = txtNameA,
                    CountryNameE = txtNameE,
                    InActive = chInActive
                };
                db.Countries.Add(country);
                db.SaveChanges();
                _4Data.msgAlert("تمت الإضافة");
                return country.CountryId;
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
                    Country country = db.Countries.Find(id);
                    if (_4Data.msgQuestion("سوف يتم حذف الدولة هل انت متأكد أنك تريد الحذف؟") == System.Windows.Forms.DialogResult.Yes)
                    {
                        db.Countries.Remove(country);
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
            dgv.DataSource = db.Countries.ToList();
        }

        public void Search(string findWay)
        {
            try
            {
                var countries = db.Countries.ToList();
                switch (findWay)
                {
                    case "Contains":
                        countries = db.Countries.Where(b => b.CountryNameA.Contains(txtFind) || b.CountryNameE.Contains(txtFind)).ToList();
                        dgv.DataSource = countries.ToList();
                        break;
                    case "StartsWith":
                        countries = db.Countries.Where(b => b.CountryNameA.StartsWith(txtFind) || b.CountryNameE.StartsWith(txtFind)).ToList();
                        dgv.DataSource = countries.ToList();
                        break;
                    case "Equals":
                        countries = db.Countries.Where(b => b.CountryNameA.Equals(txtFind) || b.CountryNameE.Equals(txtFind)).ToList();
                        dgv.DataSource = countries.ToList();
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
                    Country country = db.Countries.Find(id);
                    if (_4Data.msgQuestion("سوف يتم تعديل الدولة هل انت متأكد أنك تريد التعديل؟") == System.Windows.Forms.DialogResult.Yes)
                    {
                        country.CountryNameA = txtNameA;
                        country.CountryNameE = txtNameE;
                        country.InActive = chInActive;
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
