using System;
using UIKit;
using Foundation;


namespace Phoneworld_ios
{
	public partial class ViewController : UIViewController
	{
		private string translatedNumber;

		protected ViewController(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.

			ConfigureTranslatedNumberButton();
			ConfigureCallButton();
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}

		private void ConfigureCallButton()
		{
			CallButton.TouchUpInside += (object sender, EventArgs e) =>
			{
				var url = new NSUrl($"tel:{translatedNumber}");

				if (!UIApplication.SharedApplication.OpenUrl(url))
				{
					ShowOkAlertDialog("Sorry...",
									  "The scheme 'tel:' is not supported on this device.");
				}
			};
		}

		private void ShowOkAlertDialog(string title = "Alert", string message = "Sorry, unable to perform this action")
		{
			var alert = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);
			alert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, null));
			PresentViewController(alert, true, null);
		}

		private void ConfigureTranslatedNumberButton()
		{
			translatedNumber = string.Empty;
			TranslateButton.TouchUpInside += (object sender, EventArgs e) =>
			{
				PhoneNumberText.ResignFirstResponder();
				translatedNumber = PhoneTranslator.ToNumber(PhoneNumberText.Text);

				if (string.IsNullOrWhiteSpace(translatedNumber))
				{
					CallButton.SetTitle("Call", UIControlState.Normal);
					CallButton.Enabled = false;
				}
				else
				{
					CallButton.SetTitle($"Call {translatedNumber}", UIControlState.Normal);
					CallButton.Enabled = true;
				}
			};
		}
	}
}

