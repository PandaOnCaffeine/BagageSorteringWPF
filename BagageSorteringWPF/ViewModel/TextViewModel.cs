using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BagageSorteringWPF.ViewModel
{
    public class TextViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<string> _lines;
        //public ObservableCollection<string> Lines { get; set; }

        public ObservableCollection<string> Lines
        {
            get { return _lines; }
            set
            {
                if (value != _lines)
                {
                    _lines = value;
                    OnPropertyChanged(nameof(Lines));
                }
            }
        }

        public TextViewModel()
        {
            Lines = new ObservableCollection<string>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
