using FluentAssertions;
using MarsRover.Program.Common.Exceptions.PlateauExceptions;
using MarsRover.Program.Models;
using MarsRover.Program.Services;
using MarsRover.Program.Services.Implementations;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace MarsRover.Tests
{
    public class PlateauTests
    {
        private IPlateauService _plateauService;
        private ILogger<PlateauService> _loggerMock;
        public PlateauTests()
        {
            _loggerMock = Mock.Of<ILogger<PlateauService>>();
        }

        [Theory]
        [InlineData(null)]
        public void Validate_IsNull_ThrowsValidatePlateauException(PlateauModel model)
        {
            _plateauService = new PlateauService(_loggerMock);

            Assert.Throws<ValidatePlateauException>(() => _plateauService.Validate(model));
        }

        [Theory]
        [InlineData(-1, -1)]
        [InlineData(0, 0)]
        [InlineData(-1, 0)]
        [InlineData(0, -1)]
        public void Validate_NotGreaterThanZero_ThrowsValidatePlateauException(int width, int height)
        {
            _plateauService = new PlateauService(_loggerMock);

            PlateauModel model = new PlateauModel();
            model.Width = width;
            model.Height = height;

            Assert.Throws<ValidatePlateauException>(() => _plateauService.Validate(model));
        }

        [Theory]
        [InlineData(100, 100)]
        [InlineData(10, 100)]
        [InlineData(100, 10)]
        public void Validate_GreaterThanZero_ShouldReturnTrue(int width, int height)
        {
            _plateauService = new PlateauService(_loggerMock);

            PlateauModel model = new PlateauModel();
            model.Width = width;
            model.Height = height;

            BaseModel<bool> result = _plateauService.Validate(model);

            result.Data.Should().BeTrue();
        }

        [Theory]
        [InlineData(5, 5)]
        [InlineData(2, 7)]
        [InlineData(7, 2)]
        [InlineData(100, 100)]
        public void Create_PositiveWidthPositiveHeight_ShouldReturnSameValues(int width, int height)
        {
            _plateauService = new PlateauService(_loggerMock);

            BaseModel<PlateauModel> basePlateauModel = _plateauService.Create(width, height);

            basePlateauModel.Data.Width.Should().Equals(width);
            basePlateauModel.Data.Height.Should().Equals(height);
        }
    }
}