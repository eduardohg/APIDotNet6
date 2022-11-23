public static class ProductRepository{
    public static List<Product> Products { get; set; }

    public static void Add(Product product){
        if(Products == null)
            Products = new List<Product>();

        Products.Add(product);
    }

    public static Product GetBy(string codigo){
        return Products.FirstOrDefault(p => p.Code == codigo);
    }

    public static void Remove(Product product){
        Products.Remove(product);
    }
}