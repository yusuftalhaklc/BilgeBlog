# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj files and restore dependencies
COPY ["Presentation/BilgeBlog.WebApi/BilgeBlog.WebApi.csproj", "Presentation/BilgeBlog.WebApi/"]
COPY ["Core/BilgeBlog.Application/BilgeBlog.Application.csproj", "Core/BilgeBlog.Application/"]
COPY ["Core/BilgeBlog.Contract/BilgeBlog.Contract.csproj", "Core/BilgeBlog.Contract/"]
COPY ["Core/BilgeBlog.Domain/BilgeBlog.Domain.csproj", "Core/BilgeBlog.Domain/"]
COPY ["Infrastructure/BilgeBlog.Persistence/BilgeBlog.Persistence.csproj", "Infrastructure/BilgeBlog.Persistence/"]

RUN dotnet restore "Presentation/BilgeBlog.WebApi/BilgeBlog.WebApi.csproj"

# Copy everything else and build
COPY . .
WORKDIR "/src/Presentation/BilgeBlog.WebApi"
RUN dotnet build "BilgeBlog.WebApi.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "BilgeBlog.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 80

# Copy published files
COPY --from=publish /app/publish .

# Set environment to Production
ENV ASPNETCORE_ENVIRONMENT=Production

# Connection string and other settings will be provided via environment variable
# Example docker run command:
# docker run -d -p 5000:80 --name bilgeblog-api \
#   -e ASPNETCORE_ENVIRONMENT=Production \
#   -e ConnectionStrings__BilgeBlogConnection="Server=your-server;Database=BilgeBlogDb;User Id=user;Password=pass;TrustServerCertificate=True;" \
#   -e Jwt__Key="your-strong-secret-key-here-minimum-32-characters" \
#   -e Jwt__Issuer="https://bilgeblog.yusuftalhaklc.com" \
#   -e Jwt__Audience="https://bilgeblog.yusuftalhaklc.com" \
#   -e Cors__AllowedOrigins__0="https://bilgeblog.yusuftalhaklc.com" \
#   -e Cors__AllowedOrigins__1="http://bilgeblog.yusuftalhaklc.com" \
#   bilgeblog-api

ENTRYPOINT ["dotnet", "BilgeBlog.WebApi.dll"]

