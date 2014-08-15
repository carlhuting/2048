using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml;
using System.Text;
using System.IO;

namespace Game
{
    public partial class SettingPage : PhoneApplicationPage
    {
        private PhoneSetting phonesetting;
        public SettingPage()
        {
            InitializeComponent();

            phonesetting = PhoneSetting.GetInstance();
            this.ContentPanel.DataContext = phonesetting;
        }


        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            NavigationService.GoBack();
        }

        private void ApplicationBarIconButton_Save(object sender, EventArgs e)
        {       
            NavigationService.GoBack();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
           
           
        }

        private void cube_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Slider slider = sender as Slider;
            if(null!=slider)
            {
                slider.Value = (int)slider.Value;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.phonesetting.Reset();
        }


    }
}