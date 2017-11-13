# Mssql Rows To Postgresql Rows

What does it mean:

 * Run the project.
 * Write table name which you want to create insert queries.
 * Hit the button.
 * This project generates "insert into" queries for postgresql from mssql.


I created this project for my job. There is no other reason. 

And here's some code! :+1:

```cs
 var clResult = "";
 if (clName == "Id")
     clResult = "gid";
 else if (clName == "InsertDate" || clName == "InsertDateTime")
     clResult = "created";
 else if (clName == "LastUpdated" || clName == "UpdateDateTime")
     clResult = "modified";
 else if (clName == "InsertUserId" || clName == "InsertUser")
     clResult = "created_by";
 else if (clName == "UpdateUserId")
     clResult = "modified_by";
 else
     clResult = clName;
```

[This lines](https://github.com/atakansavas/Mssql-Rows-to-Postgresql-Rows/blob/master/Form1.cs) means, it not include that columns in the query. Thats for my base object.
