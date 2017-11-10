using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLBuilder
{
    class worldModel : INotifyPropertyChanged
    {
        string _name;
        string _id;
        SortedDictionary<string, sceneModel> _scenes;
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
                RaisePropertyChanged("ID")
            }
        }

        public SortedDictionary<string, sceneModel> Scenes
        {
            get { return _scenes }
            set
            {
                _scenes = value;
                RaisePropertyChanged("Scenes")
            }
        }

        public SortedDictionary<int, string> FlatTags
        {
            get { return _flattags }
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
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
