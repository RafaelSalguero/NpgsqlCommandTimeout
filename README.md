# NpgsqlCommandTimeout
Test Npgsql + EF 6 command timeout. Query fails always at 30 seconds

This project test the CommandTimeout of a long running query.

The database is created the first time the program runs.
There is a single table with 4 columns, the table is deleted and recreated with random data each time the test is runned
for avoiding postgresql caching.

The Npgsql Develop folder contains the Npgsql.dll and EntityFramework6.Npgsql.dll files used, both file versions are 3.1.0
