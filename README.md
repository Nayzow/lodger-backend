# Lodger Backend API

![GitHub Workflow Status](https://github.com/Nayzow/lodger-backend/actions/workflows/ci.yml/badge.svg)
![Coverage](https://img.shields.io/badge/coverage-dynamic-lightgrey?style=flat)
![License](https://img.shields.io/badge/license-MIT-blue.svg)
![Tech](https://img.shields.io/badge/.NET-9.0-blue)

API REST développée en **.NET 9** pour la plateforme de **location longue durée Lodger**, permettant de gérer les utilisateurs, documents, paiements, rôles et configurations de manière sécurisée et évolutive.

---

## 📁 Sommaire

- [Fonctionnalités principales](#fonctionnalités-principales)
- [Démarrage](#démarrage)
- [Configuration locale](#configuration-locale)
- [Structure du projet](#structure-du-projet)
- [Tests & Couverture](#tests--couverture)
- [CI/CD](#cicd)
- [Technologies](#technologies)
- [Auteurs](#auteurs)
- [Licence](#licence)

---

## 🎯 Fonctionnalités principales

- Authentification avec jeton JWT
- Création et gestion de comptes utilisateurs avec rôles
- Paiements liés aux utilisateurs
- Upload et gestion de documents
- Configuration personnalisée utilisateur
- Réinitialisation de mot de passe
- Swagger UI disponible en environnement `Development`

---

## 🚀 Démarrage

### ▶️ Exécution manuelle

```bash
dotnet build src/App/LodgerBackend.csproj
dotnet run --project src/App/LodgerBackend.csproj
```

Swagger est accessible ici : [http://localhost:5000/swagger](http://localhost:5000/swagger)

### 🐳 Exécution via Docker

```bash
docker build -t lodger-backend .
docker run -p 5000:5000 lodger-backend
```

---

## 🛠️ Configuration locale

Avant tout démarrage local, assure-toi d’avoir :

- `src/App/appsettings.Development.json`
- `src/App/serilog.Development.json`
- `src/App/Properties/launchSettings.json`

### Exemple de `launchSettings.json`

```json
{
  "profiles": {
    "http": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": true,
      "applicationUrl": "http://localhost:5000",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    }
  }
}
```

---

## 📂 Structure du projet

```
lodger-backend/
├── .github/workflows/       # CI GitHub Actions
├── src/
│   ├── App/                 # API principale
│   └── Tests/               # Projet de tests xUnit
├── README.md
└── Dockerfile
```

---

## 🧪 Tests & Couverture

Les tests sont basés sur xUnit + Coverlet. Exécution manuelle :

```bash
dotnet test src/Tests/Tests.csproj --collect:"XPlat Code Coverage"
```

Le rapport de couverture est généré en `src/Tests/Artifacts/CoverageReport`.

Un aperçu est publié sur GitHub Pages après chaque push sur `main`.

---

## ⚙️ CI/CD

Le pipeline GitHub Actions comprend :

- Restauration des dépendances
- Build du projet
- Exécution des tests avec couverture
- Génération de rapports `.trx`, `JUnit`, `Cobertura`, `HTML`
- Déploiement automatique sur GitHub Pages (`CoverageReport`)

---

## 🧰 Technologies

- [.NET 9](https://dotnet.microsoft.com/en-us/download)
- PostgreSQL
- Entity Framework Core
- Swagger / OpenAPI
- Docker
- GitHub Actions
- xUnit / Coverlet
