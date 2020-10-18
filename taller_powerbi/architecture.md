# architecture
 
 
Understanding the SQL Server Analysis Services service behind the scene makes you
think that there are two layers when talking about Power BI, Report, and the Dataset.

When you look at the *.pbix file, you will see only one file. However, when you open it
in Power BI Desktop, you have the Power BI Desktop application, and the SQL Server
Analysis Services service (let’s call it as its abbreviation from this point forward SSAS).
Power BI Desktop application keeps the report, and SSAS keeps the model or dataset.