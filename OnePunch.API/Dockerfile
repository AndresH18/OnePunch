FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["OnePunchApi/OnePunchApi.csproj", "OnePunchApi/"]
RUN dotnet restore "OnePunchApi/OnePunchApi.csproj"
COPY . .
WORKDIR "/src/OnePunchApi"
RUN dotnet build "OnePunchApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OnePunchApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OnePunchApi.dll"]

# From the solution path, type: docker build .. -f DockerFile