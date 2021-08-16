using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsUpdateServerCatClassLibrary
{
    public class WindowsMsgHelper
    {
        public static void ShowWindowsMsg(string msg)
        {
            Task.Run(() =>
            {
                System.Windows.Forms.Form _MyForm = new System.Windows.Forms.Form();
                _MyForm.Height = 200;
                _MyForm.Width = 500;
                _MyForm.Text = "脚本提示";
                System.Windows.Forms.Label label = new System.Windows.Forms.Label()
                {
                    Text = msg,
                    Height = 100,
                    Width = 300,
                    TextAlign = System.Drawing.ContentAlignment.MiddleCenter
                };
                _MyForm.Controls.Add(label);
                System.Windows.Forms.Application.Run(_MyForm);
            });
        }
    }
}
