# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# --- Copy csproj files (Contracts và các project khác) ---
COPY Contracts/Contracts/Contracts.csproj Contracts/
COPY FormService/FormService.API/FormService.API.csproj FormService.API/
COPY FormService/FormService.Application/FormService.Application.csproj FormService.Application/
COPY FormService/FormService.Domain/FormService.Domain.csproj FormService.Domain/
COPY FormService/FormService.Infrastructure.Write/FormService.Infrastructure.Write.csproj FormService.Infrastructure.Write/
COPY FormService/FormService.Infrastructure.Read/FormService.Infrastructure.Read.csproj FormService.Infrastructure.Read/

# Restore dependencies
RUN dotnet restore FormService.API/FormService.API.csproj

# --- Copy toàn bộ source code ---
COPY Contracts/ Contracts/
COPY FormService/FormService.API/ FormService.API/
COPY FormService/FormService.Application/ FormService.Application/
COPY FormService/FormService.Domain/ FormService.Domain/
COPY FormService/FormService.Infrastructure.Write/ FormService.Infrastructure.Write/
COPY FormService/FormService.Infrastructure.Read/ FormService.Infrastructure.Read/


WORKDIR /src/FormService.API

# Build & publish
RUN dotnet publish FormService.API.csproj -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80
ENTRYPOINT ["dotnet", "FormService.API.dll"]
