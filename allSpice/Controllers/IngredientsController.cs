using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace allSpice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IngredientsController : ControllerBase
{
    private readonly IngredientsService _ingredientService;
    private readonly Auth0Provider _auth0Provider;

    public IngredientsController(IngredientsService ingredientService, Auth0Provider auth0Provider)
    {
        _ingredientService = ingredientService;
        _auth0Provider = auth0Provider;
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Ingredient>> CreateIngredient([FromBody] Ingredient ingredientData)
    {
      try 
      {
        Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
        ingredientData.CreatorId = userInfo.Id;
        Ingredient ingredient = _ingredientService.CreateIngredient(ingredientData);
        return Ok(ingredient);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [Authorize]
    [HttpDelete("{ingredientId}")]
    public async Task<ActionResult<string>> RemoveIngredient(int ingredientId)
    {
      try 
      {
        Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
        _ingredientService.RemoveIngredient(ingredientId, userInfo.Id);
        return Ok("Ingredient removed");
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

}
