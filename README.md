# Team Lead Refactoring Exercise

## Instructions

You are code reviewing a junior developer’s implementation of a class library following the requirements given below.  You decide that the best way to help the Junior developer improve the quality of the library is to refactor the code yourself, so that you can demonstrate best practice including unit testing and show how this helps discovering bugs.
Except for the signature of the ‘PublishPosterToScreen’ method, you can modify the implementation any way you want including using any frameworks/NuGet packages that you see fit. 

To submit your refactoring please fork this repo then create a PR with your changes.

We expect this exercise will take up to an hour of your time.

## Requirements

Create a library exposing a method ‘PublishPosterToScreen’ that takes a single parameter PublishPosterRequest and publishes a digital poster (an image or video) to a specific screen for a specific advertising campaign. Each campaign contains a list of screens. 
The steps for publishing a poster are:
1.	Get the campaign details from the database
2.	Make sure the screen is one of the campaign’s screens 
3.	Make sure the campaign is active 
4.	Publish the poster to an FTP or Azure Blob Storage based on the screen’s publishing type
5.	Update the “Last Publish Date Time” of the screen in the database, if the poster is successfully published  
