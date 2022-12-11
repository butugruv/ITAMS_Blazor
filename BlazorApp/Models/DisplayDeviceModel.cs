using ITAMS_DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Models
{
    public class DisplayDeviceModel : IDeviceModel
    {
        [Display(Name = "Device ID#")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Device Name")]
        public string DeviceName { get; set; }

        [Display(Name = "Device Function")]
        public string DeviceFunction { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Value must be greater than 0")]
        public int DeviceTypeId { get; set; }

        [Required]
        public int ManufacturerId { get; set; }

        [Display(Name = "POC")]
        public string Poc { get; set; }

        [Required]
        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; }

        [Required]
        public string CreatedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public string ModifiedBy { get; set; }

        [Required]
        [Display(Name = "Location")]
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
