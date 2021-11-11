﻿using EduCATS.Controls.RoundedListView;
using EduCATS.Controls.SwitchFrame;
using EduCATS.Helpers.Forms;
using EduCATS.Helpers.Forms.Styles;
using EduCATS.Pages.Settings.Fonts.ViewModels;
using EduCATS.Pages.Settings.Views.Base.ViewCells;
using EduCATS.Themes;
using Nyxbull.Plugins.CrossLocalization;
using Xamarin.Forms;

namespace EduCATS.Pages.Settings.Fonts.Views
{
	public class FontsPageView : ContentPage
	{
		static Thickness _listMargin = new Thickness(10, 1, 10, 20);
		static Thickness _chooseLabelMargin = new Thickness(0, 10);
		static Thickness _frameMargin = new Thickness(0, 10, 0, 0);

		public FontsPageView()
		{
			NavigationPage.SetHasNavigationBar(this, false);
			BackgroundColor = Color.FromHex(Theme.Current.AppBackgroundColor);
			BindingContext = new FontsPageViewModel(new PlatformServices());
			createViews();
		}

		void createViews()
		{
			var headerView = createHeader();
			var list = createList(headerView);
			Content = list;
		}

		RoundedListView createList(View header)
		{
			var listView = new RoundedListView(null, true, header, () => new CheckboxViewCell(true)) {
				Margin = _listMargin
			};

			listView.SetBinding(ItemsView<Cell>.ItemsSourceProperty, "FontList");
			listView.SetBinding(ListView.SelectedItemProperty, "SelectedItem", BindingMode.TwoWay);
			return listView;
		}

		StackLayout createHeader()
		{
			var switchFrame = createSwitchFrame();
			var chooseLabel = createChooseLabel();

			return new StackLayout {
				Children = {
					switchFrame,
					chooseLabel
				}
			};
		}

		Label createChooseLabel()
		{
			var chooseLabel = new Label {
				Margin = _chooseLabelMargin,
				FontAttributes = FontAttributes.Bold,
				TextColor = Color.FromHex(Theme.Current.BaseSectionTextColor),
				Text = CrossLocalization.Translate("settings_font_choose"),
				Style = AppStyles.GetLabelStyle(NamedSize.Large, true)
			};

			return chooseLabel;
		}

		SwitchFrame createSwitchFrame()
		{
			var frame = new SwitchFrame(
				CrossLocalization.Translate("settings_font_large"),
				CrossLocalization.Translate("settings_font_large_description")) {
				Margin = _frameMargin
			};

			frame.Switch.SetBinding(Switch.IsToggledProperty, "IsLargeFont");
			return frame;
		}
	}
}
