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
    class Assembly : INotifyPropertyChanged
    {
        string _name;
        string _id;
        string _ref;
        string _asset;
        bool asset_enabled;
        ObservableCollection<Part> _parts;
        ObservableCollection<Flattag> _flattags;
        ObservableCollection<Kvtag> _kvtags;

        public Assembly()
        {
            asset_enabled = true;
            _name = "";
            _id = "";
            _ref = "";
            _asset = "";
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

        public string Asset
        {
            get { return _asset;  }
            set
            {
                _asset = value;
                RaisePropertyChanged("Asset");
            }
        }

        public ObservableCollection<Part> Parts
        {
            get { return _parts; }
            set
            {
                _parts = value;
                RaisePropertyChanged("Parts");
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
                throw new ArgumentNullException("RaiseProperty Handler is null: assemblyModel");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
