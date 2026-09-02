FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /src

COPY ["Notification.API/Notification.API.csproj", "Notification.API/"]
COPY ["Nofication.Application/Nofication.Application.csproj", "Nofication.Application/"]
COPY ["Notification.Infrastructure/Notification.Infrastructure.csproj", "Notification.Infrastructure/"]
COPY ["Notification.Core/Notification.Core.csproj", "Notification.Core/"]
COPY ["Shared/Shared.csproj", "Shared/"]

RUN dotnet restore "Notification.API/Notification.API.csproj"

COPY . .

WORKDIR "/src/Notification.API"

RUN dotnet publish "Notification.API.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final

WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Notification.API.dll"]