using BigBlueButton.Client.Parameters;
using System;
using Xunit;

namespace BigBlueButton.Client.Tests
{
    public class ParametersExtractorTests
    {
        [Fact]
        public void GenerateQueryString_ShouldGenerateQueryStringWithCamelCaseParameters()
        {
            // Arrange
            var request = new Request
            {
                String = "test",
                Int = 10,
                Boolean = true
            };

            // Act
            var queryString = ParametersExtractor.GenerateQueryString(request);

            // Assert
            Assert.Equal("string=test&int=10&boolean=True", queryString);
        }

        [Fact]
        public void GenerateQueryString_ShouldNotIncludeNulls()
        {
            // Arrange
            var request = new RequestWithNullables();

            // Act
            var queryString = ParametersExtractor.GenerateQueryString(request);

            // Assert
            Assert.Equal("", queryString);
        }

        [Fact]
        public void GenerateQueryString_ShouldThrowWhenRequiredParameterIsNull()
        {
            // Arrange
            var request = new RequestWithRequiredParameter();

            // Act
            var ex = Assert.Throws<Exception>(() => ParametersExtractor.GenerateQueryString(request));

            // Assert
            Assert.Equal("Parameter string is required.", ex.Message);
        }

        [Fact]
        public void GenerateQueryString_ShouldUseCustomName()
        {
            // Arrange
            var request = new RequestWithCustomName
            {
                String = "test"
            };

            // Act
            var queryString = ParametersExtractor.GenerateQueryString(request);

            // Assert
            Assert.Equal("custom_name=test", queryString);
        }
    }

    class Request
    {
        public string String { get; set; }
        public int Int { get; set; }
        public bool Boolean { get; set; }
    }

    class RequestWithNullables
    {
        public bool? Boolean { get; set; }
        public string String { get; set; }
        public int? Int { get; set; }
    }

    class RequestWithRequiredParameter
    {
        [BBBParameter(Required = true)]
        public string String { get; set; }
    }

    class RequestWithCustomName
    {
        [BBBParameter("custom_name")]
        public string String { get; set; }
    }
}
