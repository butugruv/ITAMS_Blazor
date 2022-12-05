using ITAMS_DAL.Models;

namespace BlazorApp.Models
{
    public class DisplayDeviceModel : IDeviceModel
    {
        public int Id { get; set; }
        public string DeviceName { get; set; }
        public string DeviceFunction { get; set; }
        public int DeviceTypeId { get; set; }
        public int ManufacturerId { get; set; }
        public string Poc { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public int LocationId { get; set; }
        public int LocationFloorId { get; set; }
        public int LocationFloorRoomId { get; set; }
        public int RmfPackageId { get; set; }
        public string model { get; set; }
    }
}
