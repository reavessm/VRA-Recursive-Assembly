using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
                throw new ArgumentException("Error Message: " + e.ToString(), "fileName");
            }
        }

        public string getFullString()
        {
            return xmlDOM.ToString();
        }

        public TreeNodeCollection getDOMTreeNodes()
        {
            TreeNodeCollection tn = new TreeNodeCollection();

            return tn;
        }

        public IEnumerable<XElement> getAllScenes()
        {
            IEnumerable<XElement> subelements = xmlDOM.Root.Elements("scene");
            return subelements;
        }

        public IEnumerable<IEnumerable<XElement>> getAllGroups(string scene_id)
        {
            var queryAllGroups = from scn in xmlDOM.Root.Elements("scene")
                                 where scn.Element("id").Value == scene_id
                                 select scn.Elements("group");
            return queryAllGroups;
        }

        public 

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
