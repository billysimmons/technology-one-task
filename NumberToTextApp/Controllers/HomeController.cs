using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NumberToTextApp.Models;

namespace NumberToTextApp.Controllers;

public class HomeController : Controller {

    public IActionResult Index() {
        return View();
    }

    [HttpPost] 
    public IActionResult Index(ConversionResultModel model){

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

        if(int.Parse(dollars) == 0 && cents == null){ 
            return "Value must be greater than 0";
        }

        if(int.Parse(dollars) == 0 && cents!= null){ 
            string centsString = ConvertHundredOrLess(int.Parse(cents));
            
            if(int.Parse(cents) == 1){
                return $"{centsString} CENT";
            }
            
            return $"{centsString} CENTS";
        }

        if(cents == null){
            string dollarsString = ConvertDollar(int.Parse(dollars));
            if(int.Parse(dollars) == 1){
                return $"{dollarsString}DOLLAR";
            }
            return $"{dollarsString}DOLLARS";
        }

        if(cents != null){
            string dollarsString = ConvertDollar(int.Parse(dollars));
            string centsString = ConvertHundredOrLess(int.Parse(cents));

            if(int.Parse(cents) == 1){
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

            if (int.Parse(group) > 0){
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
            result += ConversionMappingModel.intToText[num / 100] + " HUNDRED";
            num %= 100; 
            
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
