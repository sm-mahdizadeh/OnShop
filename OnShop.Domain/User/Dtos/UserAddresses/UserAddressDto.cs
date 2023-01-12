namespace OnShop.Domain.User.Dtos.UserAddresses
{
    public class UserAddressDto
    {
        public long Id { get; set; }
        public int ApplicationUserId { get; set; }
        public string PostCode { get; set; }
        public string Plaque { get; set; }
        public string Unit { get; set; }
        public string PostalAddress { get; set; }
        public string RecipientFirstName { get; set; }
        public string RecipientLastName { get; set; }
        public string RecipientPhoneNumber { get; set; }
        public string RecipientNationalCode { get; set; }

        public string DistrictName { get; set; }
        public string ZoneName { get; set; }
        public string ProvinceName { get; set; }
    }
    
    public  class DropDownDto
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}