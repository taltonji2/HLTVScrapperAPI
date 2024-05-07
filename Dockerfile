# Use the appropriate Windows base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0-windowsservercore-ltsc2022 AS base

# Expose ports if needed
EXPOSE 8080
EXPOSE 8081

# Use the appropriate Windows SDK base image
FROM mcr.microsoft.com/dotnet/sdk:8.0-windowsservercore-ltsc2022 AS build

# Set the working directory
WORKDIR /src

# Copy the project file and restore dependencies
COPY ["HLTVScrapperAPI.csproj", "."]
RUN dotnet restore "./HLTVScrapperAPI.csproj"

# Copy the rest of the source code
COPY . .

# Build the project
WORKDIR "/src/."
RUN dotnet build "./HLTVScrapperAPI.csproj" -c Release -o /app/build

# Use the previous stage (build) as a base
FROM build AS publish

# Publish the project
RUN dotnet publish "./HLTVScrapperAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Use the base stage as a final stage
FROM base AS final

# Set the working directory
WORKDIR /app

# Copy the published files from the previous stage
COPY --from=publish /app/publish .

# Specify the entry point for the application
ENTRYPOINT ["dotnet", "HLTVScrapperAPI.dll"]
