# mitoSoft.Razor.Logging
A Razor library with different log mechanisms to usage in APS.net core projects.

It includes is a sedcond Console logger with a different color mechanism.
Additionally there is a file logger, a logger that produces events whenever a log message arrives and a logger that stores its messages in a dictionary.

## Dependencies

Microsoft.Extensions.Logging (Version 6.0.0)
Microsoft.AspNetCore.Components.Web (Version 5.0.12)

## Example usage in a Blazor Server-Side App

```c#

    public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(builder =>
                                  builder.ClearProviders()
                                         .AddColorConsole(o =>
                                         {
                                             o.DateTimeKind = DateTimeKind.Local;
                                         })
                                         .AddPage(o =>
                                         {
                                             o.MaxRows = 2;
                                             o.DateTimeKind = DateTimeKind.Utc;
                                         })
										 .AddEvent(o =>
                                         {
                                             o.LogCallback = Program.Callback;
                                         })
                                         .AddFile(o =>
                                         {
                                             o.DateTimeKind = DateTimeKind.Local;
                                             o.Path = "{date}_log.txt";
                                         })
                                         .AddDictionary(o =>
                                         {
                                             o.DateTimeKind = DateTimeKind.Local;
                                             o.RegisterCallback = Program.RegisterLogger;
                                             o.MaxRows = 10; //max elements of each logger
                                         })                                         
                )
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
```

The color console logger produces an output as follows:

![Screenshot](ConsoleExample.png)

An example configuration for the file Logger in the appsettings.json file is given below:

```c#
    /*
    Usage in appsettings.json
     "Logging": {
       ...,
       "File": {
         "LogLevel": {
           "Default": "Information"
         }
       },
       ...
    */
```

The Aliases for the other loggers are:
-Event (EventLogger)
-ColorConsole (ColorConsoleLogger)
-Dictionary (DictionaryLogger)