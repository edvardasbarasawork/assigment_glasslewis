# assigment_glasslewis

Design and code a Web API solution in .NET Core/5+ and C# for a middle tier “Company API.”

Using this WebAPI, an end user should be able to:

  1. Create a Company record specifying the Name, Stock Ticker, Exchange, ISIN, and optionally a website URL. You are not allowed create two Companies with the same ISIN. The first two characters of an ISIN must be letters / non numeric.
  2. Retrieve an existing Company by Id
  3. Retrieve a Company by ISIN
  4. Retrieve a collection of all Companies
  5. Update an existing Company

Sample company records:

| Name  | Exchange | Ticker | ISIN | website |
| ------------- | ------------- | ------------- | ------------- | ------------- |
| Apple Inc. | NASDAQ | AAPL | US0378331005 | http://www.apple.com |
| British Airways Plc | Pink Sheets | BAIRY | US1104193065 |  |
| Heineken NV | Euronext Amsterdam | HEIA | NL0000009165 |  |
| Panasonic Corp | Tokyo Stock Exchange | 6752 | JP3866800000 | http://www.panasonic.co.jp |
| Porsche Automobil | Deutsche Börse | PAH3 | DE000PAH0038 | https://www.porsche.com/ |

Code should be testable and have some level of unit test coverage. It should run end to end and read and write to a database. Please also design the database you would need and provide all SQL scripts and source used to create the application.

If any additional steps are required to deploy or get the application running these should be documented very clearly.

Bonus points:

Provide a very simple client to call the API and present the results in a browser using any client-side web technology you like.

Even more points:

Add authentication code to secure the API.
