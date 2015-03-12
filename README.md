# FormEntiresPDFExport
Custom library to export form responses to PDF format using [iTextSharp](https://www.nuget.org/packages/iTextSharp/) 3rd party library. See exported sample .pdf document in this repository. 

### Requirements

* Sitefinity license
* .NET Framework 4
* Visual Studio 2012
* Microsoft SQL Server 2008R2 or later versions

### Video demo:

Cick on the image below to watch a demo:
 
[![Revision history settings for pages in Sitefinity](http://news.sudanvisiondaily.com/images/pdf-icon.png)](http://screencast.com/t/Bkvw0I7MqK0)

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
