#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
#ENV PATH ${PATH}:/root/.dotnet/tools
#RUN dotnet tool install --global dotnet-ef --version 5.0.7
WORKDIR /src
COPY ["EcisApi/EcisApi.csproj", "EcisApi/"]
RUN dotnet restore "EcisApi/EcisApi.csproj"
COPY . .
WORKDIR "/src/EcisApi"
#CMD ["dotnet", "ef", "--project", "./", "database", "update"]
RUN dotnet build "EcisApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EcisApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 5000/tcp
ENV ASPNETCORE_URLS http://*:5000
ENTRYPOINT ["dotnet", "EcisApi.dll", "--server.urls", "http://*:5000"]
#RUN ["chmod", "+x", "/commands.sh"]
#ENTRYPOINT ["/commands.sh"]
