using System.Runtime.InteropServices;

namespace KlxPiaoAPI
{
    /// <summary>
    /// 提供与系统操作相关的实用方法和类。
    /// </summary>
    public partial class SystemUtils
    {
        /// <summary>
        /// 用于创建快捷方式的类。
        /// </summary>
        /// <remarks>
        /// 初始化 <see cref="ShortcutCreator"/> 类的新实例。
        /// </remarks>
        /// <param name="targetFile">快捷方式指向的目标文件路径。</param>
        /// <param name="output">快捷方式文件的保存路径。</param>
        public class ShortcutCreator(string targetFilePath, string outputPath)
        {
            /// <summary>
            /// 获取或设置目标文件路径。
            /// </summary>
            public string TargetFilePath { get; set; } = targetFilePath;

            /// <summary>
            /// 获取或设置快捷方式路径。
            /// </summary>
            public string OutputPath { get; set; } = outputPath;

            /// <summary>
            /// 获取或设置快捷方式的起始位置。
            /// </summary>
            public string? WorkingDirectory { get; set; } = null;

            /// <summary>
            /// 获取或设置快捷方式的图标路径。
            /// </summary>
            public string? IconLocation { get; set; } = null;

            /// <summary>
            /// 获取或设置快捷方式的描述。
            /// </summary>
            public string? Description { get; set; } = null;

            /// <summary>
            /// 获取或设置快捷方式的快捷键（例如Ctrl+Alt+E）。
            /// </summary>
            public string? Hotkey { get; set; } = null;

            /// <summary>
            /// 获取或设置快捷方式的窗口样式。
            /// </summary>
            public FormWindowState? WindowState { get; set; } = null;

            private const string WScriptShellProgID = "WScript.Shell";

            /// <summary>
            /// 创建ShortcutCreator的实例并保存快捷方式。
            /// </summary>
            /// <param name="targetFilePath">快捷方式指向的目标文件路径。</param>
            /// <param name="outputPath">快捷方式文件的保存路径。</param>
            public static void Save(string targetFilePath, string outputPath, string? IconLocation = null, string? Description = null)
            {
                var creator = new ShortcutCreator(targetFilePath, outputPath)
                {
                    IconLocation = IconLocation,
                    Description = Description
                };
                creator.Save();
            }

            /// <summary>
            /// 保存快捷方式。
            /// </summary>
            public void Save()
            {
                try
                {
                    Type? shellType = Type.GetTypeFromProgID(WScriptShellProgID) ?? throw new InvalidOperationException($"未能获取 {WScriptShellProgID} 类型。");
                    dynamic? shell = Activator.CreateInstance(shellType) ?? throw new InvalidOperationException($"未能创建 {WScriptShellProgID} 实例。");
                    var shortcut = shell.CreateShortcut(OutputPath) ?? throw new InvalidOperationException("未能创建快捷方式的实例。");

                    shortcut.TargetPath = TargetFilePath;
                    shortcut.WorkingDirectory = string.IsNullOrEmpty(WorkingDirectory) ? Path.GetDirectoryName(TargetFilePath) : WorkingDirectory;
                    shortcut.IconLocation = string.IsNullOrEmpty(IconLocation) ? $"{TargetFilePath},0" : IconLocation;
                    shortcut.Description = Description;
                    shortcut.Hotkey = Hotkey;
                    shortcut.WindowStyle = WindowState switch
                    {
                        FormWindowState.Normal => 1,
                        FormWindowState.Maximized => 3,
                        FormWindowState.Minimized => 7,
                        _ => 1
                    };
                    shortcut.Save();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"快捷方式失败: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// 通知系统文件关联或系统图标缓存已更改，以反映这些更改。
        /// </summary>
        public static void RefreshFileAssociations()
        {
            const int SHCNE_ASSOCCHANGED = 0x8000000;   //文件关联已更改事件
            const int SHCNF_FLUSH = 0x1000;             //强制刷新

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