using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLBuilder.Models
{
    class Flattag : INotifyPropertyChanged
    {
        string _property;

        public Flattag()
        {
            _property = "";
        }

        public string Property
        {
            get { return _property; }
            set
            {
                _property = value;
                RaisePropertyChanged("Property");
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
