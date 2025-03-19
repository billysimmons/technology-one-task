using System;
using Microsoft.AspNetCore.Mvc;
using NumberToTextApp.Controllers;
using NumberToTextApp.Models;
using Xunit;

namespace NumberToTextApp.Tests
{
    public class HomeControllerTests
    {
        private readonly HomeController _controller;

        public HomeControllerTests()
        {
            _controller = new HomeController();
        }

        [Fact]
        public void Index_Get_ReturnsViewResult()
        {
            // Act
            var result = _controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Index_Post_WithValidInput_ReturnsCorrectOutput()
        {
            // Arrange
            var model = new ConversionResultModel
            {
                InputNumber = "123.45"
            };

            // Act
            var result = _controller.Index(model);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var returnedModel = Assert.IsAssignableFrom<ConversionResultModel>(viewResult.Model);
            Assert.Equal("ONE HUNDRED AND TWENTY-THREE DOLLARS AND FORTY-FIVE CENTS", returnedModel.OutputText);
        }

        [Fact]
        public void Converter_ValidInput_ReturnsCorrectResult()
        {
            // Act
            var result = HomeController.Converter("123.45");

            // Assert
            Assert.Equal("ONE HUNDRED AND TWENTY-THREE DOLLARS AND FORTY-FIVE CENTS", result);
        }

        [Fact]
        public void Converter_InputWithOnlyCents_ReturnsCorrectCents()
        {
            // Act
            var result = HomeController.Converter("0.45");

            // Assert
            Assert.Equal("FORTY-FIVE CENTS", result);
        }

        [Fact]
        public void Converter_InputWithCentsAndDollars_ReturnsCorrectText()
        {
            // Act
            var result = HomeController.Converter("123.99");

            // Assert
            Assert.Equal("ONE HUNDRED AND TWENTY-THREE DOLLARS AND NINETY-NINE CENTS", result);
        }
    }
}
