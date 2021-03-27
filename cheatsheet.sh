docker run -p 5432:5432 -e POSTGRES_PASSWORD=secret_pass -d postgres

docker run -p 5050:80 -e 'MASTER_PASSWORD_REQUIRED=False' -e 'PGADMIN_DEFAULT_EMAIL=user@domain.com' -e 'PGADMIN_DEFAULT_PASSWORD=SuperSecret' -d dpage/pgadmin4