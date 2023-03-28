#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

#endregion

namespace MyFirstRevitAddin
{
    [Transaction(TransactionMode.Manual)]
    public class Command1 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // this is a variable for the Revit application
            UIApplication uiapp = commandData.Application;

            // this is a variable for the current Revit model
            Document doc = uiapp.ActiveUIDocument.Document;

            // Your code goes here
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            collector.OfCategory(BuiltInCategory.OST_TextNotes);
            collector.WhereElementIsNotElementType();

            int counter = 0;
            Transaction t = new Transaction(doc);
            t.Start("Text to UPPER");

            foreach(Element element in collector)
            {
                TextNote textNote = element as TextNote;
                textNote.Text = textNote.Text.ToUpper();
                counter++;
            }

            t.Commit();

            TaskDialog.Show("Complete", "Changed " + counter.ToString() + " text notes to UPPER.");

            return Result.Succeeded;
        }

        public static String GetMethod()
        {
            var method = MethodBase.GetCurrentMethod().DeclaringType?.FullName;
            return method;
        }
    }
}
