using Xunit;

namespace AceBackEnd.Data_Transfer_Objects
{
    public class DTOTests
    {

        [Fact]
        public void FuelQuoteHistoryDTO_Properties_ReturnExpectedValues()
        {
            // Arrange
            int id = 1;
            int gallonsRequested = 1000;
            string deliveryAddress = "123 Main St";
            DateTime deliveryDate = new DateTime(2023, 7, 14);
            decimal suggestedPrice = 2.25m;
            decimal totalAmountDue = 2250.0m;

            // Act
            FuelQuoteHistoryDTO dto = new FuelQuoteHistoryDTO
            {
                Id = id,
                GallonsRequested = gallonsRequested,
                DeliveryAddress = deliveryAddress,
                DeliveryDate = deliveryDate,
                SuggestedPrice = suggestedPrice,
                TotalAmountDue = totalAmountDue
            };

            // Assert
            Assert.Equal(id, dto.Id);
            Assert.Equal(gallonsRequested, dto.GallonsRequested);
            Assert.Equal(deliveryAddress, dto.DeliveryAddress);
            Assert.Equal(deliveryDate, dto.DeliveryDate);
            Assert.Equal(suggestedPrice, dto.SuggestedPrice);
            Assert.Equal(totalAmountDue, dto.TotalAmountDue);
        }

        [Fact]
        public void RegisterDTO_Properties_ReturnExpectedValues()
        {
            // Arrange
            string username = "john.doe";
            string password = "password123";

            // Act
            RegisterDTO dto = new RegisterDTO
            {
                Username = username,
                Password = password
            };

            // Assert
            Assert.Equal(username, dto.Username);
            Assert.Equal(password, dto.Password);
        }

        [Fact]
        public void LoginDTO_Properties_ReturnExpectedValues()
        {
            // Arrange
            string username = "john.doe";
            string password = "password123";

            // Act
            LoginDTO dto = new LoginDTO
            {
                Username = username,
                Password = password
            };

            // Assert
            Assert.Equal(username, dto.Username);
            Assert.Equal(password, dto.Password);
        }

        [Fact]
        public void FinishProfileDTO_Properties_ReturnExpectedValues()
        {
            // Arrange
            string fullname = "John Doe";
            string addressone = "123 Main St";
            string addresstwo = "Apt 4B";
            string city = "New York";
            string state = "NY";
            string zipcode = "12345";

            // Act
            FinishProfileDTO dto = new FinishProfileDTO
            {
                Fullname = fullname,
                Addressone = addressone,
                Addresstwo = addresstwo,
                City = city,
                State = state,
                Zipcode = zipcode
            };

            // Assert
            Assert.Equal(fullname, dto.Fullname);
            Assert.Equal(addressone, dto.Addressone);
            Assert.Equal(addresstwo, dto.Addresstwo);
            Assert.Equal(city, dto.City);
            Assert.Equal(state, dto.State);
            Assert.Equal(zipcode, dto.Zipcode);
        }

        [Fact]
        public void ClientInformationDTO_Properties_ReturnExpectedValues()
        {
            // Arrange
            string username = "john.doe";
            string password = "password123";
            string fullname = "John Doe";
            string addressone = "123 Main St";
            string addresstwo = "Apt 4B";
            string city = "New York";
            string state = "NY";
            string zipcode = "12345";

            // Act
            ClientInformationDTO dto = new ClientInformationDTO
            {
                Username = username,
                Password = password,
                Fullname = fullname,
                Addressone = addressone,
                Addresstwo = addresstwo,
                City = city,
                State = state,
                Zipcode = zipcode
            };

            // Assert
            Assert.Equal(username, dto.Username);
            Assert.Equal(password, dto.Password);
            Assert.Equal(fullname, dto.Fullname);
            Assert.Equal(addressone, dto.Addressone);
            Assert.Equal(addresstwo, dto.Addresstwo);
            Assert.Equal(city, dto.City);
            Assert.Equal(state, dto.State);
            Assert.Equal(zipcode, dto.Zipcode);
        }
    }
}
