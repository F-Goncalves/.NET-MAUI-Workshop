using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MauiWorkshop.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private readonly IFlyoutNavigationService _flyoutPageNavigationService;
        private readonly IDispatcher _dispatcher;

        private FlyoutMenuItem _selectedItem;
        private Page _detailPage;

        public FlyoutMenuItem SelectedItem
		{
			get => _selectedItem;
			set => SetProperty(ref _selectedItem, value);
		}

        public Page DetailPage
        {
            get => _detailPage;
            set => SetProperty(ref _detailPage, value);
        }

		public Command SelectedItemChangedCommand { get; set; }

		public MainPageViewModel(IFlyoutNavigationService flyoutPageNavigationService, IDispatcher dispatcher)
        {
            _flyoutPageNavigationService = flyoutPageNavigationService;
            _dispatcher = dispatcher;

            SelectedItemChangedCommand = new Command(OnSelectedItemChanged);
        }

		private void OnSelectedItemChanged()
		{
			if (SelectedItem == null)
				return;

            _flyoutPageNavigationService.SetDetailPage(SelectedItem.TargetPage, true);

            _dispatcher.Dispatch(() => SelectedItem = null);
		}
	}
}
