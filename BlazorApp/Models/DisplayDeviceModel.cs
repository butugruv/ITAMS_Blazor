using ITAMS_DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Models
{
    public class DisplayDeviceModel : IDeviceModel
    {

        public int Id { get; set; }
        [Required]
        public string DeviceName { get; set; }
        public string DeviceFunction { get; set; }
        [Required]
        public int DeviceTypeId { get; set; }
        [Required]
        public int ManufacturerId { get; set; }
        public string Poc { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        public string CreatedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public string ModifiedBy { get; set; }
        [Required]
        public int LocationId { get; set; }
        [Required]
        public int LocationFloorId { get; set; }
        [Required]
        public int LocationFloorRoomId { get; set; }
        [Required]
        public int RmfPackageId { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Model { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Value must be greater than 0")]
        [Required]
        public int NetworkId { get; set; }
    }
}
