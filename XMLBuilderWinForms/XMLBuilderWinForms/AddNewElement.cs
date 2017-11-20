using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using XMLBuilder;
using System.IO;
using System.Collections.ObjectModel;
using XMLBuilder.Models;

namespace XMLBuilderWinForms
{
    public partial class AddNewElement : Form
    {
        private string elementName;
        private string elementID;
        private ObservableCollection<Kvtag> kvtags;
        private string assetUri;
        private string pdfPageNums;
    }

}