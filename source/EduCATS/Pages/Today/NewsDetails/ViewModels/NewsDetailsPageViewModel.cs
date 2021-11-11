﻿using System;
using System.Threading.Tasks;
using EduCATS.Helpers.Extensions;
using EduCATS.Helpers.Forms;
using EduCATS.Helpers.Logs;
using EduCATS.Themes;
using Xamarin.Forms;

namespace EduCATS.Pages.Today.NewsDetails.ViewModels
{
	public class NewsDetailsPageViewModel : ViewModel
	{
		readonly IPlatformServices _services;

		const int _fontPadding = 10;
		const string _fontFamily = "Arial";

		string _body;
		bool _isBusySpeech;

		public NewsDetailsPageViewModel(double fontSize, string title, string body, IPlatformServices services)
		{
			_services = services;

			HeadphonesIcon = Theme.Current.BaseHeadphonesIcon;
			NewsTitle = title;
			_body = body;
			NewsBody = $"" +
				$"<style>" +
				$"a {{ color: {Theme.Current.BaseLinksColor}; }}" +
				$"</style>" +
				$"<body style='" +
					$"font-family:{_fontFamily};" +
					$"padding:{_fontPadding}px;" +
					$"color:{Theme.Current.NewsTextColor};" +
					$"font-size: {fontSize}vw;" +
					$"background-color:{Theme.Current.AppBackgroundColor};'>" +
						$"{body}" +
				$"</body>";
		}

		string _newsTitle;
		public string NewsTitle {
			get { return _newsTitle; }
			set { SetProperty(ref _newsTitle, value); }
		}

		string _newsBody;
		public string NewsBody {
			get { return _newsBody; }
			set { SetProperty(ref _newsBody, value); }
		}

		string _headphonesIcon;
		public string HeadphonesIcon {
			get { return _headphonesIcon; }
			set { SetProperty(ref _headphonesIcon, value); }
		}

		Command _speechCommand;
		public Command SpeechCommand {
			get {
				return _speechCommand ?? (_speechCommand = new Command(async () => await speechToText()));
			}
		}

		Command _httpNavigatingCommand;
		public Command HttpNavigatingCommand {
			get {
				return _httpNavigatingCommand ?? (_httpNavigatingCommand = new Command(openWebPage));
			}
		}

		Command _closeCommand;
		public Command CloseCommand {
			get {
				return _closeCommand ?? (_closeCommand = new Command(
					async () => await closePage()));
			}
		}

		protected async Task speechToText()
		{
			try {
				if (string.IsNullOrEmpty(NewsTitle) && string.IsNullOrEmpty(NewsBody)) {
					return;
				}

				if (_isBusySpeech) {
					_isBusySpeech = false;
					_services.Device.CancelSpeech();
					HeadphonesIcon = Theme.Current.BaseHeadphonesIcon;
					return;
				}

				HeadphonesIcon = Theme.Current.BaseHeadphonesCancelIcon;
				_isBusySpeech = true;
				await _services.Device.Speak(NewsTitle);

				if (!_isBusySpeech) {
					return;
				}

				var editedNewsBody = _body?.RemoveHTMLTags();
				editedNewsBody = editedNewsBody.RemoveLinks();

				if (string.IsNullOrEmpty(editedNewsBody)) {
					_isBusySpeech = false;
					return;
				}

				await _services.Device.Speak(editedNewsBody);
				HeadphonesIcon = Theme.Current.BaseHeadphonesIcon;
				_isBusySpeech = false;
			} catch (Exception ex) {
				AppLogs.Log(ex);
			}
		}

		protected void openWebPage(object url)
		{
			if (url == null) {
				return;
			}

			_services.Device.MainThread(
				async () => await _services.Device.OpenUri(url.ToString()));
		}

		protected async Task closePage()
		{
			try {
				if (_isBusySpeech) {
					_isBusySpeech = false;
					_services.Device.CancelSpeech();
				}

				await _services.Navigation.ClosePage(true, false);
			} catch (Exception ex) {
				AppLogs.Log(ex);
			}
		}
	}
}
