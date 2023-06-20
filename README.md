# OpenAQApp
Created small application with 3 components. 
First is class library for consuming OpenAQ Api. 
2nd application is MVC application for displaying api contents eith only basic design. 
And 3rd is sample test solution where we can perform unit tests if required.
Also, I need some clarification on storing and replaying requests but for now i have added basic functionality for them as sample for storing api request in file but we can do it database as well.
Also, though i haven't implemented but we can implement caching for returning the response faster for api requests.

In MVC application, HomeController is containing code for calling the api method from class library named "APILibrary". 
APILibrary is containing class "OpenAQProcessor" for calling OpenAQ API and getting its content.
