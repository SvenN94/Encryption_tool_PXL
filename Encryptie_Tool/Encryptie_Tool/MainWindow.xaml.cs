using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;
using OpenFileDialog = System.Windows.Forms.OpenFileDialog;
using Path = System.IO.Path;
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

        string folderAes = string.Empty;
        string folderRsa = string.Empty;

        // Creates an instance of the AESWindow, sets its folderAes property and shows the window as a dialog box.
        private void AesWindowMenu_Click(object sender, RoutedEventArgs e)
        {
            AESWindow wpf = new AESWindow();
            wpf.folderAes = folderAes; // set the folderAes property of the AESWindow
            wpf.ShowDialog(); // show the AESWindow as a dialog box
        }

        // Creates an instance of the RSAWindow, sets its folder property, and shows the window as a dialog box.
        private void RsaWindowManu_Click(object sender, RoutedEventArgs e)
        {
            RSAWindow wpf = new RSAWindow();
            wpf.folder = folderRsa; // set the folder property of the RSAWindow
            wpf.ShowDialog(); // show the RSAWindow as a dialog box
        }

        // Generates an AES key and IV, saves them to a file specified by the user and sets the folderAes property to the directory where the file was saved.
        private void BtnGenAes_Click(object sender, RoutedEventArgs e)
        {
            // Check if txtName is empty
            if (string.IsNullOrEmpty(TxtFileName.Text))
            {
                MessageBox.Show("Please enter a name for the AES key file.");
                return;
            }

            using (Aes aesobj = Aes.Create())
            {
                aesobj.GenerateKey(); // generate a random key for the AES algorithm
                byte[] key = aesobj.Key;
                byte[] iv = aesobj.IV;

                string keyBase64 = Convert.ToBase64String(key);
                string ivBase64 = Convert.ToBase64String(iv);

                string aesKeyText = keyBase64 + Environment.NewLine + ivBase64;

                using (SaveFileDialog dlg = new SaveFileDialog())
                {
                    dlg.InitialDirectory = folderAes; // set the initial directory of the file dialog to the folderAes property
                    dlg.Filter = "Text file (*.txt)|*.txt"; // set the filter to show only text files
                    dlg.FileName = TxtFileName.Text + ".txt"; // set the default file name to the value of the txtName textbox

                    if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        File.WriteAllText(dlg.FileName, aesKeyText); // write the key and IV to the selected file
                        folderAes = Path.GetDirectoryName(dlg.FileName); // set the folderAes property to the directory where the file was saved
                    }
                }
            }
        }


        // This method writes a byte array to a file with the specified file name.
        private void ByteArrayToFile(string fileName, byte[] byteArray)
        {
            FileStream fs = new FileStream(fileName + ".xml", FileMode.Create, FileAccess.ReadWrite); // create a file stream
            BinaryWriter bw = new BinaryWriter(fs, Encoding.Unicode); // create a binary writer
            bw.Write(byteArray); // write the byte array to the file
            bw.Close(); // close the binary writer
            fs.Close(); // close the file stream
        }

        private void BtnGenRsa_Click(object sender, RoutedEventArgs e)
        {
            byte[] publicKeyArray, privateKeyArray;
            // Generate RSA public and private keys
            using (RSA rsaobj = RSA.Create())
            {
                privateKeyArray = rsaobj.ExportRSAPrivateKey(); // Export the private key
                publicKeyArray = rsaobj.ExportRSAPublicKey(); // Export the public key
            }
            // Save the private key to file
            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.Title = "PrivateKey";
                dlg.InitialDirectory = folderRsa; // Set the initial directory for the dialog                               

                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    ByteArrayToFile(dlg.FileName, privateKeyArray); // Save the private key to file using the ByteArrayToFile function
                }
            }
            // Save the public key to file
            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.Title = "PublicKey";
                dlg.InitialDirectory = folderRsa; // Set the initial directory for the dialog                               

                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    ByteArrayToFile(dlg.FileName, publicKeyArray); // Save the public key to file using the ByteArrayToFile function
                }
            }
        }



        private void AesFolderMenu_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbp = new FolderBrowserDialog(); // Create a folder browser dialog
            DialogResult result = fbp.ShowDialog(); // Show the folder browser dialog and store the result
            if (result == System.Windows.Forms.DialogResult.OK) // Check if the user selected a folder
            {
                folderAes = fbp.SelectedPath; // Set the AES folder path to the selected folder path
            }
        }

        private void RsaFolderMenu_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbp = new FolderBrowserDialog(); // Create a folder browser dialog
            DialogResult result = fbp.ShowDialog(); // Show the folder browser dialog and store the result
            if (result == System.Windows.Forms.DialogResult.OK) // Check if the user selected a folder
            {
                folderRsa = fbp.SelectedPath; // Set the RSA folder path to the selected folder path
            }
        }

    }
}
