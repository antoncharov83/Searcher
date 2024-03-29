#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/core/aspnet:2.1-nanoserver-1903 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.1-nanoserver-1903 AS build
WORKDIR /src
COPY ["Searcher.csproj", ""]
RUN dotnet restore "./Searcher.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Searcher.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Searcher.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Searcher.dll"]