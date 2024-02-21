using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyLibrary;
namespace Arabology_ERP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            List<City> city = new List<City>()
           {
               new City(){ID =1,Name="Cairo"},
               new City(){ID =2,Name="Giza"},
               new City(){ID =4,Name="Minea"},
               new City(){ID =3,Name="Alex"}
           };
            //ForForms.SetDataToComboBox(city, ref comboBox1);
            comboBox1.DataSource = city;
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "ID";
        }
    }
    public class City
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
