using System;
namespace GivingImporterGtk.ViewModels
{
    [Gtk.TreeNode(ListOnly = true)]
    public class DataFileListNode : Gtk.TreeNode
    {
        public DataFileListNode(int id, bool exists, string status, string fileName)
        {
            FileID = id.ToString();
            Status = status;
            Exists = exists? "true" : "false";
            File = fileName;
        }

        [Gtk.TreeNodeValue(Column = 0)]
        public string FileID { get; set; }

        [Gtk.TreeNodeValue(Column = 1)]
        public string Status { get; set; }

        [Gtk.TreeNodeValue(Column = 2)]
        public string Exists { get; set; }

        [Gtk.TreeNodeValue(Column = 3)]
        public string File { get; set; }
    }
}
