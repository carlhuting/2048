using System;
using System.ComponentModel;
using System.IO.IsolatedStorage;
using System.Reflection;

namespace Game
{
    public class PhoneSetting:INotifyPropertyChanged
    {


        private static PhoneSetting phoneInstance = new PhoneSetting();
        public static PhoneSetting GetInstance()
        {
            return phoneInstance;
        }
        public PhoneSetting()
        {

        }

        private int _cube = 4;
        [DefaultValue(4)]
        [Description("N*N 举证")]
        public int Cube
        {
            get
            {
                return _cube;
            }
            set
            {
               if(value!=_cube)
               {
                   _cube = value;
                   this.RaisePropertyChanged("Cube");
               }
            }
        }
        private bool _showNum = true;
        [DefaultValue(true)]
        public bool DisplayNumber
        {
            get
            {
                return _showNum;
            }
            set
            {
                if(value !=_showNum)
                {
                    _showNum = value;
                    this.RaisePropertyChanged("DisplayNumber");
                }
               
            }
        }

        private double tworation = 0.9;

        [DefaultValue(0.9)]
        public double TwoRation
        {
            get { return tworation; }
            set { tworation = value; }
        }

        [DefaultValue(0)]
        public int HighestSocre
        {
            get;
            set;
        }


        internal void Reset()
        {
            this.Cube = 4;
            this.tworation = 0.9;
            this.HighestSocre = 0;
            this.DisplayNumber = true;
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
