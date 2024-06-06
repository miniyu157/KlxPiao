using System.Reflection;

namespace KlxPiaoControls
{
    public class 关于KlxPiaoControls
    {
        public static string? 产品版本()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            AssemblyInformationalVersionAttribute? productVersion =
                (AssemblyInformationalVersionAttribute?)Attribute.GetCustomAttribute(assembly, typeof(AssemblyInformationalVersionAttribute));

            return productVersion?.InformationalVersion ?? "Unknown Version";
        }

        public static string 产品名称()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            AssemblyProductAttribute? productAttribute =
                (AssemblyProductAttribute?)Attribute.GetCustomAttribute(assembly, typeof(AssemblyProductAttribute));

            return productAttribute?.Product ?? "Unknown Product";
        }
    }
}