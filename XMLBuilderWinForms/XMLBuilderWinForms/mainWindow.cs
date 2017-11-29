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
        private XElement currentSelection = null;
        private string filepath = "";

        public MainWindow()
        {
            InitializeComponent();
            newXmlDOM();
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
                currentSelection = xmlDOM.Root;
                updateXMLTreeViewer();

            }
            catch (XmlException xmlEx)
            {
                MessageBox.Show(xmlEx.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void updateXMLTreeViewer()
        {
            try
            {
                XMLTreeViewer.Nodes.Clear();
                TreeNode treeNode = XMLTreeViewer.Nodes.Add(xmlDOM.Root.Attribute("id").Value);
                loadXmlElements(xmlDOM.Root, treeNode);
                XMLTreeViewer.ExpandAll();
            }
            catch(Exception e)
            {
                throw new Exception("The xml DOM is not valid");
            }
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
            updateXMLTreeViewer();
        }

        private void saveAsCommand_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "XML Doc|*.xml";
            saveFile.Title = "Save an XML File";
            saveFile.ShowDialog();
            if(saveFile.FileName != "")
            {
                xmlDOM.Save(saveFile.FileName);
            }
            updateXMLTreeViewer();
        }

        private void quitCommand_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private IEnumerable<XElement> getXElement(string id)
        {
            try
            {
                var query = from elt in xmlDOM.Descendants()
                            where (string)elt.Attribute("id") == id
                            select elt;
                return query;
            }
            catch (XmlException xe)
            {
                throw new Exception("Can't Find Element!");
            }
        }

        private XElement getOneXElement(string id)
        {
            try
            {
                return (getXElement(id)).First();
            }
            catch (XmlException xe)
            {
                throw new Exception("Can't Find Element!");
            }
        }

        private string getXElementType(XElement el)
        {
            return el.Name.LocalName;
        }

        private string getXElementType(string id)
        {
            return getXElementType(getOneXElement(id));
        }

        private void XMLTreeViewer_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                currentSelection = getOneXElement(XMLTreeViewer.SelectedNode.Text);
                nameTextBox.Text = (string) currentSelection.Attribute("name");
                idTextBox.Text = (string) currentSelection.Attribute("id");
                unityRefTextBox.Text = (string) currentSelection.Attribute("ref");
                assetTextBox.Text = (string) currentSelection.Attribute("asset");
                pdfTextBox.Text = (string) currentSelection.Attribute("pagenumber");
                KVTagBox.Text = "Placeholder Text";

            }
            catch (XmlException xe)
            {

            }

        }

        private void newSiblingButton_Click(object sender, EventArgs e)
        {
            if(currentSelection.Parent != null)
            {
                XElement tempElement;
                AddNewElement newWindow = new AddNewElement();
                var result = newWindow.ShowDialog();
                if (result == DialogResult.OK)
                {
                    tempElement = newWindow.returnElement;
                    currentSelection.Parent.Add(tempElement);
                    updateXMLTreeViewer();
                }
            }
            else
            {
                MessageBox.Show("Cannot add a sibling to root");
            }
        }

        private void newChildBtn_Click(object sender, EventArgs e)
        {
            XElement tempElement;
            AddNewElement newWindow = new AddNewElement();
            var result = newWindow.ShowDialog();
            if (result == DialogResult.OK)
            {
                tempElement = newWindow.returnElement;
                currentSelection.Add(tempElement);
                updateXMLTreeViewer();
            }
        }

        private void newCommand_Click(object sender, EventArgs e)
        {
            newXmlDOM();
        }

        private void newXmlDOM()
        {
            xmlDOM = new XDocument(new XElement("world",
                       new XAttribute("name", "name"),
                       new XAttribute("id", "world"),
                       new XAttribute("ref", "ref")));
            updateXMLTreeViewer();
            filepath = "";
        }

        private void nameTextBox_Leave(object sender, EventArgs e)
        {
            currentSelection.SetAttributeValue("name", nameTextBox.Text);
        }

        private void idTextBox_Leave(object sender, EventArgs e)
        {
            currentSelection.SetAttributeValue("id", idTextBox.Text);
            updateXMLTreeViewer();
        }

        private void unityRefTextBox_Leave(object sender, EventArgs e)
        {
            currentSelection.SetAttributeValue("ref", unityRefTextBox);
        }
        
    }
}
