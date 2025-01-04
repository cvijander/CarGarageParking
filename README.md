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
- Sluzi za poredjenje raznih vremena u nasoj aplikaciji



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
- Sluzi za poredjenje raznih int vrednosti  u nasoj aplikaciji

