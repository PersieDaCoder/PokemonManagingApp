FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore

WORKDIR /App/src/PokemonManagingApp.Web
CMD ["dotnet", "run"]

EXPOSE 7259