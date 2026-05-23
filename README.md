# 🚀 Customer-Management Development Workspace

Welcome to **Customer-Management**! This repository is a premium, professional development workspace custom-engineered for high-performance agentic engineering using C# (.NET 8.0) and the **Microsoft Hypervelocity Engineering Core (HVE-Core)**.

It contains an integrated Docker Dev Container carrying a triple-runtime environment (.NET 8 SDK, Node.js 20, Python 3) pre-wired for instant custom AI orchestration, clean REST web service structures, and version control.

> [!IMPORTANT]
> **Active Workspace Recommendation:**
> To enable automatic, out-of-the-box loading of all custom GitHub Copilot agent configurations, prompts, and settings, please open `/Users/vicenteteo/.gemini/antigravity-ide/scratch/customer-management` as your **active IDE workspace folder** in VS Code!

---

## 🛠️ Technology Stack & Integrations

1. **Microsoft HVE-Core Framework**: Integrated directly inside [settings.json](file:///.vscode/settings.json) as a git submodule (`lib/hve-core`). Pre-registers specialized agent personas like `@rpi-agent`, `@task-researcher`, `@task-planner`, `@task-implementor`, and `@task-reviewer` inside your IDE.
2. **C# .NET 8.0 Minimal Web API**: A structured customer profile and interaction manager API utilizing modern C# principles, OpenAPI/Swagger specifications, dependency injection, and in-memory seeding.
3. **Triple-Runtime Container Stack**:
   - **.NET 8.0 SDK** (backend APIs, class libraries, services)
   - **Node.js LTS (v20)** (frontend builds, tools, typescript/tsx compilation)
   - **Python 3** (local data utilities, AI scripts, shell commands)
   - **Zsh & Oh-My-Zsh** pre-configured for the `developer` user with autocompletion, syntax-highlighting, and command history persistence.
4. **Developer Make Shortcuts**: A streamlined Makefile bypassing complex Docker Compose commands with simple one-word tasks.

---

## 💻 Environment Setup & Quickstart

We've provided a simple **`Makefile`** to make managing your containerized runtime environment completely effortless.

### 1. Build and Start the Environment
Ensure Docker is active on your host machine, then run these commands from your local Mac terminal:
```bash
# Build the custom dev container image
make build

# Start the dev container in the background
make up
```

### 2. Enter the Interactive Container Shell
Once the container is running, open an interactive terminal session inside the triple-runtime Debian environment:
```bash
make shell
```
*Your shell prompt will transform to `Customer-Management-Container 🚀 /workspace $`, and full autocomplete, syntax-highlighting, and persistent command caches will be active!*

### 3. Launch and Test the C# Web API
Inside the container terminal (`make shell`), compile and run the Minimal Web API:
```bash
dotnet run
```
The API automatically binds to all interfaces on port `8000`. You can test, inspect, and execute REST requests directly from your host Mac browser:
👉 **Swagger Interactive Documentation: [http://localhost:8000/swagger](http://localhost:8000/swagger)**

---

## 🤝 Linking Your Workspace with GitHub

To link this newly created project with your personal GitHub account and push it to your remote repository, follow these quick steps:

### 1. Create a Repository on GitHub
Go to [github.com/new](https://github.com/new) and create a new repository:
- Repository Name: `customer-management`
- Public / Private (as preferred)
- **Do NOT** initialize with a README, gitignore, or license (we have already created and configured pristine files for you).

### 2. Set Up Your Identity & Commit Files
From your host terminal inside the project folder, configure your local Git properties (adjust with your GitHub credentials):
```bash
# Configure your commit details
git config --local user.name "Your Name"
git config --local user.email "your.email@example.com"

# Stage all files (excluding ignored ones)
git add .

# Create your initial repository commit
git commit -m "Initialize customer-management workspace with HVE-Core and .NET 8.0 API"
```

### 3. Link Remote and Push to GitHub
Copy the remote repository URL from your GitHub page (SSH is recommended if you have keys configured, otherwise use HTTPS) and run:
```bash
# Link the local repository to your remote GitHub repository
git remote add origin <your-github-repo-url>

# Rename branch to main
git branch -M main

# Push the code and tracking branches up to your GitHub repository
git push -u origin main
```
*Your code, submodule links, and environment files are now safely stored in your personal GitHub account!*

---

## 📂 Project Directory Structure

```text
customer-management/
├── .devcontainer/
│   └── devcontainer.json   # IDE container overrides & auto-extension installs
├── .vscode/
│   └── settings.json       # Maps relative lib/hve-core agent location paths
├── Properties/
│   └── launchSettings.json # Binds Minimal API to 0.0.0.0:8000 inside Docker
├── lib/
│   └── hve-core/           # HVE-Core cloned framework as a git submodule
├── customer-management.Tests/
│   ├── customer-management.Tests.csproj # xUnit package references
│   ├── UnitTests.cs        # Customer repository and core class checks
│   └── IntegrationTests.cs # Web Server endpoint tests via WebApplicationFactory
├── .gitignore              # Ignores local bin/obj compiles, caches, and tracking logs
├── Dockerfile              # Combines .NET 8, Node 20, Python 3, and Oh-My-Zsh
├── docker-compose.yml      # Port mappings (8000, 5173, 3000) and volumes mount
├── Makefile                # Host command shortcuts (make build, make up, make shell, make test)
├── Program.cs              # Starter Minimal C# API showcasing customer management
├── customer-management.csproj # Web SDK package properties targeting net8.0
├── appsettings.json        # Main configuration file
└── appsettings.Development.json # Development log details
```
