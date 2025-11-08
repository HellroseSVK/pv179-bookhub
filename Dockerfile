# Use the Microsoft's ASP.NET Core runtime base image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
VOLUME /app/data

# Use the SDK image to build the app and run EF migrations
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["BookHub/WebAPI.csproj", "BookHub/"]
COPY ["DataAccessLayer/DataAccessLayer.csproj", "DataAccessLayer/"]
COPY ["BusinessLayer/BusinessLayer.csproj", "BusinessLayer/"]

RUN dotnet restore "BookHub/WebAPI.csproj"
COPY . .

WORKDIR "/src/BookHub"
RUN dotnet build "WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebAPI.csproj" -c Release -o /app/publish

# We'll use an SDK image here to finalize the image
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
# Copy the DataAccessLayer project and related assets for migrations
COPY --from=build /src/DataAccessLayer /app/DataAccessLayer

# Start the app
ENTRYPOINT ["dotnet", "WebAPI.dll"]
