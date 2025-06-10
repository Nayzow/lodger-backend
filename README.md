# 🏠 Lodger — Plateforme de location longue durée en ligne

**Projet d’étude 4e année — Application web de gestion locative pour particuliers ou agences.**

---

## 🚀 Objectifs du projet

Lodger est une plateforme en ligne de gestion de locations longue durée. Elle permet à des utilisateurs de créer leur compte, gérer leur profil, envoyer des documents officiels (bail, quittance, état des lieux...), suivre leurs paiements, et gérer leurs paramètres personnels.

Ce projet a été développé dans un cadre académique, avec un backend en .NET 9, une base de données PostgreSQL et un frontend en cours de développement.

---

## 🧱 Stack technique

| Composant     | Technologie                    |
|---------------|--------------------------------|
| Backend       | ASP.NET Core 9 (Web API)       |
| Base de données | PostgreSQL (via Entity Framework Core) |
| Authentification | JWT + Refresh Token + Reset Password |
| CI/CD         | GitHub Actions + Docker        |
| Tests         | xUnit, code coverage `XPlat`   |
| Couverture    | ReportGenerator + GitHub Pages |
| Conteneurisation | Docker / Docker Compose     |

---

## 📦 Fonctionnalités principales

- 🔐 Authentification sécurisée avec 2FA optionnelle
- 👤 Gestion des utilisateurs et rôles
- 📄 Upload et gestion des documents (PDF, contrat, quittance…)
- 💸 Paiements et suivi
- ⚙️ Paramètres personnalisés (langue, thème, notifications)
- 📊 Dashboard locataire/propriétaire (à venir)
