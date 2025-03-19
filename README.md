# Technology-one-task

## How to build App

1. Clone this repo
2. `cd technology-one-test`
3. `cd NumberToTextApp`
4. `dotnet restore`
5. `dotnet buiid`
6. `dotnet run`

## How to test

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
