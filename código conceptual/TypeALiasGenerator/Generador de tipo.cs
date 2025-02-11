using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Runtime.InteropServices;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace AssemblyTypeTranslator
{
    public class TypeTranslator
    {
        private readonly HttpClient httpClient;
        private readonly Dictionary<string, string> translationCache;

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetUserDefaultUILanguage();

        [DllImport("mlang.dll")]
        private static extern int LcidToRfc1766(uint lcid, StringBuilder pszRfc1766, int nChar);

        public TypeTranslator()
        {
            httpClient = new HttpClient();
            translationCache = new Dictionary<string, string>();
        }

        public async Task GenerateSpanishTypes(string outputPath)
        {
            var assemblies = new[]
            {
                typeof(object).Assembly,                // mscorlib
                typeof(Uri).Assembly,                   // System
                typeof(List<>).Assembly,                // System.Collections.Generic
                typeof(Enumerable).Assembly,            // System.Linq
                typeof(JsonSerializer).Assembly,        // System.Text.Json
                typeof(Task).Assembly,                  // System.Threading.Tasks
                Assembly.Load("System.Collections"),
                Assembly.Load("System.IO")
            };

            var sb = new StringBuilder();
            sb.AppendLine("// Generated automatically - Spanish Type Aliases");
            sb.AppendLine("// DO NOT MODIFY MANUALLY");
            sb.AppendLine();
            sb.AppendLine("using System;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using System.Threading.Tasks;");
            sb.AppendLine("using System.IO;");
            sb.AppendLine();
            sb.AppendLine("namespace TiposEspanol");
            sb.AppendLine("{");

            foreach (var assembly in assemblies)
            {
                await ProcessAssembly(assembly, sb);
            }

            sb.AppendLine("}");

            await File.WriteAllTextAsync(outputPath, sb.ToString());
        }

        private async Task ProcessAssembly(Assembly assembly, StringBuilder sb)
        {
            Console.WriteLine($"Processing assembly: {assembly.GetName().Name}");

            var types = assembly.GetTypes()
                .Where(t => t.IsPublic && 
                           !t.IsNested && 
                           (t.IsClass || t.IsInterface || t.IsEnum || t.IsValueType) &&
                           !t.Name.StartsWith("<"))
                .OrderBy(t => t.Name);

            foreach (var type in types)
            {
                try
                {
                    await ProcessType(type, sb);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing type {type.Name}: {ex.Message}");
                }
            }
        }

        private async Task ProcessType(Type type, StringBuilder sb)
        {
            string translatedName = await TranslateTypeName(type.Name);
            
            // Clean up translated name
            translatedName = CleanTranslatedName(translatedName);

            // Handle generic types
            if (type.IsGenericTypeDefinition)
            {
                var genericArgs = type.GetGenericArguments()
                    .Select(arg => arg.Name.Length == 1 ? arg.Name : "T")
                    .ToList();
                
                string genericParams = string.Join(", ", genericArgs);
                sb.AppendLine($"    using {translatedName}<{genericParams}> = {GetFullTypeName(type)};");
            }
            else
            {
                sb.AppendLine($"    using {translatedName} = {GetFullTypeName(type)};");
            }
        }

        private string GetFullTypeName(Type type)
        {
            if (type.IsGenericTypeDefinition)
            {
                var genericArgs = type.GetGenericArguments()
                    .Select(arg => arg.Name.Length == 1 ? arg.Name : "T")
                    .ToList();
                return $"{type.Namespace}.{type.Name.Split('`')[0]}<{string.Join(", ", genericArgs)}>";
            }
            
            return $"{type.Namespace}.{type.Name}";
        }

        private async Task<string> TranslateTypeName(string typeName)
        {
            // Remove generic type markers
            typeName = typeName.Split('`')[0];

            // Check cache first
            if (translationCache.TryGetValue(typeName, out string cached))
            {
                return cached;
            }

            try
            {
                // First try Windows translation API
                string translated = await TranslateUsingWindows(typeName);
                
                // If Windows translation failed, try Azure translation
                if (translated == typeName)
                {
                    translated = await TranslateUsingAzure(typeName);
                }

                // Cache the result
                translationCache[typeName] = translated;
                return translated;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Translation error for {typeName}: {ex.Message}");
                return typeName;
            }
        }

        private async Task<string> TranslateUsingWindows(string text)
        {
            // Get system UI language
            uint langId = (uint)GetUserDefaultUILanguage();
            StringBuilder langTag = new StringBuilder(32);
            LcidToRfc1766(langId, langTag, langTag.Capacity);
            
            // You would implement Windows translation here
            // For now, return original text
            return text;
        }

        private async Task<string> TranslateUsingAzure(string text)
        {
            // Note: In a real implementation, you would:
            // 1. Get an Azure translation API key
            // 2. Use the Azure Translation API
            // For this example, we'll use a simple mapping
            var commonTranslations = new Dictionary<string, string>
            {
                {"List", "Lista"},
                {"Dictionary", "Diccionario"},
                {"String", "Cadena"},
                {"Integer", "Entero"},
                {"Double", "Doble"},
                {"Float", "Flotante"},
                {"Boolean", "Booleano"},
                {"Array", "Matriz"},
                {"Queue", "Cola"},
                {"Stack", "Pila"},
                {"Set", "Conjunto"},
                {"Map", "Mapa"},
                {"Tree", "Arbol"},
                {"Graph", "Grafo"},
                {"Node", "Nodo"},
                {"Edge", "Borde"},
                {"File", "Archivo"},
                {"Directory", "Directorio"},
                {"Path", "Ruta"},
                {"Stream", "Flujo"},
                {"Reader", "Lector"},
                {"Writer", "Escritor"},
                {"Exception", "Excepcion"},
                {"Error", "Error"},
                {"Event", "Evento"},
                {"Handler", "Manejador"},
                {"Listener", "Oyente"},
                {"Provider", "Proveedor"},
                {"Factory", "Fabrica"},
                {"Builder", "Constructor"},
                {"Manager", "Administrador"},
                {"Controller", "Controlador"},
                {"Service", "Servicio"},
                {"Repository", "Repositorio"},
                {"Cache", "Cache"},
                {"Buffer", "Buffer"},
                {"Memory", "Memoria"},
                {"Thread", "Hilo"},
                {"Task", "Tarea"},
                {"Process", "Proceso"},
                {"System", "Sistema"},
                {"Object", "Objeto"},
                {"Class", "Clase"},
                {"Interface", "Interfaz"},
                {"Type", "Tipo"},
                {"Value", "Valor"},
                {"Key", "Clave"},
                {"Pair", "Par"},
                {"Collection", "Coleccion"},
                {"Enumerable", "Enumerable"},
                {"Comparable", "Comparable"},
                {"Converter", "Convertidor"},
                {"Formatter", "Formateador"},
                {"Validator", "Validador"},
                {"Parser", "Analizador"}
            };

            return commonTranslations.TryGetValue(text, out string translation) ? translation : text;
        }

        private string CleanTranslatedName(string name)
        {
            // Remove any non-alphanumeric characters except underscore
            var cleaned = new string(name.Where(c => char.IsLetterOrDigit(c) || c == '_').ToArray());
            
            // Ensure it starts with a letter
            if (cleaned.Length > 0 && !char.IsLetter(cleaned[0]))
            {
                cleaned = "T" + cleaned;
            }
            
            return cleaned;
        }
    }

    public class Program
    {
        static async Task Main(string[] args)
        {
            var translator = new TypeTranslator();
            Console.WriteLine("Generating Spanish type aliases...");
            await translator.GenerateSpanishTypes("TiposEspanol.cs");
            Console.WriteLine("Generation completed.");
        }
    }
}
