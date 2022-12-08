using ITAMS_DAL.Models;

namespace BlazorApp.Models
{
    public class DisplayDeviceTypeModel : IDeviceTypeModel
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
        public string TypeDescription { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
