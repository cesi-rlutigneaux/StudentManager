Mise à jour .NET et configuration local

Résumé des modifications:
- Migration du projet vers .NET 10 (TargetFramework = net10.0) dans `StudentManager.csproj`.
- Mise à jour des packages Microsoft.AspNetCore / EF Core vers 10.0.1.
- Ajout de la source NuGet `nuget.org` si nécessaire.
- Nettoyage des anciennes chaînes de connexion dans `appsettings.json`.
- Ajout d'une chaîne de connexion de développement active dans `appsettings.Development.json` pointant sur l'instance LocalDB utilisée pour le développement : `Server=(localdb)\\LOCALDB#2CE2BF9B;Database=StudentManagerDb_Local;...`.
- Installation de `dotnet-ef` (global) et application des migrations. La base `StudentManagerDb_Local` a été créée.

Comment exécuter localement (développement):

1. S'assurer d'avoir .NET SDK installé (vérifié localement: `dotnet --list-sdks` retourne `10.0.401`).
2. Démarrer l'instance LocalDB si nécessaire:

```powershell
sqllocaldb start "LOCALDB#2CE2BF9B"
```

3. Appliquer les migrations (si vous voulez recréer la DB sur l'instance):

```powershell
dotnet tool install --global dotnet-ef --version 10.0.1
dotnet ef database update
```

4. Lancer l'application:

```powershell
dotnet run
```

L'application écoute par défaut sur `http://localhost:5257` en environnement Development (voir `Properties/launchSettings.json`).

Notes et recommandations:
- Les anciennes chaînes de connexion ont été supprimées de `appsettings.json` pour éviter les conflits; la configuration active est dans `appsettings.Development.json`.
- Si vous préférez utiliser une instance SQL Server nommée (SSMS), mettez à jour `appsettings.Development.json` avec la chaîne appropriée puis exécutez `dotnet ef database update`.
- Il reste quelques avertissements de nullability et des alertes NuGet (paquets avec vulnérabilités connues). Vous pouvez les corriger séparément si souhaité.
