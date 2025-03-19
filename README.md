# Technology-one-task

NOTE: Explanation of choices are in the ProjectOverview.txt file

## How to build App

1. Clone this repo
2. `cd technology-one-test`
3. `cd NumberToTextApp`
4. `dotnet restore`
5. `dotnet buiid`
6. `dotnet run`

## How to use

1. Enter the dollar amount in the tab (handles up to 2DP) and press submit
2. The number in text will be displayed on the screen just below the submit button

## How to run unit tests

1. cd back into repo
2. `cd NumberToTextApp.Tests`
3. `dotnet restore `
4. `dotnet test`

May have to install the following:

1. Microsoft.NET.Test.Sdk
2. xunit

## Extra Functionality:

- Handles 0.0 input and 1 cent/ dollar edge cases
- Accepts one digit cent values
- Accepts values with no cent input

## Assumptions:

- Number isn’t greater than the int32 max of 2_147_483_647
- Cents are rounded to 2dp
