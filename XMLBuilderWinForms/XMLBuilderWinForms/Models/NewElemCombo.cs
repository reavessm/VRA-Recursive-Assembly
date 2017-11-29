using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLBuilder.Models
{
    public class NewElemCombo
    {
        public string name { get; set; }
        public IList<String> types { get; set; }
        public NewElemCombo(string _name)
        {
            types = new List<String>();
            name = _name;
        }
    }
}
