# Quel est ce projet ?  

TFTStats est une API web en C# & [ASP.NET](http://ASP.Net) Core Web API qui suit les principes de la **Vertical Slice Architecture**. Le projet récupèrera des informations depuis l'API Riot Games pour fournir une analyse du jeu Teamfight Tactics.

C'est un projet organisé en micro-services (un sera responsable des informations de Summoners et l'autre sera responsable des informations des matchs et leur analyse)
  
# Sommaire  
  
- [Quel est ce projet ?](#Quel-est-ce-projet-?)  
- [Sommaire](#Sommaire)  
- [Technologies Utilisées](#Technologies-Utilisées)  
- [Objectif](#Objectif)
- [État du projet](#État-du-projet)
- [Comment lancer le projet en local avec Docker](#Comment-lancer-le-projet-en-local-avec-Docker)
- [Mention spéciale](#Mention-spéciale)

# Technologies utilisées  
  
* [ASP.NET](http://ASP.Net) Core (.NET 8) Web API
* Entity Framework Core (EFCore 8)
* PostgreSQL
* SwaggerUI
* Docker
* RabbitMQ
* MassTransit
* Refit
* Bibliothèque [SPMF](https://www.philippe-fournier-viger.com/spmf/)
  
# Objectif

L'objectif de ce projet est de s'essayer aux micro-services dans un projet d'analyse de données. C'est également une première occasion de consommer une API publique.

De plus, je me suis imposé de réaliser une version minimale de ce projet en une semaine pour voir ce dont je suis capable.

# État du projet

Le projet fonctionne actuellement, il est donc possible de requêter l'API de Riot pour obtenir des informations sur les Summoners (joueurs) de TFT puis de récupérer les informations de leurs derniers matchs.

Le projet devait être déployé à l'aide de Kubernetes mais c'est contre la politique de Riot qui souhaite tout d'abord valider les différents projets qui consomment leur API avant qu'ils soient déployés.
L'autre problème concerne le Rate Limiting de l'API Riot qui force à faire très peu de demandes (20requêtes en 1 seconde et 100requêtes en 1minute) tant que l'on est pas enregistré.

Un grand absent de ce projet concerne les tests et la gestion d'erreurs que je n'ai pas eu le temps de mettre en place dans le délai que je m'étais imposé.

# Comment lancer le projet en local avec Docker ?

1. Installer **.NET** 8 SDK  
2. Installer **Docker** Desktop (Windows) / **Docker** (Linux/Mac)  
3. Cloner la solution  
4. Obtenir une clé-API sur https://developer.riotgames.com/ puis la renseigner dans les paramètres d'application de développement "appsettings.Development.json" des DEUX projets
5. Se placer dans le répertoire local de la solution et entrez la commande suivante dans un terminal
```  
> docker compose build && docker compose up  
```  
6. Lorsque les conteneurs sont démarrés avec succès, naviguer sur http://localhost:5194/swagger/index.html pour récupérer les informations de Summoners qui ont atteint un certain Rang
7. Naviguer sur http://localhost:5033/swagger/index.html pour récupérer les informations de Matchs des différents Summoners enregistrés en utilisant l'endpoint requestMatchesInformations
8. Toujours sur le même url, en utilisant l'autre endpoint (mostUsedCompositions), obtenir une proposition d'analyse des compositions les plus jouées

# Licence

Ce projet est sous licence MIT.

# Mention spéciale

TFTStats isn't endorsed by Riot Games and doesn't reflect the views or opinions of Riot Games or anyone officially involved in producing or managing Riot Games properties. Riot Games, and all associated properties are trademarks or registered trademarks of Riot Games, Inc.

