using ITAMS_DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Models
{
    public class DisplayDeviceIpsModel : IDeviceIpModel
    {
        public int Id { get; set; }
        public int DeviceId { get; set; }
        public string IpAddress { get; set; }
        [Required]
        public string MacAddress { get; set; }
    }
}