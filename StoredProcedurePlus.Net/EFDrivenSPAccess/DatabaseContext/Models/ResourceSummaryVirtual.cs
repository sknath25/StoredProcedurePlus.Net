using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDrivenSPAccess.DatabaseContext.Models
{
    public class ResourceSummaryVirtual
    {
        public virtual int PersonId { get; set; }
        public virtual string PersonName { get; set; }
        public virtual string Country { get; set; }
        public virtual string State { get; set; }
        public virtual string City { get; set; }
        public virtual string District { get; set; }
        public virtual string Pin { get; set; }
        public virtual string Street { get; set; }
        public virtual string HouseNo { get; set; }
        public virtual string MothersName { get; set; }
        public virtual string FathersName { get; set; }
        public virtual string Employer { get; set; }
        public virtual string EmployerCountry { get; set; }
        public virtual string EmployerState { get; set; }
        public virtual string EmployerCity { get; set; }
        public virtual string EmployerDistrict { get; set; }
        public virtual string EmployerPin { get; set; }
        public virtual string EmployerStreet { get; set; }
        public virtual string EmployerHouseNo { get; set; }
        public virtual decimal CTC { get; set; }
        public virtual decimal NET { get; set; }
        public virtual decimal Gross { get; set; }
        public virtual string EmailAddress { get; set; }
        public virtual string EmailAddress2 { get; set; }
        public virtual string MobileNo { get; set; }
        public virtual string MobileNo2 { get; set; }
        public virtual string HomePhoneNo { get; set; }
    }
}
