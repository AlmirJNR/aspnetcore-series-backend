FROM postgres:alpine

COPY ./psql.sql /docker-entrypoint-initdb.d/
