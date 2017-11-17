using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLBuilder
{
    class groupModel : INotifyPropertyChanged
    {
        string _name;
        string _id;
        string _ref;
        SortedDictionary<string, assemblyModel> _assemblies;
        SortedDictionary<int, string> _flattags;
        SortedDictionary<string, string> _kvtags;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged("Name");
            }
        }

        public string ID
        {
            get { return _id; }
            set
            {
                _id = value;
                RaisePropertyChanged("ID");
            }
        }

        public SortedDictionary<string, assemblyModel> Assemblies
        {
            get { return _assemblies; }
            set
            {
                _assemblies = value;
                RaisePropertyChanged("Assemblies");
            }
        }

        public SortedDictionary<int, string> FlatTags
        {
            get { return _flattags; }
            set
            {
                _flattags = value;
                RaisePropertyChanged("FlatTags");
            }
        }

        public SortedDictionary<string, string> KVTags
        {
            get { return _kvtags; }
            set
            {
                _kvtags = value;
                RaisePropertyChanged("KVTags");
            }
        }

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
            else
            {
                throw new ArgumentNullException("RaiseProperty Handler is null: groupModel");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
