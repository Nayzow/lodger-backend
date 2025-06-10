CREATE DATABASE lodger;
\c lodger;

-- Table des adresses
CREATE TABLE addresses (
    id SERIAL PRIMARY KEY,
    address_name VARCHAR(255) NOT NULL,
    code_postal VARCHAR(10) NOT NULL
);

-- Table des utilisateurs
CREATE TABLE users (
    id SERIAL PRIMARY KEY,
    name VARCHAR(100) NULL,
    firstname VARCHAR(255),
    phone TEXT,
    email VARCHAR(255) UNIQUE NOT NULL,
    password TEXT NOT NULL,
    address_id INT, -- Référence à adresses.id
    gender INT NULL,
    nationality INT NULL,
    is2fa BOOLEAN NOT NULL DEFAULT false,

    CONSTRAINT fk_address FOREIGN KEY (address_id) REFERENCES addresses(id)
);
CREATE TABLE refresh_tokens (
    id INT PRIMARY KEY IDENTITY(1,1),
    user_id INT NOT NULL,
    token NVARCHAR(500) NOT NULL,
    expiration DATETIME NOT NULL,
    acces_token_expiration DATETIME NOT NULL,
    is_revoked BIT NOT NULL,
    CONSTRAINT FK_RefreshTokens_User FOREIGN KEY (user_id) REFERENCES Users(id) -- Assurez-vous que la table Users existe
);

CREATE TABLE roles
(
    id   SERIAL PRIMARY KEY,
    name VARCHAR(100) NOT NULL
);

CREATE TABLE user_roles
(
    id      SERIAL PRIMARY KEY,
    user_id INT NOT NULL,
    role_id INT NOT NULL,
    FOREIGN KEY (user_id) REFERENCES users (id),
    FOREIGN KEY (role_id) REFERENCES roles (id)
);

CREATE TABLE payments
(
    id      SERIAL PRIMARY KEY,
    user_id INT            NOT NULL,
    amount  NUMERIC(10, 2) NOT NULL,
    FOREIGN KEY (user_id) REFERENCES users (id) ON DELETE CASCADE
);

CREATE TABLE settings
(
    id                    SERIAL PRIMARY KEY,
    user_id               INT NOT NULL,
    theme                 VARCHAR(50) DEFAULT 'light',
    notifications_enabled BOOLEAN     DEFAULT TRUE,
    language              VARCHAR(10) DEFAULT 'en',
    FOREIGN KEY (user_id) REFERENCES users (id) ON DELETE CASCADE
);

-- Nouvelle table document_types
CREATE TABLE document_types
(
    id   SERIAL PRIMARY KEY,
    code VARCHAR(50) UNIQUE NOT NULL,
    name VARCHAR(100)       NOT NULL
);

CREATE TABLE documents
(
    id               SERIAL PRIMARY KEY,
    user_id          INT          NOT NULL,
    name             VARCHAR(255) NOT NULL,
    file_url         TEXT         NOT NULL,
    document_type_id INT,
    FOREIGN KEY (user_id) REFERENCES users (id) ON DELETE CASCADE,
    FOREIGN KEY (document_type_id) REFERENCES document_types (id)
);

CREATE TABLE refresh_tokens
(
    id                     SERIAL PRIMARY KEY,
    user_id                INT          NOT NULL,
    token                  VARCHAR(500) NOT NULL,
    expiration             TIMESTAMP    NOT NULL,
    acces_token_expiration TIMESTAMP    NOT NULL,
    is_revoked             BOOLEAN      NOT NULL
);

CREATE TABLE reset_password_request
(
    id           SERIAL PRIMARY KEY,
    user_id      INT       NOT NULL,
    token        VARCHAR(10000),
    requested_at TIMESTAMP NOT NULL,
    expires_at   TIMESTAMP NOT NULL,
    FOREIGN KEY (user_id) REFERENCES users (id) ON DELETE CASCADE
);

-- Insertion des types de document
INSERT INTO document_types (code, name)
VALUES ('DOSSIER', 'Dossier'),
       ('PAIEMENT', 'Paiement'),
       ('QUITTANCE', 'Quittance'),
       ('BAIL', 'Bail'),
       ('ETAT_DES_LIEUX', 'État des lieux');
---- Insertion des utilisateurs
--INSERT INTO users (id, name, email, password)
--VALUES (1, 'Alice Doe', 'alice@example.com', 'hashed_password_1'),
--       (2, 'Bob Smith', 'bob@example.com', 'hashed_password_2');

---- Insertion des paramètres utilisateur
--INSERT INTO settings (user_id, theme, notifications_enabled, language)
--VALUES (1, 'dark', true, 'fr'),
--       (2, 'light', false, 'en');

---- Insertion des paiements
--INSERT INTO payments (user_id, amount)
--VALUES (1, 100.50),
--       (2, 200.75);

---- Insertion des documents
--INSERT INTO documents (user_id, name, file_url)
--VALUES (1, 'Contrat_Alice.pdf', 'https://example.com/alice_doc.pdf'),
--       (2, 'Facture_Bob.pdf', 'https://example.com/bob_invoice.pdf');

---- Insertion des dossiers
--INSERT INTO dossiers (user_id, name)
--VALUES (1, 'Dossier Alice'),
--       (2, 'Dossier Bob');

---- Association des documents aux dossiers
--INSERT INTO dossier_documents (dossier_id, document_id)
--VALUES (1, 1),
--       (2, 2);