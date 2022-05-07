FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["tickets/tickets.csproj", "tickets/"]
RUN dotnet restore "tickets/tickets.csproj"
COPY . .
WORKDIR "/src/tickets"
RUN dotnet build "tickets.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "tickets.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "tickets.dll"]
