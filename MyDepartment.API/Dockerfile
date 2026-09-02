FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /src

COPY ["MyDepartment.API/Department.API.csproj", "MyDepartment.API/"]
COPY ["Department.Application/Department.Application.csproj", "Department.Application/"]
COPY ["Department.Infrastructure/Department.Infrastructure.csproj", "Department.Infrastructure/"]
COPY ["DepartmentCore.Core/DepartmentCore.Core.csproj", "DepartmentCore.Core/"]
COPY ["Shared/Shared.csproj", "Shared/"]

RUN dotnet restore "MyDepartment.API/Department.API.csproj"

COPY . .

WORKDIR "/src/MyDepartment.API"

RUN dotnet publish "Department.API.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final

WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Department.API.dll"]