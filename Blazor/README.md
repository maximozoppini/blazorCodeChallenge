# Code challenge #

The goal is to implement a new page to enter credit card information.
A sample how it should look like you can see in the creditcard.gif
The challenge is divided into two parts, but you can also mix it... you do not need to do the steps in the same order.

## Basic Steps (workload 1-3h)
Implement the basic functionality and layout in the prepared CreditCard.razor
- Basic layout:
  - Implement a basic layout in the same way you see in the creditcard.gif with the same positions and controls, but no real styling like animations, shadows, etc.
- Basic functionality:
  - Enter number and have a live update to show the typed numbers above in the credit card preview dummy
  - Implement all controls
  - Implement validations
  - Validate if credit card is valid
  - Validate the credit card number as well

### Advanced
- Store credit card information in an in memory db. For example with Entity Framework Core
  - Create a model to save the credit card information
  - Show stored information in the UI as well
- Style it in the same way as the creditcard.gif. Shadows, animations, etc. (The credit card need not look like 100% the same, but it should like a credit card ;) )
- Write a small unit test
