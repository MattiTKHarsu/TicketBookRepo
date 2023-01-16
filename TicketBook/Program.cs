//-----------------------------------------------------------------------------
//   -- T I C K E T   R E P O S I T O R Y   E X A M P L E   P R O G R A M --
//   
//		Company: 		Etteplan Oy
//   	
//		Programmer: 	Matti Harsu
//
//      Function:       Start file for Application ( .Net 6 )
//
//		Functionality:
//      This program work as Back-End part for recording servive request.
//      Application contains 1) Create record, 2) Read record, 3) Upadate
//      record and 4) Delete record function. In addition all service records
//      and records regarding defined devices can be listed.
//
//		Libraries:
//		- 
//
//-----------------------------------------------------------------------------
global using TicketBook.Data;                    // Own definitions added, MHa
global using Microsoft.EntityFrameworkCore;      // Own definitions added, MHa

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Own database connection has to defined before it can be used.       //  MHa
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

