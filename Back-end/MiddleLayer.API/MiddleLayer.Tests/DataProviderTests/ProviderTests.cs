namespace MiddleLayer.Tests.DataProviderTests
{
    public class ProviderTests :TestBase
    {
        [Test]
        public async Task FetchDataSuccess()
        {
            var result = await HttpClient.FetchData();
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task DeleteDataSuccess()
        {
            var result = await HttpClient.DeleteData();
            Assert.IsNotNull(result);
        }
    }
}
