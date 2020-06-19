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

namespace studio
{
    public class BoolStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? "true" : "false";
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class CShapeTuple
    {
        public CShapeTuple(Type type, IValueConverter stringConverter, object defaultValue)
        {
            Type = type;
            StringConverter = stringConverter;
            DefaultValue = defaultValue;
        }

        public Type Type { get; }
        public IValueConverter StringConverter { get; }
        public object DefaultValue { get; }
    }

    class CppCShapeTypeMapping
    {
        static private Dictionary<string, CShapeTuple> _typeMap = new Dictionary<string, CShapeTuple>
        {
            { "bool", new CShapeTuple(typeof(bool), new BoolStringConverter(), false)},
            { "char", new CShapeTuple(typeof(char), new BoolStringConverter(), false)},
            { "signedchar", new CShapeTuple(typeof(sbyte), new BoolStringConverter(), false)},
            { "unsignedchar", new CShapeTuple(typeof(byte), new BoolStringConverter(), false)},
            { "wchar", new CShapeTuple(typeof(short), new BoolStringConverter(), false)},
            { "short", new CShapeTuple(typeof(short), new BoolStringConverter(), false)},
            { "unsignedshort", new CShapeTuple(typeof(ushort), new BoolStringConverter(), false)},
            { "int", new CShapeTuple(typeof(int), new BoolStringConverter(), false)},
            { "unsignedint", new CShapeTuple(typeof(uint), new BoolStringConverter(), false)},
            { "long", new CShapeTuple(typeof(long), new BoolStringConverter(), false)},
            { "unsignedlong", new CShapeTuple(typeof(ulong), new BoolStringConverter(), false)},
            { "__int64", new CShapeTuple(typeof(Int64), new BoolStringConverter(), false)},
            { "unsigned__int64", new CShapeTuple(typeof(UInt64), new BoolStringConverter(), false)},
            { "float", new CShapeTuple(typeof(float), new BoolStringConverter(), false)},
            { "double", new CShapeTuple(typeof(double), new BoolStringConverter(), false)},
            { "longdouble", new CShapeTuple(typeof(decimal), new BoolStringConverter(), false)},
            { "std::string", new CShapeTuple(typeof(string), new BoolStringConverter(), false)},
            { "std::vector<bool>", new CShapeTuple(typeof(List<bool>), new BoolStringConverter(), false)},
            { "std::vector<int>", new CShapeTuple(typeof(List<int>), new BoolStringConverter(), false)},
            { "std::vector<float>", new CShapeTuple(typeof(List<float>), new BoolStringConverter(), false)},
            { "std::vector<double>", new CShapeTuple(typeof(List<double>), new BoolStringConverter(), false)},
            { "classnb::Thickness", new CShapeTuple(typeof(Thickness), new BoolStringConverter(), false)},
            { "classstd::shared_ptr<classnb::Transform>", new CShapeTuple(typeof(Transform), new BoolStringConverter(), false)},
            { "classstd::shared_ptr<classnb::ImageSource>", new CShapeTuple(typeof(ImageSource), new BoolStringConverter(), false)},
            { "classstd::shared_ptr<classnb::Brush>", new CShapeTuple(typeof(Brush), new BoolStringConverter(), false)},
            { "classstd::shared_ptr<classnb::ControlTemplate>", new CShapeTuple(typeof(ControlTemplate), new BoolStringConverter(), false)},
            { "classstd::shared_ptr<classnb::Font>", new CShapeTuple(typeof(FontFamily), new BoolStringConverter(), false)},
            { "classstd::shared_ptr<classnb::UIElement>", new CShapeTuple(typeof(UIElement), new BoolStringConverter(), false)},
        };

        static public Google.Protobuf.ByteString ConverToString(object obj)
        {
            Google.Protobuf.ByteString ret;
            if (obj is bool || obj is char || obj is sbyte || obj is byte || obj is short || obj is ushort || obj is int || obj is uint
                 || obj is long || obj is ulong || obj is Int64 || obj is UInt64 || obj is float || obj is double || obj is decimal || obj is string)
            {
                ret = Google.Protobuf.ByteString.CopyFromUtf8(obj.ToString());
            }
            else if(obj is List<bool> || obj is List<int> || obj is List<float> || obj is List<double>)
            {
                ret = Google.Protobuf.ByteString.CopyFromUtf8(obj.ToString());
            }
            else if(obj is SolidColorBrush)
            {
                Color c = (obj as SolidColorBrush).Color;
                ret = Google.Protobuf.ByteString.CopyFromUtf8(string.Format("{0},{1},{2},{3}", c.R, c.G, c.B, c.A));
            }
            else if (obj is ImageBrush)
            {
                ImageBrush imgBrush = obj as ImageBrush;
                var bm = imgBrush.ImageSource as BitmapImage;
                string path = bm.UriSource.AbsolutePath;
                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                byte[] bytes = new byte[fs.Length];
                fs.Read(bytes, 0, bytes.Length);

                ret = Google.Protobuf.ByteString.CopyFrom(bytes);
            }
            else
            {
                //throw new Exception("ConverToString");
                ret = Google.Protobuf.ByteString.CopyFromUtf8("");
            }
            return ret;
        }
        
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
                case "float":               return new Tuple<Type, object>(typeof(double), new double());
                case "double":              return new Tuple<Type, object>(typeof(double), new double());
                case "longdouble":          return new Tuple<Type, object>(typeof(decimal), new decimal());
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
