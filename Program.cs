using System.Data.SqlClient;


string connection = "Server = LAPTOP-JIFALFHT\\MSSQLSERVER01; Database=Burgers;Trusted_Connection=True";
SqlConnection sqlConnection= new SqlConnection(connection);

bool IsRun = true;
Console.WriteLine("1. Get By Category");
Console.WriteLine("2. Show All Burgers");
Console.WriteLine("3. Create Burger");
Console.WriteLine("4. Remove Burger");
Console.WriteLine("5. Update Burger");

int.TryParse(Console.ReadLine(), out int request);
while (IsRun)
{
    switch (request)
    {
        case 1:
            Console.Clear();
            GetByCategory();
            break;
        case 2:
            Console.Clear();
            GetAllBurgers();
            break;
        case 3:
            Console.Clear();
            CreateBurgers();
            break;
        case 4:
            Console.Clear();
            RemoveBurger();
            break;
        case 5:
            Console.Clear();
            UpdateBurger();
            break;

        case 0:
            return;

        default:
            Console.WriteLine("Add valid option");
            break;
    }

    Console.WriteLine("1. Get By Category");
    Console.WriteLine("2. Show All Burgers");
    Console.WriteLine("3. Create Burger");
    Console.WriteLine("4. Remove Burger");
    Console.WriteLine("5. Update Burger");

    int.TryParse(Console.ReadLine(), out request);
}


void GetByCategory()
{
    SqlCommand cmd = new SqlCommand("Select * from Category", sqlConnection);
    sqlConnection.Open();
    var result = cmd.ExecuteReader();


    while (result.Read())
    {
        Console.WriteLine(result["CategoryName"] + " " + result["CategoryId"]);
    }


    sqlConnection.Close();
}

void GetAllBurgers()
{
    SqlCommand cmd = new SqlCommand("Select * from Product", sqlConnection);
    sqlConnection.Open();
    var result = cmd.ExecuteReader();


    while (result.Read())
    {
        Console.WriteLine(result["ProductId"] + " " + result["ProductName"] + " " + result["CategoryId"]);
    }


    sqlConnection.Close();
}

void CreateBurgers()
{
    Console.WriteLine("Add Burger Name: ");
    string name = Console.ReadLine();
    Console.WriteLine("Add Burger Category: ");

    int.TryParse(Console.ReadLine(), out int categoryId);
    SqlCommand cmd = new SqlCommand($"INSERT INTO Product VALUES('{name}','{categoryId}')", sqlConnection);
    sqlConnection.Open();
    cmd.ExecuteNonQuery();
    sqlConnection.Close();
}

void RemoveBurger()
{
    Console.WriteLine("Add Id: ");
    int.TryParse(Console.ReadLine(), out int ProductId);
    SqlCommand cmd1 = new SqlCommand($"Select *  from Product where ProductId= {ProductId}", sqlConnection);
    sqlConnection.Open();
    SqlDataReader reader = cmd1.ExecuteReader();

    if (reader.Read())
    {
        sqlConnection.Close();
        sqlConnection.Open();

        SqlCommand cmd = new SqlCommand($"Delete from Product where ProductId = {ProductId}", sqlConnection);
        cmd.ExecuteNonQuery();
        Console.WriteLine("Burger Deleted ");
    }
    else
    {
        Console.WriteLine("Product is not found");
    }
    sqlConnection.Close();
}

void UpdateBurger()
{
    Console.WriteLine("Enter ProductId:");
    int.TryParse(Console.ReadLine(), out int ProductId);

    Console.WriteLine("Enter Burger Category \n Meat-1\n Chicken-2\n Fish-3 ");
    int.TryParse(Console.ReadLine(), out int CategoryId);

    SqlCommand cmd1 = new SqlCommand($"SELECT * FROM Product WHERE ProductId = {ProductId}", sqlConnection);
    sqlConnection.Open();
    SqlDataReader reader = cmd1.ExecuteReader();

    if (reader.Read())
    {
        sqlConnection.Close();
        sqlConnection.Open();
        Console.WriteLine("Update Burger");
        string newProductName = Console.ReadLine();

        SqlCommand cmd = new SqlCommand($"UPDATE Product SET ProductName = '{newProductName}', CategoryId = {CategoryId} WHERE ProductId = {ProductId}", sqlConnection);
        cmd.ExecuteNonQuery();
        Console.WriteLine("Burger updated successfully");
    }
    else
    {
        Console.WriteLine("Product is not found");
    }

    sqlConnection.Close();
}
