using ITAMS_DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Models
{
    public class DisplayDeviceModel : IDeviceModel
    {
        [Editable(false)]
        public int Id { get; set; }
        [Required]
        public string DeviceName { get; set; }
        public string DeviceFunction { get; set; }
        public int DeviceTypeId { get; set; }
        public int ManufacturerId { get; set; }
        public string Poc { get; set; }
        [Editable(false)]
        public DateTime CreatedDate { get; set; }
        [Editable(false)]
        public string CreatedBy { get; set; }
        [Editable(false)]
        public DateTime ModifiedDate { get; set; }
        [Editable(false)]
        public string ModifiedBy { get; set; }
        public int LocationId { get; set; }
        public int LocationFloorId { get; set; }
        public int LocationFloorRoomId { get; set; }
        public int RmfPackageId { get; set; }
        public string model { get; set; }
    }
}
