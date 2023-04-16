using Microsoft.Win32;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.IO;
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
            wpf.folderAes = folderAes;
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
            string keyBase64, IvBase64;
            using ( Aes aesobj = Aes.Create())
            {
                aesobj.GenerateKey();
                keyBase64 = Convert.ToBase64String(aesobj.Key);
                aesobj.GenerateIV();
                IvBase64 = Convert.ToBase64String(aesobj.IV);
            }
            using (SaveFileDialog dlg = new SaveFileDialog()) 
            { 
                dlg.InitialDirectory = folderAes;
                dlg.Filter = "Text file (*.txt)|*.txt";                
                string AEsKey = keyBase64 + Environment.NewLine + IvBase64;           
                
                if(dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    File.WriteAllText(dlg.FileName, AEsKey);
                }
            }
        }
        private void ByteArrayToFile(string fileName, byte[] byteArray)
        {
            FileStream fs = new FileStream(fileName + ".xml", FileMode.Create, FileAccess.ReadWrite);
            BinaryWriter bw = new BinaryWriter(fs, Encoding.Unicode);
            bw.Write(byteArray);
            bw.Close();
            fs.Close();
        }

        private void BtnGenRsa_Click(object sender, RoutedEventArgs e)
        {            
            byte[] publicKeyArray, privateKeyArray;            
            using (RSA rsaobj = RSA.Create())
            {
                privateKeyArray = rsaobj.ExportRSAPrivateKey();
                publicKeyArray = rsaobj.ExportRSAPublicKey();
                
            }
            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.Title = "PrivateKey";
                dlg.InitialDirectory = folderRsa;                                

                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                   ByteArrayToFile(dlg.FileName, privateKeyArray);
                }
            }   
            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.Title = "PublicKey";
                dlg.InitialDirectory = folderRsa;                                

                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                   ByteArrayToFile(dlg.FileName, publicKeyArray);
                }
            }   

        }
       
        string folderAes = string.Empty;
        string folderRsa = string.Empty;

        private void AesFolderMenu_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbp = new FolderBrowserDialog();
            DialogResult result = fbp.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                folderAes = fbp.SelectedPath;
            }
        }
        
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
