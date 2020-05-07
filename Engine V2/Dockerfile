FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["Engine V2/Engine_V2.csproj", "Engine V2/"]

RUN dotnet restore "Engine V2/Engine_V2.csproj"
COPY . .
WORKDIR "/src/Engine V2"
RUN dotnet build "Engine_V2.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Engine_V2.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Engine_V2.dll"]