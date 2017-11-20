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



        public void buildRootNode()
        {

        }
    }
}
