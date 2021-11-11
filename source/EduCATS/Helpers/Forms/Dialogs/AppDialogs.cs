﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using Nyxbull.Plugins.CrossLocalization;
using Xamarin.Forms;

namespace EduCATS.Helpers.Forms.Dialogs
{
	/// <summary>
	/// <see cref="IDialogs"/> implementation.
	/// </summary>
	public class AppDialogs : IDialogs
	{
		/// <summary>
		/// Localized "OK" text.
		/// </summary>
		static string _baseOK => CrossLocalization.Translate("base_ok");

		/// <summary>
		/// Localized "No" text.
		/// </summary>
		static string _baseNo => CrossLocalization.Translate("base_no");

		/// <summary>
		/// Localized "Yes" text.
		/// </summary>
		static string _baseYes => CrossLocalization.Translate("base_yes");

		/// <summary>
		/// Localized "Error" text.
		/// </summary>
		static string _baseError => CrossLocalization.Translate("base_error");

		/// <summary>
		/// Localized "Cancel" text.
		/// </summary>
		static string _baseCancel => CrossLocalization.Translate("base_cancel");

		/// <summary>
		/// Localized "Loading" text.
		/// </summary>
		static string _baseLoading => CrossLocalization.Translate("base_loading");

		/// <summary>
		/// Property for getting <see cref="Application.Current.MainPage"/>.
		/// </summary>
		Page mainPage =>
			Application.Current.MainPage;

		/// <summary>
		/// Show error dialog.
		/// </summary>
		/// <param name="message">Dialog description.</param>
		public void ShowError(string message) =>
			mainPage.DisplayAlert(_baseError, message, _baseOK);

		/// <summary>
		/// Show message dialog.
		/// </summary>
		/// <param name="title">Dialog title.</param>
		/// <param name="message">Dialog description.</param>
		public void ShowMessage(string title, string message) =>
			mainPage.DisplayAlert(title, message, _baseOK);

		/// <summary>
		/// Show loading dialog.
		/// </summary>
		public void ShowLoading() =>
			UserDialogs.Instance.ShowLoading(_baseLoading);

		/// <summary>
		/// Show loading dialog.
		/// </summary>
		/// <param name="message">Dialog description.</param>
		public void ShowLoading(string message) =>
			UserDialogs.Instance.ShowLoading(message);

		/// <summary>
		/// Hide loading dialog.
		/// </summary>
		public void HideLoading() =>
			UserDialogs.Instance.HideLoading();

		/// <summary>
		/// Show progress dialog.
		/// </summary>
		/// <param name="message">Dialog message.</param>
		/// <param name="cancelText">Cancel button text.</param>
		/// <param name="onCancel">Action on cancel.</param>
		/// <returns>Progress dialog.</returns>
		public object ShowProgress(string message, string cancelText, Action onCancel) =>
			UserDialogs.Instance.Progress(message, onCancel, cancelText);

		/// <summary>
		/// Update progress dialog with percent.
		/// </summary>
		/// <param name="dialog">
		/// Progress dialog instance
		/// (retrieved from <see cref="ShowProgress(string, string, Action)"/>).
		/// </param>
		/// <param name="percent">Percent to apply.</param>
		public void UpdateProgress(object dialog, int percent)
		{
			var progressDialog = getProgressDialog(dialog);

			if (progressDialog == null) {
				return;
			}

			progressDialog.PercentComplete = percent;
		}

		/// <summary>
		/// Hide progress dialog.
		/// </summary>
		/// <param name="dialog">
		/// Progress dialog instance
		/// (retrieved from <see cref="ShowProgress(string, string, Action)"/>).
		/// </param>
		public void HideProgress(object dialog) =>
			getProgressDialog(dialog)?.Hide();

		/// <summary>
		/// Show alert sheet.
		/// </summary>
		/// <param name="title">Dialog title.</param>
		/// <param name="buttons">Dialog buttons (id and name).</param>
		/// <param name="command">Command to execute on button click.</param>
		/// <returns>Chosen button name.</returns>
		public void ShowSheet(string title, Dictionary<int, string> buttonList, ICommand command)
		{
			var config = new ActionSheetConfig().SetTitle(title);

			foreach (var button in buttonList) {
				config.Add(button.Value, () => command.Execute(button.Key));
			}

			config.SetCancel(_baseCancel, () => command.Execute(-1));
			UserDialogs.Instance.ActionSheet(config);
		}

		/// <summary>
		/// Show confirmation dialog.
		/// </summary>
		/// <param name="title">Dialog title.</param>
		/// <param name="message">Dialog description.</param>
		/// <returns>Dialog result.</returns>
		public async Task<bool> ShowConfirmationMessage(string title, string message) =>
			await mainPage.DisplayAlert(title, message, _baseYes, _baseNo);

		/// <summary>
		/// Convert object to <see cref="IProgressDialog"/>.
		/// </summary>
		/// <param name="dialog">Dialog object.</param>
		/// <returns>Progress dialog.</returns>
		IProgressDialog getProgressDialog(object dialog)
		{
			if (dialog == null || !(dialog is IProgressDialog)) {
				return null;
			}

			var progressDialog = dialog as IProgressDialog;
			return progressDialog;
		}
	}
}
