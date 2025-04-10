using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace VD2
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]

    class baitap : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            ElementCategoryFilter filter = new ElementCategoryFilter(BuiltInCategory.OST_StructuralColumns);
            IList<Element> columnsList = collector.WherePasses(filter).WhereElementIsNotElementType().ToElements();
            StringBuilder output = new StringBuilder("Danh sách các vật liệu trong bim mode:\r\n");
            foreach (Element elem in columnsList)
            {
                int n;
                n = 0;
                n = n + 1;
                FamilyInstance familyInstance = elem as FamilyInstance;
                FamilySymbol familySymbol = familyInstance.Symbol;
                Family family = familySymbol.Family;
                string elemName = "category: " + elem.Category.Name + " - family: " + family.Name + " - type: " + elem.Name + "\r\n";
                output.Append(elemName);


            }

            MessageBox.Show(output.ToString());
            return Result.Succeeded;

        }
    }
}
