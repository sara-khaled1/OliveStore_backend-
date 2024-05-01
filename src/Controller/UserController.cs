using Microsoft.AspNetCore.Mvc;
using sda_onsite_2_csharp_backend_teamwork.src.Abstraction;
using sda_onsite_2_csharp_backend_teamwork.src.Database;
using sda_onsite_2_csharp_backend_teamwork.src.DTO;
using sda_onsite_2_csharp_backend_teamwork.src.Entity;
using sda_onsite_2_csharp_backend_teamwork.src.Service;

namespace sda_onsite_2_csharp_backend_teamwork.src.Controller;
public class UserController : BaseController
{
    private IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public IEnumerable<UserReadDto> FindAll()
    {
        return _userService.FindAll();
    }

    [HttpGet("{UserId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<UserReadDto?> FindOne(string userId)
    {

        UserReadDto? foundProduct = _userService.FindOne(userId);
        return Ok(foundProduct);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<IEnumerable<UserReadDto>> CreateOne([FromBody] User user)
    {
        if (user is not null)
        {
            var createdUser = _userService.CreateOne(user);
            return CreatedAtAction(nameof(CreateOne), createdUser);
        }
        return BadRequest();
    }

    [HttpPatch("{UserId}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<UserReadDto> UpdateOne(string email, User user)
    {
        UserReadDto? updatedUser = _userService.UpdateOne(email, user);
        if (updatedUser is not null)
        {
            return CreatedAtAction(nameof(UpdateOne), updatedUser);
        }
        else return BadRequest();
    }
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult DeleteOne(string id, User user)
    {
        bool isDeleted = _userService.DeleteOne(id);
        if (!isDeleted)
        {
            return NotFound();
        }
        return NoContent();
    }
}
