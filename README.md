# SmartBudget AI – .NET 8, Clean Architecture & ML.NET

SmartBudget AI est une application moderne en .NET permettant de gérer un budget personnel, tout en intégrant un module d'intelligence artificielle basé sur ML.NET pour classifier automatiquement les dépenses.

Ce projet a été conçu pour démontrer une maîtrise professionnelle de .NET, de l’architecture logicielle, des API REST, et de l’intégration IA.

---

## Objectifs du projet

SmartBudget AI permet de :

- Suivre les dépenses et revenus
- Afficher un dashboard d'analyse financière
- Catégoriser automatiquement les transactions grâce à un modèle ML.NET
- Visualiser l'évolution mensuelle du budget
- Utiliser une API propre basée sur Clean Architecture et CQRS
- Déployer facilement via Docker

---

## Fonctionnalités principales

### Core

- Authentification JWT (Register / Login)
- CRUD complet des transactions
- Dashboard avec statistiques et graphiques
- Catégorisation automatique basée sur le texte de la transaction
- Export CSV / PDF (optionnel)
- Analyse mensuelle des dépenses et revenus

### Module IA (ML.NET)

Le projet inclut un modèle supervisé capable de prédire la catégorie d'une transaction en se basant sur sa description.

Pipeline ML.NET utilisé :

- TextFeaturizer
- LbfgsMaximumEntropy

Endpoint IA :  
`POST /api/ai/predict`

---

## Architecture

Projet structuré selon Clean Architecture + CQRS :

```
/src
   /SmartBudget.Domain         → Entités, interfaces, exceptions
   /SmartBudget.Application    → UseCases, CQRS, DTOs, Validation
   /SmartBudget.Infrastructure → EF Core, Repository, Logging, ML
   /SmartBudget.Api            → Controllers, Middlewares, Swagger
   /SmartBudget.Tests          → Tests unitaires
```

---

## Stack Technique

### Backend

- .NET 8 – ASP.NET Core Web API
- Entity Framework Core 8
- SQL Server ou SQLite
- CQRS
- FluentValidation
- Serilog
- AutoMapper
- ML.NET

### Frontend

- Blazor WebAssembly  
ou  
- React + TypeScript

### DevOps

- Docker et docker-compose
- Tests unitaires xUnit
- GitHub Actions (CI/CD optionnel)

---

## Endpoints principaux

| Méthode | Route | Description |
|--------|-------|-------------|
| POST | `/api/auth/login` | Authentification JWT |
| POST | `/api/auth/register` | Création de compte |
| GET | `/api/transactions` | Liste des transactions |
| POST | `/api/transactions` | Ajouter une transaction |
| POST | `/api/ai/predict` | Prédiction IA |
| GET | `/api/dashboard/summary` | Statistiques du dashboard |

Swagger UI est activé par défaut.

---

## Installation et exécution

### 1. Cloner le projet

```bash
git clone https://github.com/tonprofil/SmartBudgetAI.git
cd SmartBudgetAI
```

### 2. Restaurer les packages

```bash
dotnet restore
```

### 3. Installer la base de données

```bash
dotnet ef database update --project src/SmartBudget.Infrastructure
```

### 4. Lancer l’API

```bash
dotnet run --project src/SmartBudget.Api
```

Swagger sera disponible à :

```
https://localhost:5001/swagger
```

---

## Exécution avec Docker

```bash
docker-compose up --build
```

---

## Tests unitaires

```bash
dotnet test
```

---

## Pourquoi ce projet existe

Ce projet a été créé pour :

- Démontrer une expertise moderne en .NET 8
- Mettre en pratique Clean Architecture et CQRS
- Illustrer l’intégration d’un modèle ML.NET dans une API
- Fournir un projet complet et professionnel publiable sur LinkedIn
- Aider les recruteurs à évaluer les compétences techniques

---

## Auteur

Nom : OURT ABDELILAH  
Formation : Élève ingénieur en IA – ENIAD  
GitHub : https://github.com/tonprofil  
LinkedIn : https://linkedin.com/in/tonprofil

---

## Description prête pour LinkedIn

Je viens de finaliser SmartBudget AI, une application complète en .NET 8 utilisant Clean Architecture, CQRS, EF Core, Blazor/React et un modèle ML.NET pour catégoriser automatiquement les dépenses.

L’objectif était de combiner IA, API REST et architecture professionnelle dans un projet unique, propre et maintenable.

Le projet est open-source ici :  
[lien GitHub]

N'hésitez pas à me faire vos retours techniques.
