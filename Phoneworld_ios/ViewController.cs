using System;
using UIKit;
using Foundation;
using System.Collections.Generic;


namespace Phoneworld_ios
{
	public partial class ViewController : UIViewController
	{
		private string translatedNumber;
		private string phoneWord;
		public List<string> PhoneNumbers { get; set; }

		protected ViewController(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.
			PhoneNumbers = new List<string>();
			ConfigureTranslatedNumberButton();
			ConfigureCallButton();
		}

		private void ConfigureCallButton()
		{
			CallButton.TouchUpInside += (object sender, EventArgs e) =>
			{
				CallButtonHandler();
			};
		}

		private void ConfigureTranslatedNumberButton()
		{
			translatedNumber = string.Empty;
			TranslateButton.TouchUpInside += (object sender, EventArgs e) =>
			{
				TranslateNumberButtonHandler();
			};
		}

		private void TranslateNumberButtonHandler()
		{
			PhoneNumberText.ResignFirstResponder();
			phoneWord = PhoneNumberText.Text.Trim();
			translatedNumber = PhoneTranslator.ToNumber(phoneWord);

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
		}

		private void CallButtonHandler()
		{
			PhoneNumbers.Add($"{phoneWord} ({translatedNumber})");
			var url = new NSUrl($"tel:{translatedNumber}");

			if (!UIApplication.SharedApplication.OpenUrl(url))
			{
				ShowOkAlertDialog("Sorry...",
								  "The scheme 'tel:' is not supported on this device.");
			}
		}

		private void ShowOkAlertDialog(string title = "Alert", string message = "Sorry, unable to perform this action")
		{
			var alert = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);
			alert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, null));
			PresentViewController(alert, true, null);
		}




		public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
		{
			base.PrepareForSegue(segue, sender);

			var callHistoryController = segue.DestinationViewController as CallHistoryController;
			if (callHistoryController != null)
			{
				callHistoryController.PhoneNumbers = PhoneNumbers;
			}
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}

