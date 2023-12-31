﻿using GetGoApp.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GetGoApp.Views.Home
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home_Primary : ContentPage
    {
        private string link, Details, userId;

        public Home_Primary()
        {
            InitializeComponent();
            Initialize();
            InitializeContent();
        }
        private void Initialize()
        {
            Details = AppData.Instance.Details;

            // Your initialization logic here...
        }

        private void InitializeContent()
        {

            if (!string.IsNullOrEmpty(Details))
            {
                string[] detailsArray = Details.Split('|');
                if (detailsArray.Length >= 2)
                {
                    link = detailsArray[0];
                    userId = detailsArray[1].Replace(":", "=");
                    WebView(link, userId);
                }
            }
            else
            {
                DisplayAlert("URL GOT LOST", "ERROR","OK");
            }
         
        }

        private async void Apply_Clicked(object sender, EventArgs e)
        {

           string url = $"{link}/Views/UserApp/Home/Default.aspx?{userId}";
            try
            {
                // Send the HTTP request
                using (HttpClient httpClient = new HttpClient())
                {
                    HttpResponseMessage response = await httpClient.GetAsync(url);

                    if (!response.IsSuccessStatusCode)
                    {
                        await DisplayAlert("Error", "Failed to connect to the server", "OK");
                    
                        return;
                    }

                    response.EnsureSuccessStatusCode();

                    string responseContent = await response.Content.ReadAsStringAsync();

                    // Check if the response content contains "Wrong pass"
                    if (responseContent.Contains("46FFE43A74AC2CAF593E9DCEAB229"))
                    {
                        await DisplayAlert("Alert", "Not verfied yet, Please wait for verification. Thank you!", "Go Back");
                        return;
                    }


                    AppData.Instance.Details = Details;
               
                    // Navigate to the Home_Primary page
                    await Navigation.PushAsync(new Home_ApplyLoan());
                    //loadingLayout.IsVisible = false;
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions and display an error message
                await DisplayAlert("Error", ex.Message, "OK");
                //loadingLayout.IsVisible = false;
            }
        }
    

        private void WebView(string link, string input) => webView.Source = new UrlWebViewSource { Url = $"{link}/Views/UserApp/Home/Home_Default.aspx?{input}" };

        private async void Button_Clicked(object sender, EventArgs e)
        {
            string url = $"{link}/Views/UserApp/Home/Default.aspx?{userId}";
            try
            {
                // Send the HTTP request
                using (HttpClient httpClient = new HttpClient())
                {
                    HttpResponseMessage response = await httpClient.GetAsync(url);

                    if (!response.IsSuccessStatusCode)
                    {
                        await DisplayAlert("Error", "Failed to connect to the server", "OK");

                        return;
                    }

                    response.EnsureSuccessStatusCode();

                    string responseContent = await response.Content.ReadAsStringAsync();

                    // Check if the response content contains "Wrong pass"
                    if (responseContent.Contains("46FFE43A74AC2CAF593E9DCEAB229"))
                    {
                        await DisplayAlert("Alert", "Not verfied yet, Please wait for verification. Thank you!", "Go Back");
                        return;
                    }


                   await webView.EvaluateJavaScriptAsync("RepaymentModal();");
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions and display an error message
                await DisplayAlert("Error", ex.Message, "OK");
                //loadingLayout.IsVisible = false;
            }
           
        }
        private async void Withdraw_Clicked(object sender, EventArgs e)
        {
            string url = $"{link}/Views/UserApp/Home/Default.aspx?{userId}";
            try
            {
                // Send the HTTP request
                using (HttpClient httpClient = new HttpClient())
                {
                    HttpResponseMessage response = await httpClient.GetAsync(url);

                    if (!response.IsSuccessStatusCode)
                    {
                        await DisplayAlert("Error", "Failed to connect to the server", "OK");

                        return;
                    }

                    response.EnsureSuccessStatusCode();

                    string responseContent = await response.Content.ReadAsStringAsync();

                    // Check if the response content contains "Wrong pass"
                    if (responseContent.Contains("46FFE43A74AC2CAF593E9DCEAB229"))
                    {
                        await DisplayAlert("Alert", "Not verfied yet, Please wait for verification. Thank you!", "Go Back");
                        return;
                    }


                    AppData.Instance.Details = Details;

                    // Navigate to the Home_Primary page
                    await Navigation.PushAsync(new Home_Withdrawal());
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions and display an error message
                await DisplayAlert("Error", ex.Message, "OK");
                //loadingLayout.IsVisible = false;
            }
          
        }


        private void HomeImage_Tapped(object sender, EventArgs e) => Navigation.PushAsync(new Home_Primary());


        private void MenuImage_Tapped(object sender, EventArgs e) => Navigation.PushAsync(new Menu());

        //private void HomeButton_Clicked(object sender, EventArgs e)
        //{

        //    Navigation.PushAsync(new Views.Home.Home_Primary());
        //}

        //private void MenuButton_Clicked(object sender, EventArgs e)
        //{
        //    Navigation.PushAsync(new Views.Menu());
        //}
        private void VerifyAccount()
        {

        }
    }
}