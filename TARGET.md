## 目标

- 重做 Demo 程序
- 重做 KlxPiaoTabControl 以及外壳
- 修复 SlideSwitch 某些默认属性不生效的 bug
- 在 1.2.0.4 中，因为重写了 RoundedButton 交互样式动画的逻辑，新的逻辑暂时无法适配 ```Size``` 属性，所以暂时移除相关属性
- 组件首次创建或父容器背景色改变时，```BaseBackColor``` 属性不能自动适应
- KlxPiaoTrackBar 交互时也要加入动画相关属性
- 所有组件的边框大小类型都要改为 ```float```

以优化 KlxPiaoForm (可以考虑改个名字) 为主，小控件做不好就不要做了(<br>
例如 1.2.0.0 移除的 KlxPiaoText，和套上圆角 Panel 效果是一样的

[返回 README](/README.md)

![target](screenshot/target.gif)