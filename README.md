# KlxPiao

提供 WinForms 原版美化和一些小控件

> [可以点击这里查看更新日志](/CHANGELOG.md)

> [(未完成的目标)](/TARGET.md)

> ![1206](/screenshot/1206.png)
> 1.2.0.6 实现标题按钮透明 [<u>Demo.exe</u>](https://github.com/miniyu157/KlxPiao/releases/download/v1.2.0.6/KlxPiaoDemo.exe)

> Demo 程序重做未完成，以下截图截止到版本 1.2.0.0

## 欢迎使用 KlxPiaoControls & KlxPiaoAPI !

主要针对于原版强化进行创作，目前 KlxPiaoControl 仍在不断优化中。

```KlxPiaoForm``` 实际上是继承自 ```Form``` 的WinForms 无边框窗体 (FormBorderStyle = None)，所以任何元素都是可以接管的

## 简介

解决方案提供了两个项目
- KlxPiaoControls:<br>
  提供原版增强的控件，创造这个控件库的初衷是为了能有一个原版增强的 Form<br>

  **安装和使用:** <br>
  - 安装
	> ```
	> Install-Package KlxPiaoControls
	> ```
  - 使用
    > 
    > C#
    > ```
    > public partial class MainForm : KlxPiaoControls.KlxPiaoForm
    > ```
    > VB.NET (.Designer 文件中修改继承)
    > ```
    > Inherits KlxPiaoControls.KlxPiaoForm
    > ```
  
  ![home](screenshot/home.png)

  其中标题框高度、标题框颜色、标题按钮大小以及样式等等，都是作为属性可以调整的<br>
  标题按钮目前内置了两种风格 ```Windows``` 和 ```Mac``` (标题按钮可以设置为左边仿 MacOS)

  ![formproperties](screenshot/formproperties.png)

  控件库也提供了一些原版增强的控件，大部分都添加了圆角边框的属性

  ![roundedpicturebox](screenshot/roundedpicturebox.png)

  ![panel](screenshot/panel.png)

- KlxPiaoAPI:<br>
  创造 **KlxPiaoControl** 时产生的工具类库<br>
  提供了各种类型的扩展方法以及实用方法，例如数据处理、图片处理、布局计算、GDI+快速绘制圆角等
   
    > ```
    > Install-Package KlxPiaoAPI
    > ```

## 预览

![themeedit](screenshot/themeedit.png)

![label](screenshot/label.png)

![roundedbutton](screenshot/roundedbutton.png)

![slideswitch](screenshot/slideswitch.png)

![api_control](screenshot/api_control.png)