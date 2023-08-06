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
        [HttpPost("Purchase")]
        public IActionResult GetFuelQuotePrice([FromBody] FuelQuoteFormPurchaseDTO dtoObject)
        {
            try
            {
                // Retrieve the latest PurchaseId from the FuelQuoteForm table (assumes Id is an auto-incrementing primary key)
                int? latestPurchaseId = _dbContext.FuelQuoteForms.OrderByDescending(p => p.Id).Select(p => p.Id).FirstOrDefault();

                // Increment the latestPurchaseId by one to get the new PurchaseId for the new purchase
                int nextPurchaseId = (latestPurchaseId.HasValue ? latestPurchaseId.Value : 0) + 1;
                
                decimal amount = (decimal) (dtoObject.amount);
                decimal pricePerGallon = (decimal) (dtoObject.pricePerGallon);
                FuelQuoteForm myFuelQuote = new FuelQuoteForm { 
                    PricePerGallon = amount, 
                    Amount = pricePerGallon, 
                    DeliveryAddress = dtoObject.deliveryAddress.ToString(), 
                    DeliveryDate = new DateTime(dtoObject.dateYear, dtoObject.dateMonth, dtoObject.dateDay), 
                    GallonsRequested = dtoObject.gallonsRequested,
                    FuelQuoteTotal = pricePerGallon,
                    ClientId = dtoObject.clientId,
                    Id = nextPurchaseId
                };

                // return BadRequest(myFuelQuote);


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
