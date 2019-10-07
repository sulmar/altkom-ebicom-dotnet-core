using Altkom.DotnetCore.IRepositories;
using Altkom.DotnetCore.Models.SearchCriterias;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Altkom.DotnetCore.WebApi.Controllers
{
   

    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository customerRepository;

        public CustomersController(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        // GET api/customers

        //[HttpGet]
        //public IActionResult Get()
        //{
        //    var customers = customerRepository.Get();

        //    return Ok(customers);
        //}

        // GET api/customers/marcin.sulecki@akademia.altkom.pl
        [HttpGet("{email}", Order = 2)]
        public IActionResult Get(string email)
        {
            throw new NotImplementedException();

        }

        [HttpGet("{peselId:pesel}", Order = 2)]
        public IActionResult GetByPesel(string peselId)
        {
            throw new NotImplementedException();
        }


        // GET api/customers/10
        [HttpGet("{id:int}", Order = 1)]
        public IActionResult Get(int id)
        {
            var customer = customerRepository.Get(id);

            return Ok(customer);
        }

        // GET api/customers/active
        [HttpGet]
        [Route("active")]
        public IActionResult GetActive()
        {
            throw new NotImplementedException();
        }

        // GET api/orders
        [HttpGet]
        [Route("~/api/orders")]
        public IActionResult GetOrders()
        {
            throw new NotImplementedException();

        }

        // GET api/customers?City=Katowice
        [HttpGet]
        public IActionResult GetByCity([FromQuery] string city)
        {
            throw new NotImplementedException();
        }

        // GET api/customers?City=Katowice&street=Sokolska&country=Poland
        //[HttpGet]
        //public IActionResult GetByAddress([FromQuery] string city, [FromQuery] string street, [FromQuery] string country)
        //{
        //    throw new NotImplementedException();
        //}

        // GET api/customers?City=Katowice&street=Sokolska&country=Poland
        [HttpGet]
        public IActionResult GetByAddress([FromQuery] CustomerSearchCriteria criteria)
        {

            throw new NotImplementedException();
        }

    }
}
