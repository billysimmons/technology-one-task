using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NumberToTextApp.Models;

namespace NumberToTextApp.Controllers;

public class HomeController : Controller {

    public IActionResult Index() { // handles GET requests for the Index page
        return View();
    }

    [HttpPost] 
    public IActionResult Index(ConversionResultModel model){ // handles POST requests when the form is submitted

        if (model.InputNumber == null){
            model.OutputText = "Input cannot be null";
        }
        else if(double.Parse(model.InputNumber) >= Int32.MinValue && double.Parse(model.InputNumber) <= Int32.MaxValue){
            model.OutputText = Converter(model.InputNumber);
        }
        else {
            model.OutputText = "Please enter a smaller number.";
        }

        return View(model); 
    }
    
    public static string Converter(string input){
        string[] inputStringArr = input.Split('.');
        string dollars = inputStringArr[0];
        string? cents = "";

        if (inputStringArr.Length > 1){
            cents = inputStringArr[1].PadRight(2, '0');

            if(double.Parse(cents) > 99){
                return "Please enter a valid value for cents";
            } 
            else {
                if(double.Parse(cents) <= 0){ 
                    cents = null;
                }
            }
        } else {
            cents = null;
        }

        if(int.Parse(dollars) == 0 && cents == null){ // 0.0 edge case
            return "Value must be greater than 0";
        }

        if(int.Parse(dollars) == 0){ // only cents 
            string centsString = ConvertHundredOrLess(int.Parse(cents));
            
            if(int.Parse(cents) == 1){ // 1 CENT edge case
                return $"{centsString} CENT";

            }
            return $"{centsString} CENTS";
        }

        if(cents == null){ // only dollars
            string dollarsString = ConvertDollar(int.Parse(dollars));
            if(int.Parse(dollars) == 1){ // 1 DOLLAR edge case
                return $"{dollarsString}DOLLAR";
            }
            return $"{dollarsString}DOLLARS";
        }

        if(cents != null){ // both cents and dollars
            string dollarsString = ConvertDollar(int.Parse(dollars));
            string centsString = ConvertHundredOrLess(int.Parse(cents));

            if(int.Parse(cents) == 1){ // 1 CENT edge case
                return $"{dollarsString}DOLLARS AND {centsString} CENT";
            }
            return $"{dollarsString}DOLLARS AND {centsString} CENTS";
        }

        return "Error: Invalid Input";
    }

    private static string ConvertDollar(int num){
        string numString = num.ToString();
        char[] numStringDigits = numString.ToCharArray();

        List<string> resultList = new List<string>();
        
        int groupCount = 0;
        int length = numStringDigits.Length;

        while (length > 0){
            int startIndex = length - 3;
            if (startIndex < 0) startIndex = 0; 
            string group = numString.Substring(startIndex, length - startIndex);  

            if (int.Parse(group) > 0){ // non 0 group
                string item = ConvertHundredOrLess(int.Parse(group)) + " " + ConversionMappingModel.intToGroup[groupCount];
                resultList.Add(item);
            }

            length -= 3;
            groupCount++;
        }

        resultList.Reverse();
        return string.Join(" ", resultList);
    }

    private static string ConvertHundredOrLess(int num){
        string result = "";
        
        if(num >= 100){
            result += ConversionMappingModel.intToText[num / 100] + " HUNDRED"; // divide by 100 for the hundreds digit (e.g. 1 for 123)
            num %= 100; // to get remainder of /100 (e.g. 23 for 123)
            
            if (num != 0){
                result += " AND ";
            }
        }

        if (num >= 20){
            result += ConversionMappingModel.intToText[num / 10 * 10];
            if (num % 10 != 0){
                result += "-" + ConversionMappingModel.intToText[num % 10];
            }
            return result;

        } else {
            result += ConversionMappingModel.intToText[num];
            return result;
        }
    }    

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    
}
