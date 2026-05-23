# Premium Triple-Runtime Container: .NET 8.0 SDK, Node.js 20, Python 3
FROM mcr.microsoft.com/dotnet/sdk:8.0

# 1. Environment & Setup Variables
ENV DEBIAN_FRONTEND=noninteractive
ENV NODE_VERSION=20.x
ENV PYTHONUNBUFFERED=1

# 2. System Utilities & Package Repositories
RUN apt-get update && apt-get install -y --no-install-recommends \
    apt-transport-https \
    ca-certificates \
    curl \
    gnupg \
    lsb-release \
    git \
    sudo \
    zsh \
    vim \
    tmux \
    htop \
    jq \
    make \
    python3 \
    python3-pip \
    python3-venv \
    && rm -rf /var/lib/apt/lists/*

# 3. Install Node.js LTS (v20) & Build Packages
RUN mkdir -p /etc/apt/keyrings \
    && curl -fsSL https://deb.nodesource.com/gpgkey/nodesource-repo.gpg.key | gpg --dearmor -o /etc/apt/keyrings/nodesource.gpg \
    && echo "deb [signed-by=/etc/apt/keyrings/nodesource.gpg] https://deb.nodesource.com/node_20.x nodistro main" | tee /etc/apt/sources.list.d/nodesource.list \
    && apt-get update && apt-get install -y nodejs \
    && npm install -g typescript tsx pm2 \
    && rm -rf /var/lib/apt/lists/*

# 4. Developer User Configuration with Passwordless Sudo
RUN useradd -m -s /bin/zsh developer \
    && echo "developer ALL=(ALL) NOPASSWD:ALL" >> /etc/sudoers

USER developer
WORKDIR /workspace

# 5. Shell Customization: Oh-My-Zsh & Helper Tools
RUN sh -c "$(curl -fsSL https://raw.githubusercontent.com/ohmyzsh/ohmyzsh/master/tools/install.sh)" "" --unattended \
    && git clone https://github.com/zsh-users/zsh-autosuggestions ${HOME}/.oh-my-zsh/custom/plugins/zsh-autosuggestions \
    && git clone https://github.com/zsh-users/zsh-syntax-highlighting ${HOME}/.oh-my-zsh/custom/plugins/zsh-syntax-highlighting

# Configure Shell Prompt, History, and Auto-Completion Plugins
RUN sed -i 's/plugins=(git)/plugins=(git zsh-autosuggestions zsh-syntax-highlighting)/g' ${HOME}/.zshrc \
    && echo "export HISTFILE=/workspace/.devcontainer/.zsh_history" >> ${HOME}/.zshrc \
    && echo "export HISTSIZE=10000" >> ${HOME}/.zshrc \
    && echo "export SAVEHIST=10000" >> ${HOME}/.zshrc \
    && echo "setopt APPEND_HISTORY" >> ${HOME}/.zshrc \
    && echo "setopt SHARE_HISTORY" >> ${HOME}/.zshrc \
    && echo 'PROMPT="%F{cyan}Customer-Management-Container 🚀 %F{yellow}%~ %F{white}$ "' >> ${HOME}/.zshrc

CMD [ "sleep", "infinity" ]
