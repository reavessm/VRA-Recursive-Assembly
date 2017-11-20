using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using XMLBuilder.Models;

namespace XMLBuilderWinForms
{
    class documentHandler
    {
        private XDocument xmlDOM;

        public documentHandler()
        {
            xmlDOM = new XDocument();
            buildRootNode();
        }

        public documentHandler(string fileName)
        {
            try
            {
                xmlDOM = XDocument.Load(fileName);
            }
            catch (Exception e)
            {
                throw new ArgumentException("Specified file cannot be read.", "fileName");
            }
        }

        public string getFullString()
        {
            return xmlDOM.ToString();
        }

        public IEnumerable<XElement> getAllSubelements()
        {
            IEnumerable<XElement> subelements = xmlDOM.Root.Elements("parent").Elements("parent");
            return subelements;
        }

        //private void AddNode(XmlNode inXmlNode, TreeNode inTreeNode)
        //{
        //    XmlNode xNode;
        //    TreeNode tNode;
        //    XmlNodeList nodeList;
        //    int i;

        //    // Loop through the XML nodes until the leaf is reached.
        //    // Add the nodes to the TreeView during the looping process.
        //    if (inXmlNode.HasChildNodes)
        //    {
        //        nodeList = inXmlNode.ChildNodes;
        //        for (i = 0; i <= nodeList.Count - 1; i++)
        //        {
        //            xNode = inXmlNode.ChildNodes[i];
        //            inTreeNode.Nodes.Add(new TreeNode(xNode.Name));
        //            tNode = inTreeNode.Nodes[i];
        //            AddNode(xNode, tNode);
        //        }
        //    }
        //    else
        //    {
        //        // Here you need to pull the data from the XmlNode based on the
        //        // type of node, whether attribute values are required, and so forth.
        //        inTreeNode.Text = (inXmlNode.OuterXml).Trim();
        //    }

        //}

        public void buildRootNode()
        {

        }
    }
}
