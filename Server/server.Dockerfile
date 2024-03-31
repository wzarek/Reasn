FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
COPY . /source
WORKDIR /source/ReasnAPI/ReasnAPI
ARG TARGETARCH
RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages \
    dotnet publish -a ${TARGETARCH/amd64/x64} --use-current-runtime --self-contained false -o /app 

FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS development
COPY . /source 
WORKDIR /source/ReasnAPI/ReasnAPI
CMD ["dotnet", "run", "--no-launch-profile"]

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS production
WORKDIR /app
COPY --from=build /app .
USER $APP_UID
ENTRYPOINT ["dotnet", "ReasnAPI.dll"]
