using System;
using System.ComponentModel;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace ZxingXamarinForms
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            Button btnScan = new Button
            {
                Text = "Start Scan",
                BackgroundColor = Color.FromRgb(207, 197, 159),
                TextColor = Color.White,
                BorderRadius = 5,
                TranslationY = 120
            };
            //Attach the click event
            var ScanResult = new Label();
            btnScan.Clicked += btnScan_Clicked;
            this.Content = new StackLayout
            {
                BackgroundColor = Color.FromRgb(150, 172, 135),
                Spacing = 10,
                Padding = 25,
                Children =
            {
                btnScan,
                ScanResult
            }
            };
            async void btnScan_Clicked(object sender, EventArgs e)
            {
                var scan = new ZXingScannerPage();
                await this.Navigation.PushAsync(scan);
                scan.OnScanResult += (eee) =>
                 {
                     Device.BeginInvokeOnMainThread(async () =>
                     {
                         await this.Navigation.PopAsync();
                         ScanResult.Text = eee.Text;
                     });
                 };
            }
        }
    }
}
