# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# --- Copy csproj files (Contracts và các project khác) ---
COPY Contracts/Contracts/Contracts.csproj Contracts/
COPY OrderService/OrderService.API/OrderService.API.csproj OrderService.API/
COPY OrderService/OrderService.Application/OrderService.Application.csproj OrderService.Application/
COPY OrderService/OrderService.Domain/OrderService.Domain.csproj OrderService.Domain/
COPY OrderService/OrderService.Infrastructure.Write/OrderService.Infrastructure.Write.csproj OrderService.Infrastructure.Write/
COPY OrderService/OrderService.Infrastructure.Read/OrderService.Infrastructure.Read.csproj OrderService.Infrastructure.Read/

# Restore dependencies
RUN dotnet restore OrderService.API/OrderService.API.csproj

# --- Copy toàn bộ source code ---
COPY Contracts/ Contracts/
COPY OrderService/OrderService.API/ OrderService.API/
COPY OrderService/OrderService.Application/ OrderService.Application/
COPY OrderService/OrderService.Domain/ OrderService.Domain/
COPY OrderService/OrderService.Infrastructure.Write/ OrderService.Infrastructure.Write/
COPY OrderService/OrderService.Infrastructure.Read/ OrderService.Infrastructure.Read/


WORKDIR /src/OrderService.API

# Build & publish
RUN dotnet publish OrderService.API.csproj -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80
ENTRYPOINT ["dotnet", "OrderService.API.dll"]
