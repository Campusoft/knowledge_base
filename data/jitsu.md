# jitsu

The Open Source Segment Alternative

Jitsu is a fully-scriptable data ingestion engine for modern data teams. Set-up a real-time data pipeline in minutes, not days


Jitsu Server Architecture
https://jitsu.com/docs/internals/jitsu-server


Event API

- Jitsu has API for direct event collection. You can use it for sending events directly from apps or backends.

Bulk API

- Jitsu has events bulk API. The endpoint can consume ~50,000 events in one HTTP request and store them into destinations synchronously.

GIF Pixel API

- Jitsu has a GIF Pixel API endpoint for tracking email opens, impressions of advertisements


https://jitsu.com/

# Architecture

Jitsu is written primarily in Go with the frontend written in JavaScript.

# Install

The easiest way to start Jitsu locally is docker-compose. 
https://jitsu.com/docs/deployment/deploy-with-docker/docker-compose


----------
Windows:

Docker Compose

```
Error response from daemon: create compose-data/redis/data: "compose-data/redis/data" includes invalid characters for a local volume name, only "[a-zA-Z0-9][a-zA-Z0-9_.-]" are allowed. If you intended to pass a host directory, use absolute path
```

El problema se origina por path relativos utilizados en los volumenes del archivo docker-compose. En Windows con WSL no es soportado

```
Note: Relative host paths MUST only be supported by Compose implementations that deploy to a local container runtime. This is because the relative path is resolved from the Compose file’s parent directory which is only applicable in the local case. Compose Implementations deploying to a non-local platform MUST reject Compose files which use relative host paths with an error. To avoid ambiguities with named volumes, relative paths SHOULD always begin with . or ..
```
https://docs.docker.com/compose/compose-file/#volumes


----------
Linux:
- No existe inconvenientes


-----------------

[ERROR]: ❌ Airbyte integration is disabled: error executing docker image ls: Cannot connect to the Docker daemon at unix:///var/run/docker.sock. Is the docker daemon running?. For using Airbyte run Jitsu docker with: -v /var/run/docker.sock:/var/run/docker.sock


## License
MIT License
https://github.com/jitsucom/jitsu/blob/master/LICENSE

# Extending Jitsu

With Jitsu SDK you can implement extension for Jitsu using Typescript. Each extension is a separate node package that could be published to public or private npm repository. Jitsu server downloads extension code and executes in within internal V8 JS virtual machine

https://jitsu.com/docs/extending/overview


# Revision
Airbyte is an open-source ETL-framework. Jitsu supports Airbyte as an of the connector backend (the other one being Singer and native connectors