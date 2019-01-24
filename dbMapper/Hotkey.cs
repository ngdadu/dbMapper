using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DBMapper
{
    public static class Hotkey
    {
        //System-DLL einbinden
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        [Flags]
        public enum KeyModifier
        {
            None = 0,
            Alt = 1,
            Control = 2,
            Shift = 4,
            WinKey = 8
        }

        public static bool RegisterHotKey(this Form form, int keyId, KeyModifier modifiers, Keys key)
        {
            return RegisterHotKey(form.Handle, keyId, (int)modifiers, key.GetHashCode());
        }
        public static bool UnregisterHotKey(this Form form, int keyId)
        {
            return UnregisterHotKey(form.Handle, keyId);
        }
    }
}
