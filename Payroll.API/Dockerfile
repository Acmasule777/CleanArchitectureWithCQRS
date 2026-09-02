FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /src

COPY ["Payroll.API/Payroll.API.csproj", "Payroll.API/"]
COPY ["Payroll.Application/Payroll.Application.csproj", "Payroll.Application/"]
COPY ["Payroll.Infrastructure/Payroll.Infrastructure.csproj", "Payroll.Infrastructure/"]
COPY ["Payroll.core/Payroll.core.csproj", "Payroll.core/"]
COPY ["Shared/Shared.csproj", "Shared/"]

RUN dotnet restore "Payroll.API/Payroll.API.csproj"

COPY . .

WORKDIR "/src/Payroll.API"

RUN dotnet publish "Payroll.API.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final

WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Payroll.API.dll"]