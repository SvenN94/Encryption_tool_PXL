using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Forms;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        private string aesFolderPath = "1.1.1";
        private string cipherFolderPath = string.Empty;
        private string plainFolderPath = string.Empty;
        private string selectedImageFilePath = string.Empty;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadAesKeys();
        }

        private void LoadAesKeys()
        {
            string[] files = Directory.GetFiles(aesFolderPath, "*.key", SearchOption.TopDirectoryOnly);
            foreach (string file in files)
            {
                AesListBox.Items.Add(Path.GetFileNameWithoutExtension(file));
            }
        }

        private void AesFolderMenu_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            DialogResult result = dialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                aesFolderPath = dialog.SelectedPath;
                AesListBox.Items.Clear();
                LoadAesKeys();
            }
        }

        private void CipherFolderMenu_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            DialogResult result = dialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                cipherFolderPath = dialog.SelectedPath;
            }
        }

        private void PlainFolderMenu_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            DialogResult result = dialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                plainFolderPath = dialog.SelectedPath;
            }
        }

        private void AesListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (AesListBox.SelectedItem != null)
            {
                string keyFilePath = Path.Combine(aesFolderPath, AesListBox.SelectedItem.ToString() + ".key");
                if (File.Exists(keyFilePath))
                {
                    AesKeyTextBox.Text = File.ReadAllText(keyFilePath);
                }
            }
        }

        private void BrowseImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image Files (*.bmp, *.jpg, *.png)|*.bmp;*.jpg;*.png";

            DialogResult result = dialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                selectedImageFilePath = dialog.FileName;
                ImageFilePathTextBox.Text = selectedImageFilePath;
            }
        }

        private void EncryptButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(selectedImageFilePath))
            {
                System.Windows.MessageBox.Show("Please select an image file to encrypt.");
                return;
            }

            if (string.IsNullOrEmpty(AesKeyTextBox.Text))
            {
                System.Windows.MessageBox.Show("Please select an AES key to use for encryption.");
                return;
            }

            byte[] iv = new byte[16];
            byte[] key = Encoding.UTF8.GetBytes(AesKeyTextBox.Text);

            string cipherFileName = Path.Combine(cipherFolderPath,
                Path.GetFileNameWithoutExtension(selectedImageFilePath) + ".enc");
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;

                using (FileStream input = new FileStream(selectedImageFilePath, FileMode.Open))
                using (FileStream output = new FileStream(cipherFileName, FileMode.Create))
                using (CryptoStream cs = new CryptoStream(output, aesAlg.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    byte[] buffer = new byte[4096];
                    int bytesRead;

                    while ((bytesRead = input.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        cs.Write(buffer, 0, bytesRead);
                    }
                }
            }

            System.Windows.MessageBox.Show("Encryption complete.");
        }
    }
}
