using System.ComponentModel;

namespace Game
{
    public class SettingViewModel : INotifyPropertyChanged
    {
        private int _dimension;
        public int Dimension 
        { 
            get { return _dimension; } 
            set 
            {
                if(value !=_dimension)
                {
                    _dimension = value;
                    this.RaisePropertyChanged("Dimension");
                }
            } 
        }

        private bool _diaplayNumber;
        public bool DisplayNumber { get { return _diaplayNumber; }
            set
            {
                if (value != _diaplayNumber)
                {
                    _diaplayNumber = value;
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            // take a copy to prevent thread issues
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
