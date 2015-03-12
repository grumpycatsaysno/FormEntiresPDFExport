# FormEntiresPDFExport
Custom library to export form responses to PDF format

### Requirements

* Sitefinity license
* .NET Framework 4
* Visual Studio 2012
* Microsoft SQL Server 2008R2 or later versions

### License information

This project has been released under the Apache License, version 2.0, the text of which is included in the repository.

### Installation instructions

* Clone the repository to your file system
* Open SitefintiyWebApp in Visual Studio
* Copy the class library to your solution and build it
* Add a reference from the pdfExporter Class Library to SitefinityWebApp
* Build the solution
* Go to Administration -> Settings -> Advanced -> ContentView -> Controls -> FormsBackend -> Views -> FormsBackendListDetail -> Master definition config -> Toolbar -> Sections -> Entries -> Items -> ExportWidget. Change the text to "Export as PDF" and remove the value from Global resource class ID and Save changes.
* Restart Sitefinity application


### Additional resources:

For more detailed explanation on the code see:
[My personal blog - Sitefinity tips and tricks](http://www.sitefinitytipsandtricks.net/)
