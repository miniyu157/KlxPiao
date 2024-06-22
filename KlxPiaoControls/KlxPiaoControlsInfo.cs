using System.Reflection;

namespace KlxPiaoControls
{
    /// <summary>
    /// 提供关于 KlxPiaoControls 的信息的方法。
    /// </summary>
    public class KlxPiaoControlsInfo
    {
        /// <summary>
        /// 获取 KlxPiaoControls 的产品版本。
        /// </summary>
        /// <returns>产品版本。</returns>
        public static string? GetProductVersion()
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

        /// <summary>
        /// 获取 KlxPiaoControls 的产品名称。
        /// </summary>
        /// <returns>产品名称。</returns>
        public static string GetProductName()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            AssemblyProductAttribute? productAttribute =
                (AssemblyProductAttribute?)Attribute.GetCustomAttribute(assembly, typeof(AssemblyProductAttribute));

            return productAttribute?.Product ?? "Unknown Product";
        }
    }
}