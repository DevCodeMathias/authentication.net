FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY . .
RUN dotnet restore ./API_AUTENTICATION.csproj
RUN dotnet publish ./API_AUTENTICATION.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:5141
EXPOSE 5141

ENTRYPOINT ["dotnet", "API_AUTENTICATION.dll"]
