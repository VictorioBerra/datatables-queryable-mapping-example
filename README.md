# DataTables.Queryable Strongly Typed Column Mapping Layer Example

Thats a heck of a title isnt it?

`dotnet run`

POST to `http://localhost:5411/api/Home?param1=HelloWorld`

Payload:

```
{
  "draw": 1,
  "columns": [
    {
      "data": "Breed.Name",
      "name": "",
      "searchable": false,
      "orderable": true,
      "search": {
        "value": "",
        "regex": false
      }
    }
  ],
  "order": [
    {
      "column": 0,
      "dir": "asc"
    }
  ],
  "start": 0,
  "length": 30,
  "search": {
    "value": "",
    "regex": false
  }
}
```