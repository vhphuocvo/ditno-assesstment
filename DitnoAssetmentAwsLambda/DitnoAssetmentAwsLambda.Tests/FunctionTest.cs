using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.TestUtilities;
using Newtonsoft.Json;
using DitnoCalculateBusinessDay.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DitnoCalculateBusinessDay.Tests;

[TestClass]
public class FunctionTest
{
    [TestMethod]
    [DataRow("04/08/2021", "06/08/2021", 1)]
    [DataRow("02/08/2021", "12/08/2021", 7)]
    [DataRow("30/08/2022", "06/09/2022", 4)]
    [DataRow("11/09/2022", "17/09/2022", 5)]
    public void CalculateBusinessDayTest(string from, string to, int expectedResult)
    {
        // Arrange
        var function = new Function();
        var context = new TestLambdaContext();
        APIGatewayHttpApiV2ProxyRequest request;
        using (var reader = new StreamReader("TestData/ApiGatewayRequest.json"))
        {
            var json = reader.ReadToEnd();
            request = JsonConvert.DeserializeObject<APIGatewayHttpApiV2ProxyRequest>(json);
        }

        request.Body = JsonConvert.SerializeObject(new DateRange
            { FromDate = TestHelper.ConvertToDate(from), ToDate = TestHelper.ConvertToDate(to) });

        // Act
        var response = function.FunctionHandler(request, context);
        
        // Assert
        Assert.AreEqual(200, response.Result.StatusCode);
        var actual = JsonConvert.DeserializeObject<int>(response.Result.Body);
        Assert.AreEqual(expectedResult, actual);

    }
}
