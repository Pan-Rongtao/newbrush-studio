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

namespace studio
{
    class CppCShapeTypeParser
    {
        //返回<类型, 默认值>
        static public Tuple<Type, object> ValueTypeCppToShape(string typeName)
        {
            switch(typeName)
            {
                case "bool":                return new Tuple<Type, object>(typeof(bool), false);
                case "char":                return new Tuple<Type, object>(typeof(char), new char());
                case "signedchar":          return new Tuple<Type, object>(typeof(sbyte), new sbyte());
                case "unsignedchar":        return new Tuple<Type, object>(typeof(byte), new byte());
                case "wchar":               return new Tuple<Type, object>(typeof(char), new char());
                case "short":               return new Tuple<Type, object>(typeof(short), new short());
                case "unsignedshort":       return new Tuple<Type, object>(typeof(ushort), new ushort());
                case "int":                 return new Tuple<Type, object>(typeof(int), new int());
                case "unsignedint":         return new Tuple<Type, object>(typeof(uint), new uint());
                case "long":                return new Tuple<Type, object>(typeof(long), new long());
                case "unsignedlong":        return new Tuple<Type, object>(typeof(ulong), new ulong());
                case "__int64":             return new Tuple<Type, object>(typeof(Int64), new Int64());
                case "unsigned__int64":     return new Tuple<Type, object>(typeof(UInt64), new UInt64());
                case "float":               return new Tuple<Type, object>(typeof(float), new float());
                case "double":              return new Tuple<Type, object>(typeof(double), new double());
                case "longdouble":          return new Tuple<Type, object>(typeof(Decimal), new Decimal());
                case "std::string":         return new Tuple<Type, object>(typeof(string), string.Empty);
                case "std::vector<bool>":   return new Tuple<Type, object>(typeof(List<bool>), new List<bool>());
                case "std::vector<int>":    return new Tuple<Type, object>(typeof(List<int>), new List<int>()); ;
                case "std::vector<float>":  return new Tuple<Type, object>(typeof(List<float>), new List<float>());
                case "std::vector<double>": return new Tuple<Type, object>(typeof(List<double>), new List<double>());
                case "classnb::Thickness":  return new Tuple<Type, object>(typeof(Thickness), new Thickness()); 
                case "classstd::shared_ptr<classnb::Transform>":  return new Tuple<Type, object>(typeof(Transform), new TranslateTransform());
                case "classstd::shared_ptr<classnb::ImageSource>": return new Tuple<Type, object>(typeof(ImageSource), new BitmapImage());
                case "classstd::shared_ptr<classnb::Brush>":      return new Tuple<Type, object>(typeof(Brush), null);
                case "classstd::shared_ptr<classnb::ControlTemplate>": return new Tuple<Type, object>(typeof(ControlTemplate), new ControlTemplate());
                case "classstd::shared_ptr<classnb::Font>": return new Tuple<Type, object>(typeof(FontFamily), new FontFamily());
                case "classstd::shared_ptr<classnb::UIElement>": return new Tuple<Type, object>(typeof(UIElement), new UIElement());
                default:                    return null;
            }
        }
        
        static public PackIconKind TypeNameToIcon(string typeName)
        {
            PackIconKind icon = PackIconKind.GlobeLight;
            try
            {
                icon = _typeIconDictionary[typeName];
            }
            catch (Exception) { }
            return icon;
        }

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

    }
}
