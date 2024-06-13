using System.Reflection;

namespace KlxPiaoControls
{
    /// <summary>
    /// 提供关于 KlxPiaoControls 的信息的方法。
    /// </summary>
    public class 关于KlxPiaoControls
    {
        public static string? 产品版本()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            AssemblyInformationalVersionAttribute? productVersion =
                (AssemblyInformationalVersionAttribute?)Attribute.GetCustomAttribute(assembly, typeof(AssemblyInformationalVersionAttribute));

            if (productVersion?.InformationalVersion is string versionStr)
            {
                var plusSymbolIndex = versionStr.IndexOf('+');
                if (plusSymbolIndex != -1)
                {
                    versionStr = versionStr[..plusSymbolIndex];
                }

                return versionStr;
            }

            return "Unknown Version";
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