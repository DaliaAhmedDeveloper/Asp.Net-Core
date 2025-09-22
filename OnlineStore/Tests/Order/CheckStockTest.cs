namespace OnlineStore.Tests;

using Xunit;  // needed for [Fact]
using Moq;
using OnlineStore.Models;
using OnlineStore.Models.Dtos.Responses;

public class CheckStockTest : IClassFixture<OrderHelperFixture>
{
    private readonly OrderHelperFixture _fixture;

    public CheckStockTest(OrderHelperFixture fixture)
    {
        _fixture = fixture;
    }

    public async Task CheckStock()
    {
        // perpare CartDto
        var mockCart = new CartDto
        {
            Id = 1,
            UserId = 123,
            Items = new List<CartItemDto>
            {
                new CartItemDto
                {
                    Id = 1,
                    Quantity = 2,
                    ProductVariantId = 101,
                    Product = new ProductSimpleDto
                    {
                        Id = 201,
                        Price = 50m,
                        SalePrice = 40m,
                        Title = "Product 1",
                        _imageUrl = "product1.png"
                    },
                    ProductVariant = new ProductVariantDto
                    {
                        Id = 101,
                        Price = 50m,
                        SalePrice = 40m,
                        Stock = 10,
                        VariantAttributes = new List<VariantAttributeValueDto>
                        {
                            new VariantAttributeValueDto
                            {
                                Attribute = new AttributeDto
                                {
                                    Id = 301,
                                    Slug = "color",
                                    Title = "Color",
                                    AttributeValue = new AttributeValueDto
                                    {
                                        Id = 401,
                                        Slug = "red",
                                        Title = "Red"
                                    }
                                }
                            }
                        }
                    }
                },

                new CartItemDto
                {
                    Id = 2,
                    Quantity = 1,
                    ProductVariantId = 102,
                    Product = new ProductSimpleDto
                    {
                        Id = 202,
                        Price = 30m,
                        SalePrice = 25m,
                        Title = "Product 2",
                        _imageUrl = "product2.png"
                    },
                    ProductVariant = new ProductVariantDto
                    {
                        Id = 102,
                        Price = 30m,
                        SalePrice = 25m,
                        Stock = 5,
                        VariantAttributes = new List<VariantAttributeValueDto>
                        {
                            new VariantAttributeValueDto
                            {
                                Attribute = new AttributeDto
                                {
                                    Id = 302,
                                    Slug = "size",
                                    Title = "Size",
                                    AttributeValue = new AttributeValueDto
                                    {
                                        Id = 402,
                                        Slug = "m",
                                        Title = "M"
                                    }
                                }
                            }
                        }
                    }
                }
            }
        };

        // Mock Stock ,, fake data
        _fixture.MockUnitOfWork.Setup(u => u.Stock.GetByVariantIdAsync(1))
            .ReturnsAsync(new Stock { TotalQuantity = 10 , ReservedQuantity = 3 });
        _fixture.MockUnitOfWork.Setup(u => u.Stock.GetByVariantIdAsync(2))
            .ReturnsAsync(new Stock { TotalQuantity = 15, ReservedQuantity = 7 });

        // Mock ProductVariant
        _fixture.MockUnitOfWork.Setup(u => u.ProductVariant.GetByIdAsync(1))
            .ReturnsAsync(new ProductVariant { Id = 1, ProductId = 101, Price = 50, SalePrice = 45 });
        _fixture.MockUnitOfWork.Setup(u => u.ProductVariant.GetByIdAsync(2))
            .ReturnsAsync(new ProductVariant { Id = 2, ProductId = 102, Price = 30, SalePrice = 25 });

        // Mock Product
        _fixture.MockUnitOfWork.Setup(u => u.Product.GetByIdAsync(101))
            .ReturnsAsync(new Product { Id = 101, Price = 50, SalePrice = 45 });
        _fixture.MockUnitOfWork.Setup(u => u.Product.GetByIdAsync(102))
            .ReturnsAsync(new Product { Id = 102, Price = 30, SalePrice = 25 });

        var stock = await _fixture.OrderHelper.CheckStock(mockCart);

        //Assert 
        Assert.Equal(70m, stock.TotalBefore);
        Assert.Equal(65m, stock.TotalAfter);

    }

}
