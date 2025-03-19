using Microsoft.AspNetCore.Mvc;
using NumberToTextApp.Controllers;
using NumberToTextApp.Models;
using Xunit;

namespace NumberToTextApp.Tests{
    
    public class HomeControllerTests{
        private readonly HomeController _controller;

        public HomeControllerTests(){
            _controller = new HomeController();
        }

        [Fact]
        public void Index_Get_ReturnsViewResult(){
            var result = _controller.Index();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Test1(){
            var result = HomeController.Converter("123.45");
            Assert.Equal("ONE HUNDRED AND TWENTY-THREE DOLLARS AND FORTY-FIVE CENTS", result);
        }

        [Fact]
        public void Test2(){
            var result = HomeController.Converter("0.45");
            Assert.Equal("FORTY-FIVE CENTS", result);
        }

        [Fact]
        public void Test3(){
            var result = HomeController.Converter("123.99");
            Assert.Equal("ONE HUNDRED AND TWENTY-THREE DOLLARS AND NINETY-NINE CENTS", result);
        }

        [Fact]
        public void Test4(){
            var result = HomeController.Converter("10");
            Assert.Equal("TEN DOLLARS", result);
        }

        [Fact]
        public void Test5(){
            var result = HomeController.Converter("1");
            Assert.Equal("ONE DOLLAR", result);
        }

        [Fact]
        public void Test6(){
            var result = HomeController.Converter("0.01");
            Assert.Equal("ONE CENT", result);
        }
    }
}
