#!/bin/bash

# Verifica que se hayan pasado ambos argumentos
if [ "$#" -ne 2 ]; then
    echo "Uso: $0 <path> <puerto>"
    exit 1
fi

# Guarda los argumentos en variables
LOG_PATH=$1
PORT=$2

echo 'Construcción de imagen en progreso...'
docker build -t menu:2.0.0 .

echo 'Imagen construida exitosamente...'
echo 'Levantando contenedor...'

docker run -d -v "$LOG_PATH:/app/logs" -p "$PORT:7008" --name menu-api menu:2.0.0

echo 'Contenedor levantado exitosamente...'
