using System;
using System.Threading.Tasks;
using Grpc.Core;
using SomeSimpleEcommerce;

namespace Server
{
    public class EcommerceService : SomeSimpleEcommerce.ShopService.ShopServiceBase
    {
        public override Task<TimeResponse> GiveMeTheTime(Empty request, ServerCallContext context)
        {
            var time = DateTime.UtcNow;

            var response = new TimeResponse()
            {
                Hour = time.Hour,
                Minute = time.Minute
            };

            return Task.FromResult(response);
        }

        public override Task<CartResponse> ShowChart(CartRequest request, ServerCallContext context)
        {
            var cart = new CartResponse()
            {
                Items = {
                    new CartItem()
                    {
                        ItemID = 1,
                        ItemName = "Portocale",
                        ItemQuantity = 1
                    },
                    new CartItem()
                    {
                        ItemID = 2,
                        ItemName = "Cartofi",
                        ItemQuantity = 30
                    }
                },
                Total = 123.34f
            };

            if (request.UserId == 1)
                return Task.FromResult(cart);
            else
            {
                throw new RpcException(new Status(StatusCode.Internal, "User invalid"));
            }
        }
    }
}