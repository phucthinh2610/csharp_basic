using CSharp_Basic.Object;
using CSharp_Basic.SQLAdappter;


Console.WriteLine("Hello, World!");
string ConnectString = "Data Source=DESKTOP-42KI5S0\\MYSQL;Initial Catalog=CSharp_Basic;Integrated Security=True";
Console.WriteLine($"ConnectString: {ConnectString}");

UsersSQLAdapter usersSqlAdapter = new usersSQLAdapter(ConnectString);
//userSqlAdapter.GetData();
//Console.WriteLine(userSqlAdapter.GetData());

List<USERS> ListUser = usersSqlAdapter.GetData();
foreach (USERS user in ListUser)
{
    Console.WriteLine($"id: {user.Id} , Name: {user.fullName}, Email: {user.email}");
}

String[] Chosse = { "User", "Product", "Cart", "Order" };

Console.WriteLine($" Enter according to the instructions \n" +
                   "1 -> for Interaction OBject User");
int i = int.Parse(Console.ReadLine());
switch (i)
{
    case 6:
        Console.WriteLine("Today is Saturday.");

        break;
    case 7:
        Console.WriteLine("Today is Sunday.");
        break;
    default:
        Console.WriteLine("Looking forward to the Weekend.");
        break;
}
