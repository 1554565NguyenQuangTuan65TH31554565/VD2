using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace VD2
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]

    public class _4_8_2025 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            ICollection<ElementId> ids = uidoc.Selection.GetElementIds();
            Document doc = uidoc.Document;
            string modelPath = doc.PathName;
            string modelTitle = doc.Title;
            StreamWriter sw = new StreamWriter(modelPath.Replace(doc.Title + ".rvt", "Result.txt"));
            sw.WriteLine("BaseConstraint\tVolume\tArea\tLength");
            foreach (ElementId elemid in ids)
            {
                Element element = doc.GetElement(elemid);
                Wall wall = element as Wall;
                if (wall != null)
                {
                    string condition = findCondition(wall, doc);
                    double volume = findVolume(wall);
                    double area = findArea(wall);
                    double length = findLength(wall);
                    sw.WriteLine(condition + "\t" + volume + "\t" + area + "\t" + length);
                }

            }
            sw.Close();
            MessageBox.Show("Da xong");
            return Result.Succeeded;
        }


        private double findVolume(Element element)
        {
            double volume = 0;
            volume = element.LookupParameter("Volume").AsDouble();
            return volume;
        }
        private string findCondition(Element element, Document doc)
        {
            string name = null;
            name = doc.GetElement(element.LookupParameter("BaseConstraint").AsElementId()).Name;
            return name;
        }
        private double findArea(Element element)
        {
            double area = 0;
            area = element.LookupParameter("Area").AsDouble();
            return area;
        }
        private double findLength(Element element)
        {
            double length = 0;
            length = element.LookupParameter("Length").AsDouble();
            return length;
        }
    }
}    
