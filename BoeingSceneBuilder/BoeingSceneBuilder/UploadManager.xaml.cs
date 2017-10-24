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
using System.IO;
using Microsoft.Win32;

namespace BoeingSceneBuilder
{
    /// <summary>
    /// Interaction logic for UploadManager.xaml
    /// </summary>
    public partial class UploadManager : Window
    {
        public UploadManager()
        {
            InitializeComponent();
        }

        private void UMWindow_Loaded(object sender, RoutedEventArgs e)
        {

            BoeingSceneBuilder.UploadedModulesDataSet uploadedModulesDataSet = ((BoeingSceneBuilder.UploadedModulesDataSet)(this.FindResource("uploadedModulesDataSet")));
            // Load data into the table Files. You can modify this code as needed.
            BoeingSceneBuilder.UploadedModulesDataSetTableAdapters.FilesTableAdapter uploadedModulesDataSetFilesTableAdapter = new BoeingSceneBuilder.UploadedModulesDataSetTableAdapters.FilesTableAdapter();
            uploadedModulesDataSetFilesTableAdapter.Fill(uploadedModulesDataSet.Files);
            System.Windows.Data.CollectionViewSource filesViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("filesViewSource")));
            filesViewSource.View.MoveCurrentToFirst();
        }

        private void UMDragFiles_Drop(object sender, DragEventArgs e)
        {

        }

        private void UMBrowse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog filePicker = new OpenFileDialog();
            filePicker.ShowDialog();
        }
    }
}
