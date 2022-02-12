using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyGroupPlugin
{
    [TransactionAttribute(TransactionMode.Manual)]
    public class CopyGroup: IExternalCommand
    {        
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uiDoc = commandData.Application.ActiveUIDocument;
            Document doc = uiDoc.Document;

            Reference reference = uiDoc.Selection.PickObject(Autodesk.Revit.UI.Selection.ObjectType.Element, "Choose Group");
            Element element = doc.GetElement(reference);
            Group group = element as Group;

            XYZ point = uiDoc.Selection.PickPoint("Choose Point");

            Transaction transaction = new Transaction(doc);
            transaction.Start("Copy Group");
            doc.Create.PlaceGroup(point, group.GroupType);
            transaction.Commit();

            return Result.Succeeded;

        }
    }
}
