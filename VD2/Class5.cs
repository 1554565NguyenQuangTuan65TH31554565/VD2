using Aspose.Cells.Charts;
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

    public class Class5 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            ElementCategoryFilter filter = new ElementCategoryFilter(BuiltInCategory.OST_Walls);
            IList<Element> walllist = collector.WherePasses(filter).WhereElementIsNotElementType().ToElements();
            StringBuilder output = new StringBuilder("cac wall trong bim :" + walllist.Count() + "\r\n");
            foreach (Element t1 in walllist)
            {
                string eleName = t1.Id.ToString();  
                ElementType type = doc.GetElement(t1.GetTypeId()) as ElementType;
              
                Parameter w = type.LookupParameter("Width") ;
               
                eleName = eleName+ "-"+ t1.Name +" - "  + "w =" + w.AsDouble() * 304.8 +" "+ "mm " +  "\r\n";
               
                output.Append(eleName);
                output.AppendLine("  " + "\r\n");

            }
            MessageBox.Show(output.ToString());
            return Result.Succeeded;

        }
        

    }
}
