using AceBackEnd.Data_Transfer_Objects;
using AceBackEnd.Models;
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
        private readonly AceDbContext _dbContext;
        public FuelQuoteFormController(AceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
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
        public ActionResult<IEnumerable<FuelQuoteHistoryDTO>> GetFuelQuotePrice([FromBody] FuelQuoteFormDTO dtoObject)
        {
            try
            {
                FuelQuoteForm myFuelQuote =  new FuelQuoteForm { PricePerGallon = (decimal)dtoObject.pricePerGallon, Amount = (decimal)(dtoObject.gallonsRequested * 2.8), DeliveryAddress = dtoObject.deliveryAddress.ToString(), DeliveryDate = new DateTime(dtoObject.deliveryDate.Year, dtoObject.deliveryDate.Month, dtoObject.deliveryDate.Day), GallonsRequested = dtoObject.gallonsRequested };

                _dbContext.FuelQuoteForms.Add(myFuelQuote);
                _dbContext.SaveChanges();

                return Ok(myFuelQuote);
            }
            catch (Exception)
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }
        }
    }
}
