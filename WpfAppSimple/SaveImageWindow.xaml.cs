using Aspose.Imaging.ImageOptions;
using Aspose.Imaging;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfAppSimple.Models;
using System.IO;
using LibDatabase.Helpers;
using System.Drawing.Imaging;
using System.Drawing;
using System.Security.Policy;
using WpfAppSimple.Helpers;

namespace WpfAppSimple
{
    /// <summary>
    /// Interaction logic for SaveImageWindow.xaml
    /// </summary>
    public partial class SaveImageWindow : Window
    {
        public SaveImageWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == true)
            {
                string exp = System.IO.Path.GetExtension(dlg.FileName);
                //if(exp ==".webp")
                //{
                //    MessageBox.Show("Формат не підтримується!");
                //    return;
                //}
                string inputFile = dlg.FileName;
                inputImage.Source = UserVM.toBitmap(File.ReadAllBytes(inputFile));

                try
                {
                    using (MemoryStream stream = new MemoryStream(File.ReadAllBytes(inputFile)))
                    {
                        var img = System.Drawing.Image.FromStream(stream);
                        Bitmap bmp = new Bitmap(img);
                        string fileName = System.IO.Path.GetRandomFileName() + ".jpeg";
                        string[] sizes = { "32", "100", "300", "600", "1200" };
                        foreach (string size in sizes)
                        {
                            int width = int.Parse(size);
                            var saveBmp = ImageWorker.CompressImage(bmp, width, width, false);
                            saveBmp.Save($"{MyAppConfig.GetSectionValue("FolderSaveImages")}/{size}_{fileName}",
                                ImageFormat.Jpeg);
                        }
                        outImage.Source= UserVM.toBitmap(File.ReadAllBytes($"{MyAppConfig.GetSectionValue("FolderSaveImages")}/{300}_{fileName}"));
                    }
                }
                catch(Exception ex) 
                {
                    MessageBox.Show(ex.ToString()); 
                }
            }
        }
    }
}
