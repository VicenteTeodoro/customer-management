# Shortcut Developer commands for customer-management Docker Stack

.PHONY: build up down shell logs restart ps clean test

# 1. Image and Container Lifecycle
build:
	docker compose build

up:
	docker compose up -d

down:
	docker compose down

restart:
	docker compose restart

# 2. Interactive Terminal and Inspection
shell:
	docker compose exec -it dev /bin/zsh

logs:
	docker compose logs -f dev

ps:
	docker compose ps

test:
	docker compose exec dev dotnet test

# 3. Cache & Container Cleansing
clean:
	docker compose down -v
	docker system prune -f
