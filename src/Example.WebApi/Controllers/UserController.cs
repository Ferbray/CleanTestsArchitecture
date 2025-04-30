using Example.WebApi.Data;
using Example.WebApi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Example.WebApi.Controllers;

[ApiController]
[Route("api/users")]
public class WeatherForecastController(EFContext context) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var users = await context.Users.ToListAsync();

        return Ok(users);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var user = await context.Users.FirstOrDefaultAsync(x => x.Id == id);

        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> Create(UserEntity user)
    {
        user.Id = Guid.NewGuid();

        if (await context.Users.AnyAsync(x => x.Email == user.Email))
        {
            return BadRequest();
        }

        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();

        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Update(UserEntity user)
    {
        if (!await context.Users.AnyAsync(x => x.Id == user.Id))
        {
            return NotFound();
        }

        context.Users.Update(user);
        await context.SaveChangesAsync();

        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var user = await context.Users.FirstOrDefaultAsync(x => x.Id == id);

        if (user == null)
        {
            return NotFound();
        }

        context.Users.Remove(user);
        await context.SaveChangesAsync();

        return Ok();
    }
}
