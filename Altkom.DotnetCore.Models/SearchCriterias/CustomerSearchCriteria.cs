using System;
using System.Collections.Generic;
using System.Text;

namespace Altkom.DotnetCore.Models.SearchCriterias
{
    public abstract class SearchCriteria
    {

    }

    public class CustomerSearchCriteria : SearchCriteria
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PostCode { get; set; }

        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }
}
