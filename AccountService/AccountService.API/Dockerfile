# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# --- Copy csproj files (Contracts và các project khác) ---
COPY Contracts/Contracts/Contracts.csproj Contracts/
COPY AccountService/AccountService.API/AccountService.API.csproj AccountService.API/
COPY AccountService/AccountService.Application/AccountService.Application.csproj AccountService.Application/
COPY AccountService/AccountService.Domain/AccountService.Domain.csproj AccountService.Domain/
COPY AccountService/AccountService.Infrastructure.Write/AccountService.Infrastructure.Write.csproj AccountService.Infrastructure.Write/
COPY AccountService/AccountService.Infrastructure.Read/AccountService.Infrastructure.Read.csproj AccountService.Infrastructure.Read/

# Restore dependencies
RUN dotnet restore AccountService.API/AccountService.API.csproj

# --- Copy toàn bộ source code ---
COPY Contracts/ Contracts/
COPY AccountService/AccountService.API/ AccountService.API/
COPY AccountService/AccountService.Application/ AccountService.Application/
COPY AccountService/AccountService.Domain/ AccountService.Domain/
COPY AccountService/AccountService.Infrastructure.Write/ AccountService.Infrastructure.Write/
COPY AccountService/AccountService.Infrastructure.Read/ AccountService.Infrastructure.Read/


WORKDIR /src/AccountService.API

# Build & publish
RUN dotnet publish AccountService.API.csproj -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80
ENTRYPOINT ["dotnet", "AccountService.API.dll"]
