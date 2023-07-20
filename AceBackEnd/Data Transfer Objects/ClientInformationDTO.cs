using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Reflection;
using System.Text.RegularExpressions;

namespace AceBackEnd.Data_Transfer_Objects
{
    public class ClientInformationDTO
    {
        public int ClientId { get; set; }
        public string Username { get; set; }    
        public string Password { get; set; }
        public string Fullname { get; set; }
        public string Addressone { get; set; }
        public string Addresstwo { get; set; }
        public string City { get; set; }    
        public string State { get; set; }

        public string Zipcode { get; set; }
        
    }
}
