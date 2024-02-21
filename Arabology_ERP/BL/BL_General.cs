using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Arabology_ERP.BL
{
    public interface IGeneralProcessing
    {
        string txtId { get; set; }
        string txtNameA { get; set; }
        string txtNameE { get; set; }
        bool chInActive { get; set; }
        string txtFind { get; set; }

        DataGridView dgv { get; set; }
        int Add();
        void DeclareControls(ref string id, ref string nameA, ref string nameE, ref bool inActive, ref string find, ref DataGridView grid);
        void Delete();
        void FillDGV();
        void Update();
        void Search();
        void SetDGV();
    }
    public class BL_General
    {
    }
}
