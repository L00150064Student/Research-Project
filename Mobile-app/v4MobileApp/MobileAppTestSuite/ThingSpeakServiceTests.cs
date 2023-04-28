
using v4MobileApp.Services;

namespace MobileAppTestSuite;

public class ThingSpeakServiceTests
{


    [Fact]
    public void ThingSpeakService_Constructor_CreatesRestClient()
    {
        // Arrange + Act
        var thingSpeakService = new ThingSpeakService();

        // Assert
        Assert.NotNull(thingSpeakService);
    }

}
