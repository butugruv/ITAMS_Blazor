using ITAMS_DAL.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;

namespace BlazorApp.Pages
{
    public partial class DeviceFormModal
    {
        [Parameter]
        public bool WindowVisible { get; set; }

        [Parameter]
        public int Id { get; set; }

        [Parameter]
        public EventCallback<string> OnSubmitted { get; set; }

        [CascadingParameter]
        private Task<AuthenticationState> AuthenticationStateTask { get; set; }

        private IDeviceModel device = new DeviceModel();
        private List<DeviceTypeModel> deviceTypes;
        private List<RmfPackageModel> rmfPackages;
        private List<LocationModel> locations;
        private List<LocationFloorModel> locationFloors;
        private List<NetworkModel> networks;
        private IdentityUser currentUser;

        protected override async Task OnInitializedAsync()
        {
            var user = (await AuthenticationStateTask).User;

            if (user.Identity.IsAuthenticated)
            {
                currentUser = await userManager.GetUserAsync(user);
            }
        }

        protected override async Task OnParametersSetAsync()
        {
            if (Id > 0)
            {
                device = await deviceData.GetDeviceById(Id);
            }
            else
            {
                device = new DeviceModel();
            }

            deviceTypes = await deviceTypeData.GetDeviceTypes();
            rmfPackages = await rmfPackageData.GetRmfPackages();
            networks = await networkData.GetNetworks();
            locations = await locationData.GetLocations();

            if (device != null)
            {
                // populate cascading lists
                locationFloors = await locationData.GetLocationFloors(device.LocationId);
            }

        }

        private async Task HandleValidSubmit()
        {
            if (Id > 0)
            {
                device.ModifiedDate = DateTime.Now;
                device.ModifiedBy = currentUser.UserName;
                await deviceData.UpdateDevice(device);
                await OnSubmitted.InvokeAsync("save");
            }
            else
            {
                device.CreatedDate = DateTime.Now;
                device.CreatedBy = currentUser.UserName;
                await deviceData.CreateDevice(device);
                await OnSubmitted.InvokeAsync("create");
            }
        }

        public void CloseWindow()
        {
            // for this situation, just mainly using this to create a trigger on the parent component to do stuff from
            OnSubmitted.InvokeAsync("submitted");
        }

        private async void HandleLocationOnChange(object newValue)
        {
            locationFloors = await locationData.GetLocationFloors((int)newValue);
        }

        private async Task HandleDeleteOnClick()
        {
            await deviceData.DeleteDevice(Id);
            await OnSubmitted.InvokeAsync("delete");
        }

    }
}
