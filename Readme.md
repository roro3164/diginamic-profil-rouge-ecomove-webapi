# EcoMove

Projet de formation CDA .NET chez Diginamic.

Développement d'une application de location de véhicules et de covoiturages.

## Déploiement

Pour déployer le projet, ouvrez le Packager Manager Nuget et lancer les commandes suivantes :

```bash
  Add-Migration Initit
  Update-Database
```
Après ces commandes la base de données devrait étre créée.

## Fixtures

Les tables suivantes ont été remplis avec des données :
 - Brands
 - Models
 - Motorizations
 - Status
 - Categories
 - Vehicle


## Rôle ADMIN

Voici les identifiants pour vous connecter avec un compte administrateur

`Login : admin@ecomove.com`

`Mot de passe : Azerty1!`

## Rôle USER

Voici les identifiants pour vous connecter avec un compte utilisateur

`Login : user@ecomove.com` ou `user2@ecomove.com`

`Mot de passe : Azerty1!`

## Points restants à dévelopepr

- Envoi d'un email aux passagers d'un covoiturage si celui-ci est annulé
- Filtres de recherche
- Tests unitaires
- Refactoring
- Mot de passe plus fort
- L'Admin doit pouvoir modifier, supprimer une réservation de véhicule, un covoiturage...

