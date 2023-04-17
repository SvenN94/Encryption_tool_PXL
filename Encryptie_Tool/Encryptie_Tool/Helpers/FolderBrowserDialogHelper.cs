using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Encryptie_Tool.Helpers
{
    public static class FolderBrowserDialogHelper
    {
        public static string SelectFolder(string folder)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                fbd.SelectedPath = folder;

                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    return fbd.SelectedPath;
                }
            }

            return null; // or throw an exception
        }
    }
}

