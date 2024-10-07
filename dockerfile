#USE THE OFFICIAL IMAGES AS A PARENT IMAGE
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env


#set timezone for argentina
ENV TZ=America/Argentina/Buenos_Aires

# set the working directory
WORKDIR /app

#copy the file from yout host to your current location

COPY . ./

#borrar los appsettings.json originales
RUN rm Api/appsettings.Development.json Api/appsettings.json

# RUN the command inside your image filesystem
RUN dotnet restore

RUN dotnet build "Api/Api.csproj" -c Release -o /app/build

#BUILD the application
RUN dotnet publish "Api/Api.csproj" -c Release -o /app/publish

#BUILD runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0

#setear zona horaria argentina
RUN ln -sf /usr/share/zoneinfo/America/Argentina/Buenos_Aires /etc/localtime && \
    echo "America/Argentina/Buenos_Aires" > /etc/timezone

WORKDIR /app

COPY --from=build-env /app/publish .

#copiar el archivo appsettings.json en el directorio del contenedor
COPY appsettings.json /app/appsettings.json


EXPOSE 7008

#RUN the specified command with the container
ENTRYPOINT [ "dotnet","Api.dll" ]