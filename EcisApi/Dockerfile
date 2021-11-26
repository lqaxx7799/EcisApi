#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
RUN dotnet tool install --global dotnet-ef --version 5.0.7
ENV PATH="${PATH}:/root/.dotnet/tools"

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["EcisApi/EcisApi.csproj", "EcisApi/"]
RUN dotnet restore "EcisApi/EcisApi.csproj"
COPY . .
WORKDIR "/src/EcisApi"
RUN dotnet build "EcisApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EcisApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 5000/tcp
ENV ASPNETCORE_URLS http://*:5000
ENTRYPOINT ["dotnet", "EcisApi.dll", "--server.urls", "http://*:5000"]

WORKDIR /src
CMD ["dotnet", "ef", "--project", "./EcisApi", "database", "update"]