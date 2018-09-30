using Pace.Server.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pace.Server.ViewModel
{
    public class ClientViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Client> Clients { get; set; }

        public ClientViewModel()
        {
            Clients = new ObservableCollection<Client>()
            {
                new Client()
                {
                    Identifier = "PACE-01",
                    Address = "127.0.0.1",
                    Port = 5674,
                    Username = "Bruno",
                    ComputerName = "CORE",
                    OS = "Windows 10 Home"
                }
            };
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}