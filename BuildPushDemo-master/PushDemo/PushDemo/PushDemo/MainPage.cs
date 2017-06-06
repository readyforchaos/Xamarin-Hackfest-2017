using Refit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace PushDemo
{
    public class MainPage : ContentPage
    {
        public static string deviceId;

        private IDataService restAPI;

        private Button buttonBegin, buttonEnd;

        /// <summary>
        /// Building view
        /// </summary>
        public MainPage()
        {
            buttonBegin = new Button
            {
                Text = "Begin",
                WidthRequest = 100,
                HeightRequest = 60,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                IsEnabled = true
            };
            buttonBegin.Clicked += ButtonBegin_Clicked;

            buttonEnd = new Button
            {
                Text = "End",
                WidthRequest = 100,
                HeightRequest = 60,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                IsEnabled = false
            };
            buttonEnd.Clicked += ButtonEnd_Clicked;

            var content = new StackLayout();
            content.Children.Add(buttonBegin);
            content.Children.Add(buttonEnd);
            
            Content = content;
            
            BackgroundColor = Color.FromRgb(61, 95, 150);

            restAPI = RestService.For<IDataService>("http://buildpushdemo.azurewebsites.net");
        }

        /// <summary>
        /// Event click for work end. REST call will result in notification.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonEnd_Clicked(object sender, EventArgs e)
        {
            restAPI.End(deviceId);

            buttonEnd.IsEnabled = false;
            buttonBegin.IsEnabled = true;
        }

        /// <summary>
        /// Event click for work begin. REST call will result in notification.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonBegin_Clicked(object sender, EventArgs e)
        {
            restAPI.Begin(deviceId);

            buttonEnd.IsEnabled = true;
            buttonBegin.IsEnabled = false;
        }
    }

    public class MainPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
