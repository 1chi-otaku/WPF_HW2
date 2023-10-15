using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Homework_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int seconds;
        private DispatcherTimer timer = new DispatcherTimer();
        List<int> valuesList= new List<int>();
        public MainWindow()
        {
            InitializeComponent();
            DisableButtons();
            seconds = 60;
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (progressBar.Value != 0)
            {
                progressBar.Value--;
            }
            else
            {
                timer.Stop();
                NewGameButton.IsEnabled = true;
                TimerTextBox.IsEnabled = true;
                MessageBox.Show("You Lost!");
                DisableButtons();
            }
        }

        public void DisableButtons()
        {
            foreach (Button button in uniformGrid.Children)
            {
                button.IsEnabled = false;
                button.Content = "0";
            }
        }
        public void EnableButtins()
        {
            foreach (Button button in uniformGrid.Children)
            {
                button.IsEnabled = true;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(int.TryParse(TimerTextBox.Text, out seconds)){
                NewGameButton.IsEnabled = false;
                TimerTextBox.IsEnabled = false;
                seconds = Convert.ToInt32(TimerTextBox.Text);
                progressBar.Maximum = seconds;
                progressBar.Value = seconds;
                timer.Start();
                valuesList.Clear();
                EnableButtins();
                FillButtons();
            }
            else
            {
                MessageBox.Show("Wrong input in text box!");
            }
        }
        private void Value_Click(object sender, RoutedEventArgs e)
        {
            Button a = (Button)sender;
            

            if(a.Content.ToString() == valuesList[0].ToString())
            {
                Listbox.Items.Add(a.Content.ToString());
                valuesList.RemoveAt(0);
                a.IsEnabled= false;
                if (valuesList.Count == 0)
                {
                   
                    NewGameButton.IsEnabled = true;
                    TimerTextBox.IsEnabled = true;
                    timer.Stop();
                    progressBar.Value = 0;
                    MessageBox.Show("Win!");
                }
            }
            else
            {
                progressBar.Value -= seconds * 0.05;
            }
        }

        public void FillButtons()
        {
            Random random = new Random();
            Listbox.Items.Clear();
            foreach (Button button in uniformGrid.Children)
            {
                int randomValue;
                do
                {
                    randomValue = random.Next(1, 101);
                }
                while (valuesList.Contains(randomValue));
                valuesList.Add(randomValue);
                button.Content = randomValue.ToString();
            }
            valuesList.Sort();
        }
      

    }
}
