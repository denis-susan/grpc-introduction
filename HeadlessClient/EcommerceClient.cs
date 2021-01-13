using System.Threading.Tasks;
using Grpc.Core;
using SomeSimpleEcommerce;

namespace HeadlessClient
{
    public class EcommerceClient
    {
        private SomeSimpleEcommerce.ShopService.ShopServiceClient _client;
        public EcommerceClient()
        {
            var channel = new Channel("localhost:34567", ChannelCredentials.Insecure);

            _client = new ShopService.ShopServiceClient(channel);
        }

        /// <summary>
        /// Wrapper peste clientul de grpc.
        /// Beneficii: logging, sanitizare, handle errors unrelated de business logic, etc
        /// Dezavantaje: Boilerplate code...
        /// </summary>
        /// <returns></returns>
        public TimeResponse GiveMeTheTime()
        {
            return _client.GiveMeTheTime(new Empty());
        }

        public async Task<TimeResponse> GiveMeTheTimeAsync()
        {
            return await _client.GiveMeTheTimeAsync(new Empty());
        }

        public CartResponse ShowChart(int userId)
        {
            return _client.ShowChart(new CartRequest()
            {
                UserId = userId
            });
        }

        public async Task<CartResponse> ShowChartAsync(int userId)
        {
            return await _client.ShowChartAsync(new CartRequest()
            {
                UserId = userId
            });
        }
    }
}