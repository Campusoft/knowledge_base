# Runtime, configuración y concurrencia

## Configurations 


Multiple Ways To Access Configurations In .NET Applications. (appsettings)
- IConfiguration
- GetValue
- GetSection
- GetChildren
- Exists
- Bind
- Options Pattern
https://thecodeblogger.com/2021/04/20/multiple-ways-to-access-configurations-in-net-applications/




TimeSpan configuration values in .NET Core by Mark Seemann
- Leer numero entero. (Total segundos, minutos, o la medida que se establece que significa el numero)

Configuracion Json
```
{
  "SeatingDurationInSeconds": "9000"
}
```
Codigo
```
var seatingDuration = TimeSpan.FromSeconds(Configuration.GetValue<int>("SeatingDurationInSeconds"));
```

Use the [standard TimeSpan string](https://docs.microsoft.com/en-us/dotnet/standard/base-types/standard-timespan-format-strings) representation instead:

Configuracion Json
```
{
  "SeatingDuration": "2:30:00"
}
```
Codigo
```
var seatingDuration = Configuration.GetValue<TimeSpan>("SeatingDuration");
```
https://blog.ploeh.dk/2019/11/25/timespan-configuration-values-in-net-core/ 


**Options pattern**

- Is registered as a Singleton and can be injected into any service lifetime.

Options pattern suggests two things:

- there should be a class, Options class, which is not abstract with public parameter-less constructor
- which has public read-write properties
	
IOptionsSnapshot:
When you use IOptionsSnapshot<TOptions>, options are computed once per request when accessed and are cached for the lifetime of the request. Changes to the configuration are read after the app starts when using configuration providers that support reading updated configuration values.

	
IOptionsMonitor:

Some file systems, such as Docker containers and network shares, may not reliably send change notifications. When using the IOptionsMonitor<TOptions> interface in these environments, set the DOTNET_USE_POLLING_FILE_WATCHER environment variable to 1 or true to poll the file system for changes. The interval at which changes are polled is every four seconds and is not configurable.	

### Revisiones

IOption<T> o IOptionsSnapshot<T> o tal vez IOptionsMonitor<T>

## Channels

Channel basically is a data structure which supports the communication between a producer and consumer, it’s thread safe and has an excellent notification framework in both directions.

The System.Threading.Channels namespace provides us with the necessary constructs to make building a pipeline of producers/consumers easier, without having to worry about locking and other potential concurrency issues. It also provides bounded and unbounded “channels”

- System.Threading.Channels is always available in .NET Core
- Thread safe
- Concurrency


Now, a Channel can be bounded or unbounded:

- Bounded Channels have a finite capacity for incoming messages, meaning that a Producer can publish only a specific amount of times before fulfilling the space. Then it will have to wait for the Consumers to execute their work and free up some space for new messages.
- Unbounded Channels instead don’t have this limitation, meaning that Publishers can publish as many times as they want, hoping that the Consumers are able to keep up.


Reading from a Channel

Option: ReadAllAsync
Another option ("Option 1" in the code) is to use "ReadAllAsync".

```
// Read person objects from the channel until the channel is closed
    // OPTION 1: Using ReadAllAsync (IAsyncEnumerable)
    await foreach (Person person in channel.Reader.ReadAllAsync())
    {
        Console.WriteLine($"{person.ID}: {person}");
    }
```

 "ReadAllAsync" is a method on the channel "Reader" property. This returns an "IAsyncEnumerable<Person>". This combines the power of enumeration ("foreach") with the concurrency of "async".

The "foreach" loop condition looks pretty normal -- we iterate on the result of "ReadAllAsync" and use the "Person" object from that iteration.

What is a little different is that there is an "await" before the "foreach". This means that the "foreach" loop may pause between iterations if it needs to wait for an item to be populated in the channel.

The effect is that the "foreach" will continue to provide values until the channel is closed and empty. Inside the loop, we write the value to the console. 


Option: WaitToReadAsync / TryRead
"Option 2" uses a couple of "while" loops to read from the channel.

```
    // Read person objects from the channel until the channel is closed
    // OPTION 2: Using WaitToReadAsync / TryRead
    while (await channel.Reader.WaitToReadAsync())
    {
      while (channel.Reader.TryRead(out Person person))
      {
        Console.WriteLine($"{person.ID}: {person}");
      }
    }
```

Closing a Channel

When we are done writing to a channel, we should close it. This tells anyone reading from the channel that there will be no more values, and they can stop reading. We'll see why this is important when we look at reading from a channel. 




### Referencias

Producer/consumer pipelines with System.Threading.Channels
- Explicacion con un escanario
- El ejemplo son una serie pasos que se colocan en pipeline
- producer-consumer pattern
https://blog.maartenballiauw.be/post/2020/08/26/producer-consumer-pipelines-with-system-threading-channels.html


An Introduction to Channels in C# 
- Formas de leer desde un canal.
https://jeremybytes.blogspot.com/2021/02/an-introduction-to-channels-in-c.html

## CancellationToken 


Utilizar HttpContext.RequestAborted, como CancellationToken

```
public class AspNetUserManager<TUser> : UserManager<TUser>, IDisposable where TUser : class
{
    private readonly CancellationToken _cancel;

    /// <summary>
    /// Constructs a new instance of <see cref="AspNetUserManager{TUser}"/>.
    /// </summary>
    /// <param name="store">The persistence store the manager will operate over.</param>
    /// <param name="optionsAccessor">The accessor used to access the <see cref="IdentityOptions"/>.</param>
    /// <param name="passwordHasher">The password hashing implementation to use when saving passwords.</param>
    /// <param name="userValidators">A collection of <see cref="IUserValidator{TUser}"/> to validate users against.</param>
    /// <param name="passwordValidators">A collection of <see cref="IPasswordValidator{TUser}"/> to validate passwords against.</param>
    /// <param name="keyNormalizer">The <see cref="ILookupNormalizer"/> to use when generating index keys for users.</param>
    /// <param name="errors">The <see cref="IdentityErrorDescriber"/> used to provider error messages.</param>
    /// <param name="services">The <see cref="IServiceProvider"/> used to resolve services.</param>
    /// <param name="logger">The logger used to log messages, warnings and errors.</param>
    public AspNetUserManager(IUserStore<TUser> store,
        IOptions<IdentityOptions> optionsAccessor,
        IPasswordHasher<TUser> passwordHasher,
        IEnumerable<IUserValidator<TUser>> userValidators,
        IEnumerable<IPasswordValidator<TUser>> passwordValidators,
        ILookupNormalizer keyNormalizer,
        IdentityErrorDescriber errors,
        IServiceProvider services,
        ILogger<UserManager<TUser>> logger)
        : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
    {
        _cancel = services?.GetService<IHttpContextAccessor>()?.HttpContext?.RequestAborted ?? CancellationToken.None;
    }

    /// <summary>
    /// The cancellation token associated with the current HttpContext.RequestAborted or CancellationToken.None if unavailable.
    /// </summary>
    protected override CancellationToken CancellationToken => _cancel;
}
```

https://github.com/dotnet/aspnetcore/blob/main/src/Identity/Core/src/AspNetUserManager.cs

## Generic Host

Host definition

A host is an object that encapsulates an app's resources, such as:

-    Dependency injection (DI)
-    Logging
-    Configuration
-    IHostedService implementations


Hosted services are long running asynchronous services.
https://wildermuth.com/2020/08/02/NET-Core-Console-Apps---A-Better-Way

Building a Console App with .NET Generic Host
https://dfederm.com/building-a-console-app-with-.net-generic-host/

IHostApplicationLifetime

Inject the IHostApplicationLifetime (formerly IApplicationLifetime) service into any class to handle post-startup and graceful shutdown tasks. Three properties on the interface are cancellation tokens used to register app start and app stop event handler methods. The interface also includes a StopApplication method.
https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/generic-host?view=aspnetcore-5.0#ihostapplicationlifetime

Background tasks with hosted services in ASP.NET Core
In ASP.NET Core, background tasks can be implemented as hosted services.
- BackgroundService is a base class for implementing a long running IHostedService.
https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/hosted-services?view=aspnetcore-6.0&tabs=visual-studio

## Thread

Semaphore and SemaphoreSlim

Async Waiting inside C# Locks
https://blog.cdemi.io/async-waiting-inside-c-sharp-locks/

## Async

Sync over async

Understanding Async, Avoiding Deadlocks in C#
https://medium.com/rubrikkgroup/understanding-async-avoiding-deadlocks-e41f8f2c6f5d

Avoid GetAwaiter().GetResult() at all cost
https://gsferreira.com/archive/2020/08/avoid-getawaiter-getresult-at-all-cost/

## Microsoft.VisualStudio.Threading

 The Microsoft.VisualStudio.Threading is a xplat library that provides many threading and synchronization primitives used in Visual Studio and other applications. 
https://github.com/microsoft/vs-threading
