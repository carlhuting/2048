using Game.Resources;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.IO;

namespace Game
{
    public partial class MainPage : PhoneApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();
            BuildLocalizedApplicationBar();

            mainmodel = new MainViewModel();
            this.DataContext = mainmodel;
            InitGame();
        }





        /// <summary>
        /// when cube Changed
        /// </summary>
        private void UpdateStatus()
        {
            this.bordGrid.Children.Clear();
            this.bordGrid.ColumnDefinitions.Clear();
            this.bordGrid.RowDefinitions.Clear();
            for (int i = 0; i < Cube; i++)
            {
                this.bordGrid.RowDefinitions.Add(new RowDefinition());
                this.bordGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            if (emptyposition == null)
                emptyposition = new List<int>(capacity);
            if (Cells == null)
                Cells = new List<Cell>(capacity);

            this.emptyposition.Clear();
            this.Cells.Clear();

            for (int i = 0; i < capacity; i++)
            {
                int positionx = i / Cube;
                int positiony = i % Cube;
                Cell cell = new Cell();
                cell.SetValue(Cell.DisplayProperty, this.DisplayNumber);
                cell.SetValue(Cell.ValueProperty, 0);
                cell.SetValue(Grid.RowProperty, positionx);
                cell.SetValue(Grid.ColumnProperty, positiony);

                this.emptyposition.Add(i);
                this.Cells.Add(cell);
                this.bordGrid.Children.Add(cell);
            }
            NewGame();
        }




        /// <summary>
        /// 矩阵包含元素的容量cube*cube
        /// </summary>
        public int capacity = 4;

        private int _cube = 0;
        /// <summary>
        /// 矩阵的维度
        /// </summary>
        public int Cube
        {
            get { return _cube; }
            set
            {
                if (_cube != value)
                {
                    _cube = value;
                    this.capacity = _cube * _cube;
                    this.UpdateStatus();
                }
            }
        }

        private bool _displayNumber;
        public bool DisplayNumber
        {
            get { return _displayNumber; }
            set
            {
                if (value != _displayNumber)
                {
                    this._displayNumber = value;
                    this.UpdateVisual();
                }
            }
        }

        private void UpdateVisual()
        {
            if (this.Cells != null)
            {
                foreach (Cell cell in this.Cells)
                {
                    cell.SetValue(Cell.DisplayProperty, this.DisplayNumber);
                }
            }
        }

        public int defaultvalue = 0;
        /// <summary>
        /// 保存空位置
        /// </summary>
        List<int> emptyposition;

        /// <summary>
        /// 当前数字卡牌集合
        /// </summary>
        List<Cell> Cells;
        /// <summary>
        /// 配置信息
        /// </summary>
        public PhoneSetting phonesetting;
        /// <summary>
        /// 主视图的视图模型
        /// </summary>
        private MainViewModel mainmodel;

        private bool isingame;

        private Random random = new Random();

        private void InitGame()
        {
            phonesetting = PhoneSetting.GetInstance();

            this.Cube = this.phonesetting.Cube;
            this.DisplayNumber = this.phonesetting.DisplayNumber;
            this.mainmodel.BestScore = this.phonesetting.HighestSocre;
           
        }

        private void NewGame()
        {
            this.isingame = true;

            this.mainmodel.Score = 0;

            this.emptyposition.Clear();

            for (int i = 0; i < capacity; i++)
            {
                this.emptyposition.Add(i);
                this.Cells[i].Value = 0;
            }

            for (int j = 0; j < 2; j++)
            {
                int index = this.random.Next(0, this.emptyposition.Count);
                int positon = this.emptyposition[index];

                this.Cells[positon].SetValue(Cell.ValueProperty, 2);
                this.emptyposition.RemoveAt(index);
            }
        }

        private bool Down()
        {
            bool flag = false; //标示是否出现移位或者合并

            for (int col = capacity - Cube; col < capacity; col++)//最后一行开始查询
            {
                for (int i = col; i >= 0; i = i - Cube)
                {
                    if (this.Cells[i].Value == 0) continue;

                    int current = i;
                    int next = current + Cube;

                    while (next <= col)
                    {
                        if (this.Cells[next].Value == 0)
                        {
                            this.Cells[next].Value = this.Cells[current].Value;
                            this.Cells[current].Value = 0;
                            this.emptyposition.Remove(next);
                            this.emptyposition.Add(current);
                            flag = flag || true;
                        }
                        else
                        {
                            if (this.Cells[current].Value == this.Cells[next].Value)
                            {
                                this.Cells[next].Value = this.Cells[current].Value * 2;
                                this.mainmodel.Score += this.Cells[next].Value;
                                this.Cells[next].Play();
                                this.Cells[current].Value = 0;
                                this.emptyposition.Remove(next);
                                this.emptyposition.Add(current);
                                flag = flag || true;
                            }
                            break;
                        }
                        current = next;
                        next = current + Cube;
                    }
                }
            }
            return flag;
        }
        private bool Up()
        {
            bool flag = false;
            for (int col = 0; col < Cube; col++)
            {

                for (int i = col; i < capacity; i = i + Cube)
                {
                    if (this.Cells[i].Value == 0) continue;

                    int current = i;
                    int next = current - Cube;

                    while (next >= 0)
                    {
                        if (this.Cells[next].Value == 0)
                        {
                            this.Cells[next].Value = this.Cells[current].Value;
                            this.Cells[current].Value = 0;
                            this.emptyposition.Remove(next);
                            this.emptyposition.Add(current);
                            flag = flag || true;
                        }
                        else
                        {
                            if (this.Cells[current].Value == this.Cells[next].Value)
                            {
                                this.Cells[next].Value = this.Cells[current].Value * 2;
                                this.mainmodel.Score += this.Cells[next].Value;
                                this.Cells[next].Play();
                                this.Cells[current].Value = 0;
                                this.emptyposition.Remove(next);
                                this.emptyposition.Add(current);
                                flag = flag || true;
                            }
                            break;
                        }
                        current = next;
                        next = current - Cube;
                    }
                }
            }
            return flag;
        }
        private bool Left()
        {
            bool flag = false;

            for (int col = capacity - Cube; col >= 0; col -= Cube)
            {
                for (int i = col; i < col + Cube; i++)
                {
                    if (this.Cells[i].Value == 0) continue;

                    int current = i;
                    int next = current - 1;

                    while (next >= col)
                    {
                        if (this.Cells[next].Value == 0)
                        {
                            this.Cells[next].Value = this.Cells[current].Value;
                            this.Cells[current].Value = 0;
                            this.emptyposition.Remove(next);
                            this.emptyposition.Add(current);
                            flag = flag || true;
                        }
                        else
                        {
                            if (this.Cells[current].Value == this.Cells[next].Value)
                            {
                                this.Cells[next].Value = this.Cells[current].Value * 2;
                                this.mainmodel.Score += this.Cells[next].Value;
                                this.Cells[next].Play();
                                this.Cells[current].Value = 0;
                                this.emptyposition.Remove(next);
                                this.emptyposition.Add(current);
                                flag = flag || true;
                            }
                            break;
                        }
                        current = next;
                        next = current - 1;
                    }
                }
            }
            return flag;
        }

        private bool Right()
        {
            bool flag = false;
            for (int col = capacity - 1; col >= 0; col -= Cube)
            {
                for (int i = col; i > col - Cube; i = i - 1)
                {
                    if (this.Cells[i].Value == 0) continue;

                    int current = i;
                    int next = current + 1;

                    while (next <= col)
                    {
                        if (this.Cells[next].Value == 0)
                        {
                            this.Cells[next].Value = this.Cells[current].Value;
                            this.Cells[current].Value = 0;
                            this.emptyposition.Remove(next);
                            this.emptyposition.Add(current);
                            flag = flag || true;
                        }
                        else
                        {
                            if (this.Cells[current].Value == this.Cells[next].Value)
                            {
                                this.Cells[next].Value = this.Cells[current].Value * 2;
                                this.mainmodel.Score += this.Cells[next].Value;
                                this.Cells[next].Play();
                                this.Cells[current].Value = 0;
                                this.emptyposition.Remove(next);
                                this.emptyposition.Add(current);
                                flag = flag || true;
                            }
                            break;
                        }
                        current = next;
                        next = current + 1;
                    }
                }
            }
            return flag;
        }

        private bool IsGameOver()
        {
            bool flag = true;
            int i, j;
            for (i = 0; i < Cube; i += 2)
            {
                int rowmin = i * Cube;
                int rowmax = (i + 1) * Cube - 1;
                for (j = 0; j < Cube; j += 2)
                {

                    int index = i * Cube + j;
                    int up = index - Cube;
                    if (up >= i && this.Cells[index].Value == this.Cells[up].Value)
                    {
                        flag = false;
                        goto exit;
                    }
                    int bottom = index + Cube;
                    if (bottom < capacity && this.Cells[index].Value == this.Cells[bottom].Value)
                    {
                        flag = false;
                        goto exit;
                    }
                    int left = index - 1;
                    if (left >= rowmin && this.Cells[index].Value == this.Cells[left].Value)
                    {
                        flag = false;
                        goto exit;
                    }
                    int right = index + 1;
                    if (right <= rowmax && this.Cells[index].Value == this.Cells[right].Value)
                    {
                        flag = false;
                        goto exit;
                    }
                }
            }
            for (i = 1; i < Cube; i += 2)
            {
                int rowmin = i * Cube;
                int rowmax = (i + 1) * Cube - 1;
                for (j = 1; j < Cube; j += 2)
                {

                    int index = i * Cube + j;
                    int up = index - Cube;
                    if (up >= i && this.Cells[index].Value == this.Cells[up].Value)
                    {
                        flag = false;
                        goto exit;
                    }
                    int bottom = index + Cube;
                    if (bottom < capacity && this.Cells[index].Value == this.Cells[bottom].Value)
                    {
                        flag = false;
                        goto exit;
                    }
                    int left = index - 1;
                    if (left >= rowmin && this.Cells[index].Value == this.Cells[left].Value)
                    {
                        flag = false;
                        goto exit;
                    }
                    int right = index + 1;
                    if (right <= rowmax && this.Cells[index].Value == this.Cells[right].Value)
                    {
                        flag = false;
                        goto exit;
                    }
                }
            }
        exit:
            return flag;
        }
        private void bordGrid_ManipulationCompleted(object sender, System.Windows.Input.ManipulationCompletedEventArgs e)
        {
            Point direction = e.TotalManipulation.Translation;
            if (!isingame || (Math.Abs(direction.X) + Math.Abs(direction.Y) < 4)) return;
            bool flag = false;
            if (Math.Abs(direction.Y) > Math.Abs(direction.X))//垂直方向
            {
                if (direction.Y > 0)//向下
                {
                    flag = flag || Down();
                }
                else//向上
                {
                    flag = flag || Up();
                }

            }
            else
            {
                if (direction.X > 0)//向右
                {
                    flag = flag || Right();
                }
                else//向左
                {
                    flag = flag || Left();
                }
            }
            if (this.emptyposition.Count > 0 && flag)
            {
                int index = random.Next(0, this.emptyposition.Count);
                int position = this.emptyposition[index];
                this.emptyposition.RemoveAt(index);
                if (random.NextDouble() > this.phonesetting.TwoRation)
                {
                    this.Cells[position].Value = 4;
                }
                else
                {
                    this.Cells[position].Value = 2;
                }

            }
            if (this.emptyposition.Count == 0)
            {
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    if (IsGameOver())
                    {
                        this.isingame = false;

                        if (mainmodel.Score > mainmodel.BestScore)
                        {
                            mainmodel.BestScore = mainmodel.Score;
                            phonesetting.HighestSocre = mainmodel.BestScore;
                        }

                        if (MessageBox.Show(AppResources.GameOver, "", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                        {

                            NewGame();
                        }
                    }
                }));
            }
        }

        private void LayoutRoot_ManipulationStarted(object sender, System.Windows.Input.ManipulationStartedEventArgs e)
        {

        }

        private void LayoutRoot_ManipulationDelta(object sender, System.Windows.Input.ManipulationDeltaEventArgs e)
        {

        }

        private void BuildLocalizedApplicationBar()
        {
            // Set the page's ApplicationBar to a new instance of ApplicationBar.
            ApplicationBar = new ApplicationBar();

            // Create a new button and set the text value to the localized string from AppResources.
            ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar3/refresh.png", UriKind.Relative));
            appBarButton.Text = AppResources.NewGameBtn;
            ApplicationBar.Buttons.Add(appBarButton);
            appBarButton.Click += ApplicationBarIconButton_Click_1;

            ApplicationBarIconButton appBarBtnSetting = new ApplicationBarIconButton(new Uri("/Assets/AppBar/feature.settings.png", UriKind.Relative));
            appBarBtnSetting.Text = AppResources.SettingBtn;
            ApplicationBar.Buttons.Add(appBarBtnSetting);
            appBarBtnSetting.Click += Settings_Click;

            ApplicationBarIconButton appBarBtnShare = new ApplicationBarIconButton(new Uri("/Assets/AppBar2/share.png", UriKind.Relative));
            appBarBtnShare.Text = AppResources.ShareBtn;
            ApplicationBar.Buttons.Add(appBarBtnShare);
            appBarBtnShare.Click += appBarBtnShare_Click;

            // Create a new menu item with the localized string from AppResources.
            ApplicationBarMenuItem appBarMenuItem =
                new ApplicationBarMenuItem(AppResources.Ratebtn);
            appBarMenuItem.Click += appBarMenuItem_Click;
            ApplicationBar.MenuItems.Add(appBarMenuItem);
        }

        void appBarMenuItem_Click(object sender, EventArgs e)
        {
            MarketplaceReviewTask marketplaceReviewTask = new MarketplaceReviewTask();
            marketplaceReviewTask.Show();
        }

        void appBarBtnShare_Click(object sender, EventArgs e)
        {

        }


        private void ApplicationBarIconButton_Click_1(object sender, EventArgs e)
        {
            NewGame();
        }

        private void Settings_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri(@"/SettingPage.xaml", UriKind.Relative));

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            this.Cube = phonesetting.Cube;
            this.DisplayNumber = phonesetting.DisplayNumber;
            this.mainmodel.BestScore = phonesetting.HighestSocre;
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

        }

    }
}