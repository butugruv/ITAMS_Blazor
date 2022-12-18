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
            locations = await locationData.GetLocations();
        }

        private async Task HandleValidSubmit()
        {
            if (Id > 0)
            {
                device.ModifiedDate = DateTime.Now;
                device.ModifiedBy = currentUser.UserName;
                await deviceData.UpdateDevice(device);
            }
            else
            {
                device.CreatedDate = DateTime.Now;
                device.CreatedBy = currentUser.UserName;
                await deviceData.CreateDevice(device);
            }

            CloseWindow();

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

    }
}
