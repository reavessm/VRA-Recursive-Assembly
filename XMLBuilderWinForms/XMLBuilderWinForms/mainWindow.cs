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

namespace XMLBuilderWinForms
{
    public partial class MainWindow : Form
    {
        private XDocument xmlDOM = new XDocument();
        private string filepath = "";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void XMLTreeViewer_Load(object sender, EventArgs e)
        {
            KVTagBox.Text = Application.StartupPath + "\\sample.xml";
        }

        private void openCommand_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd1 = new OpenFileDialog();
                ofd1.InitialDirectory = Application.StartupPath;
                ofd1.Title = "Open an Existing XML File";
                ofd1.DefaultExt = "xml";
                ofd1.ShowDialog();
                xmlDOM = XDocument.Load(ofd1.FileName);
                filepath = ofd1.FileName;
            }
            catch (XmlException xmlEx)
            {
                MessageBox.Show(xmlEx.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MessageBox.Show(xmlDOM.ToString());
            updateXMLTreeViewer();
        }
        
        private void updateXMLTreeViewer()
        {
            XMLTreeViewer.Nodes.Clear();
            TreeNode treeNode = XMLTreeViewer.Nodes.Add(xmlDOM.Root.Attribute("id").Value);
            loadXmlElements(xmlDOM.Root, treeNode);
            XMLTreeViewer.ExpandAll();
        }

        private void loadXmlElements(XElement xElem, TreeNode treeNode)
        {
            foreach (XElement element in xElem.Elements())
            {
                if (element.HasElements)
                {
                    if (element.FirstAttribute != null)
                    {
                        TreeNode tempNode = treeNode.Nodes.Add(element.Attribute("id").Value);
                        loadXmlElements(element, tempNode);
                    }
                    else
                        loadXmlElements(element, treeNode);
                }
                else
                    treeNode.Nodes.Add(element.Attribute("id").Value);

            }
        }

        private void saveCommand_Click(object sender, EventArgs e)
        {
            if(filepath != "") //might need to add functionality to check that file exists
            {
                xmlDOM.Save(filepath);
            }
            else
            {
                saveAsCommand_Click(sender, e);
            }
        }

        private void saveAsCommand_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "XML Doc|*.xml";
            saveFile.Title = "Save an XML File";
            saveFile.ShowDialog();
            if(saveFile.Title != "")
            {
                xmlDOM.Save(saveFile.FileName);
            }
        }

        private void quitCommand_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private IEnumerable<XElement> getXElement(string id)
        {
            var query = from elt in xmlDOM.Descendants()
                        where elt.Attribute("id").Value == id
                        select elt;
            return query;
        }

        private XElement getOneXElement(string id)
        {
            return (getXElement(id)).First();
        }

        private string getXElementType(XElement el)
        {
            return el.Name.LocalName;
        }

        private string getXElementType(string id)
        {
            return getXElementType(getOneXElement(id));
        }
    }
}
