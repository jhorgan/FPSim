echo off
if "%1"=="" (
    pg_dump --schema-only --no-owner -d fpsim -h localhost -p 5432 -U postgres -c
) else (
    pg_dump --schema-only --no-owner -d fpsim -h localhost -p 5432 -U postgres -c > %1
)
