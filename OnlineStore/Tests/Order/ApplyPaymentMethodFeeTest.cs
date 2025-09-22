namespace OnlineStore.Tests;
using Xunit;  // needed for [Fact]
using Moq;
using OnlineStore.Models.Dtos.Requests;
using OnlineStore.Models;

public class ApplyPaymentMethodFeeTest : IClassFixture<OrderHelperFixture>
{
    private readonly OrderHelperFixture _fixture;

    public ApplyPaymentMethodFeeTest(OrderHelperFixture fixture)
    {
        _fixture = fixture;
    }
    
    // check payment fees 
    [Fact]
    public async Task ApplyPaymentMethodFee_Cash()
    {
        // Act
        // final price after apply payment method fees
        var finalPrice = await  _fixture.OrderHelper.ApplyPaymentFee(75m, PaymentMethod.Cash); // adds 20

        // Assert
        Assert.Equal(95m, finalPrice);
    }
    [Fact]
     public async Task ApplyPaymentMethodFee_Card()
    {
        // Act
        // final price after apply payment method fees
        var finalPrice = await  _fixture.OrderHelper.ApplyPaymentFee(75m, PaymentMethod.Card); // adds 0

        // Assert
        Assert.Equal(75m, finalPrice);
    }
}
