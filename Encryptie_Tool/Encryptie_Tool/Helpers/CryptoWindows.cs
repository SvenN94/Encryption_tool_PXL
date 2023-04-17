using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryptie_Tool.Helpers
{
    public class CryptoWindows
    {
        public static void OpenAesWindow()
        {
            AESWindow wpf = new AESWindow();
            wpf.Show();
        }

        public static void OpenRsaWindow()
        {
            RSAWindow wpf = new RSAWindow();
            wpf.Show();
        }
    }
}
