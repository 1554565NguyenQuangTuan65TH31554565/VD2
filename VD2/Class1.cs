using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace VD2
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]  
    public class cookie : IExternalCommand 
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            ICollection<ElementId> selectElements = uidoc.Selection.GetElementIds();//Lấy danh sách các phần tử được chọn

            string list = "Đây là danh sách: \n";
            foreach (ElementId e1 in selectElements)//Duyệt danh sách các phần tử được chọn
            {
                Element element = uidoc.Document.GetElement(e1);//Lấy thông tin của phần tử
                list += element.Id + "\n"; //Thêm id của phần tử vào danh sách

            }
            MessageBox.Show(list);
            return Result.Succeeded;

        }
    }
}
