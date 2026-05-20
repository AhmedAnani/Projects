# Animal Shelter Management System

A C# console application for managing an animal shelter.
Staff can add, view, search, update, and remove animals.
All data is **saved automatically** after every operation — no manual save needed.

---

## How to Run

```bash
dotnet run
```

---

## Project Structure

```
src/
├── Models/
│   ├── AnimalStatus.cs        ← Enum: Available, Adopted, UnderMedicalCare, PendingAdoption
│   ├── IAdoptable.cs          ← Interface: Adopt(), IsAvailable(), GetAdoptionInfo()
│   ├── ICareRecord.cs         ← Interface: AddCareNote(), GetCareHistory(), UpdateVaccineInfo()
│   ├── Animal.cs              ← Abstract base class — shared state & behaviour
│   ├── Dog.cs                 ← Subclass: Breed, Size, IsVaccinated
│   ├── Cat.cs                 ← Subclass: Color, IsIndoor, IsVaccinated
│   ├── Bird.cs                ← Subclass: Species, CanFly, WingSpan
│   └── SmallAnimal.cs         ← Subclass: AnimalType, Habitat, IsNocturnal
│
├── Repos/
│   ├── IAnimalRepository.cs   ← Interface: Add, Remove, Find, Adopt, UpdateStatus, GetAll
│   ├── AnimalManager.cs       ← In-memory only implementation (no file I/O)
│   ├── AnimalFileRepository.cs← File-aware implementation — auto-saves after every change
│   └── ShelterFileHandler.cs  ← Reads/writes the .txt file (knows nothing about the app)
│
├── Services/
│   ├── IAnimalService.cs      ← Interface for all business operations
│   └── AnimalService.cs       ← Business logic, delegates to IAnimalRepository
│
├── Factory/
│   └── AnimalFactory.cs       ← Prompts user and builds the correct Animal subclass
│
├── UI/
│   ├── ConsoleInput.cs        ← ReadInt, ReadString, ReadBool — all validated
│   └── AnimalDisplay.cs       ← ShowAll, ShowDetail, ShowSuccess, ShowError
│
├── Controllers/
│   └── AppController.cs       ← Menu loop + 5 menu actions
│
└── Program.cs                 ← Entry point — wires everything together

data/
└── shelter_data.txt           ← Auto-created, pipe-delimited, one animal per line
```

# Class Diagram
<img width="4096" height="1489" alt="class_diagram" src="https://github.com/user-attachments/assets/364228b1-d780-4ff9-b4b6-30f88a208db7" />


---

## How the Layers Talk to Each Other

```
Program.cs
│  Creates: ShelterFileHandler → AnimalFileRepository → AnimalService → AppController
│
└── AppController  (depends on IAnimalService)
    └── AnimalService  (depends on IAnimalRepository)
        └── AnimalFileRepository  (implements IAnimalRepository)
            ├── List<Animal>        ← in-memory store
            └── ShelterFileHandler  ← file I/O
```


---

## File-by-File Explanation

### `AnimalStatus.cs`
An enum defining 4 lifecycle states.

# State Diagram
<img width="692" height="811" alt="State Diagram" src="https://github.com/user-attachments/assets/569e320a-c005-418b-88a0-0892a2b897e6" />


### `IAdoptable.cs` and `ICareRecord.cs`
Two separate interfaces `IAdoptable` covers adoption behaviour. `ICareRecord` covers medical notes and vaccines. Every `Animal` subclass automatically gets both because `Animal` implements both.

### `Animal.cs` — Abstract Base Class
The most important class. It:
- Holds all shared fields: `Id`, `Name`, `Age`, `Status`, `IntakeDate`, `AdopterName`, `VaccineInfo`, care history.
- Protects sensitive state — `AdopterName` is `private set`, only settable through `Adopt()`.
- Declares `GetSpeciesInfo()` as `abstract` — every subclass must override it.
- Implements `Adopt()` with validation: only `Available` or `PendingAdoption` animals can be adopted.

### `Dog`, `Cat`, `Bird`, `SmallAnimal`
Each inherits all of `Animal` and adds 3 fields. Each overrides `GetSpeciesInfo()` with its own species description.

### `IAnimalRepository.cs`
The contract for data storage. Includes `Adopt()` and `AddCareNote()` at the repository level — this allows `AnimalFileRepository` to call `Save()` immediately after each domain operation.

### `AnimalManager.cs`
A pure in-memory implementation of `IAnimalRepository`. No file I/O at all. Useful for testing or if you want to run the app without persistence.

### `ShelterFileHandler.cs`
Handles only one thing: reading and writing the `.txt` file. It knows nothing about the menu, services, or business rules.

### `AnimalFileRepository.cs`
A file-aware implementation of `IAnimalRepository`. It wraps both a `List<Animal>` and a `ShelterFileHandler`. After every mutation (`Add`, `Remove`, `UpdateStatus`, `Adopt`, `AddCareNote`) it calls `Save()` — so data is always persisted immediately.

### `AnimalService.cs`
Business logic layer. Validates inputs and delegates to `IAnimalRepository`. It does not know whether the repo is file-backed or in-memory.

### `AnimalFactory.cs`
Prompts the user and builds the right animal subclass. If you add a 5th animal type, you only need to update this factory — the rest of the app is unaffected.

### `ConsoleInput.cs`
All input reading. Every method loops until valid input is received. The app can never crash from bad user input.

### `AnimalDisplay.cs`
All output. The controller never calls `Console.WriteLine` directly.

### `AppController.cs`
Owns the `do-while` menu loop. Receives `IAnimalService` — never touches the repository or file directly.

### `Program.cs`
Builds the dependency chain and starts the app:
```
ShelterFileHandler → AnimalFileRepository → AnimalService → AppController → Run()
```

---

## File Handler

### The Two-Class Design

File handling is split between two classes with different jobs:

**`ShelterFileHandler`** — knows how to read and write the `.txt` file. Nothing else.

**`AnimalFileRepository`** — knows how to manage a `List<Animal>` and when to call `Save()`. It uses `ShelterFileHandler` as a tool.

---

### The Path — Why `AppDomain` Is Used

```csharp
private static readonly string DataFile = Path.GetFullPath(
    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\data\shelter_data.txt")
);
```

When `dotnet run` executes, the working directory is `bin/Debug/net8.0/`. Without this path calculation the file would be created inside the build output folder — you'd never find it.

- `AppDomain.CurrentDomain.BaseDirectory` → `bin/Debug/net8.0/`
- `@"..\..\..\"` → steps up 3 levels to the project root
- `data\shelter_data.txt` → the correct final location

`Path.GetFullPath()` resolves it to one clean absolute path.

---

### How `SaveAll()` Works — Step by Step

```csharp
public void SaveAll(IReadOnlyList<Animal> animals)
```

**Step 1 — Create the folder if it doesn't exist:**
```csharp
string dir = Path.GetDirectoryName(fullPath);
if (!Directory.Exists(dir))
    Directory.CreateDirectory(dir);
```
First time the program runs there is no `data/` folder. This line creates it automatically.

**Step 2 — Open the file, overwriting any previous content:**
```csharp
using var writer = new StreamWriter(fullPath, append: false);
```
`append: false` means a clean file every time — the entire current state is written fresh.
`using` guarantees the file is closed even if an exception occurs.

**Step 3 — Write shared base fields for each animal:**
```csharp
string line = $"{animal.GetType().Name}|{animal.Id}|{animal.Name}|{animal.Age}|{animal.Status}|{animal.AdopterName}|";
```
`animal.GetType().Name` returns `"Dog"`, `"Cat"`, `"Bird"`, or `"SmallAnimal"` — this is saved as the first field so `LoadAll()` knows which constructor to call.

**Step 4 — Append the subclass-specific fields:**
```csharp
line += animal switch
{
    Dog  d => $"{d.Breed}|{d.IsVaccinated}|{d.Size}",
    Cat  c => $"{c.Color}|{c.IsIndoor}|{c.IsVaccinated}",
    ...
};
```
Each type contributes exactly 3 extra fields at positions `[6]`, `[7]`, `[8]`.

**Resulting file:**
```
Dog|1|Buddy|3|Available||Labrador|True|Large
Cat|2|Luna|2|Adopted|Jane Smith|Black|True|True
Bird|3|Tweety|1|Available||Canary|True|Small
SmallAnimal|4|Hammy|1|UnderMedicalCare||Hamster|Desert|True
```

---

### How `LoadAll()` Works — Step by Step

**Step 1 — Check if the file exists:**
```csharp
if (!File.Exists(fullPath))
    return result;  // Empty list — first run
```
No file = no crash. The shelter starts empty.

**Step 2 — Read one line at a time:**
```csharp
while ((line = reader.ReadLine()) != null)
```

**Step 3 — Split back into fields:**
```csharp
string[] p = line.Split('|');
// p[0]="Dog" p[1]="1" p[2]="Buddy" p[3]="3" p[4]="Available" p[5]="" p[6]="Labrador" ...
```

**Step 4 — Parse shared fields:**
```csharp
var status = Enum.Parse<AnimalStatus>(p[4]);
```
`Enum.Parse<AnimalStatus>("Available")` converts the string name back to the enum value. This is why the status is saved as its name (not a number) — readable and easy to parse.

**Step 5 — Build the correct subclass:**
```csharp
Animal animal = typeName switch
{
    "Dog" => new Dog(id, name, age, breed: p[6], isVaccinated: bool.Parse(p[7]), size: p[8], status),
    ...
};
```

**Step 6 — Restore adopter name:**
```csharp
if (status == AnimalStatus.Adopted && !string.IsNullOrWhiteSpace(adopterName))
    animal.Adopt(adopterName);
```
`AdopterName` is `private set` — the only way to restore it is calling `Adopt()` again. This is safe because the animal's status is already set to `Adopted` from the constructor.

**Step 7 — Per-line error handling:**
```csharp
catch (Exception ex)
{
    Console.WriteLine($"[FILE WARN] Skipping line {lineNumber}: {ex.Message}");
}
```
One bad line is skipped with a warning — the rest of the file still loads.

---

### How `AnimalFileRepository` Connects Everything

```csharp
public AnimalFileRepository(ShelterFileHandler fileHandler)
{
    _fileHandler = fileHandler;
    var loaded = _fileHandler.LoadAll();   // Restore from file on startup
    _animals.AddRange(loaded);
    if (_animals.Any())
        _nextId = _animals.Max(a => a.Id) + 1;  // Correct the ID counter
}

private void Save() => _fileHandler.SaveAll(_animals);  // Called after every mutation

public void Add(Animal animal)
{
    _animals.Add(animal);
    if (animal.Id >= _nextId) _nextId = animal.Id + 1;
    Save();   // ← Auto-save
}
```

The user never needs to think about saving. Every `Add`, `Remove`, `UpdateStatus`, `Adopt`, and `AddCareNote` calls `Save()` automatically.

---

## OOP & SOLID Concepts

| Concept | Where |
|---|---|
| **Abstract Class** | `Animal` — `abstract GetSpeciesInfo()` |
| **Inheritance** | `Dog`, `Cat`, `Bird`, `SmallAnimal` extend `Animal` |
| **Polymorphism** | `ShowAll()` calls `GetSpeciesInfo()` — different result per type |
| **Encapsulation** | Private fields, validated properties, `private set` on `AdopterName` |
| **Interfaces** | `IAdoptable`, `ICareRecord`, `IAnimalRepository`, `IAnimalService` |
| **Enum** | `AnimalStatus` — type-safe status values |
| **SRP** | Each class has one job — display, input, storage, file I/O are all separate |
| **OCP** | Swap `AnimalFileRepository` for `AnimalManager` with one line in `Program.cs` |
| **LSP** | Any subclass works wherever `Animal` is expected |
| **ISP** | `IAdoptable` and `ICareRecord` are separate interfaces |
| **DIP** | `AppController` depends on `IAnimalService`; `AnimalService` depends on `IAnimalRepository` |

---

## Data File Format

```
Type | Id | Name | Age | Status | AdopterName | Field1 | Field2 | Field3
```

| Index | Field | Example |
|---|---|---|
| 0 | Type | `Dog` |
| 1 | ID | `1` |
| 2 | Name | `Buddy` |
| 3 | Age | `3` |
| 4 | Status | `Available` |
| 5 | Adopter | *(empty if not adopted)* |
| 6 | Subclass field 1 | `Labrador` |
| 7 | Subclass field 2 | `True` |
| 8 | Subclass field 3 | `Large` |
