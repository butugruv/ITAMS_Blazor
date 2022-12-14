using ITAMS_DAL.Models;
using Telerik.Blazor;
using Telerik.Blazor.Components;

namespace BlazorApp.Pages
{
    public partial class Devices
    {
        private List<IDevicesWithLookupsModel> deviceList;
        //private ObservableCollection<IDevicesWithLookupsModel> devicesListObserved;
        public bool windowVisible = false;
        private int deviceId;
        public TelerikNotification Notification { get; set; }

        protected override async Task OnInitializedAsync()
        {
            deviceList = await deviceData.GetDevicesWithLookups();
            //devicesListObserved = new ObservableCollection<IDevicesWithLookupsModel>(deviceList);
        }

        private void HandleEditButtonClick(IDevicesWithLookupsModel device)
        {
            //int id = (args.Item as IDevicesWithLookupsModel).Id; 
            //navigationManager.NavigateTo($"Devices/DeviceForm/{id}");
            deviceId = device.Id;
            windowVisible = true;

        }

        private void HandleCreateButtonClick()
        {
            deviceId = 0;
            windowVisible = true;

        }

        public async Task HandleOnSubmitted()
        {
            windowVisible = false;
            deviceList = await deviceData.GetDevicesWithLookups();
            ShowNotifications();

        }

        public void ShowNotifications()
        {
            var closeDelay = 1500;

            Notification.Show(new NotificationModel()
            {
                Text = "Device successfully updated",
                ThemeColor = ThemeConstants.Notification.ThemeColor.Success,
                CloseAfter = closeDelay
            });
        }
    }
}
