# CarGarageParking

## Opis projekta 
Ova aplikacija omogucava nam da pratimo kad je neko vozilo uslo, izaslo iz garaze, uz evidencije vremena i placanja i istorije poseta, kao i kreiranje novih garaza i vozila. 

## Osnovne funkcionalnosti 
- Pravljenje novih garaza 
- Pravljanje novih vozila 
- Ulazak i izlazk iz garaza 
- Naplata 
- Istorija boravka vozila u garazi
- Aplikacija ako korisnik je registrovan clan 


## Struktura modela 

## A

Application predstavlja model koji povezuje korisnika (Owner) sa njegovim vozilima i kreditima u aplikaciji.

### Application

```csharp
public class Application
{
    public int ApplicationId { get; set; }

    public int OwnerId { get; set; }

    public Owner Owner { get; set; }

    public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();

    public decimal Credit { get; set; }

    public bool HasActiveMembership { get; set; }   
}
```

- `ApplicationId`: jedinstvaeni identifikator. 
- `OwnerId`: strani kljuc ka entitetu `Owner`.
- `Vehicles`: kolekcija vozila povezanih sa apliakacijom.
- `Credit`: Kredit koji korisnik moze da koristi za popuste ili placanja.
- `HasActiveMembership`: Da li korisnik ima aktivno clanstvo.  

## V

Vehicle je model za osnovne podatke o vozilu, koja ima njegov geristarski broj i podaci o vlasniku. 

### Vehicle (Vozilo)

```csharp
public class Vehicle
{
    public int VehicleId { get; set; }

    public string LicencePlate { get; set; }

    public int? OwnerId { get; set; }

    public Owner? Owner { get; set; }
}
```
- `VehicleId` : jedinstveni identifikator.
- `LicencePlate` : registracioni broj vozila.
- `OwnerId`: strani kljuc ka entitetu `Owner`.
- 'Owner' : veza ka korisniku `Owner`.

## G

Garaza je ima osnove podatke o sebi, kao sto su ime, lokacija, kapacitet, trenutnu zauzetost , slobodnoa mesta  i listu vozila koja su trenutno u njoj.

### Garage 

```csharp
 public class Garage
{
    public int GarageId { get; set; }

    public string Name { get; set; }

    public string Location { get; set; }

    public int Capacity { get; set; }

    public int CurrentOccupancy { get; set; }

    public int AvailableSpots { get
        {
            return Capacity - CurrentOccupancy;
        }
    }
    public ICollection<VehicleInGarage> VehicleInGarage { get;set; } = new List<VehicleInGarage>(); 

    public bool IsFull { get
        {
             return CurrentOccupancy >= Capacity;
        } 
    }
}
```

- `GarageId` : jedinstveni identifikator.
-  `Name` : ime garaze.
-  `Location` : lokacija garaze.
-  'Capacity' : ukupan broj parking mesta.
-  `CurrentIccupancy` : broj trenutno zauzetih mesta
-  `AvailableSposts` : broj slobodnih mesta koji se automatski izracunava oduzimanjem ukupnog broja sa brojem trenutno zauzetih mesta.
-  `IsFull` : oznaka da li je garaza puna , oznacava  da je broj zauzatih mesta  veci ili jednak ukupnom broju parking mesta.
-  `VehicleInGarage` : kolekcija vozila koja se trenutno nalaze u garazi.

## VIG

Ovaj model predstavlja vezu izmedju vozila sa jedne strane i garaze sa druge i to fizicki predstavlja vozilo u garazi, koje ima svoj id. Takodje sadrzi spoljne kljuceve ka Vehicle i Garazi i od dodatnik properitija vremena ulaska i izlaska, kao i cene po satu i proveru da li je vozilo jos uvek u garazi.

### VehicleInGarage

```csharp
public class VehicleInGarage
{
    public int VehicleInGarageId { get; set; }

    public int VehicleId { get; set; }

    public Vehicle Vehicle { get; set; }

    public int GarageId { get; set; }

    public Garage Garage  { get; set; }

    public DateTime EntryTime { get; set; }

    public DateTime? ExitTime { get; set; }

    public decimal HourlyRate { get; set; }

    public int? OwnerId { get; set; }

    public Owner? Owner { get; set; }

    public bool IsVehicleStillInGarage { get; set; } = true;   
    
}
```

- `VehicleInGarageId` : jedinstevni identifikator.
- `VehicleId` : strani kljuc ka entititenu `Vehicle`.
- `Vehicle`: veza ka entitetu `Vehicle` .
- `GarageId`: strani kljuc ka entitetu `Garage`.
- `Garage` : veza ka entitetu `Garage`.
- `EntryTime` : vreme ulaska u vozila u garazu.
- `ExitTime` : vreme izlaska vozila iz garaze.
- `HourlyRate` : cena po satu parkiranja.
- `OwnerId` : strani kljuc ka entitetu `Owner`.
- `Owner` : veza ka entititu `Owner`.
- `IsVehicleStillInGarage` : proverava da li je vozilo jos u garazi.

  ## P

Ovaj model je zaduzen za placanje, koji povezuje koja je ukupna kolicina placenja, kad i koje vozilo koje je bilo u garazi ja zaduzeno za taj iznos 

### Payment

```csharp
 public class Payment
{
    public int PaymentId { get; set; }

    public decimal TotalCharge { get; set; }

    public bool IsPaid { get; set; }

    public DateTime PaymentTime { get; set; }

    public DateTime ExpirationTime { get; set; }  // payment time + 15 minuta ili krece novi obracun 

    public int VehicleInGarageId { get; set; }

    public VehicleInGarage VehicleInGarage { get; set; } = null!;
}
```
- `PaymentId` : jedinsteni identifikator.
- `TotalCharge`: ukupan iznos za placanje.
- `IsPaid`: provera da li je racun placen.
- `PaymentTime` : vreme kad je placanje izvrseno.
- `ExpirationTime` : vreme do kada vozilo mora da napusti garazu ili krece nova naplata.
- `VehicleInGarage`: strani kljuc  ka entitetu `VehicleInGarage`.
- `VehicleInGarage` : veza ka entitetu `VehicleInGarage`.

  ## O
  
Model koji predstavlja vlasnika ili vec korisnika vozila, zbog kasnijeg prosirenja aplikacije, kao bi imali podatke o njemu  i odredjene popuste .
### Owner
```csharp
public class Owner
{
    public int OwnerId { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();

}
```
- `OwnerId` : jedisnteveni identifikator.
- `FirstName` : ime korisnika.
- `LastName` : prezime korisnika.
- `Vehicles` : kolekcija vozila koje korisnik poseduje.

   

## Instrukcije za instalaciju i pokretanje
1. Klonirajte repozitorijum:
   ```bash
   git clone <link>
   ```
2. Instalirajte potrebne pakete:
   ```bash
   dotnet restore
   ```
3. Pokrenite migracije za bazu:
   ```bash
   dotnet ef database update
   ```
4. Pokrenite aplikaciju:
   ```bash
   dotnet run
   ```

## Kako koristiti aplikaciju
- **Dodavanje garaze**: Korisnik moze registrovati novu garazu sa nazivom, lokacijom, kapacitetom i cenom po satu.
- **Dodavanje vozila**: Registracija vozila sa informacijama o registarskim oznakama i vlasniku vozila.
- **Evidencija ulaska/izlaska**: Zapisivanje vremena ulaska i izlaska vozila u/iz garaze.
- **Naplata**: Automatski obracun cene na osnovu vremena provedenog u garazi.

## Tehnički detalji
- **Platforma**: .NET Core 8
- **ORM**: Entity Framework Core
- **Baza podataka**: SQL Server


## istorija radova 
### 1)  Kreiranje modela koji su navedeni 
- Vehicle,
- VehicleInGarage,
- Garage,
- Owner,
- Payment,
- Application

- ### 2)  Kreiranje controlera koji su navedeni 
- **OwnerControler,**
- VehicleInGarage,
- **GarageControler,**
- Owner,
- Payment,
- Application

-  ### 3)  Kreiranje View za modele koji su navedeni 
- **Owner** - `Index`, `Info`
- VehicleInGarage,
- **Garage** -`Index`,`Info`
- Owner,
- Payment,
- Application

 -  ### 4)  Dodavanje datavalidation - validacije u okviru modela

 -  ### Application

```csharp
public class Application
{
    public int ApplicationId { get; set; }

    [Required]
    public int OwnerId { get; set; }

    public Owner Owner { get; set; }

    public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();

    [Range(0, double.MaxValue ,ErrorMessage = "Credit must be graeter than zero.")]
    public decimal Credit { get; set; }

    public bool HasActiveMembership { get; set; }   
}
```

- `ApplicationId`: jedinstvaeni identifikator. 
- `OwnerId`: strani kljuc ka entitetu `Owner`. neophodno properti
- `Vehicles`: kolekcija vozila povezanih sa apliakacijom.
- `Credit`: Kredit koji korisnik moze da koristi za popuste ili placanja. - mora da ima vrednost vecu od 0 
- `HasActiveMembership`: Da li korisnik ima aktivno clanstvo.  

## V

Vehicle je model za osnovne podatke o vozilu, koja ima njegov geristarski broj i podaci o vlasniku. 

### Vehicle (Vozilo)

```csharp
public class Vehicle
{
    public int VehicleId { get; set; }

    [Required(ErrorMessage = "Licence plate is required.")]
    [StringLength(15,ErrorMessage = "Licence plate can not exced 15 characters.")]

    public string LicencePlate { get; set; }

    public int? OwnerId { get; set; }

    public Owner? Owner { get; set; }
}
```
- `VehicleId` : jedinstveni identifikator.
- `LicencePlate` : registracioni broj vozila. neophodan properti i mora da ima do 15 karaktera 
- `OwnerId`: strani kljuc ka entitetu `Owner`.
- 'Owner' : veza ka korisniku `Owner`.

## G

Garaza je ima osnove podatke o sebi, kao sto su ime, lokacija, kapacitet, trenutnu zauzetost , slobodnoa mesta  i listu vozila koja su trenutno u njoj.

### Garage 

```csharp
 public class Garage
 {
     public int GarageId { get; set; }

     [Required(ErrorMessage ="Garage name is required.")]
     [StringLength(100,ErrorMessage =" Garage name can not exceed 100 characters.")]
     public string Name { get; set; }

     [Required(ErrorMessage ="Location is required.")]
     [StringLength(150,ErrorMessage = "Garage location length can not exceed 150 characters.")]
     public string Location { get; set; }

     [Range(1, int.MaxValue,ErrorMessage ="Capacity must be greater than zero")]
     public int Capacity { get; set; }

     [Range(0,int.MaxValue,ErrorMessage = "Current occupancy can not be nagetive.")]
     [IntTypeGreaterThan("Capacity", ErrorMessage = "Capacity  must be greater or equal than current capacity.")]
     public int CurrentOccupancy { get; set; }

     public int AvailableSpots { get
         {
             return Capacity - CurrentOccupancy;
         }
     }

     public ICollection<VehicleInGarage> VehicleInGarage { get;set; } = new List<VehicleInGarage>(); 

     public bool IsFull { get
         {
              return CurrentOccupancy >= Capacity;
         } 
     }

 }
```

- `GarageId` : jedinstveni identifikator.
-  `Name` : ime garaze. neophdan properti i da je duzina do 100 karaktera 
-  `Location` : lokacija garaze. neophdan properti i da je duzina do 150 karaktera 
-  'Capacity' : ukupan broj parking mesta. Vrednost  da je od 0 do max 
-  `CurrentIccupancy` : broj trenutno zauzetih mesta  Vrednost da je od 0 do max i da poredi od capacity 
-  `AvailableSposts` : broj slobodnih mesta koji se automatski izracunava oduzimanjem ukupnog broja sa brojem trenutno zauzetih mesta.
-  `IsFull` : oznaka da li je garaza puna , oznacava  da je broj zauzatih mesta  veci ili jednak ukupnom broju parking mesta.
-  `VehicleInGarage` : kolekcija vozila koja se trenutno nalaze u garazi.

## VIG

Ovaj model predstavlja vezu izmedju vozila sa jedne strane i garaze sa druge i to fizicki predstavlja vozilo u garazi, koje ima svoj id. Takodje sadrzi spoljne kljuceve ka Vehicle i Garazi i od dodatnik properitija vremena ulaska i izlaska, kao i cene po satu i proveru da li je vozilo jos uvek u garazi.

### VehicleInGarage

```csharp
public class VehicleInGarage
{
    public int VehicleInGarageId { get; set; }

    [Required]
    public int VehicleId { get; set; }

    public Vehicle Vehicle { get; set; }

    
    [Required]
    public int GarageId { get; set; }

    public Garage Garage  { get; set; }

    
    
    [Required(ErrorMessage = "Enrty time is required.")]
    [DataType(DataType.DateTime)]
    public DateTime EntryTime { get; set; }


    [DateGreaterThan("EntryTime", ErrorMessage = "Exit time must be greater than entry time.")]
    [DataType(DataType.DateTime)]
    public DateTime? ExitTime { get; set; }

    [Required]
    [Range(0.01,double.MaxValue,ErrorMessage = "HourlyRate rate must be greather than zero." )]
    public decimal HourlyRate { get; set; }

    public int? OwnerId { get; set; }

    public Owner? Owner { get; set; }

    public bool IsVehicleStillInGarage { get; set; } = true;    
    

}
```

- `VehicleInGarageId` : jedinstevni identifikator.
- `VehicleId` : strani kljuc ka entititenu `Vehicle`. neophodan properti
- `Vehicle`: veza ka entitetu `Vehicle` .
- `GarageId`: strani kljuc ka entitetu `Garage`.   neophodan properti
- `Garage` : veza ka entitetu `Garage`.
- `EntryTime` : vreme ulaska u vozila u garazu.  neopdodan properti i da je tipa date
- `ExitTime` : vreme izlaska vozila iz garaze.  neopdodan properti i da je tipa date i da poredi se sa exit time
- `HourlyRate` : cena po satu parkiranja.  neophodan properti i da ima vrednost od 0 do max
- `OwnerId` : strani kljuc ka entitetu `Owner`.
- `Owner` : veza ka entititu `Owner`.
- `IsVehicleStillInGarage` : proverava da li je vozilo jos u garazi.

  ## P

Ovaj model je zaduzen za placanje, koji povezuje koja je ukupna kolicina placenja, kad i koje vozilo koje je bilo u garazi ja zaduzeno za taj iznos 

### Payment

```csharp
 public class Payment
{
    public int PaymentId { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "Total charge must be greather than zero")]
    [IntTypeGreaterThan("VehicleHorlyRate", ErrorMessage = "Total charge   must be greater or equal than hourly rate.")]
    public decimal TotalCharge { get; set; }

    public bool IsPaid { get; set; }

    [Required(ErrorMessage = "Payment time is required.")]
    [DataType(DataType.DateTime)]

    public DateTime PaymentTime { get; set; }

    [DateGreaterThan("PaymentTime", ErrorMessage = "Expiration date must be greater than payment time.")]
    [DataType(DataType.DateTime)]
    public DateTime ExpirationTime { get; set; }  // payment time + 15 minuta ili krece novi obracun 

    [Required]
    public int VehicleInGarageId { get; set; }

    [Range(0.01,double.MaxValue, ErrorMessage = "Vehicle hourly rate must be greather than zero.")]
    public decimal VehicleHourlyRate { get; set; }
    public VehicleInGarage VehicleInGarage { get; set; } = null!;
}
```
- `PaymentId` : jedinsteni identifikator.
- `TotalCharge`: ukupan iznos za placanje.  da je vrednost veca od 0 do max i da mora da bude veca od vehicleHourlyRate
- `IsPaid`: provera da li je racun placen.
- `PaymentTime` : vreme kad je placanje izvrseno.  neophodan properi i da je i da je tipa datetime 
- `ExpirationTime` : vreme do kada vozilo mora da napusti garazu ili krece nova naplata. neophodan properi i da je i da je tipa datetime  i poredjene sa  payment time 
- `VehicleInGarageId`: strani kljuc  ka entitetu `VehicleInGarage`.  neophoddan properti
- `VehicleInGarage` : veza ka entitetu `VehicleInGarage`.

  ## O
  

### Owner
```csharp
public class Owner
{
    public int OwnerId { get; set; }

    [Required(ErrorMessage = "First name is reqired.")]
    [StringLength(50,ErrorMessage ="First name can not exceed 50 characters.")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Last name is required.")]
    [StringLength(50, ErrorMessage ="Last name can not exceed 50 characters.")]
    public string LastName { get; set; }

    public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();

}
```
- `OwnerId` : jedisnteveni identifikator.
- `FirstName` : ime korisnika. neophodan properti i duzina do 50 karaktera
- `LastName` : prezime korisnika.  neophodan properti i duzina do 50 karaktera 
- `Vehicles` : kolekcija vozila koje korisnik poseduje.


### Util folder koji sadrzi custom klase za validaciju 

### DateGreaterThanAttribute
```csharp
 public class DateGreaterThanAttribute: ValidationAttribute
 {
     private readonly string _comparisonProperty;

     public DateGreaterThanAttribute(string comparisonProperty)
     {
         _comparisonProperty = comparisonProperty;
     }

     protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
     {
         if (value == null)
         {
             return ValidationResult.Success;
         }

         var currentValue = (DateTime?)value;

         var comparisonProperty = validationContext.ObjectType.GetProperty(_comparisonProperty);

         if (comparisonProperty == null)
         {
             return new ValidationResult($"Uknown property {_comparisonProperty}");
         }

         var comparisonValue = (DateTime?)comparisonProperty.GetValue(validationContext.ObjectInstance);

         if (value == null || comparisonValue == null)
         {
             return ValidationResult.Success;
         }

         if (value != null && comparisonValue != null && currentValue <= comparisonValue)
         {
             return new ValidationResult(ErrorMessage ?? $"{validationContext.DisplayName} must be greater then {_comparisonProperty}");
         }

         return ValidationResult.Success;
     }
 }
```


 Sluzi za poredjenje raznih vremena u nasoj aplikaciji

### IntTypeGreaterThan

```csharp
public class IntTypeGreaterThan :ValidationAttribute
{
       private readonly string _comparisonProperty;

        public IntTypeGreaterThan(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            var currentValue = (int)value;

            var comparisonProperty = validationContext.ObjectType.GetProperty(_comparisonProperty);

            if (comparisonProperty == null)
            {
                return new ValidationResult($"Uknown property {_comparisonProperty}");
            }

            var comparisonValue = (int)comparisonProperty.GetValue(validationContext.ObjectInstance);

            if (value == null || comparisonValue == null)
            {
                return ValidationResult.Success;
            }

            if (value != null && comparisonValue != null && currentValue < comparisonValue)
            {
                return new ValidationResult(ErrorMessage ?? $"{validationContext.DisplayName} must be greater then {_comparisonProperty}");
            }

            return ValidationResult.Success;
        }
    }
 ```

 Sluzi za poredjenje raznih int vrednosti  u nasoj aplikaciji

### Payment :IValidatableObject za validaciju 

```csharp
public class Payment :IValidatableObject
{
    public int PaymentId { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "Total charge must be greather than zero")]
    [IntTypeGreaterThan("VehicleHorlyRate", ErrorMessage = "Total charge   must be greater or equal than hourly rate.")]
    public decimal TotalCharge { get; set; }

    public bool IsPaid { get; set; }

    [Required(ErrorMessage = "Payment time is required.")]
    [DataType(DataType.DateTime)]

    public DateTime PaymentTime { get; set; }

    [DateGreaterThan("PaymentTime", ErrorMessage = "Expiration date must be greater than payment time.")]
    [DataType(DataType.DateTime)]
    public DateTime ExpirationTime { get; set; }  // payment time + 15 minuta ili krece novi obracun 

    [Required]
    public int VehicleInGarageId { get; set; }

    [Range(0.01,double.MaxValue, ErrorMessage = "Vehicle hourly rate must be greather than zero.")]
    public decimal VehicleHourlyRate { get; set; }
    public VehicleInGarage VehicleInGarage { get; set; } = null!;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (!IsPaid)
        {
            yield return new ValidationResult("Payment has not been completed", new[] { nameof(IsPaid)});
        }

        if ((DateTime.Now - PaymentTime).TotalMinutes > 15)
        {
            VehicleInGarage.EntryTime = ExpirationTime;

            yield return new ValidationResult("You have exceed time to leave a garage, new cycle has started.", new[] { nameof(ExpirationTime) });
        }

        var totalHours = Math.Ceiling((PaymentTime - VehicleInGarage.EntryTime).TotalHours);
        var reiquiredCharge = (decimal)totalHours * VehicleHourlyRate;

        if(TotalCharge < reiquiredCharge)
        {
            yield return new ValidationResult($"Total charge must be at least {reiquiredCharge} based on hourly rate", new[] {nameof(TotalCharge) });
        }
    }
}
```

Dodajemo IvalidateObject za proveru placanja cime mozemo da proverimo da li je placeno, da li je vreme napustanja garaze manje od 15 minuta u odnosu va vreme placanja, ako jeste onda je novi krug se pocinje, 
takodje proveravamo da li je iznos novca koji treba platiti jednak ili vec manji od totalCharge 


### 5)  Povezivanje sa Entity frajmworkom 
 - 1 - instalacija preko nuget managera sledecih paketa
     - Microsoft.EntityFrameworkCore.Entity
     - Microsoft.EntityFrameworkCore.SqlServer
     - Microsoft.EntityFrameworkCore.Tools


- 2  - Dodavanje konekcionog stringa u     appsettings.json
```charp
         {
          "Logging": {
            "LogLevel": {
              "Default": "Information",
              "Microsoft.AspNetCore": "Warning"
            }
          },
          "ConnectionStrings": {
            "DefaultConnection": "Server=DESKTOP-FEP7AFG\\SQLEXPRESS;Database=CarGarageDb;Trusted_Connection=True;MultipleActiveResultSets=True;Encrypt=False;"
          },
          "AllowedHosts": "*"
        }
```

- 3  - Kreiranje klase CarGarageParkingDBContext.cs - koja je spona sa bazom podataka i koristeci DBSetove pravimo tabele u bazi, dok koristeci modelBuilder dodajemo  dodatna ogranicenja

```csharp
using CarGarageParking.Models;
using Microsoft.EntityFrameworkCore;

namespace CarGarageParking
{
    public class CarGarageParkingDBContext : DbContext
    {
        public CarGarageParkingDBContext(DbContextOptions<CarGarageParkingDBContext> options) : base(options) { }

        public DbSet<Application> Applications { get; set; }

        public DbSet<Garage> Garages    { get; set; }

        public DbSet<Owner> Owners { get; set; }

        public DbSet<Payment> Payments  { get; set; }

        public DbSet<Vehicle> Vehicles { get; set; }

        public DbSet<VehicleInGarage> VehicleInGarages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Garage>()
                 .HasMany(g => g.VehicleInGarage)
                 .WithOne(vg => vg.Garage)
                 .HasForeignKey(vg => vg.GarageId)
                 .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Owner>()
                .HasMany(o => o.Vehicles)
                .WithOne(v => v.Owner )
                .HasForeignKey(v => v.OwnerId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Application>()
                .Property(a => a.Credit)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Payment>()
                .Property(p => p.TotalCharge)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Payment>()
               .Property(p => p.VehicleHourlyRate)
               .HasPrecision(18, 2);

            modelBuilder.Entity<VehicleInGarage>()
              .Property(vg => vg.HourlyRate)
              .HasPrecision(18, 2);

        }

    }
}
```

- 4 - Povezivanje u okviru buidera dodajemo servise koji sadrzi konekcioni string

```csharp
  using CarGarageParking;
using CarGarageParking.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<CarGarageParkingDBContext>(options =>
   options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IOwnerService, OwnerService>();
builder.Services.AddScoped<IVehicleService, VehicleService>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
```

        
### 6)  Kreiranje Servisnog sloja koristeci Service (folder) i interfejse 
Servisni sloj nam omgucava da "presecemo" vezu M-V-C tj M-C-V  gde ustvari svaki upit sa baze se preko konotrlela salje na odgovoarajuci View, ubacujuci servisni sloj, spustamo Model dole sa DBconktexto, cime dovodimo 
do lakse ogranizacije koda, do vece modularnosti i kasnije mozemo da menjamo u budcnosti bazu bez da uticemo na ostatak aplikacije i takodje prilikom koriscenja testiranja, bice nam pretraga i testiranje olaksano i dosta ubrzano, zbog kolicine 

- 1 - Kreiramo folder Service  radi lakse orgranizacije podataka
  
- 2 - Kreiramo interfejs `IOwnerService` - koji predstavlja osnovne CRUD operacije - kreiranje, citanje, azuriranje i brisanje i dodatna metoda ako nam je neophodna 

  ```csharp
  using CarGarageParking.Models;

namespace CarGarageParking.Services
{
    public interface IOwnerService
    {
        IEnumerable<Owner> GetAllOwners();

        IEnumerable<Owner> GetAllOwnersWithVehicles();

        Owner GetOwnerById(int id);

        void CreateOwner(Owner owner);

        void UpdateOwner(Owner owner);

        void DeleteOwner(int id);
    }
}
```
- 3 - Kreiramo klasu `OwnerService` koja implementa interfejs `IOwnerService` - dakle ona je zaduzena da nam da kod tj implementira ponasanje ovih metoda, posto je sama klasa zavisi od instance dbcontexta zato je i pozivamo u okvriu konstruktora
  i onda shodno metodama pozivamo metode koje Db kontext sadrzi, tako npf ono sto smo koristili lambda izraz FirstOrDefault ili vec single ovde mozemo da koristimo metodu Find() - za pronalazenje vrednosti preko id, itd.. 

  ```csharp
using CarGarageParking.Models;
using Microsoft.EntityFrameworkCore;

namespace CarGarageParking.Services
{
    public class OwnerService : IOwnerService
    {
        private readonly CarGarageParkingDBContext _context;

        public OwnerService(CarGarageParkingDBContext context)
        {
            _context = context;
        }


        public void CreateOwner(Owner owner)
        {
            _context.Owners.Add(owner);
            
        }

        public void DeleteOwner(int id)
        {
           Owner owner = _context.Owners.Find(id);

            if(owner != null)
            {
                _context.Owners.Remove(owner);
                 
            }
        }

        public IEnumerable<Owner> GetAllOwners()
        {
            return _context.Owners.ToList();
        }

        public IEnumerable<Owner> GetAllOwnersWithVehicles()
        {
            return _context.Owners.Include(o => o.Vehicles).ToList();
        }

        public Owner GetOwnerById(int id)
        {
            return _context.Owners.Find(id);
        }

        public void UpdateOwner(Owner owner)
        {
            _context.Owners.Update(owner);
            
        }
    }
}
```

- 4 - prevezujemo sada `OwnerController` tako da sada prihvata umesto `dbcontexta` privhata nas IOwnerServise


```csharp
      using CarGarageParking.Models;
    using CarGarageParking.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Security.Cryptography.X509Certificates;

namespace CarGarageParking.Controllers
{
    public class OwnerController : Controller
    {
             

        private readonly IOwnerSerice _ownerService;
           

        public OwnerController(IOwnerSerice ownerService)
        {
            _ownerService = ownerService;
        }

        public IActionResult Index(string firstName, string lastName, int? numberOfCars)
        {

            var owners =  _ownerService.GetAllOwnersWithVehicles();
                 

            if (firstName !=null)
            {
                owners = owners.Where(o => o.FirstName.ToLower() == firstName.Trim().ToLower());
            }

            if(lastName !=null)
            {
                owners = owners.Where(o => o.LastName.ToLower() == lastName.Trim().ToLower());
            }

            if(numberOfCars.HasValue)
            {
                owners = owners.Where(o => o.Vehicles.Count() == numberOfCars);
            }

            return View(owners);
        }

        public IActionResult Info(int id)
        {
           Owner singleOwner =  _ownerService.GetOwnerById(id);            
           singleOwner.Vehicles = _vehicleService.GetVehicleByCondition(v => v.OwnerId == id).ToList();      


            return View(singleOwner);
        }
        
    }
}
```
