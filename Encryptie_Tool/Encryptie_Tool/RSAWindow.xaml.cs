using Encryptie_Tool.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Encryptie_Tool
{
    /// <summary>
    /// Interaction logic for RSAWindow.xaml
    /// </summary>
    public partial class RSAWindow : Window
    {
        public string folder;
        public RSAWindow()
        {
            InitializeComponent();
        }
        #region Menu

        //Config 
        public string folderAes = string.Empty;
        string folderRsa = string.Empty;
        string folderAesCipher = string.Empty;
        string folderAesPlain = string.Empty;
        string folderRsaCipher = string.Empty;
        string folderRsaPlain = string.Empty;


        //Instantiate new AES window
        private void AesWindowMenu_Click(object sender, RoutedEventArgs e)
        {
            CryptoWindows.OpenAesWindow();
        }

        //Instantiate new RSA window

        private void RsaWindowManu_Click(object sender, RoutedEventArgs e)
        {
            CryptoWindows.OpenRsaWindow();
        }

        //All functions to select an output path for the desired encryption or decryption

        private void AesFolderMenu_Click(object sender, RoutedEventArgs e)
        {
            folderAes = FolderBrowserDialogHelper.SelectFolder(folderAes);
        }

        private void RsaFolderMenu_Click(object sender, RoutedEventArgs e)
        {
            folderRsa = FolderBrowserDialogHelper.SelectFolder(folderRsa);

        }

        private void AesCipherMenu_Click(object sender, RoutedEventArgs e)
        {
            folderAesCipher = FolderBrowserDialogHelper.SelectFolder(folderAesCipher);
        }

        private void AesPlainMenu_Click(object sender, RoutedEventArgs e)
        {

            folderAesPlain = FolderBrowserDialogHelper.SelectFolder(folderAesPlain);
        }

        private void RsaPlainMenu_Click(object sender, RoutedEventArgs e)
        {

            folderRsaPlain = FolderBrowserDialogHelper.SelectFolder(folderRsaPlain);
        }

        private void RsaCipherMenu_Click(object sender, RoutedEventArgs e)
        {
            folderRsaCipher = FolderBrowserDialogHelper.SelectFolder(folderRsaCipher);
        }
        #endregion
    }
}
