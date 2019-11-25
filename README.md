# NppPlugin
c# notepad++ plugin template/c# notepad++插件模板

# 从新建项目开始
1、新建类库项目（名字随意），从[MarkdownViewerPlusPlus](https://github.com/nea/MarkdownViewerPlusPlus)插件源代码中把插件基础设施 (PluginInfrastructure) 目录中除了 DllExport 文件夹之外的全部代码文件包括到项目中。

2、UnmanagedExports.cs去掉 NppPlugin.DllExport 命名空间引用

3、下载3F的[DllExport](https://github.com/3F/DllExport)批处理，通过命令行加参数-action Configure执行，即DllExport -action Configure，等弹出窗口后选择Installed复选框，命名空间的下拉文本框选择System.Runtime.InteropServices，点击Apply。回到VS中重新加载项目。

4、添加System.Drawing和System.Windows.Forms引用。

5、重命名Class1.cs和类名，添加以下代码：
```
    public const string PluginName = nameof(NppPlugin);

    public static void CommandMenuInit()
    {
        PluginBase.SetCommand(0, "ShowMsgBox", ShowMsgBox);
    }

    public static void SetToolBarIcon()
    {
    }

    public static void PluginCleanUp()
    {
    }

    public static void OnNotification(ScNotification notification)
    {
    }

    private static void ShowMsgBox()
    {
        MessageBox.Show("Hello NppPlugin by C#!");
    }
```
具体代码可以看项目中的[Main.cs](https://github.com/Yokosama/NppPlugin/blob/master/NppPlugin/Main.cs)

6、编译后可在Debug目录下在找到32位和64位的dll。复制到Notepad++的plugins目录下，新建与dll相同名称的文件夹，放入dll，然后打开npp即可。

[引用博客](https://www.topcl.net/custom-technology/write-notepad-plus-plus-plugin-get-started-use-c-sharp-in-visual-studio-2017-and-correct-dllexport.html)

# Screenshot

![avatar](https://github.com/Yokosama/NppPlugin/blob/master/Screenshot/screenshot.png)
