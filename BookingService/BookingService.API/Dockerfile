# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# --- Copy csproj files (Contracts và các project khác) ---
COPY Contracts/Contracts/Contracts.csproj Contracts/
COPY BookingService/BookingService.API/BookingService.API.csproj BookingService.API/
COPY BookingService/BookingService.Application/BookingService.Application.csproj BookingService.Application/
COPY BookingService/BookingService.Domain/BookingService.Domain.csproj BookingService.Domain/
COPY BookingService/BookingService.Infrastructure.Write/BookingService.Infrastructure.Write.csproj BookingService.Infrastructure.Write/
COPY BookingService/BookingService.Infrastructure.Read/BookingService.Infrastructure.Read.csproj BookingService.Infrastructure.Read/

# Restore dependencies
RUN dotnet restore BookingService.API/BookingService.API.csproj

# --- Copy toàn bộ source code ---
COPY Contracts/ Contracts/
COPY BookingService/BookingService.API/ BookingService.API/
COPY BookingService/BookingService.Application/ BookingService.Application/
COPY BookingService/BookingService.Domain/ BookingService.Domain/
COPY BookingService/BookingService.Infrastructure.Write/ BookingService.Infrastructure.Write/
COPY BookingService/BookingService.Infrastructure.Read/ BookingService.Infrastructure.Read/


WORKDIR /src/BookingService.API

# Build & publish
RUN dotnet publish BookingService.API.csproj -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80
ENTRYPOINT ["dotnet", "BookingService.API.dll"]
