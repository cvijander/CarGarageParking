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



