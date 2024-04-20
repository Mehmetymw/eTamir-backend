FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["/MyMicroservice.csproj", "MyMicroservice/"]
RUN dotnet restore "MyMicroservice/MyMicroservice.csproj"
COPY . .
WORKDIR "/src/MyMicroservice"
RUN dotnet build "MyMicroservice.csproj" -c Release -o /app/build

# Publish işlemi
FROM build AS publish
RUN dotnet publish "MyMicroservice.csproj" -c Release -o /app/publish

# Runtime image oluşturma
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyMicroservice.dll"]
