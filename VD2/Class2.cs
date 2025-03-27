using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Windows.Forms;

namespace VD2
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    public class Class2 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            XYZ point1 = uidoc.Selection.PickPoint();
            XYZ point2 = uidoc.Selection.PickPoint();
            XYZ point3 = uidoc.Selection.PickPoint();

           StringBuilder sb = new StringBuilder();
            sb.AppendLine("Điểm 1: " + point1.ToString());
            sb.AppendLine("Điểm 2: " + point2.ToString());
            sb.AppendLine("Điểm 3: " + point3.ToString());
            MessageBox.Show("Bạn đã lựa chọn các điểm :\n"+sb.ToString());
            return Result.Succeeded;    
        }
    }
}
