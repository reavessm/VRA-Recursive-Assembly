using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLBuilder.Models
{
    class Kvtag : INotifyPropertyChanged
    {
        string _key;
        string _value;

        public Kvtag()
        {
            _key = "";
            _value = "";
        }

        public string Key
        {
            get { return _key; }
            set
            {
                _key = value;
                RaisePropertyChanged("Key");
            }
        }

        public string Value
        {
            get { return _value; }
            set
            {
                _value = value;
                RaisePropertyChanged("Value");
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
                throw new ArgumentNullException("RaiseProperty Handler is null: kvtagModel");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
