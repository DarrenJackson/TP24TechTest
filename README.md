## TP24 Tech Test
Probably spent about 4 hours doing this. 
Made up some rules about extracting some summaries about the data without fully understanding the domain.

Shows basic CRUD type API for managing the receivables, and a data summary API for returning invoices for a debtor. Invoices returned can be specified as open, close or neither (returning all invoices for that debtor).

All data is stored in memory for the purposes of this test.

Further API developments could be made to extract invoices based on dates and to refactor the remove logic from payload controller.

It was devoloped using Rider and used Swagger pages to drive the API for testing.

## Payload Assumptions

* Reference: unique identifier for a payload
* DebtorReference: unique identifier for a debtor

Payload API to 
* Create a new payload and save it
* Delete a payload
* Update an existing payload

Invoice API to return a list of invoices for a debtor reference, including:
* DebtorReference,
* List of invoices
* Total of all returned invoices due amount 
* Total of all returned invoices paid amount
