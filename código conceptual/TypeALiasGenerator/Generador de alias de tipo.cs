using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace SpanishTypeGenerator
{
    public class TypeAliasGenerator
    {
        private readonly Dictionary<string, string> typeTranslations = new()
        {
            // Primitive types
            {"bool", "booleano"},
            {"byte", "octeto"},
            {"sbyte", "octetoConSigno"},
            {"char", "caracter"},
            {"decimal", "decimal"},
            {"double", "doble"},
            {"float", "flotante"},
            {"int", "entero"},
            {"uint", "enteroSinSigno"},
            {"long", "largo"},
            {"ulong", "largoSinSigno"},
            {"short", "corto"},
            {"ushort", "cortoSinSigno"},
            {"string", "cadena"},
            {"object", "objeto"},
            
            // Common types
            {"DateTime", "Fecha"},
            {"TimeSpan", "Duracion"},
            {"Guid", "Identificador"},
            {"Exception", "Excepcion"},
            {"Array", "Matriz"},
            {"List", "Lista"},
            {"Dictionary", "Diccionario"},
            {"Queue", "Cola"},
            {"Stack", "Pila"},
            {"HashSet", "ConjuntoHash"},
            {"LinkedList", "ListaEnlazada"},
            {"SortedList", "ListaOrdenada"},
            {"SortedSet", "ConjuntoOrdenado"},
            {"SortedDictionary", "DiccionarioOrdenado"},
            {"KeyValuePair", "ParClaveyValor"},
            {"Tuple", "Tupla"},
            {"Task", "Tarea"},
            {"Thread", "Hilo"},
            {"Process", "Proceso"},
            {"File", "Archivo"},
            {"Directory", "Directorio"},
            {"Path", "Ruta"},
            {"Stream", "Flujo"},
            {"TextReader", "LectorTexto"},
            {"TextWriter", "EscritorTexto"},
            {"Random", "Aleatorio"},
            {"Uri", "DireccionWeb"},
            {"Type", "Tipo"},
            {"Delegate", "Delegado"},
            {"Event", "Evento"},
            {"Enum", "Enumeracion"},
            {"Attribute", "Atributo"},
            {"Assembly", "Ensamblado"},
            
            // Collection interfaces
            {"IEnumerable", "IEnumerable"},
            {"ICollection", "IColeccion"},
            {"IList", "ILista"},
            {"IDictionary", "IDiccionario"},
            {"ISet", "IConjunto"},
            {"IQueryable", "IConsultable"},
            
            // Common interfaces
            {"IDisposable", "IDesechable"},
            {"IComparable", "IComparable"},
            {"IEquatable", "IEquitativo"},
            {"ICloneable", "IClonable"},
            {"IConvertible", "IConvertible"}
        };

        public void GenerateAliasFile(string outputPath)
        {
            var sb = new StringBuilder();
            
            // Add file header
            sb.AppendLine("// Generado automáticamente - Alias de tipos en español");
            sb.AppendLine("// NO MODIFICAR MANUALMENTE");
            sb.AppendLine();
            sb.AppendLine("using System;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using System.Threading.Tasks;");
            sb.AppendLine("using System.IO;");
            sb.AppendLine();
            
            sb.AppendLine("namespace TiposEspanol");
            sb.AppendLine("{");
            
            // Generate primitive type aliases
            foreach (var translation in typeTranslations.Where(t => !t.Key.StartsWith("I")))
            {
                sb.AppendLine($"    using {translation.Value} = {GetFullTypeName(translation.Key)};");
            }
            
            sb.AppendLine();
            
            // Generate generic type aliases
            GenerateGenericTypeAliases(sb);
            
            // Generate interface aliases
            foreach (var translation in typeTranslations.Where(t => t.Key.StartsWith("I")))
            {
                sb.AppendLine($"    using {translation.Value} = {GetFullTypeName(translation.Key)};");
            }
            
            sb.AppendLine("}");
            
            File.WriteAllText(outputPath, sb.ToString());
        }

        private string GetFullTypeName(string typeName)
        {
            // Add System namespace for known system types
            var systemTypes = new[]
            {
                "DateTime", "TimeSpan", "Guid", "Exception", "Uri", "Type",
                "Array", "Random", "Process", "Thread"
            };

            return systemTypes.Contains(typeName) ? $"System.{typeName}" : typeName;
        }

        private void GenerateGenericTypeAliases(StringBuilder sb)
        {
            // Generate common generic type aliases
            sb.AppendLine("    // Generic type aliases");
            sb.AppendLine($"    using Lista<T> = System.Collections.Generic.List<T>;");
            sb.AppendLine($"    using Diccionario<TKey, TValue> = System.Collections.Generic.Dictionary<TKey, TValue>;");
            sb.AppendLine($"    using Cola<T> = System.Collections.Generic.Queue<T>;");
            sb.AppendLine($"    using Pila<T> = System.Collections.Generic.Stack<T>;");
            sb.AppendLine($"    using ConjuntoHash<T> = System.Collections.Generic.HashSet<T>;");
            sb.AppendLine($"    using ListaEnlazada<T> = System.Collections.Generic.LinkedList<T>;");
            sb.AppendLine($"    using ListaOrdenada<T> = System.Collections.Generic.SortedSet<T>;");
            sb.AppendLine($"    using DiccionarioOrdenado<TKey, TValue> = System.Collections.Generic.SortedDictionary<TKey, TValue>;");
            sb.AppendLine($"    using ParClaveyValor<TKey, TValue> = System.Collections.Generic.KeyValuePair<TKey, TValue>;");
            sb.AppendLine($"    using Tarea<T> = System.Threading.Tasks.Task<T>;");
            sb.AppendLine();
        }

        public void GenerateExtensionMethods(string outputPath)
        {
            var sb = new StringBuilder();
            
            sb.AppendLine("using System;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine();
            sb.AppendLine("namespace TiposEspanol");
            sb.AppendLine("{");
            sb.AppendLine("    public static class ExtensionesEspanol");
            sb.AppendLine("    {");
            
            // Generate common extension methods with Spanish names
            sb.AppendLine("        public static int Longitud<T>(this ICollection<T> coleccion) => coleccion.Count;");
            sb.AppendLine("        public static bool EstaVacio<T>(this ICollection<T> coleccion) => coleccion.Count == 0;");
            sb.AppendLine("        public static void Agregar<T>(this ICollection<T> coleccion, T elemento) => coleccion.Add(elemento);");
            sb.AppendLine("        public static bool Contiene<T>(this ICollection<T> coleccion, T elemento) => coleccion.Contains(elemento);");
            sb.AppendLine("        public static void Limpiar<T>(this ICollection<T> coleccion) => coleccion.Clear();");
            sb.AppendLine("        public static T[] AMatriz<T>(this ICollection<T> coleccion) => new List<T>(coleccion).ToArray();");
            
            sb.AppendLine("    }");
            sb.AppendLine("}");
            
            File.WriteAllText(outputPath, sb.ToString());
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var generator = new TypeAliasGenerator();
            
            Console.WriteLine("Generando alias de tipos en español...");
            generator.GenerateAliasFile("TiposEspanol.cs");
            
            Console.WriteLine("Generando métodos de extensión...");
            generator.GenerateExtensionMethods("ExtensionesEspanol.cs");
            
            Console.WriteLine("Generación completada.");
        }
    }
}
