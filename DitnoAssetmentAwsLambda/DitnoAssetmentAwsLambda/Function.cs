using System.Net;
using System.Text.Json;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using DitnoCalculateBusinessDay.Models;
using DitnoCalculateBusinessDay.Services;
using Microsoft.Extensions.DependencyInjection;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace DitnoCalculateBusinessDay;

public class Function
{
    private readonly IBusinessDayService _businessDayService;

    public Function()
    {
        var startup = new Startup();
        var serviceProvider = startup.Setup();

        _businessDayService = serviceProvider.GetRequiredService<IBusinessDayService>();
    }


    /// <summary>
    /// A lambda function that calculate business days between two dates, excluded the from date and to date.
    /// </summary>
    /// <param name="request">APIGatewayHttpApiV2ProxyRequest</param>
    /// <param name="context">ILambdaContext</param>
    /// <returns>Number of business days</returns>
    public async Task<APIGatewayHttpApiV2ProxyResponse> FunctionHandler(APIGatewayHttpApiV2ProxyRequest request, ILambdaContext context)
    {
        if (request.RequestContext.Http.Method != HttpMethod.Post.Method)
        {
            return new APIGatewayHttpApiV2ProxyResponse
            {
                Body = "This function only accept POST method.",
                StatusCode = (int)HttpStatusCode.MethodNotAllowed,
                Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
            };
        }
        if (string.IsNullOrEmpty(request.Body))
        {
            return new APIGatewayHttpApiV2ProxyResponse
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
            };
        }
        try
        {
            var dateRange = JsonSerializer.Deserialize<DateRange>(request.Body);
            if (dateRange == null)
            {
                var message = $"Request body is not in correct format of type {nameof(DateRange)}";
                context.Logger.LogError(message);

                return new APIGatewayHttpApiV2ProxyResponse
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Body = message,
                    Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
                };
            }

            var result = _businessDayService.CalculateBusinessDays(dateRange);
            return new APIGatewayHttpApiV2ProxyResponse
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = JsonSerializer.Serialize(result),
                Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
            };
        }
        catch (Exception ex)
        {
            var message = $"Error in function calculate business days. Message: {ex.Message}";
            context.Logger.LogError(message);

            return new APIGatewayHttpApiV2ProxyResponse
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Body = message,
                Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
            };
        }
    }
}
