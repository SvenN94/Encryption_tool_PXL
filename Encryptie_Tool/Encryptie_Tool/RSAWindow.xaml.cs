﻿using System;
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
        
        private void AesFolderMenu_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbp = new FolderBrowserDialog();
            DialogResult result = fbp.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                folderAes = fbp.SelectedPath;
            }
        }

        string folderAes = string.Empty;
        string folderRsa = string.Empty;
        string folderAesCipher = string.Empty;
        string folderAesPlain = string.Empty;
        string folderRsaCipher = string.Empty;
        string folderRsaPlain = string.Empty;

        private void RsaFolderMenu_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbp = new FolderBrowserDialog();
            DialogResult result = fbp.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                folderRsa = fbp.SelectedPath;
            }

        }

        private void AesCipherMenu_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbp = new FolderBrowserDialog();
            DialogResult result = fbp.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                folderAesCipher = fbp.SelectedPath;
            }
        }

        private void AesPlainMenu_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbp = new FolderBrowserDialog();
            DialogResult result = fbp.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                folderAesPlain = fbp.SelectedPath;
            }
        }

        private void RsaPlainMenu_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbp = new FolderBrowserDialog();
            DialogResult result = fbp.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                folderRsaPlain = fbp.SelectedPath;
            }
        }

        private void RsaCipherMenu_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbp = new FolderBrowserDialog();
            DialogResult result = fbp.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                folderRsaCipher = fbp.SelectedPath;
            }
        }

        private void BtnEncrypt_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnDecrypt_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SlctPrivateKeyBtn_Click(object sender, RoutedEventArgs e)
        {
            string path = privateLstb.SelectedValuePath;
        }

        private void SlctPublicKeyBtn_Click(object sender, RoutedEventArgs e)
        {

        }

       private void Getfile(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    using(RSA rsa = RSA.Create())
                    {

                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var filenames = Directory.EnumerateFiles(folderAes, "*.txt", SearchOption.TopDirectoryOnly);
            foreach (var filename in filenames)
            {
                if (filename.Contains("Private"))
                {
                    privateLstb.Items.Add(filename);
                }
                else if (filename.Contains("Public"))
                {
                    publicLstb.Items.Add(filename);
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Geen folder geselecteerd!");
                }
            }
        }
    }
}
