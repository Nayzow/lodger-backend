# Étape 1 : Build de l'application Nuxt
FROM node:20 AS builder

WORKDIR /app

# Copie des fichiers de configuration et d'environnement
COPY package*.json ./
RUN npm install

# Copie du code de l’application et exécution du build de production SSR
COPY . .
RUN rm -rf .nuxt && npm run build  # Construit pour la production

# Étape 2 : Image de production avec serveur Node pour Nuxt SSR et installation de NGINX
FROM node:20

WORKDIR /app

# Installer NGINX et créer l'utilisateur nginx
RUN apt-get update && apt-get install -y nginx && \
    useradd -r -d /var/cache/nginx -s /sbin/nologin nginx && \
    rm -rf /var/lib/apt/lists/*

# Copie du build final et des configurations NGINX
COPY --from=builder /app /app
COPY docker/nginx.conf /etc/nginx/nginx.conf
COPY docker/default.conf /etc/nginx/conf.d/default.conf

# Crée un script d’entrée pour démarrer Nuxt et NGINX
RUN echo "#!/bin/sh" > /entrypoint.sh && \
    echo "node .output/server/index.mjs &" >> /entrypoint.sh && \
    echo "nginx -g 'daemon off;'" >> /entrypoint.sh
RUN chmod +x /entrypoint.sh

# Définit les variables d'environnement pour activer le mode production et SEO
ENV NODE_ENV=production
ENV NUXT_ENV_MODE=SSR

# Expose le port 80 pour NGINX
EXPOSE 80

# Commande d'entrée
CMD ["/entrypoint.sh"]
