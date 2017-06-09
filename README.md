# C# Week 3 Code Review: Hair Salon Application

#### A program that allows users to create to track and manage Hair Stylists and their Clients. 6/9/17

#### By **Andrew Dalton**

## Description

A website created with C# and HTML where a user can create a list of Hair Stylists and their Clients and use those lists to track which Hair Stylist a Client is attached to and how many Clients a Hair Stylist is attached to.


### Specifications
| Behavior | Input | Output |
| :------- | :---- | :----- |
| **Employees can create a Stylist | "Add Beth Smith" | "Beth Smith has been successfully added." |
| **Employees can create a Client with a Stylist | "Add Rick Sanchez" | "Rick Sanchez has been successfully added. His stylist is Beth Smith." |
| **A Client can only have one Stylist | "Rick Sanchez" | "Rick Sanchez's Stylist is Beth Smith" |
| **A Stylist can have more than one Client | "Beth Smith" | "Beth Smith has the following clients: Rick Sanchez, Jerry Smith, Summer Smith, Morty Smith" |
| **Clients can be viewed | "Bojack Horseman" | "Bojack Horseman's Stylist is Princess Carolyn." |
| **Stylists can be viewed with all their Clients listed | "Mr. Meeseeks" | "Mr. Meeseeks has the following clients: Jerry Smith, Beth Smith, and Summer Smith" |
| **Clients can be updated | "Richard Sanchez" | "Rick Sanchez is now Richard Sanchez" |
| **Stylists can be updated | "Beth Smith, Beth.Smith@yahoo.com" | "Beth Smith's new email is Beth.Smith@yahoo.com" |
| **Clients can be deleted | "Jerry Smith - Delete" | "Jerry Smith has been removed from clients." |
| **Stylists can be deleted | "Beth Smith - Delete" | "Beth Smith has been removed from Stylists" |
| **Clients of the same Stylist can be searched | "Princess Carolyn's Clients" | "Bojack Horseman, Mr. Peanutbutter, Todd Chavez", "Diane Nguyen" |


## Setup/Installation Requirements

1. To run this program, you must have a C# compiler. I use [Mono](http://www.mono-project.com).
2. Install the [Nancy](http://nancyfx.org/) framework to use the view engine. Follow the link for installation instructions.
3. Clone this repository.
4. Open the command line--I use PowerShell--and navigate into the repository. Use the command "dnx kestrel" to start the server.
5. On your browser, navigate to "localhost:5004" and enjoy!

## Known Bugs
* No known bugs at this time.

## Technologies Used
* C#
  * Nancy Framework
  * Razor View Engine
  * ASP.NET Kestrel HTTP server
  * xUnit
  * SQL and SSMS Database Software

* HTML

## Support and contact details

_If you notice any issues regarding my page or my code, please contact me at expandrew@gmail.com._

### License

*{This software is licensed under the GPL license}*

Copyright (c) 2017 **_{Andrew Dalton, Epicodus}_**
