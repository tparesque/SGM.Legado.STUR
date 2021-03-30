#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/SGM.Legado.STUR.Api/SGM.Legado.STUR.Api.csproj", "src/SGM.Legado.STUR.Api/"]
COPY ["src/SGM.Legado.STUR.Core/SGM.Legado.STUR.Core.csproj", "src/SGM.Legado.STUR.Core/"]
RUN dotnet restore "src/SGM.Legado.STUR.Api/SGM.Legado.STUR.Api.csproj"

COPY . .
WORKDIR "/src/src/SGM.Legado.STUR.Api"
RUN dotnet build "SGM.Legado.STUR.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SGM.Legado.STUR.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SGM.Legado.STUR.Api.dll"]