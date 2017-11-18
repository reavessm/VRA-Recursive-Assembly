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
    class World : INotifyPropertyChanged
    {
        string _name;
        string _id;
        bool asset_enabled;
        ObservableCollection<Scene> _scenes;
        ObservableCollection<Flattag> _flattags;
        ObservableCollection<Kvtag> _kvtags;

        public World()
        {
            asset_enabled = false;
            _name = "";
            _id = "";
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

        public ObservableCollection<Scene> Scenes
        {
            get { return _scenes; }
            set
            {
                _scenes = value;
                RaisePropertyChanged("Scenes");
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
                throw new ArgumentNullException("RaiseProperty Handler Is Null in worldModel.");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
