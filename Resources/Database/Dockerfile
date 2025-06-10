FROM postgres:16.4

ENV POSTGRES_USER=root
ENV POSTGRES_PASSWORD=test
#COPY database.sql /docker-entrypoint-initdb.d/

RUN echo " \
    # Configuration de l'encodage et du tri par défaut \
    client_encoding = 'UTF8'; \
    # Configuration de la codification du serveur \
    server_encoding = 'UTF8'; \
    # Configuration du tri du serveur \
    lc_collate = 'en_US.UTF-8'; \
    lc_ctype = 'en_US.UTF-8'; \
" > /docker-entrypoint-initdb.d/postgres-custom-config.sql
