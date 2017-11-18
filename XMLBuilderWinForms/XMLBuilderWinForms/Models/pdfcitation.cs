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
    class Pdfcitation : INotifyPropertyChanged
    {
        string _id;
        string _url;
        string _pagenumber;
        ObservableCollection<string> _assets;
        ObservableCollection<Flattag> _flattags;
        ObservableCollection<Kvtag> _kvtags;

        public Pdfcitation()
        {
            _id = "";
            _url = "";
            _pagenumber = "";
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

        public string URL
        {
            get { return _url; }
            set
            {
                _url = value;
                RaisePropertyChanged("URL");
            }
        }

        public string PageNumber
        {
            get { return _pagenumber; }
            set
            {
                _pagenumber = value;
                RaisePropertyChanged("PageNumber");
            }
        }

        public ObservableCollection<string> Assets
        {
            get { return _assets; }
            set
            {
                _assets = value;
                RaisePropertyChanged("Asset");
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
                throw new ArgumentNullException("RaiseProperty Handler is null: pdfcitationModel");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
