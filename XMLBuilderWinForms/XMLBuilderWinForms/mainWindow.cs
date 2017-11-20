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
            updateXMLTreeViewer();
            //MessageBox.Show(xmlDOM.ToString());
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
                        tempNode.Tag = element;
                        loadXmlElements(element, tempNode);
                    }
                    else
                        loadXmlElements(element, treeNode);
                }
                else
                    treeNode.Nodes.Add(element.Attribute("id").Value);

            }
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
            
        }
    }
}
