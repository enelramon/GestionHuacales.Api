# UiTests

## Descripción

Este proyecto contiene las pruebas automatizadas de api

## Configuración del Entorno

### Requisitos Previos

1. **.NET 9.0 SDK** instalado
2. **Node.js** (requerido por Playwright)
3. **Navegadores** instalados por Playwright

## Instalación y Configuración

### 1. Clonar el repositorio y navegar al proyecto
```bash
git clone https://tu-repositorio.git
cd GestionHuacales.Api.UiTests
```

### 2. Restaurar paquetes NuGet
```powershell
dotnet restore
dotnet build
```

### 3. Instalar navegadores de Playwright
```powershell
./bin/Debug/net9.0/playwright.ps1 install
```

## Ejecución de Pruebas

### Ejecutar todas las pruebas
```powershell
dotnet test
```


### Ejecutar una prueba específica
```powershell
dotnet test --filter "FullyQualifiedName~JugadoresApiTest"
```

### Modo Debug de Playwright
Para depurar pruebas paso a paso con el navegador visible:
```powershell
# Activar modo debug
$env:PWDEBUG=1
dotnet test --filter "FullyQualifiedName~TuPruebaEspecifica"
```
