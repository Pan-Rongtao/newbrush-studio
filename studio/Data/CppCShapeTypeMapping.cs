using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using MaterialDesignThemes.Wpf;
using System.Windows.Controls;
using System.Windows.Data;
using System.Globalization;
using System.IO;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;

namespace studio
{
    public class PropertyEditorInfo
    {
        public PropertyEditorInfo(Type propertyType, Type editorType, object defaultValue)
        {
            PropertyType = propertyType;
            EditorType = editorType;
            DefaultValue = defaultValue;
        }

        public Type PropertyType { get; }
        public Type EditorType { get; }
        public object DefaultValue { get; }
    }

    class TypeMapping
    {
        static private Dictionary<string, PackIconKind> _typeIconDictionary = new Dictionary<string, PackIconKind>
        {
            { "nb::Canvas", PackIconKind.DrawingBox },
            { "nb::DockPanel", PackIconKind.ViewQuilt },
            { "nb::WrapPanel", PackIconKind.FormatTextWrappingWrap },
            { "nb::StackPanel", PackIconKind.ViewWeek },
            { "nb::Grid", PackIconKind.Grid },
            { "nb::UniformGrid", PackIconKind.GridLarge },
            { "nb::Button", PackIconKind.AlphabetBBoxOutline },
            { "nb::RepeatButton", PackIconKind.GestureTapButton },
            { "nb::Window", PackIconKind.Airplay },
            { "nb::Line", PackIconKind.VectorLine },
            { "nb::Polyline", PackIconKind.VectorPolyline },
            { "nb::Polygon", PackIconKind.PentagonOutline },
            { "nb::Path", PackIconKind.MapMarkerPath },
            { "nb::Rectangle", PackIconKind.RectangleOutline },
            { "nb::Ellipse", PackIconKind.EllipseOutline },
            { "nb::Image", PackIconKind.FileImageOutline },
            { "nb::TextBlock", PackIconKind.FormatTextRotationNone },
        };

        static public PackIconKind GetIcon(string cppType)
        {
            PackIconKind icon = PackIconKind.GlobeLight;
            try
            {
                icon = _typeIconDictionary[cppType];
            }
            catch (Exception) { }
            return icon;
        }



    }
}
