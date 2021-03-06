# Bovenste Verdieping

__Backend on .NET 5__

This demo contains a web-app with an architecture on separation of concerns and clean code.

The connected data source is provided by a third part API service.

## Access the online web-app

Hosting: [https://bovenste-verdieping.azurewebsites.net/](https://bovenste-verdieping.azurewebsites.net/)

## Installation

- Download ASP.NET on your computer: [https://dotnet.microsoft.com/download](https://dotnet.microsoft.com/download)

- Get an API key or ask for one at: [https://www.funda.nl/voormakelaars/verdieping/trends/](https://www.funda.nl/voormakelaars/verdieping/trends/) 

- Clone the repo and move to it with your CLI

- run: `dotnet user-secrets set "FundaApiKey" "<your-api-key>"`

- run: `dotnet run`

- Open your browser on [http://localhost:5000/](http://localhost:5000/)

## Notes

Due to API rate usage restriction the data consumption is moderately limited.
