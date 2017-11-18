using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMLBuilder.Models;

namespace XMLBuilder.Models
{
    [Serializable()]
    class Scene : INotifyPropertyChanged
    {
        string _name;
        string _id;
        string _ref;
        bool asset_enabled;
        ObservableCollection<Group> _groups;
        ObservableCollection<Flattag> _flattags;
        ObservableCollection<Kvtag> _kvtags;

        public Scene()
        {
            asset_enabled = false;
            _name = "";
            _id = "";
            _ref = "";
        }

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

        public string Ref
        {
            get { return _ref; }
            set
            {
                _ref = value;
                RaisePropertyChanged("Ref");
            }
        }

        public ObservableCollection<Group> Groups
        {
            get { return _groups; }
            set
            {
                _groups = value;
                RaisePropertyChanged("Groups");
            }
        }

        public ObservableCollection<Flattag> FlatTags
        {
            get { return _flattags; }
            set
            {
                _flattags = value;
                RaisePropertyChanged("FlatTags");
            }
        }

        public ObservableCollection<Kvtag> KVTags
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
                throw new ArgumentNullException("RaiseProperty Handler is null: sceneModel");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
