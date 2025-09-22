namespace OnlineStore.Tests;
using Xunit;  // needed for [Fact]
using Moq;
using OnlineStore.Models.Dtos.Requests;
using OnlineStore.Models;

public class FinalPriceCalculationsTest : IClassFixture<OrderHelperFixture>
{
    private readonly OrderHelperFixture _fixture;

    public FinalPriceCalculationsTest(OrderHelperFixture fixture)
    {
        _fixture = fixture;
    }
    [Fact]
    public async Task FinalPriceCalculations()
    {
        // Arrange
        int userId = 1;
        var dto = new CreateOrderDto
        {
            CouponId = 1,
            WalletAmountUsed = 10,
            PointsUsed = 5,
            PaymentMethod = PaymentMethod.Cash
        };

        decimal afterSale = 100;
        var userWallet = new Wallet { Balance = 50 };

        // Setup mocks ,, returns final price , coupon discount 
        _fixture.MockCoupon.Setup(c => c.CheckCoupon(dto.CouponId, userId, afterSale, It.IsAny<decimal>()))
                   .ReturnsAsync((90m, 10m));

        // returns final price
        _fixture.MockWallet.Setup(w => w.CheckWallet(dto.WalletAmountUsed, 90m, userWallet))
                   .Returns(80m);

        // returns finalprice ,, points discounted value
        _fixture.MockUserPoint.Setup(p => p.CheckUserPoints(userId, dto.PointsUsed, 80m))
                      .ReturnsAsync((75m, 5m));

        // Act
        var finalPrice = await _fixture.OrderHelper.FinalPriceCalculations(userId, dto, afterSale, userWallet);

        // Assert
        Assert.Equal(75m, finalPrice);
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
