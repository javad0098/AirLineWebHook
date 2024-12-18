
# Docker Compose Command Cheat Sheet

## Basic Commands

### Start Services (Up)
```bash
docker-compose up
```

Run in detached mode (background):
```bash
docker-compose up -d
```

### Stop Services
```bash
```

### Rebuild Services (if changes made to Dockerfile or docker-compose.yml)
```bash
docker-compose up --build
```

### List Running Services
```bash
docker-compose ps
```

### Remove Stopped Services
```bash
docker-compose down
```

Remove containers, networks, and volumes:
```bash
docker-compose down -v
```

### Restart Services
```bash
docker-compose restart
```

## Viewing Logs

### View logs of all services:
```bash
docker-compose logs
```

### View logs of a specific service:
```bash
docker-compose logs <service_name>
```

### View real-time logs:
```bash
docker-compose logs -f
```

## Scaling Services
```bash
docker-compose up --scale <service_name>=<number_of_instances>
```

Example:
```bash
docker-compose up --scale web=3
```

## Executing Commands Inside a Running Container
```bash
docker-compose exec <service_name> /bin/bash
```

Example:
```bash
docker-compose exec web /bin/bash
```

## Checking Resource Usage (CPU/Memory)
```bash
docker stats
```

---

## Network Commands

### List Docker networks
```bash
docker network ls
```

### Inspect a specific network
```bash
docker network inspect <network_name>
```

---

## Other Useful Commands

### Pause all running services
```bash
docker-compose pause
```

### Unpause all running services
```bash
docker-compose unpause
```
