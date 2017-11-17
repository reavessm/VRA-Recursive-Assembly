using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLBuilder
{
    class assemblyModel : INotifyPropertyChanged
    {
        string _name;
        string _id;
        string _ref;
        SortedDictionary<string, partModel> _parts;
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
            get { return _name; }
            set
            {
                _id = value;
                RaisePropertyChanged("ID");
            }
        }

        public SortedDictionary<string, partModel> Parts
        {
            get { return _parts; }
            set
            {
                _parts = value;
                RaisePropertyChanged("Parts");
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
                throw new ArgumentNullException("RaiseProperty Handler is null: assemblyModel");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
