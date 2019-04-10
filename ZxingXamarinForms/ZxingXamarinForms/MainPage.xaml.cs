using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZXing.Mobile;

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
            btnScan.Clicked += btnScan_Clicked;

            this.Content = new StackLayout
            {
                BackgroundColor = Color.FromRgb(150, 172, 135),
                Spacing = 10,
                Padding = 25,
                Children =
            {
                btnScan
            }
            };
            async void btnScan_Clicked(object sender, EventArgs e)
            {
#if __ANDROID__
	// Initialize the scanner first so it can track the current context
	MobileBarcodeScanner.Initialize (Application);
#endif

                var scanner = new ZXing.Mobile.MobileBarcodeScanner();
                scanner.UseCustomOverlay = true;

                var result = await scanner.Scan();

                if (result != null)
                    Console.WriteLine("Scanned Barcode: " + result.Text);

                scanner.TopText = "Hold the camera up to the barcode\nAbout 6 inches away";
                scanner.BottomText = "Wait for the barcode to automatically scan!";

                //This will start scanning
                 result = await scanner.Scan();

                //Show the result returned.
                HandleResult(result);
            }

            void HandleResult(ZXing.Result result)
            {
                var msg = "No Barcode!";
                if (result != null)
                {
                    msg = "Barcode: " + result.Text + " (" + result.BarcodeFormat + ")";
                }

                DisplayAlert("", msg, "Ok");
            }

        }
    }
}
