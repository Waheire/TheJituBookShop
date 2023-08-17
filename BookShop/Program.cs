using BookShop.Controller;

 class Program 
{
    public async static Task Main(string[] args)
    {
        await BookController.Init();
    }
}
