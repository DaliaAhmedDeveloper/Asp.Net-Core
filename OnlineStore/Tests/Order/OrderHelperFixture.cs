namespace OnlineStore.Tests;
using OnlineStore.Helpers;
using OnlineStore.Repositories;
using OnlineStore.Services;
using Microsoft.Extensions.Localization;
using Moq;

public class OrderHelperFixture
{
    public Mock<ICouponService> MockCoupon { get; }
    public Mock<IWalletService> MockWallet { get; }
    public Mock<IUserPointService> MockUserPoint { get; }
    public Mock<IUnitOfWork> MockUnitOfWork { get; }
    public Mock<IEmailService> MockEmail { get; }
    public Mock<PushNotificationHelper> MockPush { get; }
    public Mock<IStringLocalizer<OrderHelper>> MockLocalizer { get; }
    public AppSettingHelper AppSetting { get; }
    public OrderHelper OrderHelper { get; }

    public OrderHelperFixture()
    {
        MockCoupon = new Mock<ICouponService>();
        MockWallet = new Mock<IWalletService>();
        MockUserPoint = new Mock<IUserPointService>();
        MockUnitOfWork = new Mock<IUnitOfWork>();
        MockEmail = new Mock<IEmailService>();
        MockPush = new Mock<PushNotificationHelper>();
        MockLocalizer = new Mock<IStringLocalizer<OrderHelper>>();
        var mockAppSettingService = new Mock<IAppSettingService>();
        AppSetting = new AppSettingHelper(mockAppSettingService.Object);

        OrderHelper = new OrderHelper(
            MockCoupon.Object,
            MockWallet.Object,
            MockUserPoint.Object,
            MockUnitOfWork.Object,
            MockEmail.Object,
            AppSetting,
            MockPush.Object,
            MockLocalizer.Object
        );
    }
}
