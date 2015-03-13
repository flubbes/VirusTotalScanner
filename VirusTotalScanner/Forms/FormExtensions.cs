using System;
using System.Windows.Forms;

namespace VirusTotalScanner.Forms
{
    public static class FormExtensions
    {
        public static void HandleInvoke(this Form form, Action action)
        {
            if (form.IsDisposed)
            {
                return;
            }
            try
            {
                if (form.InvokeRequired)
                {
                    form.Invoke(new MethodInvoker(action));
                }
                else
                {
                    action.Invoke();
                }
            }
            catch
            { }
        }
    }
}
