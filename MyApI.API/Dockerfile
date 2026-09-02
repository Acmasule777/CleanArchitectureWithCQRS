FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /src

COPY ["MyApI.API/MyApI.API.csproj", "MyApI.API/"]
COPY ["MyAPI.Application/MyAPI.Application.csproj", "MyAPI.Application/"]
COPY ["MyAPI.Infrastructure/MyAPI.Infrastructure.csproj", "MyAPI.Infrastructure/"]
COPY ["MyAPI.Core/MyAPI.Core.csproj", "MyAPI.Core/"]
COPY ["Shared/Shared.csproj", "Shared/"]

RUN dotnet restore "MyApI.API/MyApI.API.csproj"

COPY . .

WORKDIR "/src/MyApI.API"

RUN dotnet publish "MyApI.API.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final

WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "MyApI.API.dll"]