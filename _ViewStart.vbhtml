@Code
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code
public async Task<IActionResult>
    AdminView()
    {
    var insurees = await _context.Insurees.ToListAsync();
    return View(insurees);
    }
    @model IEnumerable<YourNamespace.Models.Insuree>

        <h2>Admin View - All Quotes</h2>

        <table class="table">
            <thead>
                <tr>
                    <th>First Name</th>
                    <th>Last Name</th>
                    <th>Email</th>
                    <th>Quote</th>
                </tr>
            </thead>
            <tbody>
                @foreach (var insuree in Model)
                {
                <tr>
                    <td>@insuree.FirstName</td>
                    <td>@insuree.LastName</td>
                    <td>@insuree.Email</td>
                    <td>@insuree.Quote.ToString("C")</td>
                </tr>
                }
            </tbody>
        </table>
