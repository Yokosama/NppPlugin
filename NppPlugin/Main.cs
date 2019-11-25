using Kbg.NppPluginNET.PluginInfrastructure;
using NppPlugin.Forms;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace NppPlugin
{
    public class Main
    {
        internal const string PluginName = "NppPlugin";
        static string iniFilePath = null;
        static bool someSetting = false;
        static frmMyDlg frmMyDlg = null;
        static int idMyDlg = -1;
     
        public static void CommandMenuInit()
        {
            //PluginBase.SetCommand(0, "Say hello", SayHi);
            StringBuilder sbIniFilePath = new StringBuilder(Win32.MAX_PATH);
            Win32.SendMessage(PluginBase.nppData._nppHandle, (uint)NppMsg.NPPM_GETPLUGINSCONFIGDIR, Win32.MAX_PATH, sbIniFilePath);
            iniFilePath = sbIniFilePath.ToString();
            if (!Directory.Exists(iniFilePath)) Directory.CreateDirectory(iniFilePath);
            iniFilePath = Path.Combine(iniFilePath, PluginName + ".ini");
            someSetting = (Win32.GetPrivateProfileInt("SomeSection", "SomeKey", 0, iniFilePath) != 0);

            PluginBase.SetCommand(0, "MyMenuCommand", myMenuFunction, new ShortcutKey(false, false, false, Keys.None));
            PluginBase.SetCommand(1, "MyDockableDialog", myDockableDialog); idMyDlg = 1;
        }

        internal static void myMenuFunction()
        {
            MessageBox.Show("Hello N++!");
        }

        internal static void myDockableDialog()
        {
            if (frmMyDlg == null)
            {
                frmMyDlg = new frmMyDlg();

                NppTbData _nppTbData = new NppTbData
                {
                    hClient = frmMyDlg.Handle,
                    pszName = "My dockable dialog",
                    dlgID = idMyDlg,
                    uMask = NppTbMsg.DWS_DF_CONT_RIGHT | NppTbMsg.DWS_ICONTAB | NppTbMsg.DWS_ICONBAR,
                    hIconTab = (uint)frmMyDlg.Icon.Handle,
                    pszModuleName = PluginName
                };
                IntPtr _ptrNppTbData = Marshal.AllocHGlobal(Marshal.SizeOf(_nppTbData));
                Marshal.StructureToPtr(_nppTbData, _ptrNppTbData, false);

                Win32.SendMessage(PluginBase.nppData._nppHandle, (uint)NppMsg.NPPM_DMMREGASDCKDLG, 0, _ptrNppTbData);
            }
            else
            {
                Win32.SendMessage(PluginBase.nppData._nppHandle, (uint)NppMsg.NPPM_DMMSHOW, 0, frmMyDlg.Handle);
            }
        }

        private static void SayHi()
        {
            MessageBox.Show("Hello NppPlugin by C#!");
        }

        public static void SetToolBarIcon()
        {
           
        }

        public static void PluginCleanUp()
        {
            Win32.WritePrivateProfileString("SomeSection", "SomeKey", someSetting ? "1" : "0", iniFilePath);
        }

        public static void OnNotification(ScNotification notification)
        {
        }
    }

    internal class Win32Window : IWin32Window
    {
        public IntPtr Handle { get; }

        public Win32Window(IntPtr handle)
        {
            Handle = handle;
        }
    }
}
