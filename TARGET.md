## 目标

- 重写 Demo 程序
- 重写 KlxPiaoTabControl 以及外壳
- 修复 SlideSwitch 某些默认属性不生效的 bug
- 修复 RoundedButton 的各种问题
  > - 当 ```InteractionStyle``` 中设置了 ```Down```，未设置 ```Over``` 属性时<br>
  >   若在弹起动画进行中移出鼠标，则会被恢复为弹起时 (``` Down ```) 的属性<br>
  >   这是因为移出时检测 ```Over``` 属性为空，则不会执行恢复操作（没有移出动画来打断弹起动画，所以就被恢复为弹起时 (``` Down ```) 的属性了）<br>
  >   解决办法：把 ```Over``` 属性设置为未交互时的属性 (<s>作者还未想出更好的解决方案，只能先这样了</s>)<br>
  > - 若已经存储原始值，再次修改属性将会无效 (这应该得重写存储原始值的逻辑，再加上动画完成后就可以能设置属性才能彻底解决)
  > - Demo 程序的 roundedButton2 鼠标弹起有时属性恢复为深灰色 (应该恢复为移入时的背景色)，还未找到原因
  > - 颜色动画会终止大小动画 (不知道当 bug 修了还是保留为特性更好)
  > - 重写存储原始值的逻辑之后，顺便把 InteractionStyleClass 加一个边框大小的属性

以优化 KlxPiaoForm (可以考虑改个名字) 为主，小控件做不好就不要做了(<br>
例如 1.2.0.0 移除的 KlxPiaoText，和套上圆角 Panel 效果是一样的

[返回 README](/README.md)

![target](screenshot/target.gif)