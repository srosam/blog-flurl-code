using System.Net;
using System.Threading.Tasks;
using Flurl.Http.Testing;
using NUnit.Framework;

namespace FlurlTesting
{
    public class TestsUtilisingUsing
    {
        // Here we are making use of the using statement to clean up
        // after were done with the Flurl HttpTest object
        // The downside is the code isn't as clean as we'd like

        [TestCase(HttpStatusCode.OK, true)]
        [TestCase(HttpStatusCode.Found, false)]
        public async Task CodeUnderTestReturnsCorrectValue(HttpStatusCode statusCode, bool expectedResult)
        {
            //Arrange
            bool result;
            var codeToTest = new ExternalApiCaller();

            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(statusCode.ToString(), (int)statusCode);

                //Act
                result = await codeToTest.MakeTheCall();
            }

            //Assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}