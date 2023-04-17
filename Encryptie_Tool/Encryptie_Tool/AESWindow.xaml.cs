using Encryptie_Tool.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using MessageBox = System.Windows.Forms.MessageBox;
using Path = System.IO.Path;
using Window = System.Windows.Window;

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

        #region Config

        //Folderpaths
        public string folderAes = string.Empty;
        string folderRsa = string.Empty;
        string folderAesCipher = string.Empty;
        string folderAesPlain = string.Empty;
        string folderRsaCipher = string.Empty;
        string folderRsaPlain = string.Empty;
        string selectedImageFilePath = string.Empty;
        string AESKey = string.Empty;
        string saveImageToPath = string.Empty;

        #endregion

        #region Actions


        //Creates a list of all encrypted filesnames in your current directory
        public void FillListBoxAESKeys()        
        {

            if (string.IsNullOrEmpty(folderAes))
            {
                return; // exit the function if the path is null or empty
            }

            fileLstbox.Items.Clear();
            var filenames = Directory.EnumerateFiles(folderAes, "*.txt", SearchOption.TopDirectoryOnly);
            foreach (var filename in filenames)
            {
                fileLstbox.Items.Add(filename);
            }
        }

        public void SelectImage()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image Files (*.bmp, *.jpg, *.png)|*.bmp;*.jpg;*.png"; 

            DialogResult result = dialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                selectedImageFilePath = dialog.FileName;
                txtSelectedFile.Text = selectedImageFilePath;
            }
        }


        public void SelectAESKey()
        {
            if (fileLstbox.SelectedItem != null)
            {
                string file = fileLstbox.SelectedItem.ToString();
                if (File.Exists(file));
                {
                    string fileText = File.ReadAllText(file); //Read contents from txt files
                    AESKey = fileText;
                }
                txtAESKey.Text = AESKey; 
            }

        }

        public void EncryptAes(string aesKey, string image, string saveFilePath)
        {
            string keyText = aesKey;
            string[] keyParts = keyText.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            byte[] key = Convert.FromBase64String(keyParts[0]);
            byte[] iv = Convert.FromBase64String(keyParts[1]);

            // Create a new Aes object and set its Key and IV properties
            using (Aes aesobj = Aes.Create())
            {
                aesobj.Key = key;
                aesobj.IV = iv;

                // Read the selected image file into a byte array
                byte[] imageData = File.ReadAllBytes(image);

                // Encrypt the byte array using the Aes object
                byte[] encryptedData = null;
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, aesobj.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        csEncrypt.Write(imageData, 0, imageData.Length);
                        csEncrypt.FlushFinalBlock();
                        encryptedData = msEncrypt.ToArray();
                    }
                }

                // Write the encrypted byte array to the specified file path
                File.WriteAllBytes(saveFilePath, encryptedData);
            }
        }

        public void DecryptAes(string aesKey, string encryptedImage, string saveFilePath)
        {
            string keyText = aesKey;
            string[] keyParts = keyText.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            byte[] key = Convert.FromBase64String(keyParts[0]);
            byte[] iv = Convert.FromBase64String(keyParts[1]);

            // Create a new Aes object and set its Key and IV properties
            using (Aes aesobj = Aes.Create())
            {
                aesobj.Key = key;
                aesobj.IV = iv;

                // Read the encrypted image file into a byte array
                byte[] encryptedData = File.ReadAllBytes(encryptedImage);

                // Decrypt the byte array using the Aes object
                byte[] imageData = null;
                using (MemoryStream msDecrypt = new MemoryStream(encryptedData))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, aesobj.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        imageData = new byte[encryptedData.Length];
                        int bytesRead = csDecrypt.Read(imageData, 0, imageData.Length);
                        imageData = imageData.Take(bytesRead).ToArray();
                    }
                }

                // Write the decrypted byte array to the specified file path
                File.WriteAllBytes(saveFilePath, imageData);
            }
        }

        #endregion

        #region UserLayout
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            FillListBoxAESKeys();
            if (!string.IsNullOrEmpty(folderAes))//Checks if path is empty or null, fills listbox if it's not.
            {
                saveImageToPath = folderAes; 
                txtSaveImageAt.Text = saveImageToPath;
                txtAesKeyFolder.Text = folderAes;
            }
        }


        private void btnSelectFile_Click(object sender, RoutedEventArgs e)
        {
            SelectImage();
        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            using (var folderDialog = new FolderBrowserDialog()) //Open Filedialog
            {
                if (folderDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string selectedPath = folderDialog.SelectedPath;
                    saveImageToPath = selectedPath;
                    txtSaveImageAt.Text = selectedPath; //Displays filepath in texblock
                    // save the filepath to a variable if needed
                    string saveImageFilepath = selectedPath;
                }
            }
        }

        private void btnLoadAesKeys_Click(object sender, RoutedEventArgs e)
        {
            fileLstbox.Items.Clear(); 
            using (var folderDialog = new FolderBrowserDialog())
            {
                if (folderDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string selectedPath = folderDialog.SelectedPath;
                    string[] files = Directory.GetFiles(selectedPath, "*.txt");//Create array of all files in selected directory
                    foreach (string file in files)
                    {
                        fileLstbox.Items.Add(file); //Fill listbox with .txt files
                    }
                    txtAesKeyFolder.Text = selectedPath;
                }
            }
        }

        private void fileLstbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
       
            SelectAESKey();
        }

        private void btnEncrypt_Click(object sender, RoutedEventArgs e)
        {

            //Check if image file path is empty
            if (string.IsNullOrEmpty(selectedImageFilePath))
            {
                System.Windows.MessageBox.Show("Please select an image file to encrypt.");
                return;
            }
            //Check if AESKey is empty
            if (string.IsNullOrEmpty(AESKey))
            {
                System.Windows.MessageBox.Show("Please select an AES key to use for encryption.");
                return;
            }

            //Checks for valid AESKeyfolder
            if (txtAesKeyFolder.Text == "")
            {
                System.Windows.MessageBox.Show("Please select the folder that contains your AES keys.");
            }

            //Checks for valid directory to save encrypted or decrypted image in
            if (txtSaveImageAt.Text == "")
            {
                System.Windows.MessageBox.Show("Please select the folder that you want to save your image in.");
            }
            //Encrpyt the selected image using the selected AES key and save it in the selected image destination.
            EncryptAes(AESKey, selectedImageFilePath, saveImageToPath);

            System.Windows.MessageBox.Show("Encryption succesfull.");
        }

        private void btnDecrypt_Click(object sender, RoutedEventArgs e)
        {
            //Check if image file path is empty
            if (string.IsNullOrEmpty(selectedImageFilePath))
            {
                System.Windows.MessageBox.Show("Please select an image file to encrypt.");
                return;
            }
            //Check if AESKey is empty
            if (string.IsNullOrEmpty(AESKey))
            {
                System.Windows.MessageBox.Show("Please select an AES key to use for encryption.");
                return;
            }

            //Checks for valid AESKeyfolder
            if (txtAesKeyFolder.Text == "")
            {
                System.Windows.MessageBox.Show("Please select the folder that contains your AES keys.");
            }

            //Checks for valid directory to save encrypted or decrypted image in
            if (txtSaveImageAt.Text == "")
            {
                System.Windows.MessageBox.Show("Please select the folder that you want to save your image in.");
            }
            //Encrpyt the selected image using the selected AES key and save it in the selected image destination.
            EncryptAes(AESKey, selectedImageFilePath, saveImageToPath);

            System.Windows.MessageBox.Show("Decription succesfull.");
        }

        #endregion

        #region Menu

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
