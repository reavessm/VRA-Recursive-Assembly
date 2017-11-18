using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Serialization;
using XMLBuilder.Models;
using XMLBuilder.ViewModels.Commands;

namespace XMLBuilder.ViewModels
{
    class worldViewModel
    {
        private ObservableCollection<worldModel> _world;

        public worldViewModel(string xml_file_path)
        {
            _world = new ObservableCollection<worldModel>();
            TextReader file_txt = new StreamReader(xml_file_path);
            XmlSerializer world_deserializer = new XmlSerializer(typeof(worldModel));
            _world.Add((worldModel) world_deserializer.Deserialize(file_txt));
            file_txt.Close();
        }

        public ObservableCollection<worldModel> World
        {
            get { return _world;  }
            set
            {
                _world = value;
            }
        }

        private ICommand modelUpdater;
        public ICommand UpdateCommand
        {
            get
            {
                if (modelUpdater == null)
                {
                    modelUpdater = new UpdaterCommand(() => );
                }
                return modelUpdater;
            }
            set { modelUpdater = value;  }
        }
    }
}
