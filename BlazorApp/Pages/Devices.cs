using ITAMS_DAL.Models;
using System.Text.Json;
using Telerik.Blazor;
using Telerik.Blazor.Components;

namespace BlazorApp.Pages
{
    public partial class Devices
    {
        private List<IDevicesWithLookupsModel> deviceList;
        private List<GridStateModel> gridStateList;

        //private ObservableCollection<IDevicesWithLookupsModel> devicesListObserved;
        public bool windowVisible = false;
        private int deviceId;
        public TelerikNotification Notification { get; set; }
        private TelerikGrid<IDevicesWithLookupsModel> Grid;
        private string serializedState;
        private string ReportName { get; set; }
        private int SelectedReportId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            deviceList = await deviceData.GetDevicesWithLookups();
            //devicesListObserved = new ObservableCollection<IDevicesWithLookupsModel>(deviceList);
            gridStateList = await gridStateData.GetGridStates();
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

        public async Task HandleOnSubmitted(string result)
        {
            windowVisible = false;
            deviceList = await deviceData.GetDevicesWithLookups();

            ShowNotifications(result);
        }

        public void ShowNotifications(string result)
        {
            var closeDelay = 3000;
            // using LINQ to filter the list instead of calling the database sproc
            var device = deviceList.Where(d => d.Id == deviceId).FirstOrDefault();

            switch (result)
            {
                case "create":
                    Notification.Show(new NotificationModel()
                    {
                        Text = $"Device successfully created",
                        ThemeColor = ThemeConstants.Notification.ThemeColor.Success,
                        CloseAfter = closeDelay
                    });
                    break;

                case "save":
                    Notification.Show(new NotificationModel()
                    {
                        Text = $"Device {device.DeviceName} successfully updated",
                        ThemeColor = ThemeConstants.Notification.ThemeColor.Success,
                        CloseAfter = closeDelay
                    });
                    break;
                case "delete":
                    Notification.Show(new NotificationModel()
                    {
                        Text = $"Device successfully deleted",
                        ThemeColor = ThemeConstants.Notification.ThemeColor.Success,
                        CloseAfter = closeDelay
                    });
                    break;
            }

        }

        public async Task HandleSaveReportButtonClick()
        {
            var state = Grid.GetState();
            serializedState = JsonSerializer.Serialize(state);

            var gridState = new GridStateModel()
            {
                SerializedState = serializedState,
                StateName = ReportName,
                GridId = 1

            };

            await gridStateData.CreateGridState(gridState);
            gridStateList = await gridStateData.GetGridStates();
            ReportName = string.Empty;
        }

        public async Task HandleGridStateDropDownOnChange()
        {
            if (windowVisible)
            {
                windowVisible = false;
            }
            if (SelectedReportId > 0)
            {
                var gridState = await gridStateData.GetGridStateById(SelectedReportId);
                var state = JsonSerializer.Deserialize<GridState<IDevicesWithLookupsModel>>(gridState.SerializedState);
                await Grid.SetState(state);
            }
            else
            {
                await Grid.SetState(null);
            }

        }
    }

}
