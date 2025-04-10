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

    public class Class4 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            ElementCategoryFilter filter = new ElementCategoryFilter(BuiltInCategory.OST_Walls);
            IList<Element> walllist = collector.WherePasses(filter).WhereElementIsNotElementType().ToElements();
            StringBuilder sb = new StringBuilder();
            foreach (Element wall in walllist)
            {
                sb.AppendLine(wall.Name + "::"+ wall.Id);
                foreach (Parameter param in wall.Parameters)
                {
                    sb.AppendLine(wall.Name + "::" + param.Definition.Name + "::" + param.StorageType.ToString());
                    foreach(Parameter para in wall.Parameters)
                    {
                        sb.Append(this.getP(para,doc,";"));
                        sb.AppendLine();
                    }
                       
                }
                sb.AppendLine();
            }
            MessageBox.Show(sb.ToString());
            return Result.Succeeded;
        }

        private string getP(Parameter para, Document doc, string prefix)
        {
            string defName = para.Definition.Name;
            switch (para.StorageType)
            {
                case StorageType.String:
                    return prefix + defName + ": " + para.AsString();
                case StorageType.Integer:
                    return prefix + defName + ": " + para.AsInteger();
                case StorageType.Double:
                    return prefix + defName + ": " + para.AsDouble();
             
                default:
                    return prefix + defName + ": " + "unknown";
            }
        }
    }
}
