# 更新日志

## 版本 1.2.0.7
### KlxPiaoAPI
#### 优化
- TypeInterpolator.Interpolate 取消 0-1 限制
- TransiMate.Start 方法签名
- (扩展) string.ReplaceMultiple 方法签名

### KlxPiaoControls
#### 优化
- PointBar.CoordinateDisplayFormat 支持多行
- PointBar.CoordinateDisplayFormat 忽略大小写

---

## 版本 1.2.0.6
### KlxPiaoAPI
#### 优化方法重载
- LayoutUtilities.CalculateAlignedPosition
#### 重命名类
- ControlAnimator -> TransiMate
#### 重命名方法
- ControlAnimator -> Start
- CustomTransition -> Start

### KlxPiaoControls
#### 属性类型变更
- KlxPiaoPanel.BorderSize ```int``` -> ```float```
#### 新增组件
- KlxPiaoListBox
#### 新增属性
- KlxPiaoForm.EnableTitleButtonAnimation
- KlxPiaoForm.SizeChangeRefreshMode
- KlxPiaoForm.TitleButtonCornerRadius
#### 修复
- KlxPiaoForm.BackgroundImage 不生效的问题

---

## 版本 1.2.0.5
### KlxPiaoControls
#### 修复
- RoundedButton 动画显示异常

---

## 版本 1.2.0.4
### 解决方案
#### 优化
- 部分代码优化

### KlxPiaoAPI
#### 新增类
- EasingTypeUtils (提供对于缓动效果处理的使用方法)
- AnimationInfoConverter (将 ```AnimationInfo``` 类型的对象与其他类型之间进行转换的转换器)
- ControlAnimator (控件过渡动画类，提供使用贝塞尔曲线或自定义缓动函数进行过渡动画的方法)
#### 新增结构
- AnimationInfo
#### 新增接口
- IInterpolatorStrategy (插值器策略接口，定义了插值计算的方法)
#### 接口位置迁移
- TypeChecker.ITypeCollection -> KlxPiaoAPI.ITypeCollection
#### 新增方法
- EasingTypeUtils.GetControlPoints
- EasingTypeUtils.ParseEasing
- EasingTypeUtils.IsValidControlPoint
- EasingTypeUtils.ParseControlPoints
#### 新增枚举类型
- EasingType
#### 移除类
- AnimationConverter
- ControlTransitionAnimator
- RectangleExtensions
#### 重命名类
- ControlAndPropertyUtils -> ControlObjectUtils
#### 移除结构
- Animation
#### 优化
- TypeInterpolator 的工作方式

### KlxPiaoControls
#### 优化
-  子控件的 BaseBackColor 自动跟随 ```KlxPiaoForm``` 和 ```KlxPiaoPanel``` 的背景色
#### 重命名屬性
- KlxPiaoForm.TitleBoxHeight -> TitleButtonHeight
- KlxPiaoForm.TitleBoxDragThreshold -> TitleBoxHeight
#### 移除方法
- KlxPiaoForm
#### 新增类
- RoundedButton.AnimationConfigClass
#### 移除属性
- RoundedButton.SizeAnimationConfig
- RoundedButton.ColorAnimationConfig
- RoundedButton.InteractionStyleClass.OverSize
- RoundedButton.InteractionStyleClass.DownSize
#### 新增属性
- RoundedButton.InteractionStyleClass.OverBorderSize
- RoundedButton.InteractionStyleClass.DownBorderSize
#### 修复
- RoundedButton 启动后部分属性设置无效的问题
#### 属性类型变更
- RoundedButton.BorderSize ```int``` -> ```float```

---

## 版本 1.2.0.3
### KlxPiaoControls
#### 移除组件
- BezierCurve (转移至项目 ```KlxPiaoDemo```)
- SlideSwitch (后续版本重做，已被转移到 ```\oldfiles```)
- KlxPiaoTabControl (后续版本重做，已被转移到 ```\oldfiles```)
- TabControlContainer (后续版本重做，已被转移到 ```\oldfiles```)
#### 新增属性
- KlxPiaoMessageBox.SyncedFormProperties
- KlxPiaoMessageBox.ButtonBackColor
- KlxPiaoMessageBox.ButtonForeColor

---

## 版本 1.2.0.2
### 解决方案
#### 优化
- 部分代码优化

### KlxPiaoControls
#### 新增类
- RoundedButton.DisabledStyleClass
- KlxPiaoMessageBox
#### 新增属性
- RoundedButton.DisabledStyle
#### 修复
- RoundedButton 除 BackColor 以外的属性在鼠标快速移入移出时不恢复的问题
#### 优化
- RoundedButton 交互动画部分代码逻辑

---

## 版本 1.2.0.1
### 解决方案
#### 优化
- 部分代码优化
#### 枚举类型迁移
- KlxPiaoControls.RoundedButton.FormatType -> KlxPiaoAPI.ResizeMode;

### KlxPiaoControls
#### 新增组件
- ImageBox
  > 表示一个图片框控件，支持边框和圆角等自定义外观设置。<br>
  > ```ImageBox``` 继承自 ```System.Windows.Forms.Control```<br>
  > 相较于 ```KlxPiaoPictureBox```，```ImageBox``` 提供了更加自由的图片绘制选项。
#### 新增方法
- KlxPiaoForm.RefreshTitleButtonProperties
- KlxPiaoForm.SetThemeColor
- KlxPiaoForm.GetTitleBoxHeight
- KlxPiaoForm.GetTitleBoxRectangle
- (扩展, +1 重载) Bitmap.ReplaceNonFullyTransparentPixels
- (扩展, +1 重载) Bitmap.CreateTransparentBackground
#### 移除方法
- (扩展) Color.SetBrightness
- KlxPiaoForm.SetGlobalTheme
#### 新增事件
- KlxPiaoForm.CloseButtonPaint
- KlxPiaoForm.ResizeButtonPaint
- KlxPiaoForm.MinimizeButtonPaint
- KlxPiaoForm.BackgroundPaint
- KlxPiaoPanel.BackgroundPaint
- klxPiaoLabel.BackgroundPaint
#### 新增属性
- RoundedButton.TextOffset (原以 Padding 表示偏移)
- RoundedButton.MouseClickMode
- KlxPiaoTrackBar.IsAutoComplete
- KlxPiaoForm.IconDrawOffset
- KlxPiaoForm.TitleBoxDragThreshold
- KlxPiaoTrackBar.MouseDownEventOption
- KlxPiaoTrackBar.MouseMoveEventOption
- PointBar.MouseDownEventOption
- PointBar.MouseMoveEventOption
#### 隐藏属性
- KlxPiaoForm.HelpButton
- KlxPiaoForm.ControlBox
#### 修复
- KlxPiaoForm.InteractionColorScale 属性设置后不会立即生效的问题
- PointBar 拖动时偏移
- RoundedButton 鼠标快速移入移出时属性不恢复的问题
#### 优化
- RoundedButton 图像的抗锯齿效果

### KlxPiaoAPI
#### 优化方法
- (扩展) Graphics.ConvertToRoundedPath 错误处理
#### 新增方法重载
- ImageExtensions
  ```
  Image ReplaceColor(this Image originalImage, Color oldColor, Color newColor, bool isPreserveAlpha = true)
  Bitmap.ResetImage(this Bitmap originalImage, int newWidth, int newHeight, Color? baseColor = null)
  Image.ResetImage(this Image originalImage, int newWidth, int newHeight, Color? baseColor = null)
  Bitmap.AddRounded(this Bitmap original, CornerRadius cornerRadius, Color baseColor, Color? borderColor = null, int borderSize = 0)
  Image.AddRounded(this Image original, CornerRadius cornerRadius, Color baseColor, Color? borderColor = null, int borderSize = 0)
  ```
#### 移除方法
- (扩展, +1 重载) Rectangle.ScaleRectangle
#### 新增类
- ImageLayoutUtility (提供用于计算图像布局的实用方法)

---

## 版本 1.2.0.0
### 解决方案
- 部分代码优化

### KlxPiaoControls
#### 优化
- KlxPiaoForm 部分属性设置时减少闪烁
- KlxPiaoForm.OnDeactivate 处理逻辑
- KlxPiaoLabel 投影的绘制逻辑
- KlxPiaoLabel 投影的减淡颜色由 ```#FFFFFF``` 改为 ```BackColor```
- KlxPiaoPanel 投影的绘制逻辑
- KlxPiaoPanel 投影的减淡颜色由 ```#FFFFFF``` 改为 ```BaseBackColor```
#### 修复
- KlxPiaoPanel.GetClientRectangle 偏移调整
- KlxPiaoPanel.GetClientSize 偏移调整
#### 新增方法
- KlxPiaoForm.InvalidateTitleBox
- KlxPiaoForm.RefreshTitleBoxButton
- KlxPiaoForm.ResizeButtonPerformClick
- KlxPiaoPanel.GetClientLocation
#### 移除组件
- KlxPiaoButton
- KlxPiaoTextBox
#### 新增属性
- KlxPiaoForm.TitleTextOffset
- KlxPiaoLabel.DrawTextOffset (原以 Padding 表示偏移)
- KlxPiaoLabel.IsNaturalShadowEffectEnabled
#### 重命名并转为扩展方法
- ColorProcessor.GetBrightness -> (扩展) Color.GetBrightnessForYUV
#### 方法签名变更
- KlxPiaoForm.SetGlobalTheme ```(Color? themeColor = null, bool IsApplyToControls = true)``` -> ```(Color themeColor, bool IsApplyToControls = true)```
#### 结构体构造函数签名变更
- Animation ```(int time, int fps, string? controlPoint)``` -> ```(int time, int fps, string controlPoint)```

---

## 版本 1.1.1.6
### KlxPiaoControls
#### 优化
- KlxPiaoForm 减少闪烁
#### 修复方法
- KlxPiaoForm.GetClientRectangle 偏移调整
- KlxPiaoForm.GetClientSize 偏移调整
#### 新增方法
- KlxPiaoForm.GetClientLocation
#### 移除属性
- InactiveTitleBoxForeColor (改为自动适应)
- InactiveTitleBoxBackColor
- InactiveBorderColor

---

## 版本 1.1.1.5
### KlxPiaoControls
#### 新增属性
- KlxPiaoForm.EnableShadow

---

## 版本 1.1.1.4
### KlxPiaoAPI
#### 新增方法
- (扩展) Dictionary<Tkey, TValue>.SwapDictionaryElements
- (扩展) List\<T\>.SwapListElements
- (扩展) RichTextBox.ContainsImage
- (扩展) Color.ToHex
- (扩展) Color.ToHexAlpha
- ColorProcessor.FromHex
#### 转为扩展方法
- ColorProcessor.AdjustBrightness
- ColorProcessor.SetBrightness

---

## 版本 1.1.1.3
### KlxPiaoAPI
#### 新增类
- RichTextBoxExtensions (提供 RichTextBox 用于快速操作的扩展方法)
#### 新增方法
- (扩展) RichTextBox.InsertText
- (扩展) DataUtility.TruncateStringToFitWidth
- (扩展) List\<string\>.TruncateToFitWidth
- (扩展) Bitmap.ResetImage
- (扩展) Image.ResetImage
- (扩展) Icon.ResetImage
#### 方法签名变更
- NetworkOperations.GetImageFromUrlAsync ```(string url, Size? size = null)``` -> ```(string url, string? savePath = null)```

---

## 版本 1.1.1.2
### 解决方案
- 部分代码优化

### KlxPiaoAPI
#### 新增类
- DataUtility (提供数据处理的实用工具类)
#### 新增方法
- (扩展) Control.GetControlImage
- (扩展) Bitmap.ReplaceColor
- DataUtility.MergeListsToDictionary
- NetworkOperations.TryGetHTMLContentAsync
- NetworkOperations.DownloadFileAsync
- NetworkOperations.GetImageFromUrlAsync
#### 修复
- FileUtils.LoadFontFamily 注释不显示的问题
#### 转为扩展方法
- GraphicsExtensions.ConvertToRoundedPath

### KlxPiaoControls
#### 新增事件
- KlxPiaoForm.CloseButtonClick
#### 新增方法
- RoundedButton.PerformClick
- KlxPiaoForm.CloseForm

---

## 版本 1.1.1.1
### 解决方案
- 优化部分 XML 注释
- 部分代码优化

### KlxPiaoAPI
#### 新增方法
- (扩展) object.GetPropertyType
- (扩展) object.GetPropertyInfo
#### 优化方法参数
- (扩展) Control.BezierTransition 参数 ```endValue``` 可为 null
#### 修复
- (扩展) Graphics.DrawRounded ```pen``` 会抛弃原有特性的问题

### KlxPiaoControls
#### 新增属性
- BezierCurve.ZeroToOneOffset
#### 重命名属性
- BezierCurve.ZeroToOneSizeRange -> ZeroToOneSize
#### 修复
- BezierCurve.CurveColor 失效的问题

---

## 版本 1.1.1.0
### 解决方案
- 补充 XML 注释
- 更正错误的 XML 注释
- 部分代码优化

### KlxPiaoAPI
#### 重命名方法
- LayoutUtilities.ConvertToPoint -> PaddingConvertToPoint
#### 新增结构体重载
- Animation(int time, int fps, string controlPoint)
#### 修复结构体运算符重载
- Animation == Animation 逻辑错误
#### 优化方法
- Animation.ToString
- ControlTransitionAnimator.BezierTransition 加入控制点检查 (可选参数)
#### 移除方法重载
- LayoutUtilities.CalculateAlignedPosition ```padding``` 参数的重载

### KlxPiaoControls
- 全部组件的中文属性、枚举类型、方法 -> 英文（TabControlContainer 和 KlxPiaoTabControl 决定重做，暂不更改）
#### 属性类型变更
- KlxPiaoForm.TitleTextAlign (标题位置) 的类型 ```位置``` -> ```HorizontalAlignment```
- KlxPiaoTrackBar.ValueTextDrawAlign (值显示方式) 的类型 ```文字位置``` -> ```ContentAlignment```
- KlxPiaoTrackBar.ValueTextDrawOffset (值显示边距) 的类型 ```int``` -> ```Point```
#### 新增属性
- KlxPiaoPictureBox.Font
- PointBar.CornerRadius
- PointBar.CoordinateTextOffset
- PointBar.CrosshairDrawingPriority
- KlxPiaoTrackBar.IsDrawValueText
#### 移除属性
- BezierCurve.扫描线进度
- BezierCurve.扫描线颜色
#### 优化
- KlxPiaoTrackBar.ValueTextDisplayFormat (值显示格式) 支持多行输入
- KlxPiaoTrackBar 的交互属性由 InteractionStyleClass 替代
#### 修复
- BezierCurve.拖动限制 对于键盘无效
#### 移除事件
- PointBar.值Changed
#### 新增事件
- PointBar.ValueChanged
- PointBar.ValueChangedByCode

---

## 版本 1.1.0.9
### 解决方案
- 补充 XML 注释

### KlxPiaoControls
#### 修复
- KlxPiaoTextBox.启用投影 设置为 ```true``` 时失效的问题
- KlxPiaoTextBox.圆角大小 设置为 ```0``` 时失效的问题
- KlxPiaoTextBox.边框颜色 设置为 ```199, 199, 199``` 时失效的问题
- KlxPiaoTextBox 的外观更改时嵌入的 TextBox 不会立即重置的问题
- KlxPiaoTextBox.鼠标无法选取的问题
#### 优化
- KlxPiaoPanel 内存占用
#### 新增属性
- KlxPiaoText.Text
- KlxPiaoText.TextBoxAlign
- KlxPiaoText.TextBoxOffset
- KlxPiaoText.IsFillAndMultiline

### KlxPiaoAPI
#### 新增类
- EnumUtility (提供枚举类型的扩展方法)
#### 新增方法
- EnumUtility.ForEachEnum
- EnumUtility.ReorderEnumValues
#### 优化
- AnimationConverter 的 ```ToString``` 方法 和 ```ConvertFrom``` 方法进行了优化。现在，我们不再使用固定的逗号（,）作为字段分隔符，而是使用当前文化设置的列表分隔符。这样可以确保在处理不同文化设置时，我们的程序能够正确地生成和解析数据。
#### 新增方法:
- (+1 重载) LayoutUtilities.AdjustContentAlignment
- LayoutUtilities.GetVerticalAlignment
- LayoutUtilities.GetHorizontalAlignment
#### 重载结构体运算符
- CornerRadius == CornerRadius
- CornerRadius != CornerRadius
- Animation == Animation
- Animation ！= Animation
#### 重载结构体方法
- CornerRadus.GetHashCode
- CornerRadus.Equals
- Animation.GetHashCode
- Animation.Equals

---

## 版本 1.1.0.8
### 解决方案
- 补充 XML 注释

### KlxPiaoAPI
#### 新增类
- RectangleExtensions (提供 Rectangle 扩展方法的实用工具类)
- CornerRadiusExtensions (提供 CornerRadius 扩展方法的实用工具类)
- ControlAndPropertyUtils (提供操作控件和处理对象属性的静态工具方法）
- TypeInterpolator (插值器类型，用于处理各种类型的插值操作)
#### 重命名类
- 控件 -> ControlTransitionAnimator
- 网络 -> NetworkOperations
#### 重命名委托
- 控件.自定义函数 -> CustomProgressCurve
#### 重命名方法
- (扩展, +1 重载) 控件.贝塞尔过渡动画 -> BezierTransition
- (扩展) 控件.自定义过渡动画 -> CustomTransition
- 网络.获取页面内容 -> GetHTMLContentAsync
#### 方法迁移和重命名方法；
- (扩展) 控件.遍历 -> ControlAndPropertyUtils.ForEachControl
- (扩展) 控件.SetOrGetPropertyValue -> ControlAndPropertyUtils.SetOrGetPropertyValue
#### 新增方法
- (扩展, +1 重载) Rectangle.GetInnerFitRectangle
- (扩展, +1 重载) Rectangle.ScaleRectangle
- (扩展, +5 重载) CornerRadius.ToPixel
- (+6 重载) TypeInterpolator.Interpolator
#### 重载运算符
- CornerRadius + CornerRadius
- CornerRadius * int
- CornerRadius / float

### KlxPiaoControls
#### 新增属性
- SlideSwitch.IsAnimationEnabled
- SlideSwitch.TransAnim
- SlideSwitch.ColorAnim
#### 新增组件
- KlxPiaoTextBox
#### 重命名枚举类型
- SlideSwitch.Properties -> StyleProperties
#### 修复
- SlideSwitch 滑动时使用鼠标交互会闪烁
#### 优化
- KlxPiaoForm.设置全局主题 支持 KlxPiaoPanel

### KlxPiaoControls & KlxPiaoAPI Demo
#### 新增功能
- KlxPiaoAPI.控件 新增 '生成代码'

---

## 版本 1.1.0.7
### 解决方案
- 补充 XML 注释
- 更正错误的 XML 注释

### KlxPiaoAPI
#### 重命名方法
- FileUtils.加载字体 -> LoadFontFamily
- LayoutUtilities.计算网格点 -> CalculateGridPoints
- (扩展) string.批量替换 -> ReplaceMultiple
- (扩展) string.方法参数处理 -> ProcessFirstChar
- (扩展) string.转为小写 -> ToLowerRange
- (扩展) string.转为大写 -> ToUpperRange
- (扩展) string.提取中间文本 -> ExtractBetween
- (扩展) string.提取所有中间文本 -> ExtractAllBetween
- KlxPiaoAPIInfo.产品版本 -> GetProductVersion
- KlxPiaoAPIInfo.产品名称 -> GetProductName
#### 新增方法
- (+1 重载) 控件.贝塞尔过渡动画
#### 重命名类
- 关于KlxPiaoAPI -> KlxPiaoAPIInfo
#### 新增类
- Animation (结构体，用于表示一个动画的基本组成元素)
- AnimationConverter (Animation 的转换器)

### KlxPiaoControls
#### 修复
- RoundedButton.边框颜色 不能被正确重置的问题
#### 属性迁移
- RoundedButton.交互样式.启用动画 -> RoundedButton.启用动画
#### 新增属性
- RoundedButton.颜色过渡配置
- RoundedButton.大小过渡配置
- RoundedButton.交互样式.移入大小
- RoundedButton.交互样式.按下大小
#### 重命名类
- 关于KlxPiaoControls -> KlxPiaoControlsInfo
#### 重命名方法
- KlxPiaoControlsInfo.产品版本 -> GetProductVersion
- KlxPiaoControlsInfo.产品名称 -> GetProductName

---

## 版本 1.1.0.6
### 解决方案
- 补充 XML 注释
- 更正错误的 XML 注释

### KlxPiaoAPI
#### 新增方法
- (+4 重载) LayoutUtilities.CalculateAlignedPosition
- LayoutUtilities.ConvertToPoint

### KlxPiaoControl
#### 新增属性
- BezierCurve.拖动限制
- KlxPiaoPictureBox.Text
- KlxPiaoPictureBox.TextAlign
- KlxPiaoPictureBox.TextOffset
- KlxPiaoPictureBox.ShowText
- KlxPiaoPictureBox.ForeColor
- KlxPiaoPictureBox.TextDrawPriority
#### 移除事件
- KlxPiaoTrackBar.值Changed
#### 新增事件
- KlxPiaoTrackBar.ValueChanged
- KlxPiaoTrackBar.ValueChangedByCode
#### 修复
- BezierCurve 绘制控制点时产生的偏移
#### 优化
- BezierCurve 代码优化

### KlxPiaoControls & KlxPiaoAPI Demo
#### 优化
- 属性代码生成器新增 XML 注释

---

## 版本 1.1.0.5
### 解决方案
- 补充 XML 注释
- 更正错误的 XML 注释

### KlxPiaoAPI
#### 重命名类
- 类型 -> TypeChecker
- 绘图 -> GraphicsExtensions
- 图像 -> ImageExtensions
- 颜色 -> ColorProcessor
- 文件 -> FileUtils
- 字符串 -> StringExtensions
- 实用功能 -> SystemUtils
- 实用功能.快捷方式 -> ShortcutCreator
#### 移除类
- 数学
#### 重命名方法
- 类型.判断 -> IsTypes
- 绘图.绘制圆角 -> DrawRounded
- 绘图.转为圆角路径 -> ConvertToRoundedPath
- 图像.添加圆角 -> AddRounded
- 实用功能.通知系统刷新 -> RefreshFileAssociations
#### 新增方法
- ColorProcessor.SetBrightness
- KlxPiaoForm.设置全局主题
#### 移除方法
- 图像.替换颜色
#### 转为扩展方法
- TypeChecker (object).IsTypes
#### 优化
- BezierCurve.CalculateBezierPointByTime 性能优化 (可选参数)
- BezierCurve.CalculateBezierPoint 性能优化 (可选参数)
- CornerRadius 的构造函数
- TypeChecker 中内置的 ITypeCollection
- GraphicsExtensions.DrawRounded 的 object 参数优化为重载
- 控件.贝塞尔过渡动画 代码优化
- 控件.自定义过渡动画 代码优化

### KlxPiaoControls
#### 优化
- RoundedButton 内存占用过高
- BezierCurve 的控制点绘制逻辑
#### 修复
- KlxPiaoForm.设置全局字体 容器内控件失效

---

## 版本 1.1.0.4
### 解决方案
- 修复错误的 XML 注释

### KlxPiaoAPI
#### 优化方法
- 控件.贝塞尔过渡动画
- 控件.自定义过渡动画
#### 移除方法
- 控件.设置属性
- 控件.设置属性
#### 新增方法
- (扩展) object.SetOrGetPropertyValue

### KlxPiaoControls
#### 新增属性
- BezierCurve.扫描线进度
- BezierCurve.扫描线颜色
#### 移除属性
- RoundedButton.交互样式.移入大小
- RoundedButton.交互样式.按下大小
#### 优化
- RoundedButton 的动画逻辑
- SlideSwitch 的鼠标滚轮动画
- KlxPiaoForm 启动动画逻辑

---

## 版本 1.1.0.3
### 解决方案
- 补充XML注释

### KlxPiaoAPI
#### 新增类
- LayoutUtilities (提供布局和排板计算的工具类)
#### 新增方法
- LayoutUtilities.CalculateAlignedPosition
- (扩展) 图像.添加圆角
#### 方法迁移
- 数学.计算网格点 -> LayoutUtilities.计算网格点
#### 优化
- 控件.贝塞尔过渡动画 的方法参数

### KlxPiaoControls
#### 重命名
- SlideSwitch.Attributes -> Properties
- SlideSwitch.ChangeAttributes -> ChangeProperty
- KlxPiaoPictureBox.圆角百分比 -> 圆角大小
#### 优化
- KlxPiaoPictureBox 支持自定义圆角
#### 新增组件
- RoundedButton

### KlxPiaoControls & KLxPiaoAPI Demo
#### 优化
- 属性代码生成器

---

## 版本 1.1.0.2
### 解决方案
- 补充XML注释

### KlxPiaoAPI
#### 重命名类
- 动画 -> BezierCurve
#### 优化方法
- 控件.遍历

### KlxPiaoControls
#### 新增属性
- KlxPiaoTrackBar.反向绘制
- KlxPiaoTrackBar.跟随时偏移
- KlxPiaoTrackBar.值显示方式.跟随
#### 重命名属性
- KlxPiaoLabel.圆角百分比 -> 圆角大小
#### 移除组件
- Switch
#### 新增组件
- SlideSwitch
#### 优化
- KlxPiaoTrackBar 支持竖直拖动
- KlxPiaoPanel 绘制顺序
#### 修复
- KlxPiaoPanel.圆角大小 不能正确重置的问题

---

## 版本 1.1.0.1
### KlxPiaoAPI
#### 新增方法
- (扩展)String.转换小写
- (扩展)String.转换大写
- (扩展)String.方法参数处理
- (扩展)String.ToUnicode
- (扩展)String.ToChinese
- (扩展)Graphics.绘制圆角
- 实用功能.通知系统刷新
- 绘图.转为圆角路径
- 数学.等差数列求和
- 数学.等比数列求和
- 网络.获取页面内容
#### 新增类
- RectanglePoints (提供获取矩形各个位置点的扩展方法)
- TextExtractor (用于从文本中提取子字符串的实用工具类)
- 实用功能 (提供各种实用工具方法的类)
- 实用功能.快捷方式 (用于创建快捷方式的类)
- 网络 (提供网络相关操作的功能)
#### 转为扩展方法
- String.批量替换
- Control.遍历
- Control.读取属性
- Control.设置属性
- Control.贝塞尔过渡动画
- Control.自定义过渡动画
#### 仍存在问题
- 控件 中提供的异步方法优化
- 绘图 的代码逻辑优化

### KlxPiaoControls
#### 优化
- KlxPiaoLabel 的边框绘制逻辑
- KlxPiaoPanel 的边框绘制逻辑
- KlxPiaoPictureBox 的边框绘制逻辑
- KlxPiaoTrackBar 的边框绘制逻辑
#### 新增属性
- KlxPiaoTrackBar.圆角大小
#### 重命名属性
- KlxPiaoPanel.圆角百分比 -> 圆角大小
#### 仍存在问题
- KlxPiaoButton.ImageSize 属性，反复调整时图像会模糊
- KlxPiaoTabControl 不能设计时交互
- KlxPiaoForm.ShowDialog() 方法的优化问题
- KlxPiaoTrackBar 的竖直拖动

### KlxPiaoControls & KlxPiaoAPI Demo
#### 新增功能
- 简单转换器代码生成器

---

## 版本 1.1.0.0
### 解决方案
- 取消支持 .NET Framework
- 编程语言 VB.NET Framework 4.8 -> C#.NET 8.0
- 重命名 KlxPiaoAPI -> KlxPiao

### KlxPiaoControls
#### 新增组件
- BezierCurve
#### 移除组件
- KlxPiaoProgressBar
#### 重命名组件
- KlxPiaoTabControl -> TabControlContainer
- KlxPiaoTabPage -> KlxPiaoTabControl
#### 重命名属性
- KlxPiaoForm.窗体按钮 -> 标题按钮显示
#### 新增方法
- KlxPiaoForm.设置全局字体
#### 移除方法
- KlxPiaoForm.设置WindowState
- KlxPiaoForm.自动生成主题
- KlxPiaoForm.复制主题
- KlxPiaoForm.导出主题文件
- KlxPiaoForm.加载主题文件
- TabControlContainer.设置选项卡索引
- TabControlContainer.获取选项卡索引
- TabControlContainer.获取绑定的KlxPiaoTabControl
- TabControlContainer.获取选中选项卡文字
#### 新增属性
- KlxPiaoForm.标题按钮位置
- KlxPiaoForm.标题按钮图标大小
- KlxPiaoForm.标题按钮颜色反馈
- KlxPiaoForm.标题框前景色
- KlxPiaoForm.主题
- KlxPiaoForm.未激活边框颜色
- KlxPiaoForm.未激活标题框背景色
- KlxPiaoForm.未激活标题框前景色
- KlxPiaoForm.关闭按钮的功能
- KlxPiaoForm.启动顺序
- KlxPiaoForm.启用关闭按钮
- KlxPiaoForm.启用缩放按钮
- KlxPiaoForm.启用最小化按钮
- KlxPiaoForm.标题按钮未启用前景色
- KlxPiaoButton.可获得焦点
- KlxPiaoTrackBar.值显示格式
- KlxPiaoButton.ImageSize
- PointBar.坐标显示格式
#### 移除属性
- KlxPiaoForm的大部分冗余属性
- KlxPiaoTrackBar.Text
- TabControlContainer.Text
- PointBar.Text
#### 修复
- KlxPiaoForm.ShowIcon 属性无效的问题
- KlxPiaoForm的标题按钮可被Tab键选中的问题
- KlxPiaoLabel.Padding 属性无效的问题
- KlxPiaoLabel.AutoSize 属性自动设置为false的问题
#### 优化
- KlxPiaoForm的启动和关闭动画
- KlxPiaoForm的最大化 / 还原 动画
- KlxPiaoForm的标题按钮图标不再使用贴图
#### 目前仍存在问题
- KlxPiaoButton.ImageSize 的属性反复调整时，图像会模糊
- KlxPiaoTabControl 不能设计时交互
- KlxPiaoForm.ShowDialog 方法的优化问题

### KlxPiaoAPI
#### 新增方法
- 类型.判断
- 数学.计算网格点
- 动画.CalculateBezierPointByTime
- 动画.CalculateBezierPoint
- 控件.遍历
- 控件.自定义过渡动画
- 字符串.批量替换
#### 移除方法
- 控件.据时间进度返回坐标
#### 重命名方法
- 控件.过渡动画 -> 贝塞尔过渡动画
#### 新增接口
- 类型.ITypeCollection
#### 优化
- 控件.读取属性 的代码逻辑
- 控件.设置属性 的代码逻辑
#### 修复
- 控件.过渡动画 不响应Point,Size等属性的问题

---

## 版本 1.0.3.4
#### 修复
- KlxPiaoPanel.获取工作区矩形 方法在 启用投影=False 时失效
- KlxPiaoPanel.获取工作区大小 方法在 启用投影=False 时失效
#### 新增组件
- Switch

---

## 版本 1.0.3.3

KlxPiaoLabel
- 圆角抗锯齿效果优化

PointBar
- 新增属性：坐标系类型（计算机图形坐标系，数学坐标系）
- 修复问题：键盘响应时小概率失效

KlxPiaoPanel
- 新增方法：获取工作区矩形、获取工作区大小

KlxPiaoForm
- 新增方法：获取工作区矩形、获取工作区大小
- 新增方法：显示提示框

KlxPiaoAPI Demo
- 界面更新
- 功能更新

KlxPiaoTrackBar
- 新增属性：保留小数位数
- 修复问题：最小值不为0且鼠标仅按下时值错误

---

## 版本 1.0.3.2
KlxPiaoAPI
- 优化“控件.过渡动画”的代码逻辑
- 新增方法：根据时间进度返回坐标
- 移除方法：控件.BezierCurve
- 移除方法：控件.返回曲线百分比

KlxPiaoPanel
- 新增属性：圆角百分比
- 新增属性：边框外部颜色

---

## 版本 1.0.3.1
KlxPiaoAPI
- 新增方法：控件.读取属性
- 新增方法：控件.设置属性
- 新增方法：控件.过渡动画
- 新增方法：控件.返回曲线百分比
- 新增方法：控件.BezierCurve

---

## 版本 1.0.3.0
KlxPiaoForm
- 优化启动和关闭动画
- 优化代码逻辑
- 新增属性：全屏时隐藏窗体边框

KlxPiaoTrackBar
- 新增焦点反馈、移入反馈相关属性

KlxPiaoTabControl
- 支持设计时交互
- 新增方法：设置选项卡索引、获取选项卡索引、获取绑定的KlxPiaoTabPage、获取选中选项卡文字

KlxPiaoPictureBox
- 逻辑优化
- 删除属性：图片缩放
- 新增方法：返回图像

---

## 版本 1.0.2.6
KlxPiaoForm
- 新增方法：自动生成主题

KlxPiaoAPI
- 新增方法：颜色.获取亮度

KlxPiaoAPI Demo
- 新增功能："随机生成主题"
- 更新功能："根据主题色一键生成"不再有深浅限制

---

## 版本 1.0.2.5
KlxPiaoPanel
- 默认事件修改为"Click"

PointBar
- 可以通过键盘调整大小

KlxPiaoTrackBar
- 新增焦点外观相关属性
- 新增值显示相关属性
- 新增边框相关属性

---

## 版本 1.0.2.4
KlxPiaoForm
- 新增属性：启用 启动/关闭 动画
- 修复问题：Text不及时刷新
KlxPiaoTrackBar
- 新增组件

KlxPiaoPictureBox
- 效果优化

KlxPiaoLabel
- 新增方法：返回图像
- 新增属性分类：质量
- 修复问题：设置 AutoSize=False,投影连线=False 时，投影会错位

KlxPiaoTabControl
- 修复问题：鼠标右键也能响应

---

## 版本 1.0.2.1
KlxPiaoPictureBox
- 新增组件

---

## 版本 1.0.2.0
KlxPiaoTabControl
- 修复Bug

KlxPiaoAPI Demo
- 界面优化
- 
---

## 版本 1.0.1.9
KlxPiaoLabel
- 属性名称更新：遮罩 -> 边框

KlxPiaoTabControl
- 新增组件（测试中）

KlxPiaoAPI Demo
- 新增功能：.Designer代码生成器

---

## 版本 1.0.1.7
KlxPiaoLabel
- 新增属性：遮罩

KlxPiaoAPI Demo
- 优化皮肤编辑器

KlxPiaoPanel
- 投影效果优化

---

## 版本 1.0.1.6
KlxPiaoPanel
- 投影方向支持自定义
- 优化部分代码逻辑