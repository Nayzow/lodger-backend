# 🏠 Lodger — Plateforme de location longue durée

![Build](https://github.com/Nayzow/lodger-backend/actions/workflows/ci.yml/badge.svg)
![Coverage](https://img.shields.io/badge/coverage-auto--generated-green)

**Projet d’étude 4e année — Application web de gestion locative en ligne.**

---

## 🚀 Objectif

Lodger est une plateforme destinée à gérer de la location longue durée pour particuliers et professionnels. Elle permet l’inscription des utilisateurs, la gestion des documents contractuels (bail, quittance, etc.), le suivi des paiements, et la personnalisation des préférences (langue, thème...).

---

## 🧱 Stack technique

| Composant          | Technologie                      |
|--------------------|----------------------------------|
| Backend            | ASP.NET Core 9 (Web API)         |
| Base de données    | PostgreSQL                       |
| ORM                | Entity Framework Core            |
| Authentification   | JWT + Refresh Token + Reset      |
| Tests              | xUnit + Code Coverage (XPlat)    |
| CI/CD              | GitHub Actions + Docker          |
| Couverture         | ReportGenerator + GitHub Pages   |

---

## 📦 Fonctionnalités

- 🔐 Authentification sécurisée (email + refresh + 2FA)
- 🧑‍💼 Gestion des utilisateurs et rôles
- 📁 Upload de documents : bail, quittance, etc.
- 💸 Paiements et suivi financier
- ⚙️ Personnalisation : langue, thème, notifications
- 🛠️ Administration et gestion multi-rôle (propriétaire, locataire)

---

## 📡 Endpoints principaux (API REST)

| Type | Méthode | Endpoint                        |
|------|---------|---------------------------------|
| 🔐 Auth | POST    | `/auth/login`                  |
| 🔐 Auth | POST    | `/auth/register`               |
| 🔁 Token | POST    | `/auth/refresh`                |
| 🔒 Reset | POST    | `/auth/reset-password`         |
| 👤 User | GET     | `/users/{id}`                  |
| 👤 User | PUT     | `/users/{id}`                  |
| 🗑️ User | DELETE  | `/users/{id}`                  |
| 📄 Docs | GET     | `/documents`                   |
| 📄 Docs | POST    | `/documents`                   |
| 📄 Docs | DELETE  | `/documents/{id}`              |
| 💰 Paiements | GET | `/payments`                   |
| 💰 Paiements | POST| `/payments`                   |
| ⚙️ Settings | GET  | `/settings`                   |
| ⚙️ Settings | PUT  | `/settings`                   |

📎 Documentation Swagger dispo sur : `http://localhost:5000/swagger`

---

## 🧪 Tests & couverture

```bash
dotnet test src/Tests/Tests.csproj
```

Rapport HTML auto-généré et publié via GitHub Pages :
👉 https://nayzow.github.io/lodger-backend/

---

## 🐳 Docker

```bash
docker-compose up -d
```

- API REST : http://localhost:5000
- Swagger : http://localhost:5000/swagger

---

## 🔁 CI/CD GitHub Actions

- ✅ Build, tests, format check à chaque push
- 📦 Artifacts test & couverture collectés
- 🧪 Rapport HTML publié sur GitHub Pages
- 🐳 (Optionnel) Image Docker déployable via secrets
