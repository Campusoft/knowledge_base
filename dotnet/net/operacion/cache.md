# Cache .Net Core

# Cache in-memory in ASP.NET Core


# Distributed caching in ASP.NET Core

A distributed cache is a cache shared by multiple app servers, typically maintained as an external service to the app servers that access it. A distributed cache can improve the performance and scalability of an ASP.NET Core app, especially when the app is hosted by a cloud service or a server farm.


-   Distributed Memory Cache
-   Distributed SQL Server cache
-   Distributed Redis cache
-   Distributed NCache cache





Sliding Expiration: If a cache entry is accessed during this period, the period resets.
Absolute Expiration: Absolute expiration date for the cache entry.
AbsoluteExpirationRelativeToNow: Expiration time, relative to the current time.
