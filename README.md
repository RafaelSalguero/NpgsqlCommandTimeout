# NpgsqlCommandTimeout
Test Npgsql + EF 6 command timeout. Query fails always at 30 seconds

This project test the CommandTimeout of a long running query.

The database is created the first time the program runs.
There is a single table with 4 columns, the table is deleted and recreated with random data each time the test is runned
for avoiding postgresql caching.

The Npgsql Develop folder contains the Npgsql.dll and EntityFramework6.Npgsql.dll files used, both file versions are 3.1.0

The console output for the program is:

```
Conecting...
table1 count: 10000
Deleting table data...
Writing table data...
table1 count: 10000
SELECT (SELECT CAST (sum((SELECT CAST (sum("Extent3"."value2" + "Extent2"."value
1") AS float8) AS "A1" FROM "dbo"."table1" AS "Extent3" WHERE "Extent3"."value2"
 > "Extent2"."value2")) AS float8) AS "A1" FROM "dbo"."table1" AS "Extent2" WHER
E "Extent2"."value1" > "Extent1"."value1") AS "C1" FROM "dbo"."table1" AS "Exten
t1"
Executing at 08:52:01.9929689
An error occurred while reading from the store provider's data reader. See the i
nner exception for details. -->
57014: cancelando la sentencia debido a que se agotó el tiempo de espera de sent
encias -->

Now: 08:52:32.0085988
Time in ms: 30019
Test finished
```

