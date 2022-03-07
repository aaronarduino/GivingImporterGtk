# GivingImporterGtk

## Description
**GivingImporterGtk** is a utility application to import Servant Keeper export files into Planning Center Giving.

I built this application to help my church migrate to Planning Center Giving from a different online tithe platform. Other migration programs were available, but they were cost-prohibitive to use for the small amount of data to be transferred.

## Table of Contents
- **Creation Process**
- **How to build**
- **Project Status**
- **Known Issues**

## Creation Process
- I started with writing Planning Center API client
- Then I planned and built the GUI
- Then I built the service that would act as a "controller" of sorts to interface between the "models" (API and files) and the view (gtk GUI)
- I realized that allowing a lot of data to upload without a human checking the data is a bad idea.
- I solved that problem by splitting the input file into smaller files of a configurable size and then giving the user control over when the application would upload each smaller file.
- I designed the first UI with two tabs, one for the split file step and one for the upload step. However, the UI would become unresponsive due to thread blocking tasks that the file picker widget does when focused on.
- Based on how simple that application is and not having control over the underlying gtk library, I decided to pivot from a tabbed UI to each step residing in the same window side by side. I am much happier with the result.
- **More details to come...**

## How to run
1. Open in Visual Studio
1. Fill in app.config file
1. Rename config file
1. Run in release or debug mode (whichever your preference is)

## How to use
1. Select file in file selection widget on left
1. Click "Select File" button
1. Files will be split if needed.
1. Select file in the list on the right
1. Click "upload" to upload the file

## Project Status
The application is in a working state. However, I think there can be improvements to the code in consolidating concerns and simplifying premature optimization.

## Known Issues
- Can not automatically resolve Donor name mismatches.
- When running in debug, mode buttons do not work the first time.
