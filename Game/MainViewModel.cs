using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Game
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region scores
        private int _bestscore;
        public int BestScore
        {
            get { return _bestscore; }
            set
            {
                if (_bestscore != value)
                {
                    _bestscore = value; this.RaisePropertyChanged("BestScore");
                }
            }
        }

        private int _score;
        public int Score
        {
            get
            {
                return _score;
            }
            set
            {
                if (_score != value)
                {
                    this._score = value;
                    this.RaisePropertyChanged("Score");
                }
            }
        }
        #endregion scores

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
