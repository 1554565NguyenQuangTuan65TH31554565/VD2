using Aspose.Cells;
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
    public class Class6 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;
            Transaction trans = new Transaction(doc);

            trans.Start("instance binding");
            // Create a new instance binding
            FileStream fileStream = File.Create("D:\\ShareParameter.txt");
            fileStream.Close();
            //binding 
            doc.Application.SharedParametersFilename = "D:\\ShareParameter.txt";
            DefinitionFile defFile = doc.Application.OpenSharedParameterFile();
            bool bindResult = SetNewParameterToTypeWall(uidoc.Application, defFile);
            //call 
            if (bindResult == true)
            
                MessageBox.Show("Da xong");
                trans.Commit();
                return Result.Succeeded;
            
        }
          public bool SetNewParameterToTypeWall(UIApplication app, DefinitionFile myDefinitionFile)
          {
            // Create a new group in the shared parameters file
            DefinitionGroups myGroups = myDefinitionFile.Groups;
            DefinitionGroup myGroup = myGroups.Create("Revit API Course");
            // Create a type definition
            ExternalDefinitionCreationOptions option = new ExternalDefinitionCreationOptions("Note", SpecTypeId.String.Text);
            Definition myDefinition_CompanyName = myGroup.Definitions.Create(option); 
           
            // Create a category set and insert category of wall to it
            CategorySet myCategories = app.Application.Create.NewCategorySet();
            // Use BuiltInCategory to get category of wall
            Category myCategory = Category.GetCategory(app.ActiveUIDocument.Document, BuiltInCategory.OST_Walls);
            myCategories.Insert(myCategory);//add wall into the group. Of course, you can add multiple categories

            InstanceBinding instanceBinding = app.Application.Create.NewInstanceBinding(myCategories);
            // Get the BingdingMap of current document.
            BindingMap bindingMap = app.ActiveUIDocument.Document.ParameterBindings;
            //Bind the definitions to the document 
            bool typeBindOK = bindingMap.Insert(myDefinition_CompanyName, instanceBinding, BuiltInParameterGroup.PG_TEXT);
            return typeBindOK;
          }


    }
}
