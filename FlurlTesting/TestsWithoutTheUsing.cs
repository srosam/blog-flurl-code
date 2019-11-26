using System.Net;
using System.Threading.Tasks;
using Flurl.Http.Testing;
using NUnit.Framework;

namespace FlurlTesting
{
    public class TestsWithoutTheUsing
    {
        // Here we have split the setup tasks out into the Setup method called by
        // Nunit and decorated with the OneTimeSetup attribute
        //
        // The TearDown method is called after the test has done asserting to clean up any 
        // objects etc, if this were an integration test we might clean up a database or
        // other resources
        //
        // The actual test is now cleaner and more readable

        private HttpTest _httpTest;
        private ExternalApiCaller _codeToTest;

        [TestCase(HttpStatusCode.OK, true)]
        [TestCase(HttpStatusCode.Found, false)]
        public async Task CodeUnderTestReturnsCorrectValue(HttpStatusCode statusCode, bool expectedResult)
        {
            //Arrange
            _httpTest.RespondWith(statusCode.ToString(), (int)statusCode);

            //Act
            var result = await _codeToTest.MakeTheCall();

            //Assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [OneTimeSetUp]
        public void Setup()
        {
            _httpTest = new HttpTest();
            _codeToTest = new ExternalApiCaller();
        }

        [OneTimeTearDown]
        public void Teardown()
        {
            _httpTest?.Dispose();
        }
    }
}