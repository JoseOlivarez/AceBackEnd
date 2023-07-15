using AceBackEnd.Data_Transfer_Objects;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Runtime.InteropServices.ObjectiveC;
using System.Runtime.Serialization;
using System.Security.Cryptography.Xml;
using System.Text.Json.Nodes;

namespace AceBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuelQuoteFormController : ControllerBase
    {

        [HttpGet]
        public ActionResult<FuelQuoteFormDTO> FuelQuoteForm()
        {
            
          
                try
                {

                    var FuelQuoteFormObj = new FuelQuoteFormDTO { pricePerGallon = 2.8, deliveryAddress="123 South Street", deliveryDate=new DateOnly() };
                    
                    
                    
                    

                    return Ok(FuelQuoteFormObj);
                }
                catch (Exception)
                {
                    return StatusCode(500, "A problem has happened while handeling your request");
                }


           
        }
        [HttpPost]
        public ActionResult<IEnumerable<FuelQuoteHistoryDto>> GetFuelQuotePrice([FromBody] FuelQuoteFormDTO dtoObject)
        {
            try
            {
                return Ok(new FuelQuoteFormDTO { pricePerGallon = dtoObject.pricePerGallon, amount = dtoObject.gallonsRequested*2.8, deliveryAddress = dtoObject.deliveryAddress.ToString(), deliveryDate = new DateOnly(dtoObject.deliveryDate.Year, dtoObject.deliveryDate.Month, dtoObject.deliveryDate.Day), gallonsRequested=dtoObject.gallonsRequested});
            }
            catch (Exception)
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }
        }
    }
}
