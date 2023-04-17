﻿using Encryptie_Tool.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
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

        public AESWindow()
        {
            InitializeComponent();
        }

        #region UserLayout

        private void BtnEncrypt_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnDecrypt_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion

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

        //Creates a list of all encrypted filesnames in your current directory
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var filenames = Directory.EnumerateFiles(folderAes, "*.txt", SearchOption.TopDirectoryOnly);
            foreach (var filename in filenames)
            {
                fileLstbox.Items.Add(filename);
            }
        }
        #endregion


    }

}
