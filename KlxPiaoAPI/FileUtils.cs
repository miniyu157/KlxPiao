using System.Drawing.Text;
using System.Reflection;
using System.Runtime.InteropServices;

namespace KlxPiaoAPI
{
    /// <summary>
    /// 提供文件操作相关的功能。
    /// </summary>
    public class FileUtils
    {
        /// <summary>
        /// 加载指定路径或资源名称的字体。
        /// </summary>
        /// <param name="pathOrResourceName">字体文件的路径或资源名称。</param>
        /// <param name="isResource">指示是否从资源加载字体。如果为 <<c>true</c>>，则从嵌入的资源加载字体；否则从文件路径加载字体。</param>
        /// <returns>返回的 <see cref="FontFamily"/> 对象。</returns>
        /// <exception cref="Exception">指定的文件路径或资源不存在时抛出。</exception>
        public static FontFamily LoadFontFamily(string pathOrResourceName, bool isResource = false)
        {
            PrivateFontCollection privateFonts = new();

            if (isResource)
            {
                Assembly assembly = Assembly.GetExecutingAssembly();

                using Stream fontStream = assembly.GetManifestResourceStream(pathOrResourceName) ?? throw new Exception($"Resource '{pathOrResourceName}' not found.");
                byte[] fontData = new byte[fontStream.Length];
                fontStream.Read(fontData, 0, (int)fontStream.Length);

                IntPtr fontPtr = Marshal.AllocCoTaskMem(fontData.Length);
                Marshal.Copy(fontData, 0, fontPtr, fontData.Length);

                privateFonts.AddMemoryFont(fontPtr, fontData.Length);

                Marshal.FreeCoTaskMem(fontPtr);
            }
            else
            {
                if (!File.Exists(pathOrResourceName))
                {
                    throw new Exception($"File '{pathOrResourceName}' not found.");
                }

                privateFonts.AddFontFile(pathOrResourceName);
            }

            return privateFonts.Families[0];
        }
    }
}