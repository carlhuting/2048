/*
 * Copyright (c) 2011 Nokia Corporation.
 */

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Game
{
    /// <summary>
    /// Represents a single cell on the board grid
    /// </summary>
    public partial class Cell : UserControl
    {
        // dependency properties so that we can use databinding in our custom control
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(int), typeof(Cell), new PropertyMetadata(OnValueChanged));
        public static readonly DependencyProperty DisplayProperty = DependencyProperty.Register("Display", typeof(bool), typeof(Cell), new PropertyMetadata(OnDisplayChanged));

        // static brushes - optimization so that we don't create them every single time
        private static SolidColorBrush whiteBrush = new SolidColorBrush(Colors.White);
        private static SolidColorBrush blackBrush = new SolidColorBrush(Colors.Black);

        #region properties


        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set {  SetValue(ValueProperty, value); }
        }
        public bool Display
        {
            get { return (bool)GetValue(DisplayProperty); }
            set { SetValue(DisplayProperty, value); }
        }
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="panel">Parent, or owner of this cell</param>
        public Cell()
        {
            InitializeComponent();
        }
        public void Play()
        {

            this.Storyboard2.Begin();
        }
        /// <summary>
        /// Called when manipulation of the cell is completed.
        /// It's fired when user takes the finger off the screen,
        /// even if the fingers have drifted away from the control.
        /// </summary>
        /// <param name="sender">Sender of the event</param>
        /// <param name="r">Event arguments</param>
        private void UserControl_ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {
            // Start the fade out animation which returns the cell's color back to original.
            //  fadeOutAnimation.Begin();

            //if (!IsPlayerSettable)
            //    SoundHelper.PlaySound(SoundHelper.SoundType.CellSelectedSound);
            //else
            //    SoundHelper.PlaySound(SoundHelper.SoundType.NumberChosenSound);
        }

        /// <summary>
        /// Called when player touches the cell.
        /// Starts the fade in animation which gradually makes the cell red.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="r">Event arguments.</param>
        private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //  fadeInAnimation.Begin();
        }

        /// <summary>
        /// Blink the cell
        /// </summary>
        public void Blink()
        {
            //blinkAnimation.Begin();
        }

        /// <summary>
        /// Event fired when our Value dependecy property change
        /// </summary>
        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var cell = d as Cell;
            var value = (int)e.NewValue;

            if (value == 0)
                cell.textValue.Text = "";
            else
            {
                cell.textValue.Text = value.ToString();
            }

        }
        private static void OnDisplayChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var cell = d as Cell;
            var value = (bool)e.NewValue;
            if (value) cell.textValue.Visibility = Visibility.Visible;
            else cell.textValue.Visibility = Visibility.Collapsed;

        }
    }
}
