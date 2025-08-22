## Configuración de Base de Datos

### 1. Instalar EF Tools

```
dotnet tool install --global dotnet-ef
```

### 2. Hacer una migración desde la raíz del Backend

```
dotnet ef database update --project src\Infrastructure --startup-project src\Web
```
