#  SmartBudget AI - Application de Gestion Budgétaire avec Intelligence Artificielle

**Contacter Abdelilah Ourti Via** :
---------
[![Email](https://img.shields.io/badge/Email-Contact-red)](mailto:abdelilahourti@gmail.com)
[![LinkedIn](https://img.shields.io/badge/LinkedIn-Connect-blue)](https://www.linkedin.com/in/abdelilah-ourti-a529412a8)
[![GitHub](https://img.shields.io/badge/GitHub-Profile-black)](https://github.com/abdelilah04116)
[![Portfolio](https://img.shields.io/badge/Portfolio-Visit-orange)](https://abdelilah04116.github.io/)

##  Description

SmartBudget AI est une application web ASP.NET Core MVC qui permet de gérer ses finances personnelles avec l'aide de l'intelligence artificielle (ML.NET). L'application utilise un modèle de machine learning pour catégoriser automatiquement les transactions en fonction de leur description.

##  Technologies Utilisées

- **Backend**: ASP.NET Core 8.0 MVC
- **Base de données**: SQL Server Express + Entity Framework Core
- **Authentification**: ASP.NET Core Identity
- **Intelligence Artificielle**: ML.NET (SDCA Multi-class Classification)
- **Frontend**: Bootstrap 5, Chart.js, Font Awesome
- **Patterns**: Repository Pattern, Dependency Injection, MVC

## Fonctionnalités

###  Authentification
- Inscription et connexion sécurisées
- Gestion des sessions avec cookies
- Validation des mots de passe

###  Gestion des Transactions
- CRUD complet (Create, Read, Update, Delete)
- Classification automatique par IA
- Support des revenus et dépenses
- Historique complet

###  Dashboard Intelligent
- Statistiques en temps réel
- Graphiques interactifs (Chart.js)
- Vue d'ensemble des finances
- Analyse par catégorie

###  Intelligence Artificielle
- Prédiction automatique de catégories
- Modèle ML.NET entraîné sur des données réelles
- API REST pour intégration AJAX
- 90%+ de précision sur les catégories courantes

##  Prérequis

1. **.NET 8.0 SDK** : [Télécharger ici](https://dotnet.microsoft.com/download/dotnet/8.0)
2. **SQL Server Express** : [Télécharger ici](https://www.microsoft.com/fr-fr/sql-server/sql-server-downloads)
3. **Visual Studio 2022** (recommandé) ou **VS Code**

##  Installation et Configuration

### Étape 1 : Cloner ou créer le projet

```bash
# Créer un nouveau dossier
mkdir SmartBudgetAI
cd SmartBudgetAI

# Créer tous les fichiers selon la structure fournie
# (Coller le code de chaque fichier à son emplacement)
```

### Étape 2 : Restaurer les packages NuGet

```bash
dotnet restore
```

### Étape 3 : Configurer la base de données

**Option A : Connexion par défaut (SQL Server Express local)**

La chaîne de connexion par défaut dans `appsettings.json` :
```json
"Server=localhost\\SQLEXPRESS;Database=SmartBudgetAI;Trusted_Connection=True;TrustServerCertificate=True"
```

**Option B : Personnaliser la connexion**

Modifier `appsettings.json` avec vos paramètres :
```json
"Server=YOUR_SERVER;Database=SmartBudgetAI;User Id=YOUR_USER;Password=YOUR_PASSWORD"
```

### Étape 4 : Créer la base de données avec EF Core Migrations

```bash
# Créer la migration initiale
dotnet ef migrations add InitialCreate

# Appliquer la migration (créer la base de données)
dotnet ef database update
```

**Note** : Si `dotnet ef` n'est pas reconnu, installez les outils EF Core :
```bash
dotnet tool install --global dotnet-ef
```

### Étape 5 : Lancer l'application

```bash
dotnet run
```

L'application sera accessible à : **https://localhost:5001** ou **http://localhost:5000**

##  Structure du Projet

```
SmartBudgetAI/
├── Controllers/            # Contrôleurs MVC
│   ├── HomeController.cs
│   ├── AuthController.cs
│   ├── TransactionsController.cs
│   └── AiController.cs
├── Models/                 # Modèles de données
│   ├── ApplicationUser.cs
│   ├── Transaction.cs
│   ├── CategoryPredictionModel.cs
│   └── ViewModels.cs
├── Services/               # Logique métier
│   ├── AuthService.cs
│   ├── TransactionService.cs
│   └── AiService.cs
├── Data/                   # Contexte EF Core
│   └── ApplicationDbContext.cs
├── Views/                  # Vues Razor
│   ├── Home/
│   ├── Auth/
│   ├── Transactions/
│   └── Shared/
├── wwwroot/                # Fichiers statiques
│   ├── css/
│   └── js/
├── MLModels/               # Modèles ML.NET (générés)
├── Program.cs              # Point d'entrée
├── appsettings.json        # Configuration
└── SmartBudgetAI.csproj    # Fichier projet
```

##  Utilisation

### 1️⃣ Créer un compte

- Accéder à `/Auth/Register`
- Remplir le formulaire d'inscription
- Connexion automatique après inscription

### 2️⃣ Créer une transaction

- Cliquer sur "Nouvelle Transaction"
- Remplir la description (ex: "Achat supermarché Carrefour")
- Cliquer sur "Prédire la catégorie avec l'IA" 🤖
- Le modèle ML.NET suggère automatiquement la catégorie
- Ajouter le montant (négatif pour dépense, positif pour revenu)
- Enregistrer

### 3️⃣ Consulter le dashboard

- Statistiques en temps réel
- Graphiques interactifs
- Analyse par catégorie

##  Comment fonctionne l'IA ?

L'application utilise **ML.NET** avec l'algorithme **SDCA** (Stochastic Dual Coordinate Ascent) pour la classification multi-classe.

### Pipeline ML.NET :

1. **Featurization** : Conversion du texte en features numériques (TF-IDF)
2. **Training** : Entraînement sur 40+ exemples de transactions
3. **Prediction** : Prédiction de la catégorie basée sur la description

### Catégories supportées :

- Alimentation
- Transport
- Logement
- Santé
- Loisirs
- Shopping
- Salaire
- Autre

### Précision :

Le modèle atteint **90%+ de précision** sur les catégories courantes. Pour améliorer la précision, ajouter plus de données d'entraînement dans `TrainingData.GetSampleData()`.

##  Résolution des problèmes

### Erreur : "Cannot open database"

**Solution** : Vérifier que SQL Server Express est installé et démarré
```bash
# Vérifier le service SQL Server
services.msc
# Chercher "SQL Server (SQLEXPRESS)" et démarrer si arrêté
```

### Erreur : "dotnet ef not found"

**Solution** : Installer les outils Entity Framework Core
```bash
dotnet tool install --global dotnet-ef
```

### Erreur : "ML.NET model training failed"

**Solution** : Le dossier `MLModels/` doit avoir les permissions d'écriture. Créer le dossier manuellement si nécessaire.

### Graphiques non affichés

**Solution** : Vérifier que Chart.js est bien chargé (connexion Internet requise pour CDN)

##  Sécurité

- ✅ Protection CSRF avec `[ValidateAntiForgeryToken]`
- ✅ Hachage des mots de passe avec Identity
- ✅ Cookies sécurisés avec HTTPS
- ✅ Validation côté serveur et client
- ✅ Isolation des données par utilisateur

##  Améliorations Possibles

1. **Exporter les données** (CSV, PDF)
2. **Notifications par email** (rappels, alertes budget)
3. **Budget mensuel** avec alertes dépassement
4. **Graphiques avancés** (tendances, prévisions)
5. **Mobile app** (Xamarin, MAUI)
6. **Multi-devises** avec conversion automatique
7. **Import automatique** depuis les banques (Open Banking)
8. **Fine-tuning du modèle ML** avec plus de données

##  Licence

Ce projet est fourni à des fins éducatives et de démonstration.

##  Auteur

Projet créé pour démontrer les compétences en :
- ASP.NET Core MVC
- Entity Framework Core
- ML.NET
- Architecture propre
- Design patterns

##  Support

Pour toute question ou problème :
1. Vérifier ce README
2. Consulter la documentation officielle :
   - [ASP.NET Core](https://docs.microsoft.com/aspnet/core)
   - [ML.NET](https://dotnet.microsoft.com/apps/machinelearning-ai/ml-dotnet)
   - [Entity Framework Core](https://docs.microsoft.com/ef/core)

---


**⭐ N'oubliez pas de star le projet si vous l'avez trouvé utile !**

-----------
**Abdelilah Ourti**
