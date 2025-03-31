using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.Entities;

namespace UserManagementAPI.Controllers
{
    [ApiController]
    [Authorize]
    public class UserManagementController : ControllerBase
    {
        private readonly List<UserData> _users;

        public UserManagementController(List<UserData> users)
        {
            _users = users;
        }

        [HttpGet("/userdata/all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllUserData()
        {
            if (_users.Count <= 0)
                return NotFound();

            return Ok(_users);
        }

        [HttpGet("/userdata/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetUserData(int id)
        {
            var found = _users.Where(p => p.Id == id).FirstOrDefault();
            if (found == null)
                return NotFound();

            return Ok(found);
        }

        [HttpPost("/userdata")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddUserData([FromBody] UserData data)
        {
            var found = _users.Where(p => p.Id == data.Id).Any();

            if (found)
                return BadRequest("User Id is already used!");

            if (data == null || string.IsNullOrEmpty(data.Name))
                return BadRequest("User data is invalid.");

            _users.Add(data);
            return Ok("User data added successfully.");
        }

        [HttpPut("/userdata/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateUserData(int id, [FromBody] UserData data)
        {
            var found = _users.Where(p => p.Id == id).FirstOrDefault();

            if (found == null)
                return NotFound();

            found.Name = data.Name;
            return Ok("User data updated successfully.");
        }

        [HttpDelete("/userdata/{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult DeleteUserData(int id)
        {
            var found = _users.Where(p => p.Id == id).FirstOrDefault();

            if (found == null)
                return NotFound();

            _users.Remove(found);
            return NoContent();
        }
    }
}
