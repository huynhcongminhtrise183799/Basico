# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# --- Copy csproj files (Contracts và các project khác) ---
COPY Contracts/Contracts/Contracts.csproj Contracts/
COPY TicketService/TicketService.API/TicketService.API.csproj TicketService.API/
COPY TicketService/TicketService.Application/TicketService.Application.csproj TicketService.Application/
COPY TicketService/TicketService.Domain/TicketService.Domain.csproj TicketService.Domain/
COPY TicketService/TicketService.Infrastructure.Write/TicketService.Infrastructure.Write.csproj TicketService.Infrastructure.Write/
COPY TicketService/TicketService.Infrastructure.Read/TicketService.Infrastructure.Read.csproj TicketService.Infrastructure.Read/

# Restore dependencies
RUN dotnet restore TicketService.API/TicketService.API.csproj

# --- Copy toàn bộ source code ---
COPY Contracts/ Contracts/
COPY TicketService/TicketService.API/ TicketService.API/
COPY TicketService/TicketService.Application/ TicketService.Application/
COPY TicketService/TicketService.Domain/ TicketService.Domain/
COPY TicketService/TicketService.Infrastructure.Write/ TicketService.Infrastructure.Write/
COPY TicketService/TicketService.Infrastructure.Read/ TicketService.Infrastructure.Read/


WORKDIR /src/TicketService.API

# Build & publish
RUN dotnet publish TicketService.API.csproj -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80
ENTRYPOINT ["dotnet", "TicketService.API.dll"]
