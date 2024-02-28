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
    public class BL_City : IGeneralProcessing
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
                City city = new City()
                {
                    CityNameA = txtNameA,
                    CityNameE = txtNameE,
                    InActive = chInActive
                };
                db.Cities.Add(city);
                db.SaveChanges();
                _4Data.msgAlert("تمت الإضافة");
                return city.CityId;
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
                    City city = db.Cities.Find(id);
                    if (_4Data.msgQuestion("سوف يتم حذف المحافظة هل انت متأكد أنك تريد الحذف؟") == System.Windows.Forms.DialogResult.Yes)
                    {
                        db.Cities.Remove(city);
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
            dgv.DataSource = db.Cities.ToList();
        }

        public void Search(string findWay)
        {
            try
            {
                var cities = db.Cities.ToList();
                switch (findWay)
                {
                    case "Contains":
                        cities = db.Cities.Where(b => b.CityNameA.Contains(txtFind) || b.CityNameE.Contains(txtFind)).ToList();
                        dgv.DataSource = cities.ToList();
                        break;
                    case "StartsWith":
                        cities = db.Cities.Where(b => b.CityNameA.StartsWith(txtFind) || b.CityNameE.StartsWith(txtFind)).ToList();
                        dgv.DataSource = cities.ToList();
                        break;
                    case "Equals":
                        cities = db.Cities.Where(b => b.CityNameA.Equals(txtFind) || b.CityNameE.Equals(txtFind)).ToList();
                        dgv.DataSource = cities.ToList();
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
                    City city = db.Cities.Find(id);
                    if (_4Data.msgQuestion("سوف يتم تعديل المحافظة هل انت متأكد أنك تريد التعديل؟") == System.Windows.Forms.DialogResult.Yes)
                    {
                        city.CityNameA = txtNameA;
                        city.CityNameE = txtNameE;
                        city.InActive = chInActive;
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
