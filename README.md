# Quel est ce projet ?  

TFTStats est une API web en C# & [ASP.NET](http://ASP.Net) Core Web API qui suit les principes de la **Vertical Slice Architecture**. Le projet récupèrera des informations depuis l'API Riot Games pour fournir une analyse du jeu Teamfight Tactics.

C'est un projet organisé en micro-services pour s'entraîner, un micro-service pour sauvegarder sera responsable des informations de Summoners et l'autre micro-service sera responsable des informations des matchs et leur analyse.
  
# Sommaire  
  
- [Quel est ce projet ?](#Quel-est-ce-projet-?)  
- [Sommaire](#Sommaire)  
- [Technologies Utilisées](#Technologies-Utilisées)  
- [Objectif](#Objectif)
- [Mention spéciale](#Mention-spéciale)

# Technologies utilisées  
  
* [ASP.NET](http://ASP.Net) Core (.NET 8) Web API
* Entity Framework Core (EFCore 8)
* MediatR pour .NET 8
* Fluent Validation pour .NET 8
* PostgreSQL
* SwaggerUI
* Docker
* Kubernetes
* RabbitMQ
  
# Objectif

L'objectif de ce projet est de s'essayer aux micro-services dans un projet d'analyse de données. C'est également une première occasion de consommer une API publique.

# Licence

Ce projet est sous licence MIT.

# Mention spéciale

TFTStats isn't endorsed by Riot Games and doesn't reflect the views or opinions of Riot Games or anyone officially involved in producing or managing Riot Games properties. Riot Games, and all associated properties are trademarks or registered trademarks of Riot Games, Inc.

