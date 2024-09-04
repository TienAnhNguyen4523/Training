using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using Training.Authentication.Handlers;
using Training.Authentication.TokenValidators;
using Training.Domain.ViewModel.User;
using Tranining.Domain.Command.Users;
using Tranining.Domain.Service.Interface.User;
using Tranining.Domain.ViewModel.User;

namespace Training.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : BaseController
	{
		private readonly IUserService _userService;
		private readonly IProfileService _profileService;
		private readonly IHttpContextAccessor _contextAccessor;
		private readonly JwtModel _jwt;
		public UserController(IUserService userService, IOptions<JwtModel> jwt,IProfileService profileService, IHttpContextAccessor contextAccessor)
		{
			_userService = userService;
			_jwt = jwt.Value;
			_profileService = profileService;
			_contextAccessor = contextAccessor;
		}
		[HttpPost("signin")]
		[AllowAnonymous]
		public async Task<ActionResult<TokenViewModel>> signin([FromBody] LoginModelCommand model)
		{
			#region Parameters validation
			if(model == null)
			{
				model = new LoginModelCommand();
				TryValidateModel(model);
			}
			if(!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			#endregion
			var loginResult = await _userService.Login(model);
			if(loginResult == null)
			{
				return Ok(false);

			}
			var account = loginResult.UserProfile;
			var claims = new List<Claim>();
			claims.Add(new Claim(nameof(account.Id), account.Id.ToString()));
			claims.Add(new Claim(nameof(account.FullName), account.FullName.ToString()));
			if (!string.IsNullOrEmpty(account.Permissions))
			{
				claims.Add(new Claim("Permissions", account.Permissions));
			}
			foreach (var role in account.Roles)
			{
				claims.Add(new Claim("roleName", role.Name));
				//claims.Add(new Claim("RoleId", role.Id.ToString()));
			}
			
			loginResult.Code = _profileService.GenerateJwt(claims.ToArray(),_jwt);
			loginResult.LifeTime = _jwt.LifeTime;
			return Ok(loginResult);
		}
		[HttpPost("signup")]
		[AllowAnonymous]
		public async Task<IActionResult> signup([FromBody] CreateUserCommand model)
		{
			if (!GetCurrentPermission(_contextAccessor).Contains("create")) {
				throw new ArgumentException("You have no permission");
			}
			#region Parameters validation
			if(model == null)
			{
				model = new CreateUserCommand();
				TryValidateModel(model);
			}
			if(!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			#endregion
			var loginResult = await _userService.CreateAccount(model);
			return Ok(loginResult);
		}
		[HttpPut("update")]
		public async Task<IActionResult> Update([FromBody] UpdateUserCommand model)
		{
			if (!CheckAdmin(_contextAccessor))
			{
				throw new ArgumentException("U are not Admin");
			}
			if (!GetCurrentPermission(_contextAccessor).Contains("update"))
			{
				throw new ArgumentException("You have no permission");
			}
			#region Parameters validation
			if (model == null)
			{
				model = new UpdateUserCommand();
				TryValidateModel(model);
			}
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			#endregion
			var loginResult = await _userService.UpdateAccount(model);
			return Ok(loginResult);
		}
		[HttpDelete("delete")]
		public async Task<IActionResult> Delete([FromBody] DeleteUserCommand model)
		{
			if (!CheckAdmin(_contextAccessor))
			{
				throw new ArgumentException("U are not Admin");
			}
			if (!GetCurrentPermission(_contextAccessor).Contains("delete"))
			{
				throw new ArgumentException("You have no permission");
			}
			#region Parameters validation
			if (model == null)
			{
				model = new DeleteUserCommand();
				TryValidateModel(model);
			}
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			#endregion
			var loginResult = await _userService.DeleteAccount(model);
			return Ok(loginResult);
		}
		[HttpPost("listing")]
		public async Task<IActionResult> Listing([FromBody] GetUserPaginationCommand model)
		{
			if (!CheckAdmin(_contextAccessor))
			{
				throw new ArgumentException("U are not Admin");
			}
			if (!GetCurrentPermission(_contextAccessor).Contains("view"))
			{
				throw new ArgumentException("You have no permission");
			}
			var result = await _userService.Listing(model);
			return Ok(result);
		}
		[HttpPost("searching")]
		public async Task<IActionResult> Search([FromBody] GetUserSearchingCommand model)
		{
			var result = await _userService.Searching(model);
			return Ok(result);
		}
		[HttpPost("filtering")]
		public async Task<IActionResult> Filter([FromBody] GetUserFilteringCommand model)
		{
			var result = await _userService.Filtering(model);
			return Ok(result);
		}
	}
}
