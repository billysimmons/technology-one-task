namespace NumberToTextApp.Models {

    public class ConversionResultModel {
        public required string InputNumber { get; set; }
        public required string OutputText { get; set; } = "";
    }

    public class ConversionMappingModel {
        public static Dictionary<int, string> intToText = new Dictionary<int, string> {
            {0, " "}, {1, "ONE"}, {2, "TWO"}, {3, "THREE"}, {4, "FOUR"}, {5, "FIVE"},
            {6, "SIX"}, {7, "SEVEN"}, {8, "EIGHT"}, {9, "NINE"}, {10, "TEN"},
            {11, "ELEVEN"}, {12, "TWELVE"}, {13, "THIRTEEN"}, {14, "FOURTEEN"}, {15, "FIFTEEN"},
            {16, "SIXTEEN"}, {17, "SEVENTEEN"}, {18, "EIGHTEEN"}, {19, "NINETEEN"},
            {20, "TWENTY"}, {30, "THIRTY"}, {40, "FORTY"}, {50, "FIFTY"}, {60, "SIXTY"},
            {70, "SEVENTY"}, {80, "EIGHTY"}, {90, "NINETY"}
        };
        public static string[] intToGroup = { "", "THOUSAND ", "MILLION ", "BILLION " };
    }
}