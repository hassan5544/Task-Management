# Use the ASP.NET image for the base image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER root
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Use the SDK image for build and development
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy project files
COPY ["WebApi/WebApi.csproj", "WebApi/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Helpers/Helpers.csproj", "Helpers/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]

# Restore dependencies
RUN dotnet restore "WebApi/WebApi.csproj"

# Copy the rest of the code
COPY . .

# Build the project
WORKDIR "/src/WebApi"
RUN dotnet build "WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the app
FROM build AS publish
RUN dotnet publish "WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Create the final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Install the .NET SDK (required for dotnet ef)
RUN apt-get update \
    && apt-get install -y curl \
    && curl -sSL https://dotnet.microsoft.com/download/dotnet/thank-you/sdk-9.0.100-linux-x64-binaries \
    | bash

# Add dotnet tools path to the environment PATH variable
ENV PATH="${PATH}:${HOME}/.dotnet/tools"

# Set the entrypoint to run the application
ENTRYPOINT ["dotnet", "WebApi.dll"]
