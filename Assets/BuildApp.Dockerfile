FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

COPY *.sln ./
COPY ./Src ./Src
RUN dotnet restore

RUN dotnet publish -c Release -o outdir ./Src/Api/Api.csproj

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app

COPY --from=build /app/outdir .

ENTRYPOINT [ "dotnet", "Api.dll" ]
