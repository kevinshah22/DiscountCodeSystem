using Grpc.Core;
using DiscountCodeService.Services;

namespace DiscountCodeService.Services
{
    /// <summary>
    /// Discount service will use protos to generate the response based on the types of request made by the user and the request parameters. passed by the client application.
    /// </summary>
    public class DiscountServiceImpl: DiscountService.DiscountServiceBase
    {
        private readonly CodeStore _store;

        public DiscountServiceImpl(CodeStore store)
        {
            _store = store;
        }

        /// <summary>
        /// Generate code method will accept the request and send the response which configured in the protos class.
        /// </summary>
        public override async Task<GenerateResponse> GenerateCodes(GenerateRequest request, ServerCallContext context)
        {
            var (success, codes) = await _store.GenerateAsync((int)request.Count, (int)request.Length);
            return new GenerateResponse { Result = success, Codes = { codes } };
        }

        /// <summary>
        /// Use code method will udpate the record if user pass valid code and send response according to configuration in protos.
        /// </summary>
        public override async Task<UseCodeResponse> UseCode(UseCodeRequest request, ServerCallContext context)
        {
            var result = await _store.UseCodeAsync(request.Code);
            return new UseCodeResponse { Result = result };
        }
    }
}
