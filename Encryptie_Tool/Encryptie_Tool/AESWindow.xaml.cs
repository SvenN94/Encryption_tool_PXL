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
    /// Interaction logic for AESWindow.xaml
    /// </summary>
    public partial class AESWindow : Window
    {
        public string folder;
        public AESWindow()
        {
            InitializeComponent();
        }
        private void AesWindowMenu_Click(object sender, RoutedEventArgs e)
        {
            AESWindow wpf = new AESWindow();
            wpf.Show();
        }

        private void RsaWindowManu_Click(object sender, RoutedEventArgs e)
        {
            RSAWindow wpf = new RSAWindow();
            wpf.Show();
        }

        private void BtnGenAes_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnGenRsa_Click(object sender, RoutedEventArgs e)
        {

        }
        string folderAes;

        private void AesFolderMenu_Click(object sender, RoutedEventArgs e)
        {
            //OpenFileDialog ofd = new OpenFileDialog();
            //ofd.ValidateNames = false;
            //ofd.CheckFileExists = false;
            //ofd.CheckPathExists = true;
            //ofd.FileName = TxtFileName.Text;


        }
        string folderRsa;
        private void RsaFolderMenu_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog fbp = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = fbp.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                folderRsa = fbp.SelectedPath;
            }

        }
    }
}
