#!/bin/bash
set -e

psql -v ON_ERROR_STOP=1 --username "$POSTGRES_USER" --dbname "$POSTGRES_DB" <<-EOSQL
    CREATE TABLE users (
        id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
        name TEXT NOT NULL,
        birthdate TIMESTAMP WITHOUT TIME ZONE,
        active BOOLEAN NOT NULL DEFAULT true
    );
EOSQL