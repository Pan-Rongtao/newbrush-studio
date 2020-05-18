using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace studio
{
    class Commands
    {
        public static RoutedUICommand InputFolderName { get { return _inputFolderName; } }
        public static RoutedUICommand NewFolder { get { return _newFolder; } }
        public static RoutedUICommand ImportFile { get { return _importFile; } }
        public static RoutedUICommand RemoveResourceNode { get { return _removeResourceNode; } }
        public static RoutedUICommand RenameResourceNode { get { return _renameResourceNode; } }
        public static RoutedUICommand RenameResourceNodeDlg { get { return _renameResourceNodeDlg; } }

        static Commands()
        {
            _inputFolderName = new RoutedUICommand("InputFolderName", "InputFolderName", typeof(Commands));
            _newFolder = new RoutedUICommand("NewFolder", "NewFolder", typeof(Commands), new InputGestureCollection() { new KeyGesture(Key.F, ModifierKeys.Control, " Ctrl+F") });
            _importFile = new RoutedUICommand("ImportFile", "ImportFile", typeof(Commands), new InputGestureCollection() { new KeyGesture(Key.A, ModifierKeys.Control, " Ctrl+A") });
            _removeResourceNode = new RoutedUICommand("RemoveResourceNode", "RemoveResourceNode", typeof(Commands), new InputGestureCollection() { new KeyGesture(Key.Delete, ModifierKeys.None, " Del") });
            _renameResourceNode = new RoutedUICommand("RenameResourceNode", "RenameResourceNode", typeof(Commands), new InputGestureCollection() { new KeyGesture(Key.F2, ModifierKeys.None, " F2") });
            _renameResourceNodeDlg = new RoutedUICommand("RenameResourceNodeDlg", "RenameResourceNodeDlg", typeof(Commands));
        }

        private static RoutedUICommand _inputFolderName;
        private static RoutedUICommand _newFolder;
        private static RoutedUICommand _importFile;
        private static RoutedUICommand _removeResourceNode;
        private static RoutedUICommand _renameResourceNode;
        private static RoutedUICommand _renameResourceNodeDlg;

    }
}
