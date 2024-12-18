
# Docker Compose Structure Cheat Sheet

## Docker Compose File Structure (`docker-compose.yml`)

```yaml
version: '3.8'  # Specify the Docker Compose file version

services:
  app:            # Define a service (container name)
    image: myapp:latest   # Use an image from Docker Hub or build your own
    build:        # Build from a Dockerfile
      context: .  # The path to the Dockerfile
    ports:
      - "8080:80" # Expose ports (HOST:CONTAINER)
    volumes:
      - ./app:/usr/src/app   # Mount volumes (HOST:CONTAINER)
    environment:
      - DATABASE_URL=mysql://user:pass@mysql:3306/dbname # Environment variables
    depends_on:
      - mysql      # Define service dependencies

  mysql:
    image: mysql:latest
    environment:
      MYSQL_ROOT_PASSWORD: password
    volumes:
      - db_data:/var/lib/mysql   # Named volume

volumes:
  db_data:  # Define volumes

networks:
  app_net:  # Define networks
    driver: bridge
```

---

## Services Section

### Build from Dockerfile
```yaml
build:
  context: ./app   # Path to the Dockerfile
  dockerfile: Dockerfile  # Specify custom Dockerfile name
```

### Use a Docker Hub Image
```yaml
image: nginx:latest
```

### Exposing Ports
```yaml
ports:
  - "8080:80"  # HOST:CONTAINER
```

### Mounting Volumes
- **Bind Mount**
  ```yaml
  volumes:
    - ./local-folder:/container-folder
  ```

- **Named Volume**
  ```yaml
  volumes:
    - my_volume:/data
  ```

### Environment Variables
```yaml
environment:
  - NODE_ENV=production
  - DB_HOST=localhost
```

### Service Dependencies
```yaml
depends_on:
  - db
  - redis
```

### Command Override
```yaml
command: python app.py
```

### Restart Policy
```yaml
restart: always  # Options: always, on-failure, unless-stopped, no
```

---

## Volumes Section

- **Named Volume**
  ```yaml
  volumes:
    db_data:  # Define named volume
  ```

- **Bind Mount**
  ```yaml
  volumes:
    - ./local-folder:/container-folder
  ```

---

## Networks Section

- **Bridge Network (default)**
  ```yaml
  networks:
    app_net:
      driver: bridge
  ```

---
