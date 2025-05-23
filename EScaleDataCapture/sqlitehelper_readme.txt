// 创建产品表
SQLiteHelper.Execute(@"
    CREATE TABLE IF NOT EXISTS Products (
        Id INTEGER PRIMARY KEY AUTOINCREMENT,
        Name TEXT NOT NULL,
        Price DECIMAL(10,2) NOT NULL,
        Stock INTEGER DEFAULT 0,
        CreateTime DATETIME DEFAULT CURRENT_TIMESTAMP
    )");

// 插入单个产品
var affectedRows = SQLiteHelper.Execute(
    "INSERT INTO Products (Name, Price, Stock) VALUES (@Name, @Price, @Stock)",
    new Product { Name = "笔记本电脑", Price = 5999.99m, Stock = 100 });

Console.WriteLine($"插入了 {affectedRows} 条数据");

// 批量插入
var products = new List<Product>
{
    new Product { Name = "手机", Price = 3999.99m, Stock = 50 },
    new Product { Name = "平板电脑", Price = 2999.99m, Stock = 30 }
};

SQLiteHelper.ExecuteTransaction(transaction =>
{
    foreach (var product in products)
    {
        SQLiteHelper.Execute(
            "INSERT INTO Products (Name, Price, Stock) VALUES (@Name, @Price, @Stock)",
            product, transaction);
    }
});

// 查询单个产品
var product = SQLiteHelper.QueryFirst<Product>(
    "SELECT * FROM Products WHERE Id = @Id", 
    new { Id = 1 });

Console.WriteLine($"查询结果: {product?.Name} - ¥{product?.Price}");

// 查询多个产品
var cheapProducts = SQLiteHelper.Query<Product>(
    "SELECT * FROM Products WHERE Price < @MaxPrice ORDER BY Price DESC",
    new { MaxPrice = 4000 });

foreach (var p in cheapProducts)
{
    Console.WriteLine($"{p.Name}: ¥{p.Price}");
}

// 查询数量
var count = SQLiteHelper.QueryFirst<int>(
    "SELECT COUNT(*) FROM Products WHERE Stock > 0");
Console.WriteLine($"有库存的产品数量: {count}");

// 更新单个产品
var updateRows = SQLiteHelper.Execute(
    "UPDATE Products SET Price = @Price, Stock = @Stock WHERE Id = @Id",
    new { Id = 1, Price = 5499.99m, Stock = 80 });

Console.WriteLine($"更新了 {updateRows} 条数据");

// 批量更新库存
SQLiteHelper.Execute(
    "UPDATE Products SET Stock = Stock - 1 WHERE Id IN @Ids",
    new { Ids = new[] { 1, 2, 3 } });


    // 分页查询
var pageSize = 10;
var pageNumber = 1;
var pagedProducts = SQLiteHelper.Query<Product>(
    "SELECT * FROM Products ORDER BY Id LIMIT @PageSize OFFSET @Offset",
    new { PageSize = pageSize, Offset = (pageNumber - 1) * pageSize });

// 多表查询
public class ProductWithCategory
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public string CategoryName { get; set; }
}

var productsWithCategory = SQLiteHelper.Query<ProductWithCategory>(@"
    SELECT p.Id AS ProductId, p.Name AS ProductName, c.Name AS CategoryName
    FROM Products p
    LEFT JOIN Categories c ON p.CategoryId = c.Id");