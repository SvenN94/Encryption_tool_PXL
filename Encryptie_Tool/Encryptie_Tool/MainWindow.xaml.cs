using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using OpenFileDialog = System.Windows.Forms.OpenFileDialog;
using SaveFileDialog = System.Windows.Forms.SaveFileDialog;

namespace Encryptie_Tool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AesWindowMenu_Click(object sender, RoutedEventArgs e)
        {
            AESWindow wpf = new AESWindow();
            wpf.folder = folderAes;
            wpf.ShowDialog();
        }

        private void RsaWindowManu_Click(object sender, RoutedEventArgs e)
        {
            RSAWindow wpf = new RSAWindow();
            wpf.folder = folderRsa;
            wpf.ShowDialog();
        }

        private void BtnGenAes_Click(object sender, RoutedEventArgs e)
        {
            using ( Aes aesobj = Aes.Create())
            {
                aesobj.GenerateKey();
                string keyBase64 = Convert.ToBase64String(aesobj.Key);
                aesobj.GenerateIV();
                string IvBase64 = Convert.ToBase64String(aesobj.IV);
            }
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.InitialDirectory = folderAes;
            dlg.Filter = "Text file (*.txt)|*.txt";
            dlg.ShowDialog();


        }

        private void BtnGenRsa_Click(object sender, RoutedEventArgs e)
        {


        }
        string folderAes;
        
        private void AesFolderMenu_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbp = new FolderBrowserDialog();
            DialogResult result = fbp.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                folderAes = fbp.SelectedPath;
            }
        }
        string folderRsa;
        private void RsaFolderMenu_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbp = new FolderBrowserDialog();
            DialogResult result = fbp.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                folderRsa = fbp.SelectedPath;
            }

        }
    }
}
