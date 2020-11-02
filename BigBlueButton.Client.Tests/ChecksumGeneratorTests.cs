using BigBlueButton.Client.Helpers;
using Xunit;

namespace BigBlueButton.Client.Tests
{
    public class ChecksumGeneratorTests
    {
        private const string _secret = "639259d4-9dd8-4b25-bf01-95f9567eaf4b";

        [Fact]
        public void Generate_ShouldGenerateValidChecksum_ForCallWithQuery()
        {
            // Arrange
            string callName = "create";
            string query = "name=Test+Meeting&meetingID=abc123&attendeePW=111222&moderatorPW=333444";

            // Act
            string checksum = CheksumGenerator.Generate(callName, _secret, query);

            // Assert
            Assert.Equal("1fcbb0c4fc1f039f73aa6d697d2db9ba7f803f17", checksum);
        }
    }
}
