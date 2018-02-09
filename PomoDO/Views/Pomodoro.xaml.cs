using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.Toolkit.Uwp;
using Microsoft.Data.Sqlite;
using System.Diagnostics;
using DoMore;



// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace DoMore
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Pomodoro : Page
    {
        private DispatcherTimer timer;
        private DispatcherTimer GUItimer;
        private double elapsed;
        private string gui;

        public Pomodoro()
        {
            this.InitializeComponent();
        }

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Checked");
            TimerStart();
            //AddData(workingTextbox.Text);
        }

        private void TimerStart()
        {
            TimeSpan goal = new TimeSpan(0, 25, 0);
            double timevalue = goal.TotalSeconds;
            radialGauge.Maximum = timevalue;
            elapsed = 0;
            
            timer = new DispatcherTimer();
            timer.Interval = goal;

            GUItimer = new DispatcherTimer();
            GUItimer.Interval = new TimeSpan(0, 0, 1);
            GUItimer.Start();
            timer.Start();
            
   
            timer.Tick += TimerDone;
            GUItimer.Tick += GUITimerDone;

            gui = SqliteHandler.AddData(workingTextbox.Text);
            
        }

        private void GUITimerDone(object sender, object e)
        {
            elapsed += 1;
            radialGauge.Value = elapsed;
           
            SqliteHandler.UpdateData(gui);
        }

        private void TimerDone(object sender, object e)
        {
            timer.Stop();
            GUItimer.Stop();
            toggleButton.IsChecked = false;
        }

        

        private void UpdateData(string GUID)
        {

        }

        


        private void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            resetAll();

            System.Diagnostics.Debug.WriteLine("Unchecked");
        }

        private void resetAll()
        {
            timer.Stop();
            GUItimer.Stop();
            elapsed = 0;
            gui = "";
        }
    }
}
