FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# copy csproj and restore as distinct layers
COPY *.sln .
COPY ./src/Sd.Oms.Auth.Api/*.csproj ./src/Sd.Oms.Auth.Api/
COPY ./src/Sd.Oms.Auth.Core/*.csproj ./src/Sd.Oms.Auth.Core/
COPY ./src/Sd.Oms.Auth.Infrastructure/*.csproj ./src/Sd.Oms.Auth.Infrastructure/

RUN dotnet restore ./AuthService.sln

# copy everything else and build app
COPY . .
RUN dotnet publish -c Release -o /app/build-dll

FROM base AS final
WORKDIR /app
COPY --from=build /app/build-dll .
ENV ASPNETCORE_URLS=http://+:80
ENTRYPOINT ["dotnet", "Sd.Oms.Auth.Api.dll"]
