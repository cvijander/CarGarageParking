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
- `ApplicationId`: jedinstvaeni identifikator 
- `OwnerId`: strani kljuc ka entitetu `Owner`
- `Vehicles`: kolekcija vozila povezanih sa apliakacijom
- `Credit`: Kredit koji korisnik moze da koristi za popuste ili placanja
-  `HasActiveMembership`: Da li korisnik ima aktivno clanstvo  

Vehicle je model za osnovne podatke o vozilu, koja ima njegov geristarski broj i podaci o vlasniku 
### Vehicle (Vozilo)
```csharp
public class Vehicle
{
    public int Id { get; set; }

    public string LicencePlate { get; set; }

    public int OwnerId { get; set; }

    public Owner Owner { get; set; }
}
```
Garaza je ima osnove podatke o sebi, kao sto su ime, lokacija, kapacitet i listu vozila koja su trenutno u njoj.
### Garage 
```csharp
 public class Garage
 {
     public int Id { get; set; }

     public string Name { get; set; }

     public string Location { get; set; }

     public int Capacity { get; set; }

     public ICollection<VehicleInGarage> VehicleInGarage { get;set; } = new List<VehicleInGarage>(); 
 }
```
Ovaj model predstavlja vezu izmedju vozila sa jedne strane i garaze sa druge i to fizicki predstavlja vozilo u garazi, koje ima svoj id. Takodje sadrzi spoljne kljuceve ka Vehicle i Garazi i od dodatnik properitija vremena ulaska i izlaska, kao i cene naplate i cene po satu
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
     public decimal? TotalCharge { get; set; }

     public void CalculateTotalCharge()
     {
         if(ExitTime == null)
         {
             throw new InvalidDataException("Exit time must be before calculating");
         }

         var duration = ExitTime.Value - EntryTime;
         var totalHours = Math.Ceiling(duration.TotalHours);
         TotalCharge = (decimal)totalHours * HourlyRate;
     }

 }
```
Ovaj model je zaduzen za placanje, koji povezuje koja je ukupna kolicina placenja, kad i koje vozilo koje je bilo u garazi ja zaduzeno za taj iznos 
### Payment
```csharp
 public class Payment
 {
     public int Id { get; set; } 

     public decimal Amount { get; set; }

     public DateTime PaymentTime { get; set; }

     public int VehicleInGarageId { get; set; }

     public VehicleInGarage VehicleInGarage { get; set; } = null!;
 }
```
Model koji predstavlja vlasnika ili vec korisnika vozila, zbog kasnijeg prosirenja aplikacije, kao bi imali podatke o njemu  i odredjene popuste .
### Owner
```csharp
public class Owner
{
    public int OwnerId { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

}
```

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
- Payment



