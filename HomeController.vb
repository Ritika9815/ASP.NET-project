Public Class InsureeController :  Controller
{
    Private ReadOnly YourDbContext _context;

    Public InsureeController(YourDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    Public Async Task<IActionResult> Create(Insuree insuree)
    {
        // Base rate
        Decimal monthlyTotal = 50;

        // Age-based calculations
        If (insuree.Age <= 18)
            monthlyTotal += 100;
        ElseIf (insuree.Age >= 19 && insuree.Age <= 25)
            monthlyTotal += 50;
        ElseIf (insuree.Age > 25)
            monthlyTotal += 25;

        // Car year calculations
        If (insuree.CarYear < 2000)
            monthlyTotal += 25;
        ElseIf (insuree.CarYear > 2015)
            monthlyTotal += 25;

        // Car make And model calculations
        If (insuree.CarMake == "Porsche")
        {
            monthlyTotal += 25;
            If (insuree.CarModel == "911 Carrera")
                monthlyTotal += 25;
        }

        // Add cost for speeding tickets
        monthlyTotal += insuree.SpeedingTickets * 10;

        // DUI check - add 25%
        If insuree.HasDUI
            monthlyTotal *= 1.25m;

        // Full coverage check - add 50%
        If (insuree.IsFullCoverage)
            monthlyTotal *= 1.5m;

        // Set the calculated quote
        insuree.Quote = monthlyTotal;

        // Save to the database
        If (ModelState.IsValid)
        {
            _context.Add(insuree);
            await _context.SaveChangesAsync();
            Return RedirectToAction(NameOf(Index));
        }
        Return View(insuree);
    }
}

@model YourNamespace.Models.Insuree

<form asp-action="Create">
    <!-- Other form fields go here -->
    
    <!-- Hide the Quote field -->
    @* <div class="form-group">
        <Label asp-For="Quote" Class="control-label"></label>
        <input asp-For="Quote" Class="form-control" ReadOnly />
        <span asp-validation-For="Quote" Class="text-danger"></span>
    </div> *@
    
    <Button type = "submit" Class="btn btn-primary">Submit</button>
</form>
