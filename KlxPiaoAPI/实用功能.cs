using System.Runtime.InteropServices;

namespace KlxPiaoAPI
{
    /// <summary>
    /// 提供各种实用工具方法的类。
    /// </summary>
    public partial class 实用功能
    {
        /// <summary>
        /// 用于创建快捷方式的类。
        /// </summary>
        /// <remarks>
        /// 初始化 <see cref="快捷方式"/> 类的新实例。
        /// </remarks>
        /// <param name="目标文件路径value">快捷方式指向的目标文件路径。</param>
        /// <param name="快捷方式路径value">快捷方式文件的保存路径。</param>
        public class 快捷方式(string 目标文件路径value, string 快捷方式路径value)
        {
            /// <summary>
            /// 获取或设置目标文件路径。
            /// </summary>
            public string 目标文件路径 { get; set; } = 目标文件路径value;

            /// <summary>
            /// 获取或设置快捷方式路径。
            /// </summary>
            public string 快捷方式路径 { get; set; } = 快捷方式路径value;

            /// <summary>
            /// 获取或设置快捷方式的起始位置。
            /// </summary>
            public string? 起始位置 { get; set; }

            /// <summary>
            /// 获取或设置快捷方式的图标路径。
            /// </summary>
            public string? 图标 { get; set; }

            /// <summary>
            /// 获取或设置快捷方式的描述。
            /// </summary>
            public string? 描述 { get; set; }

            /// <summary>
            /// 获取或设置快捷方式的快捷键（例如Ctrl+Alt+E）。
            /// </summary>
            public string? 快捷键 { get; set; }

            /// <summary>
            /// 获取或设置快捷方式的窗口样式。
            /// </summary>
            public 窗口样式? 运行方式 { get; set; }

            /// <summary>
            /// 定义快捷方式窗口样式的枚举。
            /// </summary>
            public enum 窗口样式
            {
                /// <summary>
                /// 常规窗口。
                /// </summary>
                常规窗口 = 1,

                /// <summary>
                /// 最大化窗口。
                /// </summary>
                最大化 = 3,

                /// <summary>
                /// 最小化窗口。
                /// </summary>
                最小化 = 7
            }

            private const string WScriptShellProgID = "WScript.Shell";

            /// <summary>
            /// 保存快捷方式。
            /// </summary>
            public void 创建()
            {
                try
                {
                    Type? shellType = Type.GetTypeFromProgID(WScriptShellProgID) ?? throw new InvalidOperationException("未能获取 WScript.Shell 类型。");
                    dynamic? shell = Activator.CreateInstance(shellType) ?? throw new InvalidOperationException("未能创建 WScript.Shell 实例。");
                    var shortcut = shell.CreateShortcut(快捷方式路径) ?? throw new InvalidOperationException("未能快捷方式。");

                    shortcut.TargetPath = 目标文件路径;
                    shortcut.WorkingDirectory = string.IsNullOrEmpty(起始位置) ? Path.GetDirectoryName(目标文件路径) : 起始位置;
                    shortcut.IconLocation = string.IsNullOrEmpty(图标) ? $"{目标文件路径},0" : 图标;
                    shortcut.Description = 描述;
                    shortcut.Hotkey = 快捷键;
                    shortcut.WindowStyle = 运行方式 == null ? 窗口样式.常规窗口 : 运行方式;
                    shortcut.Save();
                }
                catch (Exception ex)
                {
                    // 记录异常或采取适当的异常处理措施
                    Console.WriteLine($"快捷方式失败: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// 通知系统以反映任何更改，例如文件关联的更改。
        /// </summary>
        public static void 通知系统刷新()
        {
            const int SHCNE_ASSOCCHANGED = 0x8000000; // 文件关联已更改事件
            const int SHCNF_FLUSH = 0x1000; // 强制刷新
            SHChangeNotify(SHCNE_ASSOCCHANGED, SHCNF_FLUSH, IntPtr.Zero, IntPtr.Zero);
        }

        /// <summary>
        /// 通知系统指定的事件发生了更改，例如文件系统事件或文件关联更改。
        /// </summary>
        /// <param name="eventId">指示发生的更改事件的标识符。</param>
        /// <param name="flags">指定如何进行更改的标志。</param>
        /// <param name="item1">此参数由事件类型定义，通常是更改的对象。</param>
        /// <param name="item2">此参数由事件类型定义，通常是更改的对象。</param>
        [LibraryImport("shell32.dll")]
        private static partial void SHChangeNotify(int eventId, int flags, IntPtr item1, IntPtr item2);

    }
}