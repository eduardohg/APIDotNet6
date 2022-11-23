using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World2!");
//app.MapPost("/user", () => new {Nome = "Eduardo Giroto", Age = "24"});



//POST
app.MapPost("/products", (Product product) => {
    ProductRepository.Add(product);
    return Results.Created($"/products/{product.Code}", product.Code);
    //return product.Code + " - " + product.Name; 
});

//GET
app.MapGet("/products", ([FromQuery] string dateStart, [FromQuery] string dateEnd) => {
    return dateStart + " - " + dateEnd;
});


app.MapGet("/products/{code}", ([FromRoute] string code) => {
    var product = ProductRepository.GetBy(code);
    if(product != null)
        return Results.Ok(product);
    else
        return Results.NotFound();
});

//GET MODIFICANDO A RESPOSTA ADICIONANDO UM HEADER
app.MapGet("/AddHeader", (HttpResponse response) => {
    response.Headers.Add("Teste", "Eduardo");
    return "Header modificado!";
});

//GET ENVIANDO DADO PELO HEADER
app.MapGet("/getproductbyheader", (HttpRequest request) => {
    return request.Headers["product-code"].ToString();
});

//PUT
app.MapPut("/products", (Product product) => {
    var prod = ProductRepository.GetBy(product.Code);
    prod.Name = product.Name;
    return Results.Ok();
});


//DELETE
app.MapDelete("/products/{code}", ([FromRoute] string code) => {
    var productsaved = ProductRepository.GetBy(code);
    ProductRepository.Remove(productsaved);
    return Results.Ok();
});

app.Run();




