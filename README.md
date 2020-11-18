# Apprise.NET
Simple .NET wrapper for [Apprise API](https://github.com/caronc/apprise-api).

## Setup
This wrapper assumes you have Apprise API running as stateless sidecar solution.

An easy and quick way to get it up and running is using docker

```bash
docker run --name apprise \
   -p 8000:8000 \
   -d caronc/apprise:latest
```

or use the provided `docker-compose.yml`

```bash
docker-compose up
```