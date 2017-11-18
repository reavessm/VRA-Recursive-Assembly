using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLBuilder.Models
{
    [Serializable()]
    class pdfcitationModel :INotifyPropertyChanged
    {
        string _id;
        string _url;
        string _pagenumber;
        SortedDictionary<string, string> _asset;
        SortedDictionary<int, string> _flattags;
        SortedDictionary<string, string> _kvtags;

        public string ID
        {
            get { return _id; }
            set
            {
                _id = value;
                RaisePropertyChanged("ID");
            }
        }

        public SortedDictionary<string, string> Asset
        {
            get { return _asset; }
            set
            {
                _asset = value;
                RaisePropertyChanged("Asset");
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
                throw new ArgumentNullException("RaiseProperty Handler is null: pdfcitationModel");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
