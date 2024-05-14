using eTamir.Services.Map.Dtos;
using eTamir.Services.Map.Models;
using eTamir.Services.Map.Services;
using eTamir.Shared.Controller;
using eTamir.Shared.Dtos;
using eTamir.Shared.Services;
using Microsoft.AspNetCore.Mvc;

namespace eTamir.Services.Map.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddressController : CustomControllerBase
    {
        private readonly IAddressService addressService;
        private readonly ISharedIdentityService sharedIdentityService;

        public AddressController(IAddressService addressService, ISharedIdentityService sharedIdentityService)
        {
            this.addressService = addressService;
            this.sharedIdentityService = sharedIdentityService;
        }

      

      
    }
}