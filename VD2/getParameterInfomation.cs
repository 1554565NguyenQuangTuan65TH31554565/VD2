using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VD2
{
    class getParameterInfomation
    {
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
                case StorageType.ElementId:
                    ElementId id = para.AsElementId();
                    if (id.IntegerValue == 0)
                        return prefix + defName + ": " + "null";
                    else
                        return prefix + defName + ": " + doc.GetElement(id).Name;
                default:
                    return prefix + defName + ": " + "unknown";
            }
        }
    }
}
