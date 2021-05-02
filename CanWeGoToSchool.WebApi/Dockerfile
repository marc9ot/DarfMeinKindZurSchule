#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["CanWeGoToSchool.WebApi/CanWeGoToSchool.WebApi.csproj", "CanWeGoToSchool.WebApi/"]
RUN dotnet restore "CanWeGoToSchool.WebApi/CanWeGoToSchool.WebApi.csproj"
COPY . .
WORKDIR "/src/CanWeGoToSchool.WebApi"
RUN dotnet build "CanWeGoToSchool.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CanWeGoToSchool.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CanWeGoToSchool.WebApi.dll"]
